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
using QtDataTrace.Access;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace QtDataTrace.Access
{
    [ServiceObject("质量数据追踪服务")]
    [ServiceBind(typeof(IQtDataTraceService))]
    public class QtDataTraceService : ServiceObject, IQtDataTraceService
    {
        public IList<MaterialInfo> GetMaterialList(QueryArgs arg)
        {
            IList<MaterialInfo> result = null;

            if (arg.ProcessCode == "BOF" || arg.ProcessCode == "LF" || arg.ProcessCode == "RH")
            {
                result = GetBOFHeatList(arg);
            }
            else if (arg.ProcessCode == "CCM")
            {
                result = GetSlabList(arg);
            }
            else if (arg.ProcessCode == "HRM")
            {
                result = GetHotCoilList(arg);
            }
            else
            {
                result = GetPltcmList(arg);
            }

            return result;
        }

        public IList<MaterialInfo> GetBOFHeatList(QueryArgs arg)
        {
            IList<MaterialInfo> result = new BindingList<MaterialInfo>();

            string whereClause = "";

            if (arg.TimeFlag)
            {
                whereClause += string.Format(" and product_day >= to_date('{0}', 'YYYYMMDDHH24MI') and product_day <= to_date('{1}', 'YYYYMMDDHH24MI') ", arg.StartTime.ToString("yyyyMMddHHmm"), arg.StopTime.ToString("yyyyMMddHHmm"));
            }

            if (arg.SteelGradeFlag)
            {
                whereClause += string.Format(" and steel_grade = '{0}'", arg.SteelGrade);
            }


            string sql = "SELECT DISTINCT heat_id,steel_grade, product_day FROM sm_bof_heat WHERE heat_id IS NOT NULL " + 
                            whereClause +
                         " order by product_day";

            OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);

            try
            {
                connection.Open();

                OleDbCommand command = new OleDbCommand(sql, connection);
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    MaterialInfo mat = new MaterialInfo();
                    result.Add(mat);

                    mat.InId = reader["heat_id"].ToString();
                    mat.OutId = reader["heat_id"].ToString();
                    mat.SteelGrade = reader["steel_grade"].ToString();
                    mat.Time = System.Convert.ToDateTime(reader["product_day"]);
                }

                reader.Close();
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

        public IList<MaterialInfo> GetCCHeatList(QueryArgs arg)
        {
            IList<MaterialInfo> result = new BindingList<MaterialInfo>();

            string whereClause = "";

            if (arg.TimeFlag)
            {
                whereClause += string.Format(" and start_date >= to_date('{0}', 'YYYYMMDDHH24MI') and stop_date <= to_date('{1}', 'YYYYMMDDHH24MI') ", arg.StartTime.ToString("yyyyMMddHHmm"), arg.StopTime.ToString("yyyyMMddHHmm"));
            }

            if (arg.SteelGradeFlag)
            {
                whereClause += string.Format(" and steel_grade_id = '{0}'", arg.SteelGrade);
            }

            string sql = "SELECT DISTINCT heat_id,steel_grade_id, start_date FROM sm_ccm_heat WHERE heat_id IS NOT NULL " +
                            whereClause +
                         " order by start_date";

            OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);

            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(sql, connection);
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    MaterialInfo mat = new MaterialInfo();
                    result.Add(mat);

                    mat.InId = reader["heat_id"].ToString();
                    mat.OutId = reader["heat_id"].ToString();
                    mat.SteelGrade = reader["steel_grade"].ToString();
                    mat.Time = System.Convert.ToDateTime(reader["product_day"]);
                }

                reader.Close();
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

        public IList<MaterialInfo> GetSlabList(QueryArgs cond)
        {
            IList<MaterialInfo> result = new BindingList<MaterialInfo>();

            string whereClause = "";

            if (cond.TimeFlag)
            {
                whereClause += string.Format(" and cut_date >= to_date('{0}', 'YYYYMMDDHH24MI') and cut_date <= to_date('{1}', 'YYYYMMDDHH24MI') ", cond.StartTime.ToString("yyyyMMddHHmm"), cond.StopTime.ToString("yyyyMMddHHmm"));
            }

            if (cond.SteelGradeFlag)
            {
                whereClause += string.Format(" and STEEL_GRADE = '{0}'", cond.SteelGrade);
            }

            if (cond.ThickFlag)
            {
                whereClause += string.Format(" and thickness >= {0} and thickness <= {1}", cond.MinThick, cond.MaxThick);
            }

            if (cond.WidthFlag)
            {
                whereClause += string.Format(" and width >= {0} and width <= {1}", cond.MinWidth, cond.MaxWidth);
            }

            string sql = "select distinct heat_id, slab_no, steel_grade steel_grade, cut_date, thickness, width " +
                         " from sm_slab_info " +
                         " where 1 = 1 " + whereClause +
                         " order by cut_date";

            OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);

            try
            {
                connection.Open();

                OleDbCommand command = new OleDbCommand(sql, connection);
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    MaterialInfo mat = new MaterialInfo();
                    result.Add(mat);

                    mat.InId = reader["heat_id"].ToString();
                    mat.OutId = reader["slab_no"].ToString();
                    mat.SteelGrade = reader["steel_grade"].ToString();
                    mat.Thickness = reader["thickness"].ToString();
                    mat.Width = reader["width"].ToString();
                    mat.Time = System.Convert.ToDateTime(reader["cut_date"]);
                }

                reader.Close();
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

        public IList<MaterialInfo> GetHotCoilList(QueryArgs cond)
        {
            IList<MaterialInfo> result = new BindingList<MaterialInfo>();

            string whereClause = "";

            if (cond.TimeFlag)
            {
                whereClause += string.Format(" and rolled_time >= to_date('{0}', 'YYYYMMDDHH24MI') and rolled_time <= to_date('{1}', 'YYYYMMDDHH24MI') ", cond.StartTime.ToString("yyyyMMddHHmm"), cond.StopTime.ToString("yyyyMMddHHmm"));
            }

            if (cond.SteelGradeFlag)
            {
                whereClause += string.Format(" and c_custom_sgc_upd = '{0}'", cond.SteelGrade);
            }

            if (cond.ThickFlag)
            {
                whereClause += string.Format(" and b.f_fmdelthktarg >= {0} and b.f_fmdelthktarg <= {1}", cond.MinThick, cond.MaxThick);
            }

            if (cond.WidthFlag)
            {
                whereClause += string.Format(" and b.f_fmdelwidtarg >= {0} and b.f_fmdelwidtarg <= {1}", cond.MinWidth, cond.MaxWidth);
            }

            string sql = "select coil_id, slab_id, c_custom_sgc_upd steel_grade, rolled_time, b.f_fmdelthktarg thickness, b.f_fmdelwidtarg width " +
                         " from hrm_l2_coilreports a, hrm_l2_pdi b " +
                         " where a.slab_id = trim(b.c_slabid) " + whereClause +
                         " order by rolled_time"; 

            OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);

            try
            {
                connection.Open();

                OleDbCommand command = new OleDbCommand(sql, connection);
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    MaterialInfo mat = new MaterialInfo();
                    result.Add(mat);

                    mat.InId = reader["slab_id"].ToString();
                    mat.OutId = reader["coil_id"].ToString();
                    mat.SteelGrade = reader["steel_grade"].ToString();
                    mat.Thickness = reader["thickness"].ToString();
                    mat.Width = reader["width"].ToString();
                    mat.Time = System.Convert.ToDateTime(reader["rolled_time"]);
                }

                reader.Close();
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

        public IList<MaterialInfo> GetPltcmList(QueryArgs cond)
        {
            IList<MaterialInfo> result = new BindingList<MaterialInfo>();

            string whereClause = "";

            if (cond.TimeFlag)
            {
                whereClause += string.Format(" and START_TIME >= to_date('{0}', 'YYYYMMDDHH24MI') and STOP_TIME <= to_date('{1}', 'YYYYMMDDHH24MI') ", cond.StartTime.ToString("yyyyMMddHHmm"), cond.StopTime.ToString("yyyyMMddHHmm"));
            }

            if (cond.SteelGradeFlag)
            {
                whereClause += string.Format(" and ST_NO = '{0}'", cond.SteelGrade);
            }

            if (cond.ThickFlag)
            {
                whereClause += string.Format(" and Out_Mat_Targ_Thick >= {0} and Out_Mat_Targ_Thick <= {1}", cond.MinThick, cond.MaxThick);
            }

            if (cond.WidthFlag)
            {
                whereClause += string.Format(" and out_mat_targ_width >= {0} and out_mat_targ_width <= {1}", cond.MinWidth, cond.MaxWidth);
            }

            string sql = "select in_mat_no_1, out_mat_no , ST_NO steel_grade, STOP_TIME rolledtime, Out_Mat_Targ_Thick thickness, out_mat_targ_width width " +
                         " from crm_pltcm_report " +
                         " where 1 = 1 " + whereClause +
                         " order by STOP_TIME";

            OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);

            try
            {
                connection.Open();

                OleDbCommand command = new OleDbCommand(sql, connection);
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    MaterialInfo mat = new MaterialInfo();
                    result.Add(mat);

                    mat.InId = reader["in_mat_no_1"].ToString();
                    mat.OutId = reader["out_mat_no"].ToString();
                    mat.SteelGrade = reader["steel_grade"].ToString();
                    mat.Thickness = reader["thickness"].ToString();
                    mat.Width = reader["width"].ToString();
                    mat.Time = System.Convert.ToDateTime(reader["rolledtime"]);
                }

                reader.Close();
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

        public DataSet GetMaterialRoute(QueryArgs cond)
        {
            DataSet data = new DataSet();
            string whereClause = "";

            if (cond.TimeFlag)
            {
                if (whereClause == "")
                    whereClause = " where ";
                else
                    whereClause += " and ";

                whereClause += string.Format(" START_TIME >= to_date('{0}', 'YYYYMMDDHH24MISS') and STOP_TIME <= to_date('{1}', 'YYYYMMDDHH24MISS') ", cond.StartTime.ToString("yyyyMMddHHmmss"), cond.StopTime.ToString("yyyyMMddHHmmss"));
            }

            //if (cond.SteelGradeFlag)
            //{
            //    if (whereClause == "")
            //        whereClause = "where";
            //    else
            //        whereClause = " and ";

            //    whereClause = string.Format("STEELGRADE = '{0}'", cond.SteelGrade);
            //}

            if (cond.MatIdFlag)
            {
                if (whereClause == "")
                    whereClause = " where ";
                else
                    whereClause += " and ";

                whereClause += string.Format("MAT_NO = '{0}'", cond.MatId);
            }

            OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);

            try
            {
                connection.Open();

                {
                    string sql = " select distinct null PARENT, heat_id ID, heat_id MAT_NO, null device_no, null start_time, null stop_time " +
                                 " from heat_track_view" +
                                whereClause;

                    OleDbDataAdapter adapter = new OleDbDataAdapter(sql, connection);

                    adapter.Fill(data, "MATERIAL_TRACE");
                }

                {
                    string sql = " select MAT_NO PARENT, MAT_NO || DEVICE_NO ID, MAT_NO, DEVICE_NO, START_TIME, STOP_TIME " +
                                 " from heat_mattrack_time t" +
                                 " where mat_no in (select distinct heat_id from heat_track_view " + whereClause + ") " +
                                 "order by start_time ";

                    OleDbDataAdapter adapter = new OleDbDataAdapter(sql, connection);

                    adapter.Fill(data, "MATERIAL_TRACE");

                }

                {
                    //slab
                    string sql = "select substr(mat_no, 0, 7) PARENT, mat_no || device_no ID, MAT_NO, DEVICE_NO, START_TIME, STOP_TIME  " +
                                 " from sm_mattrack_time " +
                                 " where substr(mat_no, 0, 7) in (select distinct heat_id from heat_track_view " + whereClause + ") " +
                                 " order by start_time ";

                    OleDbDataAdapter adapter = new OleDbDataAdapter(sql, connection);

                    adapter.Fill(data, "MATERIAL_TRACE");
                }

                {
                    //hot coil
                    string sql = "select substr(MAT_NO, 0, 7) PARENT, MAT_NO || device_no ID, mat_no, DEVICE_NO, START_TIME, STOP_TIME " +
                                 " from hrm_mattrack_time " +
                                 " where substr(mat_no, 0, 7) in (select distinct heat_id from heat_track_view " + whereClause + ") " +
                                 " order by start_time ";

                    OleDbDataAdapter adapter = new OleDbDataAdapter(sql, connection);

                    adapter.Fill(data, "MATERIAL_TRACE");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return data;
        }

        public IList<MaterialInfo> GetMaterialTracking(string matId)
        {
            List<MaterialInfo> track = new List<MaterialInfo>();
            Dictionary<string, MaterialInfo> mats = new Dictionary<string, MaterialInfo>();

            using (OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB))
            {
                connection.Open();

                string prevMatId = "";

                while (prevMatId != matId)
                {
                    string sql = string.Format(" select in_mat_id1, in_mat_id2, out_mat_id, device_no, start_time, stop_time " +
                                                " from process_mat_pedigree t " +
                                                " where out_mat_id = '{0}' ", matId);

                    try
                    {
                        OleDbCommand command = new OleDbCommand(sql, connection);

                        OleDbDataReader reader = command.ExecuteReader();

                        prevMatId = matId;

                        while (reader.Read())
                        {
                            matId = reader.GetString(0);
                            string outMatId = reader.GetString(2);

                            MaterialInfo mat;

                            if (mats.ContainsKey(outMatId))
                            {
                                mat = mats[outMatId];
                            }
                            else
                            {
                                mat = new MaterialInfo();
                                mat.Equipments = new BindingList<EquipmentInfo>();

                                mat.InId = matId;
                                mat.OutId = outMatId;
                                mats[outMatId] = mat;
                                track.Add(mat);
                            }

                            EquipmentInfo device = new EquipmentInfo();
                            mat.Equipments.Add(device);

                            device.Name = reader.GetString(3);
                            device.MatId = mat.OutId;
                            device.StartTime = reader.GetDateTime(4);
                            device.StopTime = reader.GetDateTime(5);

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

                        reader.Close();
                    }
                    catch (OleDbException ex)
                    {
                        throw ex;
                    }
                }
            }

            return track;
        }

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
                                mat.Equipments = new BindingList<EquipmentInfo>();
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
            switch(tablename)
            {
                case "SM_ELEM_ANA":
                    sql = string.Format("select {0} from {1} where rowid in (select rowid from SM_ELEM_ANA_DA where {2} and processcode_da = \'{3}\')", columnname, tablename, whereClause,arg.ProcessCode);
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


        public DataSet DataTrace(string processNo, IList<string> iDList, IList<QtDataProcessConfig> processes,bool back)
        {
            DateTime starttime = DateTime.Now;
            var factory = new DataTraceFactory(processNo, iDList, processes,back);
            DataSet result = new DataSet();
            result.Tables.Add(factory.Start());
            DateTime endtime = DateTime.Now;
            Console.WriteLine("Cost " + (endtime - starttime));
            return result;
        }
        private class DataTraceFactory
        {
            int threadcount = 15;
            string RootProcessNo;
            int RootProcessSeq;
            string RootSql;
            IList<string> IDList;
            IList<QtDataProcessConfig> Processes;
            DataSet ConfigFile;
            Dictionary<string, string> processdic = new Dictionary<string, string>();
            Dictionary<string, int> processseqs = new Dictionary<string, int>();
            Barrier barrier;
            int back;
            DataTable ResultData;
            public DataTraceFactory(string processNo, IList<string> iDList, IList<QtDataProcessConfig> processes,bool b)
            {
                this.RootProcessNo = processNo;
                this.IDList = iDList;
                this.Processes = processes;
                this.ConfigFile = new BaseTableService().GetProcessQtTableConfigFile();

                int rcount = ConfigFile.Tables["Table"].Rows.Count;
                for (int i = 0; i < rcount;i++ )
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
            public DataTable Start()
            {
                if (!processseqs.ContainsKey(RootProcessNo))
                    return null;
                DataTable mainTable = new DataTable();
                processdic.Add(RootProcessNo, getProcessString(RootProcessNo));
                //connection = new OleDbConnection(ConnectionString.LYQ_DB);
                int listcount = IDList.Count;
                int thc = this.threadcount;
                if (listcount / 3 < this.threadcount)
                    thc = listcount / 3 == 0 ? 1 : listcount / 3;
                DataTable[] threadTables = new DataTable[thc];
                barrier = new Barrier(thc + 1);
                //connection.Open();
                switch (back)
                {
                    case 1:
                        for (int i = 0; i < thc; i++)
                        {
                            threadTables[i] = new DataTable();
                            Thread th = new Thread(threadMethod_Back) { IsBackground = true};
                            th.Start(new threadInputType((listcount * i) / thc, (listcount * (i + 1)) / thc, threadTables[i]));
                        }
                        break;
                    case 0:
                        for (int i = 0; i < thc; i++)
                        {
                            threadTables[i] = new DataTable();
                            Thread th = new Thread(threadMethod_Single) { IsBackground = true };
                            th.Start(new threadInputType((listcount * i) / thc, (listcount * (i + 1)) / thc, threadTables[i]));
                        }
                        break;
                    case -1:
                        for (int i = 0; i < thc; i++)
                        {
                            threadTables[i] = new DataTable();
                            Thread th = new Thread(threadMethod_Pre) { IsBackground = true };
                            th.Start(new threadInputType((listcount * i) / thc, (listcount * (i + 1)) / thc, threadTables[i]));
                        }
                        break;
                }
                barrier.SignalAndWait();
                //connection.Close();
                for (int i = 0; i < thc; i++)
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
                this.ResultData = mainTable;
                return mainTable;
            }
            private void threadMethod_Single(object input)
            {
                threadInputType inputtype = input as threadInputType;
                OleDbCommand command = new OleDbCommand();
                OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);

                command.Connection = connection;
                connection.Open();
                DataTraceTreeNode root = new DataTraceTreeNode(RootProcessNo, RootProcessSeq, null) { Sql = RootSql };
                List<DataTraceTreeNode> leafnodes = new List<DataTraceTreeNode>();
                for (int i = inputtype.start; i < inputtype.end; i++)
                {
                    root.OutId = IDList[i];
                    root.data = new DataTable();
                    root.Nodes.Clear();
                    command.CommandText = string.Format(root.Sql, IDList[i], RootProcessNo);
                    OleDbDataAdapter adp = new OleDbDataAdapter(command);
                    adp.Fill(root.data);
                    inputtype.data.Merge(root.data);
                }
                connection.Close();
                barrier.RemoveParticipant();
            }
            private void threadMethod_Back(object input)
            {
                threadInputType inputtype = input as threadInputType;
                OleDbCommand command = new OleDbCommand();
                OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);

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
                    buildBackTraceTree(root, command,leafnodes);
                    foreach (var leafnode in leafnodes)
                    {
                        var result = analyzeLeafNode(leafnode,connection,true);
                        inputtype.data.Merge(result);
                    }
                }
                connection.Close();
                barrier.RemoveParticipant();
            }
            private void threadMethod_Pre(object input)
            {
                threadInputType inputtype = input as threadInputType;
                OleDbCommand command = new OleDbCommand();
                OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);

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
                    command.CommandText = string.Format("select IN_MAT_ID1,IN_MAT_ID2 from PROCESS_MAT_PEDIGREE where OUT_MAT_ID = '{0}' and PROCESS_CODE = '{1}'", root.OutId,root.Process);
                    OleDbDataReader reader = command.ExecuteReader();
                    Tuple<string, string> pros = null;
                    while (reader.Read())
                    {
                        pros = new Tuple<string, string>(reader[0].ToString(), reader[1] == null ? "" : reader[1].ToString());
                    }
                    reader.Close();
                    if (pros != null)
                    {
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
                        var result = analyzeLeafNode(leafnode, connection,false);
                        inputtype.data.Merge(result);
                    }
                }
                connection.Close();
                barrier.RemoveParticipant();
            }
            private class threadInputType
            {
                public int start;
                public int end;
                public DataTable data;
                public threadInputType(int s,int e,DataTable d)
                {
                    this.start = s;
                    this.end = e;
                    this.data = d;
                }
            }
            private DataTable analyzeLeafNode(DataTraceTreeNode leaf,OleDbConnection connection,bool pre)
            {
                DataTable result = new DataTable();
                if (leaf == null)
                    return null;
                result.Rows.Add(result.NewRow());
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = connection;
                OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
                do
                {
                    if (leaf.Sql!= "")
                    {
                        if (leaf.data == null)
                        {
                            cmd.CommandText = string.Format(leaf.Sql, leaf.OutId, leaf.Process);
                            leaf.data = new DataTable();
                            adp.Fill(leaf.data);
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
                if (!pre)
                    for (int i = 0; i < counta; i++)
                    {
                        result.Columns.Add(a.Columns[i].ColumnName, a.Columns[i].DataType);
                        result.Rows[0][countr + i] = a.Rows[0][i];
                    }
                else
                    for (int i = counta - 1; i >= 0; i--)
                    {
                        result.Columns.Add(a.Columns[i].ColumnName, a.Columns[i].DataType).SetOrdinal(0);
                        result.Rows[0][0] = a.Rows[0][i];
                    }
            }
            private void buildBackTraceTree(DataTraceTreeNode node, OleDbCommand command,List<DataTraceTreeNode> leafnodes)
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
                if (pros.Count>0)
                {
                    foreach (var pro in pros)
                    {
                        if (pro.Item1 == minseq)
                        {
                            string sql = "";
                            if (!processdic.TryGetValue(pro.Item2,out sql))
                            {
                                lock (processdic)
                                {
                                    if (!processdic.TryGetValue(pro.Item2, out sql))
                                        processdic.Add(pro.Item2, sql = getProcessString(pro.Item2));
                                }
                            }
                            node.Nodes.Add(new DataTraceTreeNode(pro.Item3, pro.Item2, pro.Item1, node) { Sql = sql});
                        }
                    }
                    for (int i = node.Nodes.Count - 1; i >= 0;i-- )
                    {
                        buildBackTraceTree(node.Nodes[i], command, leafnodes);
                    }

                }
                if (node.Nodes.Count == 0)
                {
                    if (node.Sql == "" && node.Parent != null)
                    {
                        node.Parent.Nodes.Remove(node);
                    }
                    else
                        leafnodes.Add(node);
                }
            }
            private void buildPreTraceTree(DataTraceTreeNode node, OleDbCommand command, List<DataTraceTreeNode> leafnodes)
            {
                if (node.Parent==null)
                    return;
                command.CommandText = string.Format("select IN_MAT_ID1,IN_MAT_ID2,PROCESS_CODE from PROCESS_MAT_PEDIGREE where OUT_MAT_ID = '{0}'", node.OutId);
                OleDbDataReader reader = command.ExecuteReader();
                Tuple<int, string, string,string> pros = null;
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
                if(pros!=null)
                {
                    if (!processdic.TryGetValue(pros.Item2,out node.Sql))
                    {
                        lock (processdic)
                        {
                            if (!processdic.TryGetValue(pros.Item2, out node.Sql))
                                processdic.Add(pros.Item2, node.Sql = getProcessString(pros.Item2));
                        }
                    }
                    node.Process = pros.Item2;
                    node.ProSeq = pros.Item1;
                    node.Nodes.Add(new DataTraceTreeNode(pros.Item3, null,int.MinValue, node));
                    if(pros.Item4!=""&&pros.Item4!=pros.Item3)
                    {
                        node.Nodes.Add(new DataTraceTreeNode(pros.Item4, null, int.MinValue, node));
                    }
                    for (int i = node.Nodes.Count - 1; i >= 0; i--)
                    {
                        buildPreTraceTree(node.Nodes[i], command, leafnodes);
                    }
                    if (node.Nodes.Count == 0)
                    {
                        if (node.Sql == "")
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
                public string Process;
                public int ProSeq;
                public List<DataTraceTreeNode> Nodes = new List<DataTraceTreeNode>();
                public DataTraceTreeNode Parent;
                public DataTable data = null;
                public string Sql = "";
                public DataTraceTreeNode(string outid, string process,int proseq,DataTraceTreeNode parent)
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
            private string getProcessString(string process)
            {
                string[] sqlparams = new string[] { "", "", "" };
                var tables = ConfigFile.Tables["Table"].Select(string.Format("PROCESS_CODE = '{0}'", process));
                if (tables.Count() == 0)
                    return "";
                var processchinese = tables.FirstOrDefault()["PROCESS_CHINESE"].ToString();
                var processconfig = Processes.FirstOrDefault((d) => { return d.ChineseName == processchinese; });
                if (processconfig != null)
                {
                    List<string> outids = new List<string>();
                    List<string> processcodes = new List<string>();
                    int tc = processconfig.Tables.Count;
                    for (int i = 0; i < tc; i++)
                    {
                        string tablechinese = processconfig.Tables[i].ChineseName;
                        var table = tables.First((input) => { return input["TABLE_CHINESE"].ToString() == tablechinese; });
                        string tablename = table["TABLE_NAME"].ToString();

                        sqlparams[1] += tablename + ", ";
                        int cc = processconfig.Tables[i].Columns.Count;
                        for (int j = 0; j < cc; j++)
                        {
                            string columnchinese = processconfig.Tables[i].Columns[j].ChineseName;
                            var column = (from r in ConfigFile.Tables["Column"].AsEnumerable() where (r.GetParentRow("Table_Column")["TABLE_NAME"].ToString() == tablename && ((r["COLUMN_CHINESE"].ToString() == null || r["COLUMN_CHINESE"].ToString().Trim() == "") ? r["COLUMN_NAME"].ToString() : r["COLUMN_CHINESE"].ToString()) == columnchinese) select r).FirstOrDefault();
                            if (column == null)
                                sqlparams[0] += string.Format("null \"{0}\", ", processchinese + "_" + columnchinese);
                            else
                            {
                                sqlparams[0] += string.Format("{0}.{1} \"{2}\", ", tablename, column["COLUMN_NAME"], processchinese + "_" + columnchinese);
                            }
                        }
                        var keycolumns = from r in ConfigFile.Tables["Column"].AsEnumerable() where r.GetParentRow("Table_Column")["TABLE_NAME"].ToString() == tablename && r["KEY"] != null && r["KEY"].ToString().Trim() != "" select r;
                        int kcc = keycolumns.Count();
                        for(int j = 0;j<kcc;j++)
                        {
                            switch (keycolumns.ElementAt(j)["KEY"].ToString())
                            {
                                case "OUT_MAT_ID": outids.Add(string.Format("{0}.{1}", tablename, keycolumns.ElementAt(j)["COLUMN_NAME"])); break;
                                case "PROCESS_CODE": processcodes.Add(string.Format("{0}.{1}", tablename, keycolumns.ElementAt(j)["COLUMN_NAME"])); break;
                            }
                        }
                    }
                    if (outids.Count > 0)
                    {
                        sqlparams[2] += "and " + outids[0] + "='{0}' ";
                        for (int i = 0; i < outids.Count - 1; i++)
                        {
                            sqlparams[2] += "and " + outids[i] + "=" + outids[i + 1] + " ";
                        }
                    }
                    if (processcodes.Count > 0)
                    {
                        sqlparams[2] += "and " + processcodes[0] + "='{1}' ";
                        for (int i = 0; i < processcodes.Count - 1; i++)
                        {
                            sqlparams[2] += "and " + processcodes[i] + "=" + processcodes[i + 1] + " ";
                        }
                    }
                    if (sqlparams[0] != "")
                        sqlparams[0] = sqlparams[0].Substring(0, sqlparams[0].Length - 2);
                    if (sqlparams[1] != "")
                        sqlparams[1] = sqlparams[1].Substring(0, sqlparams[1].Length - 2);
                }
                else
                    return "";
                string result = "select " + sqlparams[0] + " from " + sqlparams[1] + " where 1=1 " + sqlparams[2];
                return result;
            }
        }
    }
}
