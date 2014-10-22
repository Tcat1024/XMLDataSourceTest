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
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public DataSet GetWorkshopInfo()
        {
            
            DataSet data = new DataSet();

            OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);

            try
            {
                connection.Open();

                string sql;
                OleDbDataAdapter adapter;

                sql = "SELECT * FROM WORKSHOP_CODE";
                adapter = new OleDbDataAdapter(sql, connection);
                adapter.Fill(data, "WORKSHOP");
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

        public void SaveWorkshopInfo(DataSet data)
        {
            OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);

            try
            {
                connection.Open();

                string sql = "SELECT * FROM WORKSHOP_CODE WHERE 1 = 0";
                OleDbDataAdapter adapter = new OleDbDataAdapter(sql, connection);
                OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);
                adapter.AcceptChangesDuringFill = false; // Important
                adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                adapter.Update(data.Tables["WORKSHOP"]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

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

        public void SaveProcessCode(DataSet data)
        {
            OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);

            try
            {
                connection.Open();

                string sql = "SELECT * FROM PROCESS_CODE WHERE 1 = 0";
                OleDbDataAdapter adapter = new OleDbDataAdapter(sql, connection);
                OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);
                adapter.AcceptChangesDuringFill = false; // Important
                adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                adapter.Update(data.Tables["PROCESS"]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }


        public DataSet GetEquipmentInfo()
        {
            DataSet data = new DataSet();

            OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);

            try
            {
                connection.Open();

                string sql;
                OleDbDataAdapter adapter;

                sql = "SELECT * FROM WORKSHOP_CODE";
                adapter = new OleDbDataAdapter(sql, connection);
                adapter.Fill(data, "WORKSHOP");

                sql = "select * from process_code";
                adapter = new OleDbDataAdapter(sql, connection);
                adapter.Fill(data, "PROCESS");

                sql = "select * from equipment_code";
                adapter = new OleDbDataAdapter(sql, connection);
                adapter.Fill(data, "EQUIPMENT");
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return data;
        }

        public void SaveEquipmentInfo(DataSet data)
        {
            OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);

            try
            {
                connection.Open();

                string sql = "SELECT * FROM EQUIPMENT_CODE WHERE 1 = 0";
                OleDbDataAdapter adapter = new OleDbDataAdapter(sql, connection);
                OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);
                adapter.AcceptChangesDuringFill = false; // Important
                adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                adapter.Update(data.Tables["EQUIPMENT"]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public DataSet GetProcessTimeGetFunction()
        {
            DataSet data = new DataSet();

            OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);

            try
            {
                connection.Open();

                string sql = "SELECT * FROM PROCESS_TIMEGET_FUNCTION";

                OleDbDataAdapter adapter = new OleDbDataAdapter(sql, connection);
                adapter.Fill(data, "PROCESS_TIMEGET_FUNCTION");
            }
            catch (OleDbException ex)
            {
                Logger.Error(ex.Message);
                throw ex;
            }
            finally
            {
                connection.Close();
            }

            return data;
        }

        public void SaveProcessTimeGetFunction(DataSet data)
        {
            OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);

            try
            {
                connection.Open();

                string sql = "SELECT * FROM PROCESS_TIMEGET_FUNCTION WHERE 1 = 0";
                OleDbDataAdapter adapter = new OleDbDataAdapter(sql, connection);
                OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);
                adapter.AcceptChangesDuringFill = false; // Important
                adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                adapter.Update(data.Tables["PROCESS_TIMEGET_FUNCTION"]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public DataSet GetDeviceRealItemConfig()
        {
            DataSet data = new DataSet();

            OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);

            try
            {
                connection.Open();

                string sql = "SELECT * FROM DEVICE_REALITEM_CONFIG";

                OleDbDataAdapter adapter = new OleDbDataAdapter(sql, connection);
                adapter.Fill(data, "DEVICE_REALITEM_CONFIG");
            }
            catch (OleDbException ex)
            {
                Logger.Error(ex.Message);
                throw ex;
            }
            finally
            {
                connection.Close();
            }

            return data;
        }

        public void SaveDeviceRealItemConfig(DataSet data)
        {
            OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);

            try
            {
                connection.Open();

                string sql = "SELECT * FROM DEVICE_REALITEM_CONFIG WHERE 1 = 0";
                OleDbDataAdapter adapter = new OleDbDataAdapter(sql, connection);
                OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);
                adapter.AcceptChangesDuringFill = false; // Important
                adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                adapter.Update(data.Tables["DEVICE_REALITEM_CONFIG"]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
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

        public DataSet GetQtTraceDataSourceConfig()
        {
            DataSet data = new DataSet();

            OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);

            try
            {
                connection.Open();

                string sql;
                OleDbDataAdapter adapter;

                sql = "SELECT * FROM QTTRACE_DATASOURCE_CONFIG";
                adapter = new OleDbDataAdapter(sql, connection);
                adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                adapter.Fill(data, "QTTRACE_DATASOURCE_CONFIG");

                sql = "select * from process_code";
                adapter = new OleDbDataAdapter(sql, connection);
                adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                adapter.Fill(data, "PROCESS");

                sql = "select T.TABLE_NAME,T.COLUMN_NAME,T.DATA_TYPE, " +
                        "decode(T.DATA_TYPE,'VARCHAR2',T.DATA_LENGTH,'NUMBER',T.DATA_PRECISION,T.DATA_LENGTH) DATA_LENGTH,T.DATA_SCALE, " +
                        " v.POSITION,c.COMMENTS " +
                        " from user_col_comments c,user_tab_cols t,(select a.TABLE_NAME,a.COLUMN_NAME,a.POSITION from user_cons_columns a,user_constraints b where a.CONSTRAINT_NAME=b.CONSTRAINT_NAME " +
                                                                    " and b.CONSTRAINT_TYPE='P')  v  " +
                        " where t.TABLE_NAME=c.TABLE_NAME and t.COLUMN_NAME=c.COLUMN_NAME " +
                        " and c.TABLE_NAME=v.TABLE_NAME(+) and c.COLUMN_NAME=v.COLUMN_NAME(+) " +
                        " ORDER BY T.TABLE_NAME,v.POSITION,T.COLUMN_NAME ";

                adapter = new OleDbDataAdapter(sql, connection);
                adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                adapter.Fill(data, "TABLES_DEFINE");
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

        public void SaveQtTraceDataSourceConfig(DataSet data)
        {
            OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);

            try
            {
                connection.Open();

                string sql = "SELECT * FROM QTTRACE_DATASOURCE_CONFIG WHERE 1 = 0";
                OleDbDataAdapter adapter = new OleDbDataAdapter(sql, connection);
                OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);
                adapter.AcceptChangesDuringFill = false; // Important
                adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                adapter.Update(data.Tables["QTTRACE_DATASOURCE_CONFIG"]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
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

        public DataSet GetPointConfig()
        {
            DataSet xmlDataSet = new DataSet();

            string path = System.Windows.Forms.Application.StartupPath;

            xmlDataSet.ReadXmlSchema(string.Format("{0}\\..\\Config\\PointConfigure.xsd", path));

            xmlDataSet.ReadXml(string.Format("{0}\\..\\Config\\PointConfigure.xml", path));

            return xmlDataSet;
        }

        public void SavePointConfig(DataSet data)
        {
            string path = System.Windows.Forms.Application.StartupPath;
            data.WriteXml(string.Format("{0}\\..\\Config\\PointConfigure.xml", path));
        }

        public DataSet GetTagConfig()
        {
            DataSet data = new DataSet();

            SqlConnection connection = new SqlConnection(ConnectionString.LYQ_HISTORAIN);

            connection.Open();

            string sql = "SELECT NULL PARENT, substring(Tag.TagName, 1, PATINDEX ( '%.%', Tag.TagName)-1) ID, substring(Tag.TagName, 1, PATINDEX ( '%.%', Tag.TagName)-1) TagName, Description = NULL, MinRaw = NULL, MaxRaw = NULL, Unit = NULL,MinEU=NULL, MaxEU=null " +
                         " FROM runtime.dbo.AnalogTag, runtime.dbo.EngineeringUnit, runtime.dbo.Tag " +
                         " WHERE runtime.dbo.Tag.TagName = runtime.dbo.AnalogTag.TagName " +
                         " AND runtime.dbo.AnalogTag.EUKey = runtime.dbo.EngineeringUnit.EUKey " +
                         " and runtime.dbo.Tag.TagName not like 'sys%'  " +
                         " union " +
                         " SELECT distinct substring(TagName, 1, PATINDEX ( '%.%', TagName)-1) PARENT, substring(parent, 1, patindex('%.%', parent)-1) ID, substring(parent, 1, patindex('%.%', parent)-1) TagName, Description = NULL, MinRaw = NULL, MaxRaw = NULL, Unit = NULL,MinEU=NULL, MaxEU=null " +
                         " from " +
                         " (SELECT substring(Tag.TagName, PATINDEX ( '%.%', Tag.TagName)+1, LEN(Tag.TagName)) PARENT, ID = Tag.TagName, TagName = Tag.TagName, Description = Tag.Description, MinRaw, MaxRaw, Unit,MinEU, MaxEU  " +
                         " FROM runtime.dbo.AnalogTag, runtime.dbo.EngineeringUnit, runtime.dbo.Tag  " +
                         " WHERE runtime.dbo.Tag.TagName = runtime.dbo.AnalogTag.TagName " +
                         " AND runtime.dbo.AnalogTag.EUKey = runtime.dbo.EngineeringUnit.EUKey " +
                         " and runtime.dbo.Tag.TagName not like 'sys%' ) t " +
                         " union " +
                         " select substring(PARENT, 1, PATINDEX ( '%.%', PARENT)-1) PARENT, TagName ID, TagName, Description, MinRaw, MaxRaw, Unit,MinEU, MaxEU  " +
                         " from " +
                         " (SELECT substring(Tag.TagName, PATINDEX ( '%.%', Tag.TagName)+1, len(Tag.TagName)) PARENT, ID = Tag.TagName, TagName = Tag.TagName, Description = Tag.Description, MinRaw, MaxRaw, Unit,MinEU, MaxEU  " +
                         " FROM runtime.dbo.AnalogTag, runtime.dbo.EngineeringUnit, runtime.dbo.Tag  " +
                         " WHERE runtime.dbo.Tag.TagName = runtime.dbo.AnalogTag.TagName " +
                         " AND runtime.dbo.AnalogTag.EUKey = runtime.dbo.EngineeringUnit.EUKey " +
                         " and runtime.dbo.Tag.TagName not like 'sys%' ) a" +
                         " union " +
                         " SELECT distinct substring(TagName, 1, PATINDEX ( '%.%', TagName)-1) PARENT, substring(parent, 1, patindex('%.%', parent)-1) ID, substring(parent, 1, patindex('%.%', parent)-1) TagName, Description = NULL, MinRaw = NULL, MaxRaw = NULL, Unit = NULL,MinEU=NULL, MaxEU=null " +
                         " from " +
                         " (SELECT substring(Tag.TagName, PATINDEX ( '%.%', Tag.TagName)+1, len(Tag.TagName)) PARENT, ID = Tag.TagName, TagName = Tag.TagName, Description = Tag.Description, null MinRaw, null MaxRaw, null Unit, null MinEU, null MaxEU  " +
                         " FROM runtime.dbo.DiscreteTag, runtime.dbo.EngineeringUnit, runtime.dbo.Tag " +
                         " WHERE runtime.dbo.Tag.TagName = runtime.dbo.DiscreteTag.TagName " +
                         " and runtime.dbo.Tag.TagName not like 'sys%' ) b" +
                         " union " +
                         " select substring(PARENT, 1, PATINDEX ( '%.%', PARENT)-1) PARENT, TagName ID, TagName, Description, MinRaw, MaxRaw, Unit,MinEU, MaxEU  " +
                         " from " +
                         " (SELECT substring(Tag.TagName, PATINDEX ( '%.%', Tag.TagName)+1, len(Tag.TagName)) PARENT, ID = Tag.TagName, TagName = Tag.TagName, Description = Tag.Description, null MinRaw, null MaxRaw, null Unit, null MinEU, null MaxEU  " +
                         " FROM runtime.dbo.DiscreteTag, runtime.dbo.EngineeringUnit, runtime.dbo.Tag " +
                         " WHERE runtime.dbo.Tag.TagName = runtime.dbo.DiscreteTag.TagName " +
                         " and runtime.dbo.Tag.TagName not like 'sys%' ) c";

            SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
            adapter.Fill(data, "TAG_DEFINE");

            return data;
        }
    }
}
