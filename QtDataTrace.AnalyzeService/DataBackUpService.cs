using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using System.IO;
using QtDataTrace.AnalyzeIService;
using SPC.Base.Interface;
using SPC.Algorithm;
using EAS.Services;

namespace QtDataTrace.AnalyzeService
{
    [ServiceObject("质量数据备份服务")]
    [ServiceBind(typeof(ILocalizationDataBackUpService))]
    public class LocalizationDataBackUpService : ServiceObject, ILocalizationDataBackUpService
    {
        public string SaveTable(string loginid, string userid, Guid id, string tablename, int[] selected = null)
        {
            DataTable data = null;
            try
            {
                data = LocalizationDataTraceBLL.BeginAnalyzeData(userid, id);
                var root = new DirectoryInfo(System.Windows.Forms.Application.StartupPath + "\\..\\DataBackUp");
                if (!root.Exists)
                    root.Create();
                using (SQLiteConnection con = new SQLiteConnection("Data Source =" + root.FullName + "\\" + loginid + ".db"))
                {
                    con.Open();
                    using (SQLiteTransaction tran = con.BeginTransaction())
                    {
                        string sql = "drop table if exists {0}";
                        SQLiteCommand dropcmd = new SQLiteCommand(string.Format(sql,tablename),con);
                        dropcmd.ExecuteNonQuery();
                        sql = "CREATE TABLE IF NOT EXISTS {0}({1});";
                        string colf = "";
                        string insertcommand = "insert into " + tablename + " values({0})";
                        string cola = "";
                        SQLiteCommand incmd = new SQLiteCommand(con);
                        foreach (DataColumn column in data.Columns)
                        {
                            cola += "@" + column.ColumnName + ",";
                            if (column.DataType == typeof(double) || column.DataType == typeof(decimal) || column.DataType == typeof(float) || column.DataType == typeof(int))
                            {
                                colf += column.ColumnName + " double,";
                                incmd.Parameters.Add("@" + column.ColumnName, DbType.Double);
                            }
                            else if (column.DataType == typeof(DateTime))
                            {
                                colf += column.ColumnName + " time,";
                                incmd.Parameters.Add("@" + column.ColumnName, DbType.DateTime);
                            }
                            else if (column.DataType == typeof(bool))
                            {
                                colf += column.ColumnName + " boolean,";
                                incmd.Parameters.Add("@" + column.ColumnName, DbType.Boolean);
                            }
                            else
                            {
                                colf += column.ColumnName + " text,";
                                incmd.Parameters.Add("@" + column.ColumnName, DbType.String);
                            }
                        }
                        colf = colf.Substring(0, colf.Length - 1);
                        cola = cola.Substring(0, cola.Length - 1);
                        SQLiteCommand cmdCreateTable = new SQLiteCommand(string.Format(sql, tablename, colf), con);
                        cmdCreateTable.ExecuteNonQuery();
                        incmd.CommandText = string.Format(insertcommand, cola);
                        incmd.Transaction = tran;
                        if (selected == null)
                        {
                            int rcount = data.Rows.Count;
                            int ccount = data.Columns.Count;
                            int i, j;
                            for (i = 0; i < rcount; i++)
                            {
                                for (j = 0; j < ccount; j++)
                                    incmd.Parameters[j].Value = data.Rows[i][j];
                                incmd.ExecuteNonQuery();
                            }
                            tran.Commit();
                        }
                        else
                        {
                            int rcount = selected.Length;
                            int ccount = data.Columns.Count;
                            int i, j;
                            for (i = 0; i < rcount; i++)
                            {
                                for (j = 0; j < ccount; j++)
                                    incmd.Parameters[j].Value = data.Rows[selected[i]][j];
                                incmd.ExecuteNonQuery();
                            }
                            tran.Commit();
                        }
                    }
                }
                LocalizationDataTraceBLL.EndAnalyzeData(userid, id);
                return "保存成功";
            }
            catch (Exception ex)
            {
                if (data != null)
                    LocalizationDataTraceBLL.EndAnalyzeData(userid, id);
                return ex.Message;
            }
        }

        public string[] GetTableList(string loginid)
        {
            List<string> result = new List<string>();
            try
            {
                var root = new DirectoryInfo(System.Windows.Forms.Application.StartupPath + "\\..\\DataBackUp");
                if (!root.Exists)
                    return null;
                using (SQLiteConnection con = new SQLiteConnection("Data Source =" + root.FullName + "\\" + loginid + ".db"))
                {
                    con.Open();
                    string sql = "select name from sqlite_master where type='table' order by name;";
                    SQLiteCommand cmd = new SQLiteCommand(sql, con);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(reader[0].ToString().ToUpper());
                    }
                }
                return result.ToArray();
            }
            catch(Exception ex)
            {
                return new string[] { "", ex.Message };
            }
        }
        public Tuple<Guid, string> GetTable(string loginid, string userid, string tablename)
        {
            DataTable data = new DataTable();
            try
            {
                var root = new DirectoryInfo(System.Windows.Forms.Application.StartupPath + "\\..\\DataBackUp");
                if (!root.Exists)
                    return new Tuple<Guid, string>(Guid.Empty, "归档数据路径错误");
                using (SQLiteConnection con = new SQLiteConnection("Data Source =" + root.FullName + "\\" + loginid + ".db"))
                {
                    con.Open();
                    string sql = "select * from " + tablename;
                    SQLiteDataAdapter adp = new SQLiteDataAdapter(sql, con);
                    adp.Fill(data);
                }
                var factory = new DataContainerFactory(data);
                return new Tuple<Guid, string>(LocalizationDataTraceBLL.Add(userid, factory), "");
            }
            catch (Exception ex)
            {
                return new Tuple<Guid, string>(Guid.Empty, ex.Message);
            }
        }

        public string RemoveTable(string loginid, string[] tablenames)
        {
            try
            {
                var root = new DirectoryInfo(System.Windows.Forms.Application.StartupPath + "\\..\\DataBackUp");
                if (!root.Exists)
                    return "删除成功";
                using (SQLiteConnection con = new SQLiteConnection("Data Source =" + root.FullName + "\\" + loginid + ".db"))
                {
                    con.Open();
                    using (SQLiteTransaction tran = con.BeginTransaction())
                    {
                        string sql = "drop table if exists {0}";
                        SQLiteCommand dropcmd = new SQLiteCommand(con);
                        foreach (var tablename in tablenames)
                        {
                            dropcmd.CommandText = string.Format(sql, tablename);
                            dropcmd.ExecuteNonQuery();
                        }
                        tran.Commit();
                    }
                }
                return "删除成功";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
