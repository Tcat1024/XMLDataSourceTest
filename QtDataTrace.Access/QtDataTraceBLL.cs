using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using EAS.Services;
using QtDataTrace.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace QtDataTrace.Access
{
    internal class QtDataTraceBLL
    {
        private static Dictionary<string, Dictionary<Guid, DataFactoryBase>> DataFactoryContainer = new Dictionary<string, Dictionary<Guid, DataFactoryBase>>();
        public static Guid Add(string username,DataFactoryBase factory)
        {
            Guid id;
            Dictionary<Guid, DataFactoryBase> factories;
            lock (DataFactoryContainer)
            {
                if (!DataFactoryContainer.TryGetValue(username, out factories))
                {
                    factories = new Dictionary<Guid, DataFactoryBase>();
                    DataFactoryContainer.Add(username, factories);
                }
            }
            lock (factories)
            {
                if (factories.Count > 3)
                {
                    bool temp = false;
                    foreach (var k in factories.Keys)
                    {
                        var f = factories[k];
                        if (!f.Writing&&!f.Reading)
                        {
                            factories.Remove(k);
                            temp = true;
                            break;
                        }
                    }
                    if (!temp)
                        throw new Exception("历史数据均在处理，无法新建数据");
                }
                id = Guid.NewGuid();
                factories.Add(id,factory);
            }
            return id;
        }
        public static bool Remove(string username, Guid id)
        {
            Dictionary<Guid, DataFactoryBase> factories;
            if (DataFactoryContainer.TryGetValue(username, out factories))
            {
                DataFactoryBase factory;
                if (factories.TryGetValue(id, out factory))
                {
                    if (factory.Writing)
                        factory.Stop();
                    factories.Remove(id);
                    return true;
                }
            }
            return false;
        }
        public static bool Stop(string username,Guid id)
        {
            Dictionary<Guid, DataFactoryBase> factories;
            if(DataFactoryContainer.TryGetValue(username,out factories))
            {
                DataFactoryBase factory;
                if (factories.TryGetValue(id, out factory))
                {
                    if (factory.Stop())
                    {
                        factories.Remove(id);
                        return true;
                    }
                }
            }
            return false;
        }
        public static DataFactoryBase GetFactory(string username, Guid id)
        {
            Dictionary<Guid, DataFactoryBase> factories;
            if (DataFactoryContainer.TryGetValue(username, out factories))
            {
                DataFactoryBase factory;
                if (factories.TryGetValue(id, out factory))
                {
                    return factory;
                }
            }
            return null;
        }
        public static DataTable BeginAnalyzeData(string username, Guid id)
        {
            DataTable result;
            Dictionary<Guid, DataFactoryBase> factories;
            if (DataFactoryContainer.TryGetValue(username, out factories))
            {
                DataFactoryBase factory;
                if (factories.TryGetValue(id, out factory))
                {
                    if (factory.Writing)
                        throw new Exception("目标数据正在被写入，无法操作");
                    else
                    {
                        result = factory.ResultData;
                        factory.AddReader();
                        lock (factories)
                        {
                            factories.Remove(id);
                            factories.Add(id, factory);
                        }
                    }
                    return result;
                }
            }
            throw new Exception("无法获取目标数据，目标数据可能已被删除");
        }
        public static bool EndAnalyzeData(string username, Guid id)
        {
            Dictionary<Guid, DataFactoryBase> factories;
            if (DataFactoryContainer.TryGetValue(username, out factories))
            {
                DataFactoryBase factory;
                if (factories.TryGetValue(id, out factory))
                {
                    return factory.RemoveReader();
                }
            }
            return false;
        }      
    }
    public abstract class DataFactoryBase
    {
        private bool _Writing = false;
        public bool Writing 
        { 
            get
            {
                return _Writing;
            }
            protected set
            {
                this._Writing = value;
            }
        }
        public DataTable ResultData { get; protected set; }
        protected string _Error = "";
        public string Error
        {
            get
            {
                return _Error;
            }
            protected set
            {
                _Error = value;
            }
        }
        protected object readObj = new object();
        protected int reader = 0;
        public bool Reading
        {
            get
            {
                return reader != 0;
            }
        }
        public void AddReader()
        {
            lock (readObj)
            {
                reader++;
            }
        }
        public bool RemoveReader()
        {
            lock (readObj)
            {
                if (reader == 0)
                    return false;
                reader--;
            }
            return true;
        }
        public abstract int GetProgress();
        public abstract bool Stop();
    }
    public class DataTraceFactory:DataFactoryBase
    {    
        int threadcount = 15;
        int rowcount = 0;
        string RootProcessNo;
        int RootProcessSeq;
        List<string> RootSql;
        IList<string> IDList;
        IList<QtDataProcessConfig> Processes;
        DataSet ConfigFile;
        Dictionary<string, List<string>> processdic = new Dictionary<string, List<string>>();
        Dictionary<string, int> processseqs = new Dictionary<string, int>();
        int back;
        object progressobject = new object();
        int progress = 0;
        DataTable[] threadTables;
        TaskFactory taskFactory;
        CancellationTokenSource cancel;
        public DataTraceFactory(string processNo, IList<string> iDList, IList<QtDataProcessConfig> processes, bool b)
        {
            this.RootProcessNo = processNo;
            this.IDList = iDList;
            this.Processes = processes;
            this.ConfigFile = new BaseTableService().GetProcessQtTableConfigFile();
            this.rowcount = iDList.Count;
            this.Writing = false;
            int rcount = ConfigFile.Tables["Table"].Rows.Count;
            for (int i = 0; i < rcount; i++)
            {
                var temprow = ConfigFile.Tables["Table"].Rows[i];
                if (!this.processseqs.ContainsKey(temprow["PROCESS_CODE"].ToString()))
                {
                    this.processseqs.Add(temprow["PROCESS_CODE"].ToString(), int.Parse(temprow["PROCESS_SEQ"].ToString()));
                }
            }
            if (processes.Count == 1 && (ConfigFile.Tables["Table"].Select(string.Format("PROCESS_CODE = '{0}'", processNo)).FirstOrDefault()["PROCESS_CHINESE"].ToString()) == processes[0].ChineseName)
                this.back = 0;
            else
                this.back = b ? 1 : -1;
            this.RootProcessSeq = processseqs[RootProcessNo];
            this.RootSql = getProcessString(RootProcessNo);
        }   
        public bool Start()
        {
            if (!processseqs.ContainsKey(RootProcessNo))
                return false;
            this.Writing = true;
            progress = 0;
            processdic.Add(RootProcessNo, getProcessString(RootProcessNo));
            int listcount = IDList.Count;
            if (listcount / 3 < this.threadcount)
                this.threadcount = listcount / 3 == 0 ? 1 : listcount / 3;
            this.threadTables = new DataTable[this.threadcount];
            this.taskFactory = new TaskFactory();
            this.cancel = new CancellationTokenSource();
            Task[] tasks = new Task[this.threadcount];
            switch (back)
            {
                case 1:
                    for (int i = 0; i < this.threadcount; i++)
                    {
                        threadTables[i] = new DataTable();
                        tasks[i] = taskFactory.StartNew(threadMethod_Back, new threadInputType((listcount * i) / this.threadcount, (listcount * (i + 1)) / this.threadcount, threadTables[i]), cancel.Token);
                    }
                    break;
                case 0:
                    for (int i = 0; i < this.threadcount; i++)
                    {

                        threadTables[i] = new DataTable();
                        tasks[i] = taskFactory.StartNew(threadMethod_Single, new threadInputType((listcount * i) / this.threadcount, (listcount * (i + 1)) / this.threadcount, threadTables[i]), cancel.Token);
                    }
                    break;
                case -1:
                    for (int i = 0; i < this.threadcount; i++)
                    {
                        threadTables[i] = new DataTable();
                        tasks[i] = taskFactory.StartNew(threadMethod_Pre, new threadInputType((listcount * i) / this.threadcount, (listcount * (i + 1)) / this.threadcount, threadTables[i]), cancel.Token);
                    }
                    break;
            }
            taskFactory.ContinueWhenAll(tasks, threadMethod_Merge, cancel.Token);
            return true;
        }
        public override bool Stop()
        {
            cancel.Cancel();
            this.Writing = false;
            return true;
        }
        public override int GetProgress()
        {
            return Math.Min(progress, rowcount) * 99 / rowcount + Math.Max(progress, rowcount) - rowcount;
        }
        private void threadMethod_Merge(Task[] taskbefore)
        {
            try
            {
                DataTable mainTable = new DataTable();
                for (int i = 0; i < this.threadcount; i++)
                {
                    mainTable.Merge(threadTables[i]);
                }
                int num = 0;
                for (int i = 0; i < Processes.Count; i++)
                {
                    var tables = Processes[i].Tables;
                    for (int j = 0; j < tables.Count; j++)
                    {
                        var columns = tables[j].Columns;
                        foreach (var col in columns)
                        {
                            int count = 1;
                            string tempcolumnname = Processes[i].ChineseName + "_" + col.ChineseName;
                            while (true)
                            {

                                if (mainTable.Columns.IndexOf(tempcolumnname) < 0)
                                    break;
                                else if (mainTable.Columns.IndexOf(tempcolumnname) >= num)
                                {
                                    mainTable.Columns[tempcolumnname].SetOrdinal(num++);
                                    break;
                                }
                                else
                                    tempcolumnname += (count++).ToString();
                            }
                        }
                    }
                }
                lock (progressobject)
                {
                    progress++;
                }
                this.ResultData = mainTable;
            }
            catch(Exception ex)
            {
                this.Error = ex.Message;
            }
            finally
            {
                this.Writing = false;
            }
        }
        private void threadMethod_Single(object input)
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB))
                {
                    threadInputType inputtype = input as threadInputType;
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    connection.Open();
                    DataTraceTreeNode root = new DataTraceTreeNode(RootProcessNo, RootProcessSeq, null) { Sql = RootSql };
                    List<DataTraceTreeNode> leafnodes = new List<DataTraceTreeNode>();
                    for (int i = inputtype.start; i < inputtype.end; i++)
                    {
                        root.OutId = IDList[i];
                        root.data = new DataTable();
                        root.Nodes.Clear();
                        command.CommandText = string.Format("select IN_MAT_ID1,IN_MAT_ID2 from PROCESS_MAT_PEDIGREE where OUT_MAT_ID = '{0}' and PROCESS_CODE = '{1}'", root.OutId, root.Process);
                        OleDbDataReader reader = command.ExecuteReader();
                        Tuple<string, string> pros = null;
                        while (reader.Read())
                        {
                            pros = new Tuple<string, string>(reader[0].ToString(), reader[1] == null ? "" : reader[1].ToString());
                        }
                        reader.Close();
                        if (pros != null)
                        {
                            root.InId = pros.Item1;
                        }
                        OleDbDataAdapter adp = new OleDbDataAdapter(command);
                        DataTable temp;
                        foreach (string sql in root.Sql)
                        {
                            command.CommandText = string.Format(sql, IDList[i], RootProcessNo, root.InId);
                            temp = new DataTable();
                            adp.Fill(temp);
                            externtable(temp, root.data, false);
                        }
                        inputtype.data.Merge(root.data);
                        lock (progressobject)
                        {
                            progress++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Stop();
                this.Error = ex.Message;
            }
        }
        private void threadMethod_Back(object input)
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB))
                {
                    threadInputType inputtype = input as threadInputType;
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    connection.Open();
                    DataTraceTreeNode root = new DataTraceTreeNode(RootProcessNo, RootProcessSeq, null) { Sql = RootSql };
                    List<DataTraceTreeNode> leafnodes = new List<DataTraceTreeNode>();
                    for (int i = inputtype.start; i < inputtype.end; i++)
                    {
                        root.OutId = IDList[i];
                        root.data = null;
                        root.Nodes.Clear();
                        leafnodes.Clear();
                        command.CommandText = string.Format("select IN_MAT_ID1,IN_MAT_ID2 from PROCESS_MAT_PEDIGREE where OUT_MAT_ID = '{0}' and PROCESS_CODE = '{1}'", root.OutId, root.Process);
                        OleDbDataReader reader = command.ExecuteReader();
                        Tuple<string, string> pros = null;
                        while (reader.Read())
                        {
                            pros = new Tuple<string, string>(reader[0].ToString(), reader[1] == null ? "" : reader[1].ToString());
                        }
                        reader.Close();
                        if (pros != null)
                        {
                            root.InId = pros.Item1;
                        }
                        buildBackTraceTree(root, command, leafnodes);
                        foreach (var leafnode in leafnodes)
                        {
                            var result = analyzeLeafNode(leafnode, connection, true);
                            inputtype.data.Merge(result);
                        }
                        lock (progressobject)
                        {
                            progress++;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                this.Stop();
                this.Error = ex.Message;
            }
        }
        private void threadMethod_Pre(object input)
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB))
                {
                    threadInputType inputtype = input as threadInputType;
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    connection.Open();
                    DataTraceTreeNode root = new DataTraceTreeNode(RootProcessNo, RootProcessSeq, null) { Sql = RootSql };
                    List<DataTraceTreeNode> leafnodes = new List<DataTraceTreeNode>();
                    for (int i = inputtype.start; i < inputtype.end; i++)
                    {

                        root.OutId = IDList[i];
                        root.data = null;
                        root.Nodes.Clear();
                        leafnodes.Clear();
                        command.CommandText = string.Format("select IN_MAT_ID1,IN_MAT_ID2 from PROCESS_MAT_PEDIGREE where OUT_MAT_ID = '{0}' and PROCESS_CODE = '{1}'", root.OutId, root.Process);
                        OleDbDataReader reader = command.ExecuteReader();
                        Tuple<string, string> pros = null;
                        while (reader.Read())
                        {
                            pros = new Tuple<string, string>(reader[0].ToString(), reader[1] == null ? "" : reader[1].ToString());
                        }
                        reader.Close();
                        if (pros != null)
                        {
                            root.InId = pros.Item1;
                            root.Nodes.Add(new DataTraceTreeNode(pros.Item1, null, int.MinValue, root));
                            if (pros.Item2 != "" && pros.Item1 != pros.Item2)
                            {
                                root.Nodes.Add(new DataTraceTreeNode(pros.Item2, null, int.MinValue, root));
                            }
                            for (int k = root.Nodes.Count - 1; k >= 0; k--)
                            {
                                buildPreTraceTree(root.Nodes[k], command, leafnodes);
                            }
                        }
                        if (root.Nodes.Count == 0)
                            leafnodes.Add(root);
                        foreach (var leafnode in leafnodes)
                        {
                            var result = analyzeLeafNode(leafnode, connection, false);
                            inputtype.data.Merge(result);
                        }
                        lock (progressobject)
                        {
                            progress++;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                this.Stop();
                this.Error = ex.Message;
            }
        }
        private class threadInputType
        {
            public int start;
            public int end;
            public DataTable data;
            public threadInputType(int s, int e, DataTable d)
            {
                this.start = s;
                this.end = e;
                this.data = d;
            }
        }
        private DataTable analyzeLeafNode(DataTraceTreeNode leaf, OleDbConnection connection, bool pre)
        {
            DataTable result = new DataTable();
            DataTable temp;
            if (leaf == null)
                return null;
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = connection;
            OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
            do
            {
                if (leaf.Sql != null && leaf.Sql.Count != 0)
                {
                    if (leaf.data == null)
                    {
                        leaf.data = new DataTable();
                        foreach (string sql in leaf.Sql)
                        {
                            cmd.CommandText = string.Format(sql, leaf.OutId, leaf.Process, leaf.InId);
                            temp = new DataTable();
                            adp.Fill(temp);
                            externtable(temp, leaf.data, false);
                        }
                    }
                    externtable(leaf.data, result, pre);
                }
                leaf = leaf.Parent;
            } while (leaf != null);
            return result;
        }
        private void externtable(DataTable a, DataTable result, bool pre)
        {
            int counta = a.Columns.Count;
            int countr = result.Columns.Count;
            if (a.Rows.Count == 0)
                return;
            if (result.Rows.Count == 0)
                result.Rows.Add(result.NewRow());
            if (!pre)
                for (int i = 0, j = 0; i < counta; i++)
                {
                    if (result.Columns.Contains(a.Columns[i].ColumnName))
                        continue;
                    result.Columns.Add(a.Columns[i].ColumnName, a.Columns[i].DataType);
                    result.Rows[0][countr + j++] = a.Rows[0][i];
                }
            else
                for (int i = counta - 1; i >= 0; i--)
                {
                    if (result.Columns.Contains(a.Columns[i].ColumnName))
                        continue;
                    result.Columns.Add(a.Columns[i].ColumnName, a.Columns[i].DataType).SetOrdinal(0);
                    result.Rows[0][0] = a.Rows[0][i];
                }
        }
        private void buildBackTraceTree(DataTraceTreeNode node, OleDbCommand command, List<DataTraceTreeNode> leafnodes)
        {
            command.CommandText = string.Format("select OUT_MAT_ID,PROCESS_CODE from PROCESS_MAT_PEDIGREE where IN_MAT_ID1 = '{0}' or IN_MAT_ID2 = '{0}'", node.OutId);
            OleDbDataReader reader = command.ExecuteReader();
            List<Tuple<int, string, string>> pros = new List<Tuple<int, string, string>>();
            int seq = node.ProSeq;
            int minseq = int.MaxValue;
            while (reader.Read())
            {
                string protemp = reader[1].ToString();
                string idtemp = reader[0].ToString();
                int seqtemp;
                if (processseqs.TryGetValue(protemp, out seqtemp) && seqtemp > seq && seqtemp <= minseq)
                {
                    minseq = seqtemp;
                    pros.Add(new Tuple<int, string, string>(seqtemp, protemp, idtemp));
                }
            }
            reader.Close();
            if (pros.Count > 0)
            {
                foreach (var pro in pros)
                {
                    if (pro.Item1 == minseq)
                    {
                        List<string> sql;
                        if (!processdic.TryGetValue(pro.Item2, out sql))
                        {
                            lock (processdic)
                            {
                                if (!processdic.TryGetValue(pro.Item2, out sql))
                                    processdic.Add(pro.Item2, sql = getProcessString(pro.Item2));
                            }
                        }
                        node.Nodes.Add(new DataTraceTreeNode(node.OutId, pro.Item3, pro.Item2, pro.Item1, node) { Sql = sql });
                    }
                }
                for (int i = node.Nodes.Count - 1; i >= 0; i--)
                {
                    buildBackTraceTree(node.Nodes[i], command, leafnodes);
                }

            }
            if (node.Nodes.Count == 0)
            {
                if ((node.Sql == null || node.Sql.Count == 0) && node.Parent != null)
                {
                    node.Parent.Nodes.Remove(node);
                }
                else
                    leafnodes.Add(node);
            }
        }
        private void buildPreTraceTree(DataTraceTreeNode node, OleDbCommand command, List<DataTraceTreeNode> leafnodes)
        {
            if (node.Parent == null)
                return;
            command.CommandText = string.Format("select IN_MAT_ID1,IN_MAT_ID2,PROCESS_CODE from PROCESS_MAT_PEDIGREE where OUT_MAT_ID = '{0}'", node.OutId);
            OleDbDataReader reader = command.ExecuteReader();
            Tuple<int, string, string, string> pros = null;
            int seq = node.Parent.ProSeq;
            int maxseq = int.MinValue;
            while (reader.Read())
            {
                string protemp = reader[2].ToString();
                string id2temp = reader[1] == null ? "" : reader[1].ToString();
                string id1temp = reader[0].ToString();
                int seqtemp;
                if (processseqs.TryGetValue(protemp, out seqtemp) && (seqtemp < seq) && (seqtemp > maxseq))
                {
                    maxseq = seqtemp;
                    pros = new Tuple<int, string, string, string>(seqtemp, protemp, id1temp, id2temp);
                }
            }
            reader.Close();
            if (pros != null)
            {
                if (!processdic.TryGetValue(pros.Item2, out node.Sql))
                {
                    lock (processdic)
                    {
                        if (!processdic.TryGetValue(pros.Item2, out node.Sql))
                            processdic.Add(pros.Item2, node.Sql = getProcessString(pros.Item2));
                    }
                }
                node.InId = pros.Item3;
                node.Process = pros.Item2;
                node.ProSeq = pros.Item1;
                node.Nodes.Add(new DataTraceTreeNode(pros.Item3, null, int.MinValue, node));
                if (pros.Item4 != "" && pros.Item4 != pros.Item3)
                {
                    node.Nodes.Add(new DataTraceTreeNode(pros.Item4, null, int.MinValue, node));
                }
                for (int i = node.Nodes.Count - 1; i >= 0; i--)
                {
                    buildPreTraceTree(node.Nodes[i], command, leafnodes);
                }
                if (node.Nodes.Count == 0)
                {
                    if ((node.Sql == null || node.Sql.Count == 0) && node.Parent != null)
                    {
                        node.Parent.Nodes.Remove(node);
                    }
                    else
                        leafnodes.Add(node);
                }
            }
            else
                node.Parent.Nodes.Remove(node);

        }
        private class DataTraceTreeNode
        {
            public string OutId;
            public string InId;
            public string Process;
            public int ProSeq;
            public List<DataTraceTreeNode> Nodes = new List<DataTraceTreeNode>();
            public DataTraceTreeNode Parent;
            public DataTable data = null;
            public List<string> Sql = null;
            public DataTraceTreeNode(string inid, string outid, string process, int proseq, DataTraceTreeNode parent)
            {
                this.InId = inid;
                this.OutId = outid;
                this.Process = process;
                this.Parent = parent;
                this.ProSeq = proseq;
            }
            public DataTraceTreeNode(string outid, string process, int proseq, DataTraceTreeNode parent)
            {
                this.OutId = outid;
                this.Process = process;
                this.Parent = parent;
                this.ProSeq = proseq;
            }
            public DataTraceTreeNode(string process, int proseq, DataTraceTreeNode parent)
            {
                this.Process = process;
                this.Parent = parent;
                this.ProSeq = proseq;
            }
        }
        private List<string> getProcessString(string process)
        {
            List<string> result = new List<string>();
            var tables = ConfigFile.Tables["Table"].Select(string.Format("PROCESS_CODE = '{0}'", process));
            if (tables.Count() == 0)
                return result;
            var processchinese = tables.FirstOrDefault()["PROCESS_CHINESE"].ToString();
            var processconfig = Processes.FirstOrDefault((d) => { return d.ChineseName == processchinese; });
            if (processconfig != null)
            {
                List<string> inids = new List<string>();
                List<string> outids = new List<string>();
                List<string> processcodes = new List<string>();
                int tc = processconfig.Tables.Count;
                for (int i = 0; i < tc; i++)
                {
                    string[] sqlparams = new string[] { "", "", "" };
                    string tablechinese = processconfig.Tables[i].ChineseName;
                    var table = tables.First((input) => { return input["TABLE_CHINESE"].ToString() == tablechinese; });
                    string tablename = table["TABLE_NAME"].ToString();

                    sqlparams[1] = tablename;
                    int cc = processconfig.Tables[i].Columns.Count;
                    if (cc == 0)
                        continue;
                    for (int j = 0; j < cc; j++)
                    {
                        string columnchinese = processconfig.Tables[i].Columns[j].ChineseName;
                        var column = (from r in ConfigFile.Tables["Column"].AsEnumerable() where (r.GetParentRow("Table_Column")["TABLE_NAME"].ToString() == tablename && r.GetParentRow("Table_Column")["PROCESS_CODE"].ToString() == process && ((r["COLUMN_CHINESE"].ToString() == null || r["COLUMN_CHINESE"].ToString().Trim() == "") ? r["COLUMN_NAME"].ToString() : r["COLUMN_CHINESE"].ToString()) == columnchinese) select r).FirstOrDefault();
                        if (column == null)
                            sqlparams[0] += string.Format("null \"{0}\", ", processchinese + "_" + columnchinese);
                        else
                            sqlparams[0] += string.Format("{0}.{1} \"{2}\", ", tablename, column["COLUMN_NAME"], processchinese + "_" + columnchinese);
                    }
                    if (sqlparams[0] != "")
                        sqlparams[0] = sqlparams[0].Substring(0, sqlparams[0].Length - 2);
                    var keycolumns = from r in ConfigFile.Tables["Column"].AsEnumerable() where r.GetParentRow("Table_Column")["TABLE_NAME"].ToString() == tablename && r.GetParentRow("Table_Column")["PROCESS_CODE"].ToString() == process && r["KEY"] != null && r["KEY"].ToString().Trim() != "" select r;
                    int kcc = keycolumns.Count();
                    for (int j = 0; j < kcc; j++)
                    {
                        switch (keycolumns.ElementAt(j)["KEY"].ToString())
                        {
                            case "IN_MAT_ID": sqlparams[2] += "and " + keycolumns.ElementAt(j)["COLUMN_NAME"] + "='{2}' "; break;
                            case "OUT_MAT_ID": sqlparams[2] += "and " + keycolumns.ElementAt(j)["COLUMN_NAME"] + "='{0}' "; break;
                            case "PROCESS_CODE": sqlparams[2] += "and " + keycolumns.ElementAt(j)["COLUMN_NAME"] + "='{1}' "; break;
                        }
                    }
                    result.Add("select " + sqlparams[0] + " from " + sqlparams[1] + " where 1=1 " + sqlparams[2]);
                }
            }
            return result;
        }
    }
    public class DataContainerFactory:DataFactoryBase
    {
        public DataContainerFactory(DataTable target)
        {
            this.ResultData = target;
        }
        public override int GetProgress()
        {
            return 100;
        }

        public override bool Stop()
        {
            return true;
        }
    }
}
