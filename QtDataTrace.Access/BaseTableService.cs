using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using EAS.Services;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using QtDataTrace.Access;
using QtDataTrace.Interfaces;
using log4net;

namespace QtDataTrace.Access
{
    [ServiceObject("基础数据维护服务")]
    [ServiceBind(typeof(IBaseTableService))]
    public class BaseTableService : ServiceObject, IBaseTableService
    {
        public DataSet GetProcessCode()
        {
            DataSet data = new DataSet();

            OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);

            try
            {
                connection.Open();

                string sql;
                OleDbDataAdapter adapter;

                sql = "SELECT * FROM PROCESS_CODE";
                adapter = new OleDbDataAdapter(sql, connection);
                adapter.Fill(data, "PROCESS");
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
                adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                adapter.Fill(data[1], "USER_TABLES");

                sql = "select COLUMN_NAME,TABLE_NAME from USER_TAB_COLUMNS";
                adapter = new OleDbDataAdapter(sql, connection);
                adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
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
            /*
            OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);

            try
            {
                connection.Open();

                string sql = "SELECT * FROM PROCESS_QTTABLE_CONFIG WHERE 1 = 0";
                OleDbDataAdapter adapter = new OleDbDataAdapter(sql, connection);
                OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);
                adapter.AcceptChangesDuringFill = false; // Important
                adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                adapter.Update(data.Tables["PROCESS_QTTABLE_CONFIG"]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }*/
        }




        public QtDataSourceConfig GetQtDataSourceConfig(string process)
        {
            QtDataSourceConfig result = new QtDataSourceConfig();

            OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);

            try
            {
                connection.Open();

                string sql;

                sql = string.Format("SELECT * FROM PROCESS_QTTABLE_CONFIG where PROCESS_CODE = '{0}'", process);
                OleDbCommand command = new OleDbCommand(sql, connection);

                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        QtDataTableConfig table = new QtDataTableConfig();

                        table.TableName = reader["table_name"].ToString();
                        table.ChineseName = reader["TABLE_CHINESE"].ToString();

                        result.Tables.Add(table);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }

            return result;
        }




    }
}
