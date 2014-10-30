using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using EAS.Services;
using QtDataTrace.Interfaces;
using QtDataTrace.IService;

namespace QtDataTrace.Access
{
    [ServiceObject("单表数据查询服务")]
    [ServiceBind(typeof(ISingleQtTableService))]
    public class SingleQtTableService : ServiceObject, ISingleQtTableService
    {
        public List<BOFHeatInfo> GetBOFHeatInfo(string HeatID)
        {
            SingleQtTableLY210 qt = new SingleQtTableLY210();

            return qt.GetBOFHeatInfo(HeatID);
        }

        public List<BOFKeyEvents> GetBOFKeyEvents(string HeatID)
        {
            SingleQtTableLY210 qt = new SingleQtTableLY210();

            return qt.GetBOFKeyEvents(HeatID);
        }

        public List<BOF_HisDB> GetBOF_WasteGas_HisDB(string bof_campaign, string StartDateTime, string EndDateTime)
        {
            SingleQtTableLY210 qt = new SingleQtTableLY210();

            return qt.GetBOF_WasteGas_HisDB(bof_campaign, StartDateTime, EndDateTime);
        }

        public List<HisDB_HF> GetHisDB_HF(string CoilID)
        {
            SingleQtTableLY2250 qt = new SingleQtTableLY2250();

            return qt.GetHisDB_HF(CoilID);
        }

        public List<HrmPdi> GetHrmPdi(string matId)
        {
            List<HrmPdi> result;

            PersistentService<HrmPdi> pdi = new PersistentService<HrmPdi>();

            using (OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_OLEDB))
            {
                connection.Open();

                string sql = string.Format("SELECT * FROM HRM_L2_PDI WHERE c_coilid = '{0}'", matId);
                result = pdi.Load(sql, connection);
            }

            return result;
        }

        public List<HrmCoilSetup> GetHrmCoilSetup(string matId)
        {
            List<HrmCoilSetup> result;

            PersistentService<HrmCoilSetup> setup = new PersistentService<HrmCoilSetup>();

            using (OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_OLEDB))
            {
                connection.Open();

                string sql = string.Format("SELECT * FROM hrm_coil_setup WHERE piece = '{0}'", matId);
                result = setup.Load(sql, connection);
            }

