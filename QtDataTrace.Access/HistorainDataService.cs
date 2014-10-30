using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using EAS.Services;
using QtDataTrace.Interfaces;

namespace QtDataTrace.Access
{

    [ServiceObject("历史数据访问服务服务")]
    [ServiceBind(typeof(IHistorainDataService))]
    public class HistorainDataService : ServiceObject, IHistorainDataService
    {
        public IoConfig GetIoConfig()
        {
            IoConfig config = new IoConfig();
            Dictionary<string, QtDataTrace.Interfaces.Module> modules = new Dictionary<string, Interfaces.Module>();

            using (SqlConnection connection = new SqlConnection(ConnectionString.LYQ_HISTORAIN))
            {
                connection.Open();
                {
                    string sql = "SELECT distinct NULL PARENT, substring(Tag.TagName, 1, PATINDEX ( '%.%', Tag.TagName)-1) ID, substring(Tag.TagName, 1, PATINDEX ( '%.%', Tag.TagName)-1) TagName, Description = NULL, MinRaw = NULL, MaxRaw = NULL, Unit = NULL,MinEU=NULL, MaxEU=null " +
                                 " FROM runtime.dbo.AnalogTag, runtime.dbo.EngineeringUnit, runtime.dbo.Tag " +
                                 " WHERE runtime.dbo.Tag.TagName = runtime.dbo.AnalogTag.TagName " +
                                 " AND runtime.dbo.AnalogTag.EUKey = runtime.dbo.EngineeringUnit.EUKey " +
                                 " and runtime.dbo.Tag.TagName not like 'sys%'  ";

                    SqlCommand command = new SqlCommand(sql, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        if (reader.IsDBNull(0))
                        {
                            Workshop workshop = new Workshop(reader.GetString(1));

                            config.Workshops.Add(workshop);
                        }
                    }
                    reader.Close();
                }

                {//module
                    string sql = " SELECT distinct substring(TagName, 1, PATINDEX ( '%.%', TagName)-1) PARENT, substring(parent, 1, patindex('%.%', parent)-1) ID, substring(parent, 1, patindex('%.%', parent)-1) TagName, Description = NULL, MinRaw = NULL, MaxRaw = NULL, Unit = NULL,MinEU=NULL, MaxEU=null " +
                                 " from " +
                                 " (SELECT substring(Tag.TagName, PATINDEX ( '%.%', Tag.TagName)+1, LEN(Tag.TagName)) PARENT, ID = Tag.TagName, TagName = Tag.TagName, Description = Tag.Description, MinRaw, MaxRaw, Unit,MinEU, MaxEU  " +
                                 " FROM runtime.dbo.AnalogTag, runtime.dbo.EngineeringUnit, runtime.dbo.Tag  " +
                                 " WHERE runtime.dbo.Tag.TagName = runtime.dbo.AnalogTag.TagName " +
                                 " AND runtime.dbo.AnalogTag.EUKey = runtime.dbo.EngineeringUnit.EUKey " +
                                 " and runtime.dbo.Tag.TagName not like 'sys%' ) t " +
                                 " union " +
                                 " SELECT distinct substring(TagName, 1, PATINDEX ( '%.%', TagName)-1) PARENT, substring(parent, 1, patindex('%.%', parent)-1) ID, substring(parent, 1, patindex('%.%', parent)-1) TagName, Description = NULL, MinRaw = NULL, MaxRaw = NULL, Unit = NULL,MinEU=NULL, MaxEU=null " +
                                 " from " +
                                 " (SELECT substring(Tag.TagName, PATINDEX ( '%.%', Tag.TagName)+1, len(Tag.TagName)) PARENT, ID = Tag.TagName, TagName = Tag.TagName, Description = Tag.Description, null MinRaw, null MaxRaw, null Unit, null MinEU, null MaxEU  " +
                                 " FROM runtime.dbo.DiscreteTag, runtime.dbo.EngineeringUnit, runtime.dbo.Tag " +
                                 " WHERE runtime.dbo.Tag.TagName = runtime.dbo.DiscreteTag.TagName " +
                                 " and runtime.dbo.Tag.TagName not like 'sys%' ) b";

                    SqlCommand command = new SqlCommand(sql, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        QtDataTrace.Interfaces.Module module = new QtDataTrace.Interfaces.Module(reader.GetString(1));
                        modules.Add(module.Name, module);

                        string parent = reader.GetString(0);

                        foreach (Workshop shop in config.Workshops)
                        {
                            if (shop.Name == parent)
                            {
                                shop.Modules.Add(module);
                            }
                        }
                    }
                    reader.Close();
                }

                {//analog
                    string sql = " select substring(PARENT, 1, PATINDEX ( '%.%', PARENT)-1) PARENT, TagName ID, TagName, Description, MinRaw, MaxRaw, Unit,MinEU, MaxEU  " +
                                 " from " +
                                 " (SELECT substring(Tag.TagName, PATINDEX ( '%.%', Tag.TagName)+1, len(Tag.TagName)) PARENT, ID = Tag.TagName, TagName = Tag.TagName, Description = Tag.Description, MinRaw, MaxRaw, Unit,MinEU, MaxEU  " +
                                 " FROM runtime.dbo.AnalogTag, runtime.dbo.EngineeringUnit, runtime.dbo.Tag  " +
                                 " WHERE runtime.dbo.Tag.TagName = runtime.dbo.AnalogTag.TagName " +
                                 " AND runtime.dbo.AnalogTag.EUKey = runtime.dbo.EngineeringUnit.EUKey " +
                                 " and runtime.dbo.Tag.TagName not like 'sys%' ) a";

                    SqlCommand command = new SqlCommand(sql, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string parent = reader.GetString(0);
                        SignalId id = SignalId.Parser(reader.GetString(1));

                        Signal signal = new Signal();

                        signal.Id = id;
                        signal.Name = id.Name;
                        signal.Comment = reader.GetString(3);
                        signal.MinRaw = reader.GetDouble(4);
                        signal.MaxRaw = reader.GetDouble(5);
                        signal.Unit = reader.GetString(6);
                        signal.MinEU = reader.GetDouble(7);
                        signal.MaxEU = reader.GetDouble(8);

                        //tags[signal.Id.IdString] = signal;

                        if (modules.ContainsKey(parent))
                        {
                            QtDataTrace.Interfaces.Module module = modules[parent];
                            module.Analogs.Add(signal);
                        }
                    }
                    reader.Close();
                }

                {//digital
                    string sql = " select distinct substring(PARENT, 1, PATINDEX ( '%.%', PARENT)-1) PARENT, TagName ID, TagName, Description, MinRaw, MaxRaw, Unit,MinEU, MaxEU  " +
                    " from " +
                    " (SELECT substring(Tag.TagName, PATINDEX ( '%.%', Tag.TagName)+1, len(Tag.TagName)) PARENT, ID = Tag.TagName, TagName = Tag.TagName, Description = Tag.Description, null MinRaw, null MaxRaw, null Unit, null MinEU, null MaxEU  " +
                    " FROM runtime.dbo.DiscreteTag, runtime.dbo.EngineeringUnit, runtime.dbo.Tag " +
                    " WHERE runtime.dbo.Tag.TagName = runtime.dbo.DiscreteTag.TagName " +
                    " and runtime.dbo.Tag.TagName not like 'sys%' ) c";

                    SqlCommand command = new SqlCommand(sql, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string parent = reader.GetString(0);
                        SignalId id = SignalId.Parser(reader.GetString(1), true);

                        Signal signal = new Signal();

                        signal.Id = id;
                        signal.Comment = reader.GetString(3);

                        //tags[signal.Id.IdString] = signal;

                        if (modules.ContainsKey(parent))
                        {
                            QtDataTrace.Interfaces.Module module = modules[parent];
                            module.Digitals.Add(signal);
                        }
                    }
                    reader.Close();
                }
            }

            return config;
        }

