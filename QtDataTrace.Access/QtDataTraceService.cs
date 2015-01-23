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
using QtDataTrace.IService;

namespace QtDataTrace.Access
{
    [ServiceObject("质量数据追踪服务")]
    [ServiceBind(typeof(IQtDataTraceService))]
    public class QtDataTraceService : ServiceObject, IQtDataTraceService
    {
        public IList<MaterialInfo> GetMaterialList(QueryArgs arg)
        {
            IList<MaterialInfo> result = new List<MaterialInfo>();

            //string table = string.Format("{0}_material_info_view", arg.ProcessCode);
            string whereClause = " where out_mat_id IS NOT NULL ";

            if (arg.TimeFlag)
            {
                whereClause += string.Format(" and product_time >= to_date('{0}', 'YYYYMMDDHH24MI') and product_time <= to_date('{1}', 'YYYYMMDDHH24MI') ", arg.StartTime.ToString("yyyyMMddHHmm"), arg.StopTime.ToString("yyyyMMddHHmm"));
            }

            if (arg.SteelGradeFlag)
            {
                whereClause += string.Format(" and steel_grade = '{0}'", arg.SteelGrade);
            }

            if (arg.ThickFlag)
            {
                whereClause += string.Format(" and thickness >= {0} and thickness <= {1}", arg.MinThick, arg.MaxThick);
            }

            if (arg.WidthFlag)
            {
                whereClause += string.Format(" and width >= {0} and width <= {1}", arg.MinWidth, arg.MaxWidth);
            }

            //string sql1 = "select * " +
            //             " from " + table +
            //             whereClause +
            //             " order by product_time";

            OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);
            string sql = string.Format("select sqlstring from DATATRACE_TABLE_CONFIG where processcode ='{0}'", arg.ProcessCode);
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(sql, connection);
                string table = string.Format("({0})", command.ExecuteScalar());
                if (table == null || table.Trim() == "()")
                    throw new ConfigException("此工序未配置");
                sql = "select * " +
                         " from " + table +
                         whereClause +
                         " order by product_time";
                command.CommandText = sql;
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    MaterialInfo mat = new MaterialInfo();
                    result.Add(mat);

                    mat.InId = reader["in_mat_id"].ToString();
                    mat.OutId = reader["out_mat_id"].ToString();
                    mat.SteelGrade = reader["steel_grade"].ToString();
                    mat.Thickness = reader["thickness"].ToString();
                    mat.Width = reader["width"].ToString();
                    mat.Time = System.Convert.ToDateTime(reader["product_time"]);
                }

