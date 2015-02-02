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
using QtDataTrace.AnalyzeIService;

namespace QtDataTrace.AnalyzeService
{
    [ServiceObject("质量数据追踪服务")]
    [ServiceBind(typeof(ILocalizationDataTraceService))]
    public class LocalizationDataTraceService : ServiceObject, ILocalizationDataTraceService
    {
        public DataSet GetProcessQtTableConfigFile()
        {
            DataSet data = new DataSet();
            string path = System.Windows.Forms.Application.StartupPath;

            OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);
            try
            {
                data.ReadXmlSchema(string.Format("{0}\\..\\Config\\ProcessQtTableConfig.xsd", path));
                data.ReadXml(string.Format("{0}\\..\\Config\\ProcessQtTableConfig.xml", path));

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }
        public List<DataSet> GetProcessQtTableConfig()
        {
            List<DataSet> data = new List<DataSet>();
            data.Add(new DataSet());
            data.Add(new DataSet());
            string path = System.Windows.Forms.Application.StartupPath;

            OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);
            try
            {

                data[0].ReadXmlSchema(string.Format("{0}\\..\\Config\\ProcessQtTableConfig.xsd", path));
                data[0].ReadXml(string.Format("{0}\\..\\Config\\ProcessQtTableConfig.xml", path));

                connection.Open();

                string sql;
                OleDbDataAdapter adapter;

                sql = "select * from process_code";
                adapter = new OleDbDataAdapter(sql, connection);
                adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                adapter.Fill(data[1], "PROCESS");

                sql = "select TABLE_NAME from user_tables order by table_name";
                adapter = new OleDbDataAdapter(sql, connection);
                adapter.Fill(data[1], "USER_TABLES");
                sql = "select VIEW_NAME \"TABLE_NAME\" from user_views order by VIEW_NAME";
                adapter = new OleDbDataAdapter(sql, connection);
                DataTable viewtemp = new DataTable();
                adapter.Fill(data[1], "USER_TABLES");
                adapter.Fill(viewtemp);

                sql = "select COLUMN_NAME,TABLE_NAME from USER_TAB_COLs";
                adapter = new OleDbDataAdapter(sql, connection);
                adapter.Fill(data[1], "USER_TAB_COLUMNS");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }

            return data;
        }

        public void SaveProcessQtTableConfig(DataSet data)
        {
            string path = System.Windows.Forms.Application.StartupPath;
            try
            {
                data.WriteXml(string.Format("{0}\\..\\Config\\ProcessQtTableConfig.xml", path));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Tuple<Guid, string> NewDataTrace(string processNo, IList<string> iDList, IList<QtDataProcessConfig> processes, bool back, string username)
        {
            try
            {
                var factory = new DataTraceFactory(processNo,iDList,processes,back);
                var result = new Tuple<Guid, string>(LocalizationDataTraceBLL.Add(username,factory), "");
                factory.Start();
                return result;
            }
            catch (Exception ex)
            {
                return new Tuple<Guid, string>(Guid.Empty, ex.Message);
            }
        }
        public DataTable GetData(string username, Guid id)
        {
            var factory = LocalizationDataTraceBLL.GetFactory(username, id);
            if (factory != null && !factory.Writing)
            {
                return factory.ResultData;
            }
            return null;
        }
        public int GetProcess(string username, Guid id)
        {
            var temp = LocalizationDataTraceBLL.GetFactory(username, id);
            if (temp == null)
                return -2;
            if (temp.Writing)
                return temp.GetProgress();
            if (temp.Error != "")
                return -1;
            return 1000;
        }
        public string GetErrorMessage(string username, Guid id)
        {
            var factory = LocalizationDataTraceBLL.GetFactory(username, id);
            if (factory != null)
                return factory.Error;
            return null;
        }
        public bool Stop(string username, Guid id)
        {
            return LocalizationDataTraceBLL.Stop(username, id);
        }
        public string CommitData(string username,Guid id,DataTable data)
        {
            var factory = LocalizationDataTraceBLL.GetFactory(username, id);
            if(factory==null)
            {
                factory = new DataContainerFactory(data);
                try
                {
                    LocalizationDataTraceBLL.Add(username, factory,id);
                    return "";
                }
                catch(Exception ex)
                {
                    return ex.Message;
                }
            }
            else if(factory.Writing)
            {
                return "指定数据正在被写入，无法提交";
            }
            else if(factory.Reading)
            {
                return "指定数据正在被读取，无法提交";
            }
            else
            {
                factory.ResultData = data;
                return "";
            }
        }
    }
    public class ConfigException : Exception
    {
        public ConfigException(string message)
            : base(message)
        {

        }
    }
}