        public IoDataMessage GetIoData(SignalRequest[] requests, TimeRange range, ulong resolution)
        {
            IoDataMessage msg = new IoDataMessage();
            int size = (int)(range.Range.TotalMilliseconds / resolution);

            if (range.Range.TotalMilliseconds <= 0)
            {
                //Logger.DebugFormat("{0}", range.ToString());
                return null;
            }

            if (size <= 0)
            {
                //Logger.DebugFormat("buffer size is {0}", size);
                return null;
            }

            msg.timeRange = range;
            msg.requestId = 0;
            msg.data = new Array[requests.Length];

            string tags = "";
            for (int i = 0; i < requests.Length; i++)
            {
                double[] buffer = new double[(int)(size * 1.2)];
                msg.data[i] = buffer;

                tags += string.Format("Convert(nvarchar,[{0}])", requests[i].signalId.IdString);

                if (i < requests.Length - 1)
                    tags += ",";
            }

            string sql = "SET QUOTED_IDENTIFIER OFF " +
                   " SELECT * FROM OPENQUERY(INSQL,\" SELECT DateTime = convert(nvarchar, DateTime, 21), " + tags +
                   " FROM WideHistory " +
                   " WHERE wwRetrievalMode = 'Cyclic' " +
                   " AND wwResolution = {2} " +
                   " AND wwVersion = 'Latest' " +
                   " AND DateTime >='{0}' " +
                   " AND DateTime <= '{1}' " +
                   "\")";

            SqlConnection connection = new SqlConnection(ConnectionString.LYQ_HISTORAIN);

            try
            {

                DateTime start = DateTime.FromFileTime(range.Begin);
                DateTime stop = DateTime.FromFileTime(range.End);

                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = string.Format(sql, start.ToString("yyyyMMdd HH:mm:ss.fff"), stop.ToString("yyyyMMdd HH:mm:ss.fff"), resolution);

                SqlDataReader reader = command.ExecuteReader();

                for (int k = 0; reader.Read() && k < size; k++)
                {
                    for (int i = 0; i < requests.Length; i++)
                    {
                        if (reader.IsDBNull(i + 1) == false)
                        {
                            double[] buffer = (double[])msg.data[i];

                            buffer[k] = System.Convert.ToDouble(reader.GetValue(i + 1));
                        }
                    }
                }

                reader.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
                //Logger.Error(ex);
            }
            finally
            {
                connection.Close();
            }

            return msg;
        }
    }
}