            return result;
        }

        public List<HrmCTCSetup> GetHrmCTCSetup(string matId)
        {
            List<HrmCTCSetup> result;

            PersistentService<HrmCTCSetup> setup = new PersistentService<HrmCTCSetup>();

            using (OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_OLEDB))
            {
                connection.Open();

                string sql = string.Format("SELECT * FROM hrm_l2_ctcsetup WHERE piecename = '{0}'", matId);
                result = setup.Load(sql, connection);
            }

            return result;
        }

        public HotCoilInfo GetHotCoilInfo(string matId)
        {
            SingleQtTable qt = new SingleQtTable();

            return qt.GetHotCoilInfo(matId);
        }

        public List<CoilSurfaceDefect> GetCoilSurfaceDefect(string matId)
        {
            List<CoilSurfaceDefect> result;

            PersistentService<CoilSurfaceDefect> setup = new PersistentService<CoilSurfaceDefect>();

            using (OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_OLEDB))
            {
                connection.Open();

                string sql = string.Format("SELECT * FROM hrm_l2_coilsurfreport WHERE coil_id = '{0}'", matId);
                result = setup.Load(sql, connection);
            }

            return result;
        }

        public List<CoilDefect> GetCoilDefects(string matId)
        {
            List<CoilDefect> result;

            PersistentService<CoilDefect> setup = new PersistentService<CoilDefect>();

            using (OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_OLEDB))
            {
                connection.Open();

                string sql = string.Format("SELECT * FROM hrm_l2_coildefects WHERE coil_id = '{0}' order by defect_id", matId);
                result = setup.Load(sql, connection);
            }

            return result;
        }

        public List<String> GetSteelGradeList()
        {
            SingleQtTableLY210 qt = new SingleQtTableLY210();

            return qt.GetSteelGradeList();
        }

        public List<CCHeatInfo> GetCCHeatInfo(string HeatID)
        {
            SingleQtTableLY210 qt = new SingleQtTableLY210();

            return qt.GetCCHeatInfo(HeatID);
        }

        public List<string> GetSlabIDListFromHeatID(string HeatID)
        {
            SingleQtTableLY210 qt = new SingleQtTableLY210();

            return qt.GetSlabIDListFromHeatID(HeatID);
        }

        public List<CC_SlabInfo> GetCC_SlabInfo(string SlabNo)
        {
            SingleQtTableLY210 qt = new SingleQtTableLY210();

            return qt.GetCC_SlabInfo(SlabNo);
        }

        public List<CC_HisData0> GetCC_Curve(string CCM, string StrandNo, string StartDateTime, string EndDateTime)
        {
            SingleQtTableLY210 qt = new SingleQtTableLY210();

            return qt.GetCC_HisData0_HisDB(CCM, StrandNo, StartDateTime, EndDateTime);
        }

        public List<CastInfo> GetCC_CastInfo(DateTime startTime, DateTime stopTime)
        {
            List<CastInfo> result;

            PersistentService<CastInfo> setup = new PersistentService<CastInfo>();

            using (OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_OLEDB))
            {
                connection.Open();

                string sql = string.Format("SELECT * FROM cast_info_view WHERE start_time >= to_date('{0}', 'yyyymmdd') and stop_time <= to_date('{1}', 'yyyymmdd') order by cast_number", startTime.ToString("yyyyMMdd"), stopTime.ToString("yyyyMMdd"));
                result = setup.Load(sql, connection);
            }

            return result;
        }

        public List<LFHeatInfo> GetLFHeatInfo(string HeatID)
        {
            SingleQtTableLY210 qt = new SingleQtTableLY210();

            return qt.GetLFHeatInfo(HeatID);
        }

        public List<LFKeyEvents> GetLF_KeyEvents(string HeatID)
        {
            SingleQtTableLY210 qt = new SingleQtTableLY210();

            return qt.GetLF_KeyEvents(HeatID);
        }

        public List<LF_HisDB> GetLF_HisDB(string LF_Station, string TrolleyNo, string StartDateTime, string EndDateTime)
        {
            SingleQtTableLY210 qt = new SingleQtTableLY210();

            return qt.GetLF_HisDB(LF_Station, TrolleyNo, StartDateTime, EndDateTime);
        }

        public List<RHHeatInfo> GetRHHeatInfo(string HeatID)
        {
            SingleQtTableLY210 qt = new SingleQtTableLY210();

            return qt.GetRHHeatInfo(HeatID);
        }

        public List<RH_KeyEvens> GetRHKeyEvents(string HeatID)
        {
            SingleQtTableLY210 qt = new SingleQtTableLY210();

            return qt.GetRHKeyEvents(HeatID);
        }

        public List<RH_HisDB> GetRH_HisDB(string RH_Station, string StartDateTime, string EndDateTime)
        {
            SingleQtTableLY210 qt = new SingleQtTableLY210();

            return qt.GetRH_HisDB(RH_Station, StartDateTime, EndDateTime);
        }

        public List<EquipmentAreaInfo> GetDeviceAreaInfo(string device)
        {
            List<EquipmentAreaInfo> result = new List<EquipmentAreaInfo>();

            string sql = string.Format("select * from device_area_config where device_no = '{0}' order by display_num", device);

            using (OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_OLEDB))
            {
                connection.Open();

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

                        result.Add(info);
                    }
                }
            }

            return result;
        }


        public List<CoilDefectClass> GetCoilDefectClasses()
        {
            List<CoilDefectClass> result;

            PersistentService<CoilDefectClass> setup = new PersistentService<CoilDefectClass>();

            using (OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_OLEDB))
            {
                connection.Open();

                string sql = "select classid,name from HRM_DEFECT_CLASSES t where status = 'U' order by classid";
                result = setup.Load(sql, connection);
            }

            return result;
        }

        public IList<PLTCM_CoilInfo> GetPLTCM_CoilInfo(string matId)
        {
            List<PLTCM_CoilInfo> result;

            PersistentService<PLTCM_CoilInfo> setup = new PersistentService<PLTCM_CoilInfo>();

            using (OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_OLEDB))
            {
                connection.Open();

                string sql = string.Format("select * from CRM_PLTCM_REPORT where OUT_MAT_NO = '{0}'", matId);
                result = setup.Load(sql, connection);
            }

            return result;
        }

    }
}