                reader.Close();
            }
            catch (ConfigException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(sql, ex);
            }
            finally
            {
                connection.Close();
            }

            return result;
        }
        //public IList<MaterialInfo> GetMaterialTracking(string matId)
        //{
        //    List<MaterialInfo> track = new List<MaterialInfo>();
        //    Dictionary<string, MaterialInfo> mats = new Dictionary<string, MaterialInfo>();

        //    using (OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB))
        //    {
        //        connection.Open();

        //        string prevMatId = "";

        //        while (prevMatId != matId)
        //        {
        //            string sql = string.Format(" select in_mat_id1, in_mat_id2, out_mat_id, device_no, start_time, stop_time " +
        //                                        " from process_mat_pedigree t " +
        //                                        " where out_mat_id = '{0}' ", matId);

        //            try
        //            {
        //                OleDbCommand command = new OleDbCommand(sql, connection);

        //                OleDbDataReader reader = command.ExecuteReader();

        //                prevMatId = matId;

        //                while (reader.Read())
        //                {
        //                    matId = reader.GetString(0);
        //                    string outMatId = reader.GetString(2);

        //                    MaterialInfo mat;

        //                    if (mats.ContainsKey(outMatId))
        //                    {
        //                        mat = mats[outMatId];
        //                    }
        //                    else
        //                    {
        //                        mat = new MaterialInfo();
        //                        mat.Equipments = new List<EquipmentInfo>();

        //                        mat.InId = matId;
        //                        mat.OutId = outMatId;
        //                        mats[outMatId] = mat;
        //                        track.Add(mat);
        //                    }

        //                    EquipmentInfo device = new EquipmentInfo();
        //                    mat.Equipments.Add(device);

        //                    device.Name = reader.GetString(3);
        //                    device.MatId = mat.OutId;
        //                    device.StartTime = reader.GetDateTime(4);
        //                    device.StopTime = reader.GetDateTime(5);

        //                    sql = string.Format("select * from device_area_config where device_no = '{0}' order by display_num", device.Name);

        //                    OleDbCommand cmd = new OleDbCommand(sql, connection);

        //                    using (OleDbDataReader areaReader = cmd.ExecuteReader())
        //                    {
        //                        while (areaReader.Read())
        //                        {
        //                            EquipmentAreaInfo info = new EquipmentAreaInfo();

        //                            info.Name = areaReader["device_no"].ToString();
        //                            info.Workshop = areaReader["workshop_no"].ToString();
        //                            info.Area = areaReader["area_no"].ToString();
        //                            info.DisplaySeq = Convert.ToInt32(areaReader["display_num"].ToString());

        //                            device.Areas.Add(info);
        //                        }
        //                    }
        //                }

        //                reader.Close();
        //            }
        //            catch (OleDbException ex)
        //            {
        //                throw ex;
        //            }
        //        }
        //    }

        //    return track;
        //}

        public IList<MaterialInfo> GetMaterialPedigree(string matId)
        {
            List<MaterialInfo> track = new List<MaterialInfo>();

            Dictionary<string, string> tabledef = new Dictionary<string, string>();

            tabledef["LY2250"] = "HRM_MATTRACK_TIME";
            tabledef["LY210"] = "SM_MATTRACK_TIME";
            tabledef["LYCRM"] = "CRM_MATTRACK_TIME";

            using (OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB))
            {
                connection.Open();

                string prevMatId = "";

                while (prevMatId != matId)
                {
                    string sql = string.Format(" select in_mat_id1, in_mat_id2, out_mat_id, process_code " +
                                                " from process_mat_pedigree t " +
                                                " where out_mat_id = '{0}' ", matId);

                    try
                    {
                        OleDbCommand command = new OleDbCommand(sql, connection);

                        prevMatId = matId;

                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                matId = reader.GetString(0);

                                MaterialInfo mat = new MaterialInfo();
                                mat.Equipments = new List<EquipmentInfo>();
                                mat.InId = reader["in_mat_id1"].ToString();
                                mat.OutId = reader["out_mat_id"].ToString();
                                mat.Process = reader["process_code"].ToString();

                                track.Add(mat);

                                string workshop;

                                sql = string.Format("select distinct workshop_no from equipment_code " +
                                    " where process_code = '{0}'", mat.Process);

                                command = new OleDbCommand(sql, connection);
                                using (OleDbDataReader read1 = command.ExecuteReader())
                                {
                                    if (read1.Read())
                                    {
                                        workshop = read1["workshop_no"].ToString();
                                    }
                                    else
                                    {
                                        throw new Exception("工序代码错误");
                                    }
                                }

                                string table;

                                if (tabledef.ContainsKey(workshop))
                                {
                                    table = tabledef[workshop];
                                }
                                else
                                {
                                    throw new Exception("工厂代码错误");
                                }

                                sql = string.Format("select mat_no, device_no, start_time, stop_time " +
                                    " from {1} " +
                                    " where mat_no = '{0}' and device_no in " +
                                    " (select device_no from equipment_code where process_code = '{2}')",
                                    mat.InId, table, mat.Process);

                                if (mat.Process == "CCM")
                                {
                                    sql = string.Format("select mat_no, device_no, start_time, stop_time " +
                                        " from {1} " +
                                        " where mat_no = '{0}' and device_no in " +
                                        " (select device_no from equipment_code where process_code = '{2}')",
                                        mat.OutId, table, mat.Process);
                                }

                                command = new OleDbCommand(sql, connection);

                                using (OleDbDataReader equ_reader = command.ExecuteReader())
                                {
                                    while (equ_reader.Read())
                                    {
                                        EquipmentInfo device = new EquipmentInfo();
                                        mat.Equipments.Add(device);

                                        device.MatId = matId;
                                        device.Name = equ_reader["device_no"].ToString();
                                        device.Workshop = workshop;
                                        device.StartTime = System.Convert.ToDateTime(equ_reader["start_time"]);
                                        device.StopTime = System.Convert.ToDateTime(equ_reader["stop_time"].ToString());

                                        sql = string.Format("select * from device_area_config where device_no = '{0}' order by display_num", device.Name);

                                        OleDbCommand cmd = new OleDbCommand(sql, connection);

                                        using (OleDbDataReader areaReader = cmd.ExecuteReader())
                                        {
                                            while (areaReader.Read())
                                            {
                                                EquipmentAreaInfo info = new EquipmentAreaInfo();

                                                info.Name = areaReader["device_no"].ToString();
                                                info.Workshop = areaReader["workshop_no"].ToString();
                                                info.Area = areaReader["area_no"].ToString();
                                                info.DisplaySeq = Convert.ToInt32(areaReader["display_num"].ToString());

                                                device.Areas.Add(info);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (OleDbException ex)
                    {
                        throw ex;
                    }
                }
            }

            return track;
        }

        public DataSet GetQtData(QueryArgs arg, QtDataTableConfig table)
        {
            DataSet data = new DataSet();

            string whereClause = "1 = 1 ";
            string tablename = table.TableName;
            string columnname = string.Format("{0} as \"{1}\"", table.Columns[0].ColumnName, table.Columns[0].ChineseName);
            for (int i = 1; i < table.Columns.Count; i++)
            {
                columnname += string.Format(", {0} as \"{1}\"", table.Columns[i].ColumnName, table.Columns[i].ChineseName);
            }
            if (arg.TimeFlag)
            {
                whereClause += string.Format(" and starttime_da >= to_date('{0}', 'YYYYMMDDHH24MISS') and endtime_da <= to_date('{1}', 'YYYYMMDDHH24MISS') ", arg.StartTime.ToString("yyyyMMddHHmmss"), arg.StopTime.ToString("yyyyMMddHHmmss"));
            }

            if (arg.SteelGradeFlag)
            {
                whereClause += string.Format(" and steelgrade_da = '{0}'", arg.SteelGrade);
            }

            if (arg.ThickFlag)
            {
                whereClause += string.Format(" and thick_da >= {0} and thick_da <= {1}", arg.MinThick, arg.MaxThick);
            }

            if (arg.WidthFlag)
            {
                whereClause += string.Format(" and width_da >= {0} and width_da <= {1}", arg.MinWidth, arg.MaxWidth);
            }
            string sql = "";
            switch (tablename)
            {
                case "SM_ELEM_ANA":
                    sql = string.Format("select {0} from {1} where rowid in (select rowid from SM_ELEM_ANA_DA where {2} and processcode_da = \'{3}\')", columnname, tablename, whereClause, arg.ProcessCode);
                    break;
                case "HRM_L2_COILREPORTS":
                    sql = string.Format("select {0} from {1} where slab_id in (select slab_id from HRM_L2_COILREPORTS_DA where {2})", columnname, tablename, whereClause);
                    break;
                case "SM_LF_HEAT":
                    sql = string.Format("select {0} from {1} where rowid in (select rowid from SM_LF_HEAT_DA where {2})", columnname, tablename, whereClause);
                    break;
                case "SM_BOF_HEAT":
                    sql = string.Format("select {0} from {1} where rowid in (select rowid from SM_BOF_HEAT_DA where {2})", columnname, tablename, whereClause);
                    break;
                case "SM_RH_HEAT":
                    sql = string.Format("select {0} from {1} where rowid in (select rowid from SM_RH_HEAT_DA where {2})", columnname, tablename, whereClause);
                    break;
            }
            OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);

            try
            {
                connection.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(sql, connection);
                adapter.Fill(data);
            }
            catch (Exception ex)
            {
                throw new Exception(sql, ex);
            }
            finally
            {
                connection.Close();
            }

            return data;
        }
        //DataTrace//
        public Tuple<Guid, string> NewDataTrace(string processNo, IList<string> iDList, IList<QtDataProcessConfig> processes, bool back, string username)
        {
            try
            {
                var factory = new DataTraceFactory(processNo,iDList,processes,back);
                var result = new Tuple<Guid, string>(QtDataTraceBLL.Add(username,factory), "");
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
            var factory = QtDataTraceBLL.GetFactory(username, id);
            if (factory != null && !factory.Writing)
            {
                return factory.ResultData;
            }
            return null;
        }
        public int GetProcess(string username, Guid id)
        {
            var temp = QtDataTraceBLL.GetFactory(username, id);
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
            var factory = QtDataTraceBLL.GetFactory(username, id);
            if (factory != null)
                return factory.Error;
            return null;
        }
        public bool Stop(string username, Guid id)
        {
            return QtDataTraceBLL.Stop(username, id);
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
