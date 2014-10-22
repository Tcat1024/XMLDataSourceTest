using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data.OleDb;
using QtDataTrace.Interfaces;
using Expression;
using log4net;

namespace QtDataTrace.Access
{
    [System.Serializable]
    public class RtCurveTrace : MarshalByRefObject, IRtCurveTrace
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private List<MaterialTrace> track = new List<MaterialTrace>();
        private HistorainStore dataStore;
        private PointConfig pointConfig;

        public RtCurveTrace()
        {
            pointConfig = new PointConfig();
            dataStore = new HistorainStore(pointConfig);
        }

        public CPointConfig GetPointConfig()
        {
            CPointConfig config = new CPointConfig();

            OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);

            try
            {
                connection.Open();

                string sql = "select a.device_no, a.data_acqu_loc, a.data_acqu_loc_comment, a.data_start_time_eqution, a.resolution, b.data_item_name, b.data_item_comment, b.rtdb_point_config, b.rt_data_feature, b.data_acq_loc, b.data_expression  " +
                             "from process_timeget_function a, device_realitem_config b " +
                             "where a.device_no = b.device_no and a.data_acqu_loc = b.data_acq_loc " +
                             "order by a.data_acqu_loc";

                OleDbCommand command = new OleDbCommand(sql, connection);
                OleDbDataReader reader = command.ExecuteReader();

                CDevice device = null;
                CAcqLocation acq = null;

                while (reader.Read())
                {
                    if (device == null)
                    {
                        device = new CDevice();
                        device.Name = reader.GetString(0);
                        config.Devices.Add(device);
                        acq = null;
                    }
                    else if (device.Name != reader.GetString(0))
                    {
                        device = new CDevice();
                        device.Name = reader.GetString(0);
                        config.Devices.Add(device);
                        acq = null;
                    }

                    if (acq == null)
                    {
                    }
                    else if (acq.Location != reader.GetString(1))
                    {
                        acq = null;
                    }

                    if (acq == null)
                    {
                        acq = new CAcqLocation();
                        acq.Location = reader.GetString(1);
                        acq.Comment = reader.GetString(2);
                        acq.Expression = reader.GetString(3);
                        acq.Resolution = System.Convert.ToUInt32(reader.GetDecimal(4));

                        device.Locations.Add(acq);
                    }

                    CPoint point = new CPoint();

                    point.Name = reader.GetString(5);
                    point.Comment = reader.GetString(6);
                    point.Tag = reader.GetString(7);
                    point.Feature = reader.GetString(8);
                    point.Location = reader.GetString(9);
                    point.Expression = reader.IsDBNull(10) ? "" : reader.GetString(10);

                    acq.Points.Add(point);
                }

                reader.Close();
            }
            catch (OleDbException ex)
            {
                Logger.Error(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return config;
        }

        public IChannelData GetChannel(string name)
        {
            IChannelData channelData = null;
            Point point = null;
            point = pointConfig.Lookup(name);
            if (point != null)
            {
                ExpressionManager mgr = new ExpressionManager(dataStore);

                IExpressionItem exprItem = mgr.Compile(point.Expression);

                channelData = exprItem.Calculate();
            }

            return channelData;
        }

        public void Initialize(string matId)
        {
            Initialize("LY210", matId);
            Initialize("LY2250", matId);
        }

        public void Initialize(string workshop, string matId)
        {
            Load(workshop, matId);

            foreach (MaterialTrace trk in track)
            {
                Range range = new Range(trk.StartTime, trk.StopTime);

                pointConfig.Load(trk.Device, range);
            }

            pointConfig.CalculateRange(dataStore);
        }

        private void Load(string workshop, string matId)
        {
            String sql;
            string table;

            Dictionary<string, string> tabledef = new Dictionary<string, string>();

            tabledef["LY2250"] = "HRM_MATTRACK_TIME";
            tabledef["LY210"] = "SM_MATTRACK_TIME";

            if (tabledef.ContainsKey(workshop))
            {
                table = tabledef[workshop];
            }
            else
            {
                throw new Exception("工厂代码错误");
            }

            sql = string.Format("SELECT * FROM {0} WHERE MAT_NO = '{1}'", table, matId);

            OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);

            try
            {
                connection.Open();

                OleDbCommand command = new OleDbCommand(sql, connection);

                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    MaterialTrace mat = new MaterialTrace();

                    mat.MaterialId = reader.GetString(0);
                    mat.Device = reader.GetString(1);
                    mat.StartTime = reader.GetDateTime(2);
                    mat.StopTime = reader.GetDateTime(3);

                    track.Add(mat);
                }
                reader.Close();
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
        }

        private void Load(string workshop, string device, string matId)
        {
            String sql;

            string table;

            Dictionary<string, string> tabledef = new Dictionary<string, string>();

            tabledef["LY2250"] = "HRM_MATTRACK_TIME";
            tabledef["LY210"] = "SM_MATTRACK_TIME";

            if (tabledef.ContainsKey(workshop))
            {
                table = tabledef[workshop];
            }
            else
            {
                throw new Exception("工厂代码错误");
            }

            sql = string.Format("SELECT * FROM {0} WHERE MAT_NO = {1} AND DEVICE_NO = {2}", table, matId, device);

            OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);

            try
            {
                connection.Open();

                OleDbCommand command = new OleDbCommand(sql, connection);

                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    MaterialTrace mat = new MaterialTrace();

                    mat.MaterialId = reader.GetString(0);
                    mat.Device = reader.GetString(1);
                    mat.StartTime = reader.GetDateTime(2);
                    mat.StopTime = reader.GetDateTime(3);

                    track.Add(mat);
                }

                reader.Close();
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
        }
    }
}
