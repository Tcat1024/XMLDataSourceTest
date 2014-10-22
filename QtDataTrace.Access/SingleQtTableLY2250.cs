using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using QtDataTrace.Interfaces;
using iTextSharp.text;
using iTextSharp.text.pdf;
using DuHisPic;

namespace QtDataTrace.Access
{
    public class SingleQtTableLY2250
    {
        //对于起初编辑的类的引用
        SingleQtTable sqt = new SingleQtTable();
        
        //生成Pdf文件时要用的两种字体
        private iTextSharp.text.Font FontHei;
        private iTextSharp.text.Font FontSong;
        private iTextSharp.text.Font FontSong10;
        private iTextSharp.text.Font FontKai;
        private iTextSharp.text.Font FontCaption;
        //默认的背景颜色
        private iTextSharp.text.Color color_Name = new iTextSharp.text.Color(System.Drawing.Color.LightGray);
        private iTextSharp.text.Color color_Value = new iTextSharp.text.Color(System.Drawing.Color.White);

        //绘图引用的
        DuHisPic.ClsDuHisPic clsDuHisPic = new DuHisPic.ClsDuHisPic();  

        public List<String> GetSlabSteelGradeList()
        {//从2250铸坯库中的钢种列表信息

            List<String> LST = new List<string>();

            string strSQL = "SELECT DISTINCT steel_grade FROM HRM_L2_FCEREPORTS WHERE steel_grade IS NOT NULL ORDER BY steel_grade";
            DataTable dt = sqt.ReadDatatable_OraDB(strSQL);

            for (int I = 0; I < dt.Rows.Count; I++)
            {                 
                LST.Add(dt.Rows[I][0].ToString());
            }
            dt.Dispose();            

            return LST;
        }

        public void GetHisDB_HF_PreHeat(string HF_Station, string StartDateTime, string EndDateTime,ref List<HisDB_HF> LST )
        {
             
            HisDB_HF lst = new HisDB_HF();

            string[] tags = new string[4];
            tags[00] = "LYQ2250.HF" + HF_Station + ".HF3_PSTopA";
            tags[01] = "LYQ2250.HF" + HF_Station + ".HF3_PSTopB";
            tags[02] = "LYQ2250.HF" + HF_Station + ".PSBottomA";
            tags[03] = "LYQ2250.HF" + HF_Station + ".PSBottomB";
            
            string str;

            //获取历史数据
            System.Data.DataTable dt = sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(StartDateTime), Convert.ToDateTime(EndDateTime));
            //起始时间
            DateTime TimeStart = Convert.ToDateTime(StartDateTime);

            for (int I = 0; I < dt.Rows.Count; I++)
            {
                lst = new HisDB_HF();

                lst.DateAndTime = Convert.ToDateTime(dt.Rows[I][00]);
                TimeSpan ts = Convert.ToDateTime(lst.DateAndTime) - TimeStart;
                lst.Duration = (float)ts.TotalMilliseconds / 60000;

                //dt.Rows[I][00]是时间列
                str = dt.Rows[I][01].ToString(); if (str.Length > 0) lst.TopA = Convert.ToSingle(str);
                str = dt.Rows[I][02].ToString(); if (str.Length > 0) lst.TopB = Convert.ToSingle(str);
                str = dt.Rows[I][03].ToString(); if (str.Length > 0) lst.BottomA = Convert.ToSingle(str);
                str = dt.Rows[I][04].ToString(); if (str.Length > 0) lst.BottomB = Convert.ToSingle(str);                

                LST.Add(lst);
            }

            dt.Dispose();
          
        }
        public void GetHisDB_HF_SZ1(string HF_Station, string StartDateTime, string EndDateTime, ref List<HisDB_HF> LST)
        {
         

            string[] tags = new string[4];
            tags[00] = "LYQ2250.HF" + HF_Station + ".SZ1TopA";
            tags[01] = "LYQ2250.HF" + HF_Station + ".SZ1TopB";
            tags[02] = "LYQ2250.HF" + HF_Station + ".SZ1BottomA";
            tags[03] = "LYQ2250.HF" + HF_Station + ".SZ1BottomB";
            

            string str;

            //获取历史数据
            System.Data.DataTable dt = sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(StartDateTime), Convert.ToDateTime(EndDateTime));
            //起始时间
            DateTime TimeStart = Convert.ToDateTime(StartDateTime);

            for (int I = 0; I < dt.Rows.Count; I++)
            {
                HisDB_HF lst = new HisDB_HF();

                lst.DateAndTime = Convert.ToDateTime(dt.Rows[I][00]);
                TimeSpan ts = Convert.ToDateTime(lst.DateAndTime) - TimeStart;
                lst.Duration = (float)ts.TotalMilliseconds / 60000;

                //dt.Rows[I][00]是时间列              
                str = dt.Rows[I][01].ToString(); if (str.Length > 0) lst.TopA = Convert.ToSingle(str);
                str = dt.Rows[I][02].ToString(); if (str.Length > 0) lst.TopB = Convert.ToSingle(str);
                str = dt.Rows[I][03].ToString(); if (str.Length > 0) lst.BottomA = Convert.ToSingle(str);
                str = dt.Rows[I][04].ToString(); if (str.Length > 0) lst.BottomB = Convert.ToSingle(str);                

                LST.Add(lst);
            }
            dt.Dispose();
 
        }

        public List<HisDB_HF> GetHisDB_HF(string CoilID)
        {
            //获取炉次对应的板坯号信息
            List<HisDB_HF> LST = new List<HisDB_HF>();           
            string str = "";

            string HF_Station = "";
            string charge_time = DateTime.Now.ToString();
            string discharge_time = DateTime.Now.ToString();
            string in_recycle_time = "0";
            string in_pre_time = "0";
            string in_first_time = "0";
            string in_sec_time = "0";
            string in_soak_time = "0";


            string strSQL = "SELECT * FROM hrm_l2_FCEREPORTS WHERE slab_id LIKE '" + CoilID + "%'";
            DataTable dt = sqt.ReadDatatable_OraDB(strSQL);
            for (int RowIndex = 0; RowIndex < dt.Rows.Count; RowIndex++)
            {
                str = dt.Rows[RowIndex]["fceno"].ToString(); if (str.Length > 0) HF_Station = str;
                
                str = dt.Rows[RowIndex]["charge_time"].ToString(); if (str.Length > 0) charge_time = str;
                str = dt.Rows[RowIndex]["discharge_time"].ToString(); if (str.Length > 0) discharge_time = str;

                str = dt.Rows[RowIndex]["in_recycle_time"].ToString(); if (str.Length > 0) in_recycle_time = str;
                str = dt.Rows[RowIndex]["in_pre_time"].ToString(); if (str.Length > 0) in_pre_time = str;
                str = dt.Rows[RowIndex]["in_first_time"].ToString(); if (str.Length > 0) in_first_time = str;
                str = dt.Rows[RowIndex]["in_sec_time"].ToString(); if (str.Length > 0) in_sec_time = str;
                str = dt.Rows[RowIndex]["in_soak_time"].ToString(); if (str.Length > 0) in_soak_time = str;
                
                //热循环段的进出时间
                DateTime InTime =Convert.ToDateTime ( charge_time);                              
                DateTime OutTime = InTime.AddMinutes(Convert.ToDouble( in_recycle_time));
               

                //预热段的进出时间
                InTime = OutTime;
                OutTime = InTime.AddMinutes(Convert.ToDouble(in_pre_time));
                GetHisDB_HF_PreHeat(HF_Station, InTime.ToString(), OutTime.ToString(),ref LST);

                //第1加热段
                InTime = OutTime;
                OutTime = InTime.AddMinutes(Convert.ToDouble(in_first_time));
                GetHisDB_HF_HZ1(HF_Station, InTime.ToString(), OutTime.ToString(), ref LST);

                //第2加热段
                InTime = OutTime;
                OutTime = InTime.AddMinutes(Convert.ToDouble( in_sec_time));
                GetHisDB_HF_HZ2(HF_Station, InTime.ToString(), OutTime.ToString(), ref LST);

                //均热段
                InTime = OutTime;               
                GetHisDB_HF_SZ1(HF_Station, InTime.ToString(), discharge_time, ref LST);
 
            }
            dt.Dispose();

            //计算平均值
            for (int I = 0; I < LST.Count; I++)
            {
                LST[I].TopAvg = (LST[I].TopA + LST[I].TopB) / 2.0f;
                LST[I].BottomAvg = (LST[I].BottomA + LST[I].BottomB) / 2.0f;
            }
            
            return LST;
        }
        public void GetHisDB_HF_HZ1(string HF_Station, string StartDateTime, string EndDateTime, ref List<HisDB_HF> LST)
        {
            

            string[] tags = new string[4];
            tags[0] = "LYQ2250.HF" + HF_Station + ".HZ1TopA";
            tags[1] = "LYQ2250.HF" + HF_Station + ".HZ1TopB";
            tags[2] = "LYQ2250.HF" + HF_Station + ".HZ1BottomA";
            tags[3] = "LYQ2250.HF" + HF_Station + ".HZ1BottomB";
            
            string str;

            //获取历史数据
            System.Data.DataTable dt = sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(StartDateTime), Convert.ToDateTime(EndDateTime));
            //起始时间
            DateTime TimeStart = Convert.ToDateTime(StartDateTime);

            for (int I = 0; I < dt.Rows.Count; I++)
            {
                HisDB_HF lst = new HisDB_HF();

                lst.DateAndTime = Convert.ToDateTime(dt.Rows[I][00]);
                TimeSpan ts = Convert.ToDateTime(lst.DateAndTime) - TimeStart;
                lst.Duration = (float)ts.TotalMilliseconds / 60000;

                //dt.Rows[I][00]是时间列
                str = dt.Rows[I][01].ToString(); if (str.Length > 0) lst.TopA = Convert.ToSingle(str);
                str = dt.Rows[I][02].ToString(); if (str.Length > 0) lst.TopB = Convert.ToSingle(str);
                str = dt.Rows[I][03].ToString(); if (str.Length > 0) lst.BottomA = Convert.ToSingle(str);
                str = dt.Rows[I][04].ToString(); if (str.Length > 0) lst.BottomB = Convert.ToSingle(str);
                
                LST.Add(lst);
            }
            dt.Dispose();
 
        }
        public void GetHisDB_HF_HZ2(string HF_Station, string StartDateTime, string EndDateTime, ref List<HisDB_HF> LST)
        {
             

            string[] tags = new string[4];
            tags[00] = "LYQ2250.HF" + HF_Station + ".HZ2TopA";
            tags[01] = "LYQ2250.HF" + HF_Station + ".HZ2TopB";
            tags[02] = "LYQ2250.HF" + HF_Station + ".HZ2BottomA";
            tags[03] = "LYQ2250.HF" + HF_Station + ".HZ2BottomB";
            
            string str;

            //获取历史数据
            System.Data.DataTable dt = sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(StartDateTime), Convert.ToDateTime(EndDateTime));
            //起始时间
            DateTime TimeStart = Convert.ToDateTime(StartDateTime);

            for (int I = 0; I < dt.Rows.Count; I++)
            {
                HisDB_HF lst = new HisDB_HF();

                lst.DateAndTime = Convert.ToDateTime(dt.Rows[I][00]);
                TimeSpan ts = Convert.ToDateTime(lst.DateAndTime) - TimeStart;
                lst.Duration = (float)ts.TotalSeconds;

                //dt.Rows[I][00]是时间列
                str = dt.Rows[I][01].ToString(); if (str.Length > 0) lst.TopA = Convert.ToSingle(str);
                str = dt.Rows[I][02].ToString(); if (str.Length > 0) lst.TopB = Convert.ToSingle(str);
                str = dt.Rows[I][03].ToString(); if (str.Length > 0) lst.BottomA = Convert.ToSingle(str);
                str = dt.Rows[I][04].ToString(); if (str.Length > 0) lst.BottomB = Convert.ToSingle(str);
                
                LST.Add(lst);
            }
            dt.Dispose();           
        }

        public List<LY2250SlabNoInfo> GetLY2250SlabNoInfo(string SteelGrade, string StartDateTime, string EndDateTime)
        {
            //获取炉次对应的板坯号信息
            List<LY2250SlabNoInfo> LST = new List<LY2250SlabNoInfo>();
            LY2250SlabNoInfo lst = new LY2250SlabNoInfo();
            string str = "";

            string strSQL = "SELECT slab_id,steel_grade,productno,fceno,charge_time FROM hrm_l2_FCEREPORTS"
                            + " WHERE charge_time >= to_date('" + StartDateTime + "', 'yyyy-mm-dd')"
                            + " AND charge_time <= to_date('" + EndDateTime + " 23:59:59', 'yyyy-mm-dd hh24:mi:ss')";

            if (SteelGrade.Length > 2) strSQL = strSQL + " AND Steel_Grade='" + SteelGrade + "'";

            strSQL = strSQL + " ORDER BY slab_id";

            DataTable dt = sqt.ReadDatatable_OraDB(strSQL);
            for (int RowIndex = 0; RowIndex < dt.Rows.Count; RowIndex++)
            {
                lst = new LY2250SlabNoInfo();

                str = dt.Rows[RowIndex]["slab_id"].ToString(); if (str.Length > 0) lst.slab_id = str;
                str = dt.Rows[RowIndex]["productno"].ToString(); if (str.Length > 0) lst.productno = str;
                str = dt.Rows[RowIndex]["steel_grade"].ToString(); if (str.Length > 0) lst.steel_grade = str;
                str = dt.Rows[RowIndex]["fceno"].ToString(); if (str.Length > 0) lst.fceno = str;
                str = dt.Rows[RowIndex]["charge_time"].ToString(); if (str.Length > 0) lst.charge_time = str;

                LST.Add(lst);
            }
            dt.Dispose();
            return LST;
        }
        public List<LY2250SlabNoInfo> GetLY2250SlabNoInfo(string HeatID)
        {
            //获取炉次对应的板坯号信息
            List<LY2250SlabNoInfo> LST = new List<LY2250SlabNoInfo>();
            LY2250SlabNoInfo lst = new LY2250SlabNoInfo();
            string str = "";

            string strSQL = "SELECT * FROM hrm_l2_FCEREPORTS WHERE slab_id LIKE '" + HeatID + "%'";
            DataTable dt = sqt.ReadDatatable_OraDB(strSQL);
            for (int RowIndex = 0; RowIndex < dt.Rows.Count; RowIndex++)
            {
                lst = new LY2250SlabNoInfo();

                str = dt.Rows[RowIndex]["slab_id"].ToString(); if (str.Length > 0) lst.slab_id = str;
                str = dt.Rows[RowIndex]["productno"].ToString(); if (str.Length > 0) lst.productno = str;
                str = dt.Rows[RowIndex]["steel_grade"].ToString(); if (str.Length > 0) lst.steel_grade = str;
                str = dt.Rows[RowIndex]["fceno"].ToString(); if (str.Length > 0) lst.fceno = str;
                str = dt.Rows[RowIndex]["charge_time"].ToString(); if (str.Length > 0) lst.charge_time = str;

                LST.Add(lst);
            }
            dt.Dispose();
            return LST;
        }

        public List<HRM_BaseInfo_HF> GetHRM_BaseInfo_HF(string HeatID)
        {
            //获取炉次对应的板坯号信息
            List<HRM_BaseInfo_HF> LST = new List<HRM_BaseInfo_HF>();
            HRM_BaseInfo_HF lst = new HRM_BaseInfo_HF();
            string str = "";

            string strSQL = "SELECT * FROM hrm_l2_FCEREPORTS WHERE slab_id LIKE '" + HeatID + "%'";
            DataTable dt = sqt.ReadDatatable_OraDB(strSQL);
            for (int RowIndex = 0; RowIndex < dt.Rows.Count; RowIndex++)
            {
                lst = new HRM_BaseInfo_HF();

                str = dt.Rows[RowIndex]["slab_id"].ToString(); if (str.Length > 0) lst.slab_id = str;
                str = dt.Rows[RowIndex]["planno"].ToString(); if (str.Length > 0) lst.planno = str;
                str = dt.Rows[RowIndex]["productno"].ToString(); if (str.Length > 0) lst.productno = str;
                str = dt.Rows[RowIndex]["fceno"].ToString(); if (str.Length > 0) lst.fceno = str;
                str = dt.Rows[RowIndex]["fcerow"].ToString(); if (str.Length > 0) lst.fcerow = str;
                str = dt.Rows[RowIndex]["fcenorow"].ToString(); if (str.Length > 0) lst.fcenorow = str;
                str = dt.Rows[RowIndex]["charge_time"].ToString(); if (str.Length > 0) lst.charge_time = str;
                str = dt.Rows[RowIndex]["discharge_time"].ToString(); if (str.Length > 0) lst.discharge_time = str;
                str = dt.Rows[RowIndex]["slab_width"].ToString(); if (str.Length > 0) lst.slab_width = str;
                str = dt.Rows[RowIndex]["slab_length"].ToString(); if (str.Length > 0) lst.slab_length = str;
                str = dt.Rows[RowIndex]["slab_thick"].ToString(); if (str.Length > 0) lst.slab_thick = str;
                str = dt.Rows[RowIndex]["slab_weight"].ToString(); if (str.Length > 0) lst.slab_weight = str;
                str = dt.Rows[RowIndex]["slab_plan_weight"].ToString(); if (str.Length > 0) lst.slab_plan_weight = str;
                str = dt.Rows[RowIndex]["steel_grade"].ToString(); if (str.Length > 0) lst.steel_grade = str;
                str = dt.Rows[RowIndex]["in_recycle_time"].ToString(); if (str.Length > 0) lst.in_recycle_time = str;
                str = dt.Rows[RowIndex]["in_pre_time"].ToString(); if (str.Length > 0) lst.in_pre_time = str;
                str = dt.Rows[RowIndex]["in_first_time"].ToString(); if (str.Length > 0) lst.in_first_time = str;
                str = dt.Rows[RowIndex]["in_sec_time"].ToString(); if (str.Length > 0) lst.in_sec_time = str;
                str = dt.Rows[RowIndex]["in_soak_time"].ToString(); if (str.Length > 0) lst.in_soak_time = str;
                str = dt.Rows[RowIndex]["target_distemp"].ToString(); if (str.Length > 0) lst.target_distemp = str;
                str = dt.Rows[RowIndex]["ave_distemp"].ToString(); if (str.Length > 0) lst.ave_distemp = str;
                str = dt.Rows[RowIndex]["surface_distemp"].ToString(); if (str.Length > 0) lst.surface_distemp = str;
                str = dt.Rows[RowIndex]["center_distemp"].ToString(); if (str.Length > 0) lst.center_distemp = str;
                str = dt.Rows[RowIndex]["measure_rmtemp"].ToString(); if (str.Length > 0) lst.measure_rmtemp = str;
                str = dt.Rows[RowIndex]["calc_distemp"].ToString(); if (str.Length > 0) lst.calc_distemp = str;
                str = dt.Rows[RowIndex]["rm_status"].ToString(); if (str.Length > 0) lst.rm_status = str;
                str = dt.Rows[RowIndex]["time_stamp"].ToString(); if (str.Length > 0) lst.time_stamp = str;
                str = dt.Rows[RowIndex]["slab_charge_avg"].ToString(); if (str.Length > 0) lst.slab_charge_avg = str;
                str = dt.Rows[RowIndex]["hcheat_exit_avg"].ToString(); if (str.Length > 0) lst.hcheat_exit_avg = str;
                str = dt.Rows[RowIndex]["preheat_exit_avg"].ToString(); if (str.Length > 0) lst.preheat_exit_avg = str;
                str = dt.Rows[RowIndex]["fstheat_exit_avg"].ToString(); if (str.Length > 0) lst.fstheat_exit_avg = str;
                str = dt.Rows[RowIndex]["secheat_exit_avg"].ToString(); if (str.Length > 0) lst.secheat_exit_avg = str;
                str = dt.Rows[RowIndex]["shift_no"].ToString(); if (str.Length > 0) lst.shift_no = str;
                str = dt.Rows[RowIndex]["group_no"].ToString(); if (str.Length > 0) lst.group_no = str;
                str = dt.Rows[RowIndex]["steelgrade"].ToString(); if (str.Length > 0) lst.steelgrade = str;
                str = dt.Rows[RowIndex]["dsurface_distemp"].ToString(); if (str.Length > 0) lst.dsurface_distemp = str;
                str = dt.Rows[RowIndex]["slab_measure_temp"].ToString(); if (str.Length > 0) lst.slab_measure_temp = str;
                str = dt.Rows[RowIndex]["target_rm_temp"].ToString(); if (str.Length > 0) lst.target_rm_temp = str;
                str = dt.Rows[RowIndex]["real_width"].ToString(); if (str.Length > 0) lst.real_width = str;

                //热循环段的进出时间
                lst.RecycleSectionArrivalTime = lst.charge_time;
                DateTime InTime = Convert.ToDateTime(dt.Rows[RowIndex]["charge_time"]);
                DateTime OutTime = InTime.AddMinutes(Convert.ToDouble(lst.in_recycle_time));
                lst.RecycleSectionLeaveTime = OutTime.ToString();

                //预热段的进出时间
                InTime = OutTime;
                OutTime=InTime.AddMinutes(Convert.ToDouble(lst.in_pre_time));
                lst.PreHeatSectionArrivalTime = InTime.ToString();
                lst.PreHeatSectionLeaveTime = OutTime.ToString();

                //第1加热段
                InTime = OutTime;
                OutTime = InTime.AddMinutes(Convert.ToDouble(lst.in_first_time));
                lst.FirstHeatSectionArrivalTime = InTime.ToString();
                lst.FirstHeatSectionLeaveTime = OutTime.ToString();

                //第2加热段
                InTime = OutTime;
                OutTime = InTime.AddMinutes(Convert.ToDouble(lst.in_sec_time));
                lst.SecondHeatSectionArrivalTime = InTime.ToString();
                lst.SecondHeatSectionLeaveTime = OutTime.ToString();

                //均热段
                InTime = OutTime;
                OutTime = InTime.AddMinutes(Convert.ToDouble(lst.in_soak_time));
                lst.SoakSectionArrivalTime = InTime.ToString();
                lst.SoakSectionLeaveTime = OutTime.ToString();

                LST.Add(lst);
            }
            dt.Dispose();
            return LST;
        }

        public List<HRM_BaseInfo_RM>  GetHRM_BaseInfo_RM(string SlabID)
        {
            List<HRM_BaseInfo_RM> LST = new List<HRM_BaseInfo_RM>();
            HRM_BaseInfo_RM lst=new HRM_BaseInfo_RM();

            string strSQL = "SELECT SLAB_ID,COIL_ID,I_RMTOTALPASS FROM HRM_L2_RMSETUP WHERE Slab_ID='" + SlabID + "'";
            DataTable dt = sqt.ReadDatatable_OraDB(strSQL);

            if (dt.Rows.Count > 0)
            { 
                int RowIndex=0;
                lst.C_SLABID = dt.Rows[RowIndex]["SLAB_ID"].ToString();
                lst.C_COILID = dt.Rows[RowIndex]["COIL_ID"].ToString();
                lst.I_RMTOTALPASS = dt.Rows[RowIndex]["I_RMTOTALPASS"].ToString();
            }

            LST.Add(lst);
            return LST;

        }

        //精轧信息
        public List<HRM_BaseInfo_FR> GetHRM_BaseInfo_FR(string SlabID)
        {
            List<HRM_BaseInfo_FR> LST = new List<HRM_BaseInfo_FR>();
            HRM_BaseInfo_FR lst  ;

            string strSQL = "SELECT * FROM HRM_MATTRACK_TIME"
                          + " WHERE mat_no = '" + SlabID + "'"
                          + " AND device_no='LY2250_FM'";
            DataTable dt = sqt.ReadDatatable_OraDB(strSQL);

            for (int RowIndex = 0; RowIndex < dt.Rows.Count; RowIndex++)
            {
                lst= new HRM_BaseInfo_FR();
                HRM_BaseInfo_FR_Ini(ref lst);

                lst.CoilID = "";
                lst.Mat_NO = dt.Rows[RowIndex]["mat_no"].ToString();
                lst.EntryTime_FR1 = dt.Rows[RowIndex]["start_time"].ToString();
                lst.ExitTime_FR1 = dt.Rows[RowIndex]["stop_time"].ToString();

                lst.EntryTime_FR2 = dt.Rows[RowIndex]["start_time"].ToString();
                lst.ExitTime_FR2 = dt.Rows[RowIndex]["stop_time"].ToString();

                lst.EntryTime_FR3 = dt.Rows[RowIndex]["start_time"].ToString();
                lst.ExitTime_FR3 = dt.Rows[RowIndex]["stop_time"].ToString();

                lst.EntryTime_FR4 = dt.Rows[RowIndex]["start_time"].ToString();
                lst.ExitTime_FR4 = dt.Rows[RowIndex]["stop_time"].ToString();

                lst.EntryTime_FR5 = dt.Rows[RowIndex]["start_time"].ToString();
                lst.ExitTime_FR5 = dt.Rows[RowIndex]["stop_time"].ToString();

                lst.EntryTime_FR6 = dt.Rows[RowIndex]["start_time"].ToString();
                lst.ExitTime_FR6 = dt.Rows[RowIndex]["stop_time"].ToString();

                lst.EntryTime_FR7 = dt.Rows[RowIndex]["start_time"].ToString();
                lst.ExitTime_FR7 = dt.Rows[RowIndex]["stop_time"].ToString();

                LST.Add(lst);
            }
            
            return LST;

        }

        
        //卷取 基础数据
        public List<HRM_BaseInfo_DC> GetHRM_BaseInfo_DC(string SlabID)
        {
            List<HRM_BaseInfo_DC> LST = new List<HRM_BaseInfo_DC>();
            HRM_BaseInfo_DC lst;

            lst = new HRM_BaseInfo_DC();
            lst.CoilID = "";
            lst.SlabID = SlabID;

            LST.Add(lst);
            return LST;
        }
        //获取层流冷却的数据
        public List<HRM_BaseInfo_CTC> GetHRM_BaseInfo_CTC(string SlabID)
        {
            List<HRM_BaseInfo_CTC> LST = new List<HRM_BaseInfo_CTC>();
            HRM_BaseInfo_CTC lst;
            
            string strSQL = "SELECT * FROM HRM_L2_CTCSETUP"
                          + " WHERE slab_id = '" + SlabID + "'";
            DataTable dt = sqt.ReadDatatable_OraDB(strSQL);

            if ( dt.Rows.Count>0)
            {
                lst = new HRM_BaseInfo_CTC();

                int  RowIndex=0;
                lst.SLAB_ID = dt.Rows[RowIndex]["SLAB_ID"].ToString();
                lst.COIL_ID = dt.Rows[RowIndex]["COIL_ID"].ToString();                
                lst.PIECENAME = dt.Rows[RowIndex]["PIECENAME"].ToString();
                lst.HISTORYKEYTM = dt.Rows[RowIndex]["HISTORYKEYTM"].ToString();
                lst.COURSE = dt.Rows[RowIndex]["COURSE"].ToString();
                lst.STATUS = dt.Rows[RowIndex]["STATUS"].ToString();
                lst.AIR_COOL_TM = dt.Rows[RowIndex]["AIR_COOL_TM"].ToString();
                lst.SPRAY_BOT_FBK = dt.Rows[RowIndex]["SPRAY_BOT_FBK"].ToString();
                lst.SPRAY_TOP_FBK = dt.Rows[RowIndex]["SPRAY_TOP_FBK"].ToString();
                lst.SPRAY_BOT_FFWD = dt.Rows[RowIndex]["SPRAY_BOT_FFWD"].ToString();
                lst.SPRAY_TOP_FFWD = dt.Rows[RowIndex]["SPRAY_TOP_FFWD"].ToString();
                lst.FDEL_THK_TARG = dt.Rows[RowIndex]["FDEL_THK_TARG"].ToString();
                lst.FDT_TEMP_TARG = dt.Rows[RowIndex]["FDT_TEMP_TARG"].ToString();
                lst.FDEL_WIDTH_TARG = dt.Rows[RowIndex]["FDEL_WIDTH_TARG"].ToString();
                lst.GRT_IDX = dt.Rows[RowIndex]["GRT_IDX"].ToString();
                lst.LEN_HEAD = dt.Rows[RowIndex]["LEN_HEAD"].ToString();
                lst.LEN_TAIL = dt.Rows[RowIndex]["LEN_TAIL"].ToString();
                lst.SPRAY_PAT = dt.Rows[RowIndex]["SPRAY_PAT"].ToString();
                lst.IS_STEP_COOL = dt.Rows[RowIndex]["IS_STEP_COOL"].ToString();
                lst.TEMP_OFFSET_HEAD = dt.Rows[RowIndex]["TEMP_OFFSET_HEAD"].ToString();
                lst.TEMP_OFFSET_TAIL = dt.Rows[RowIndex]["TEMP_OFFSET_TAIL"].ToString();
                lst.CT_RANGE_SEL = dt.Rows[RowIndex]["CT_RANGE_SEL"].ToString();
                lst.CT_TEMP_PRED = dt.Rows[RowIndex]["CT_TEMP_PRED"].ToString();
                lst.MT_TEMP_PRED = dt.Rows[RowIndex]["MT_TEMP_PRED"].ToString();
                lst.CT_TEMP_TARG = dt.Rows[RowIndex]["CT_TEMP_TARG"].ToString();
                lst.MT_TEMP_TARG = dt.Rows[RowIndex]["MT_TEMP_TARG"].ToString();
                lst.ROT_SPRAY_STS_TOP = dt.Rows[RowIndex]["ROT_SPRAY_STS_TOP"].ToString();
                lst.ROT_SPRAY_STS_BOT = dt.Rows[RowIndex]["ROT_SPRAY_STS_BOT"].ToString();
                lst.ROT_SPRAY_STS_CROSS = dt.Rows[RowIndex]["ROT_SPRAY_STS_CROSS"].ToString();
                lst.DRY_HEAD_LEN = dt.Rows[RowIndex]["DRY_HEAD_LEN"].ToString();

                LST.Add(lst);
            }
            
            return LST;
        }

        private void HRM_BaseInfo_FR_Ini(ref HRM_BaseInfo_FR lst)
        {
            lst.CoilID = ""; lst.Mat_NO = "";
            lst.EntryTime_FR1 = DateTime.Now.ToString(); lst.ExitTime_FR1 = DateTime.Now.ToString();
            lst.EntryTime_FR2 = DateTime.Now.ToString(); lst.ExitTime_FR2 = DateTime.Now.ToString();
            lst.EntryTime_FR3 = DateTime.Now.ToString(); lst.ExitTime_FR3 = DateTime.Now.ToString();
            lst.EntryTime_FR4 = DateTime.Now.ToString(); lst.ExitTime_FR4 = DateTime.Now.ToString();
            lst.EntryTime_FR5 = DateTime.Now.ToString(); lst.ExitTime_FR5 = DateTime.Now.ToString();
            lst.EntryTime_FR6 = DateTime.Now.ToString(); lst.ExitTime_FR6 = DateTime.Now.ToString();
            lst.EntryTime_FR7 = DateTime.Now.ToString(); lst.ExitTime_FR7 = DateTime.Now.ToString();
        }

        //精轧
        public List<HisDB_FR> GetHRM_HisDB_FR(string FR_ID, string StartTime,string EndTime)
        {
            List<HisDB_FR> LST = new List<HisDB_FR>();
            HisDB_FR lst ;

            string[] tags = new string[25];            
            tags[00] = "LYQ2250.F" + FR_ID + ".BendForce";//
            tags[01] = "LYQ2250.F" + FR_ID + ".BendForceSet";//
            tags[02] = "LYQ2250.F" + FR_ID + ".BS";//
            tags[03] = "LYQ2250.F" + FR_ID + ".DraftSet";//
            tags[04] = "LYQ2250.F" + FR_ID + ".DSGap";//

            tags[05] = "LYQ2250.F" + FR_ID + ".FS";//
            tags[06] = "LYQ2250.F" + FR_ID + ".GapSet";//
            tags[07] = "LYQ2250.F" + FR_ID + ".LENGTH";//
            tags[08] = "LYQ2250.F" + FR_ID + ".OilFilm";//
            tags[09] = "LYQ2250.F" + FR_ID + ".OSGap";//

            tags[10] = "LYQ2250.F" + FR_ID + ".RollForce";//
            tags[11] = "LYQ2250.F" + FR_ID + ".RollForceSet";//
            tags[12] = "LYQ2250.F" + FR_ID + ".ShiftBot";//
            tags[13] = "LYQ2250.F" + FR_ID + ".ShiftSet";//
            tags[14] = "LYQ2250.F" + FR_ID + ".ShiftTop";//

            tags[15] = "LYQ2250.F" + FR_ID + ".Speed";//
            tags[16] = "LYQ2250.F" + FR_ID + ".SpeedSet";//
            tags[17] = "LYQ2250.F" + FR_ID + ".Tension";//
            tags[18] = "LYQ2250.F" + FR_ID + ".TensionSet";//
            tags[19] = "LYQ2250.F" + FR_ID + ".Torque";//

            tags[20] = "LYQ2250.F" + FR_ID + ".WaterFlow";//
            tags[21] = "LYQ2250.F" + FR_ID + ".WRWaterFlow";//

            tags[22] = "LYQ2250.F" + FR_ID + ".AGC_ON";//数字量
            tags[23] = "LYQ2250.F" + FR_ID + ".ON";//数字量
            tags[24] = "LYQ2250.F" + FR_ID + ".WaterOn";//数字量
            
            string str;

            //获取历史数据
            System.Data.DataTable dt = sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(StartTime), Convert.ToDateTime(EndTime));
            

            for (int I = 0; I < dt.Rows.Count; I++)
            {
                lst = new HisDB_FR();

                lst.DateAndTime = Convert.ToDateTime(dt.Rows[I][00]);
                //dt.Rows[I][00]是时间列
                str = dt.Rows[I][01].ToString(); if (str.Length > 0) lst.BendForce = Convert.ToSingle(str);//
                str = dt.Rows[I][02].ToString(); if (str.Length > 0) lst.BendForceSet = Convert.ToSingle(str);//
                str = dt.Rows[I][03].ToString(); if (str.Length > 0) lst.BS = Convert.ToSingle(str);//
                str = dt.Rows[I][04].ToString(); if (str.Length > 0) lst.DraftSet = Convert.ToSingle(str);//
                str = dt.Rows[I][05].ToString(); if (str.Length > 0) lst.DSGap = Convert.ToSingle(str);//

                str = dt.Rows[I][06].ToString(); if (str.Length > 0) lst.FS = Convert.ToSingle(str);//
                str = dt.Rows[I][07].ToString(); if (str.Length > 0) lst.GapSet = Convert.ToSingle(str);//
                str = dt.Rows[I][08].ToString(); if (str.Length > 0) lst.LENGTH = Convert.ToSingle(str);//
                str = dt.Rows[I][09].ToString(); if (str.Length > 0) lst.OilFilm = Convert.ToSingle(str);//
                str = dt.Rows[I][10].ToString(); if (str.Length > 0) lst.OSGap = Convert.ToSingle(str);//

                str = dt.Rows[I][11].ToString(); if (str.Length > 0) lst.RollForce = Convert.ToSingle(str);//
                str = dt.Rows[I][12].ToString(); if (str.Length > 0) lst.RollForceSet = Convert.ToSingle(str);//
                str = dt.Rows[I][13].ToString(); if (str.Length > 0) lst.ShiftBot = Convert.ToSingle(str);//
                str = dt.Rows[I][14].ToString(); if (str.Length > 0) lst.ShiftSet = Convert.ToSingle(str);//
                str = dt.Rows[I][15].ToString(); if (str.Length > 0) lst.ShiftTop = Convert.ToSingle(str);//

                str = dt.Rows[I][16].ToString(); if (str.Length > 0) lst.Speed = Convert.ToSingle(str);//
                str = dt.Rows[I][17].ToString(); if (str.Length > 0) lst.SpeedSet = Convert.ToSingle(str);//
                str = dt.Rows[I][18].ToString(); if (str.Length > 0) lst.Tension = Convert.ToSingle(str);//
                str = dt.Rows[I][19].ToString(); if (str.Length > 0) lst.TensionSet = Convert.ToSingle(str);//
                str = dt.Rows[I][20].ToString(); if (str.Length > 0) lst.Torque = Convert.ToSingle(str);//

                str = dt.Rows[I][21].ToString(); if (str.Length > 0) lst.WaterFlow = Convert.ToSingle(str);//
                str = dt.Rows[I][22].ToString(); if (str.Length > 0) lst.WRWaterFlow = Convert.ToSingle(str);//

                lst.AGC_ON = Convert.ToBoolean(dt.Rows[I][23]);//数字量
                lst.ON = Convert.ToBoolean(dt.Rows[I][24]);//数字量
                lst.WaterOn = Convert.ToBoolean(dt.Rows[I][25]);//数字量

                //只有长度大于0才采集
                if (lst.LENGTH>0) LST.Add(lst);
            }
            dt.Dispose();

            return LST;
           
           ////去除头部的长度不变的数据
           // //一开始，其长度为上一次的数据，很大，且保持不变
                   
           // //从前往后逐个循环，查找第一个有效数
           // int FirstPoint = 0;
           // for (int I = 1; I < LST.Count; I++)
           // {
           //     if (Math.Abs(LST[I].LENGTH - LST[I-1].LENGTH) >0) 
           //     {
           //         FirstPoint = I;
           //         continue;
           //     }
           // }
          

           // //从后往前倒序循环，查找最后第一个有效数
           // int LastPoint = LST.Count;            
           // for (int I = LST.Count; I >= FirstPoint; I--)
           // {
           //     if (Math.Abs(LST[I].LENGTH - LST[I - 1].LENGTH) > 0) 
           //     {
           //         LastPoint = I;
           //         continue;
           //     }
           // }
           

           // //定义一个新的
           // List<HisDB_FR> LST_New = new List<HisDB_FR>();
           // for (int I = FirstPoint; I <= LST.Count; I++)
           // {
           //    LST_New.Add(LST[I]);
           // }

           // return LST_New;
        }

        //超快冷
        public List<HisDB_UFC> GetHRM_HisDB_UFC(string CoilID)
        {
           
            List<HisDB_UFC> LST = new List<HisDB_UFC>();
            HisDB_UFC lst;


            //获取起止时间          
            string StartTime = "", EndTime = "";
            string strSQL = "SELECT * FROM HRM_MATTRACK_TIME WHERE mat_no='" + CoilID + "'";
            DataTable dtA = sqt.ReadDatatable_OraDB(strSQL);
            for (int RowIndex = 0; RowIndex < dtA.Rows.Count; RowIndex++)
            {
                string Device_no = dtA.Rows[RowIndex]["device_no"].ToString();
                if ("LY2250_FM" == Device_no) StartTime = dtA.Rows[RowIndex]["start_time"].ToString();
                if ("LY2250_CTC" == Device_no) EndTime = dtA.Rows[RowIndex]["stop_time"].ToString();
            }
            if ((StartTime.Length <= 0) || (EndTime.Length <= 0)) return LST;


            string[] tags = new string[43];
            tags[00] = "LYQ2250.UFC.UFCTselect";//	Analog	超快冷出口温度
            tags[01] = "LYQ2250.UFC.Water_temperature";//	Analog	超快冷水温
            tags[02] = "LYQ2250.UFC.Hpress_All_Water_press_1";//	Analog	超快冷水压

            tags[03] = "LYQ2250.UFC.Hpress1_Dn_flux_act";//	Analog	超快冷1#下集管流量
            tags[04] = "LYQ2250.UFC.Hpress1_Up_flux_act";//	Analog	超快冷1#上集管流量
            tags[05] = "LYQ2250.UFC.Hpress2_Dn_flux_act";//	Analog	超快冷2#下集管流量
            tags[06] = "LYQ2250.UFC.Hpress2_Up_flux_act";//	Analog	超快冷2#上集管流量
            tags[07] = "LYQ2250.UFC.Hpress3_Dn_flux_act";//	Analog	超快冷3#下集管流量
            tags[08] = "LYQ2250.UFC.Hpress3_Up_flux_act";//	Analog	超快冷3#上集管流量
            tags[09] = "LYQ2250.UFC.Hpress4_Dn_flux_act";//	Analog	超快冷4#下集管流量
            tags[10] = "LYQ2250.UFC.Hpress4_Up_flux_act";//	Analog	超快冷4#上集管流量
            tags[11] = "LYQ2250.UFC.Hpress5_Dn_flux_act";//	Analog	超快冷5#下集管流量
            tags[12] = "LYQ2250.UFC.Hpress5_Up_flux_act";//	Analog	超快冷5#上集管流量

            tags[13] = "LYQ2250.UFC.Hpres6_Up_flux_act";//	Analog	超快冷6#上集管流量
            tags[14] = "LYQ2250.UFC.Hpress6_Dn_flux_act";//	Analog	超快冷6#下集管流量
            tags[15] = "LYQ2250.UFC.Hpres7_Up_flux_act";//	Analog	超快冷7#上集管流量
            tags[16] = "LYQ2250.UFC.Hpress7_Dn_flux_act";//	Analog	超快冷7#下集管流量
            tags[17] = "LYQ2250.UFC.Hpres8_Up_flux_act";//	Analog	超快冷8#上集管流量
            tags[18] = "LYQ2250.UFC.Hpres8_Dn_flux_act";//	Analog	超快冷8#下集管流量
            tags[19] = "LYQ2250.UFC.Hpres9_Dn_flux_act";//	Analog	超快冷9#下集管流量
            tags[20] = "LYQ2250.UFC.Hpres9_Up_flux_act";//	Analog	超快冷9#上集管流量
            tags[21] = "LYQ2250.UFC.Hpres10_Dn_flux_act";//	Analog	超快冷10#下集管流量
            tags[22] = "LYQ2250.UFC.Hpres10_Up_flux_act";//	Analog	超快冷10#上集管流量

            tags[23] = "LYQ2250.UFC.Hpres11_Dn_flux_act";//	Analog	超快冷11#下集管流量
            tags[24] = "LYQ2250.UFC.Hpres11_Up_flux_act";//	Analog	超快冷11#上集管流量
            tags[25] = "LYQ2250.UFC.Hpres12_Dn_flux_act";//	Analog	超快冷12#下集管流量
            tags[26] = "LYQ2250.UFC.Hpres12_Up_flux_act";//	Analog	超快冷12#上集管流量
            tags[27] = "LYQ2250.UFC.Hpres13_Dn_flux_act";//	Analog	超快冷13#下集管流量
            tags[28] = "LYQ2250.UFC.Hpres13_Up_flux_act";//	Analog	超快冷13#上集管流量
            tags[29] = "LYQ2250.UFC.Hpres14_Dn_flux_act";//	Analog	超快冷14#下集管流量
            tags[30] = "LYQ2250.UFC.Hpres14_Up_flux_act";//	Analog	超快冷14#上集管流量
            tags[31] = "LYQ2250.UFC.Hpres15_Dn_flux_act";//	Analog	超快冷15#下集管流量
            tags[32] = "LYQ2250.UFC.Hpres15_Up_flux_act";//	Analog	超快冷15#上集管流量

            tags[33] = "LYQ2250.UFC.Hpres16_Dn_flux_act";//	Analog	超快冷16#下集管流量
            tags[34] = "LYQ2250.UFC.Hpres16_Up_flux_act";//	Analog	超快冷16#上集管流量
            tags[35] = "LYQ2250.UFC.Hpres17_Dn_flux_act";//	Analog	超快冷17#下集管流量
            tags[36] = "LYQ2250.UFC.Hpres17_Up_flux_act";//	Analog	超快冷17#上集管流量
            tags[37] = "LYQ2250.UFC.Hpres18_Dn_flux_act";//	Analog	超快冷18#下集管流量
            tags[38] = "LYQ2250.UFC.Hpres18_Up_flux_act";//	Analog	超快冷18#上集管流量
            tags[39] = "LYQ2250.UFC.Hpres19_Dn_flux_act";//	Analog	超快冷19#下集管流量
            tags[40] = "LYQ2250.UFC.Hpres19_Up_flux_act";//	Analog	超快冷19#上集管流量
            tags[41] = "LYQ2250.UFC.Hpres20_Dn_flux_act";//	Analog	超快冷20#下集管流量
            tags[42] = "LYQ2250.UFC.Hpres20_Up_flux_act";//	Analog	超快冷20#上集管流量

            string str;
            //获取历史数据
            System.Data.DataTable dt = sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(StartTime), Convert.ToDateTime(EndTime));

            for (int I = 0; I < dt.Rows.Count; I++)
            {
                lst = new HisDB_UFC();

                lst.DateAndTime = Convert.ToDateTime(dt.Rows[I][00]);

                str = dt.Rows[I][01].ToString(); if (str.Length > 0) lst.UFCTselect = Convert.ToSingle(str);//	Analog	超快冷出口温度
                str = dt.Rows[I][02].ToString(); if (str.Length > 0) lst.Water_temperature = Convert.ToSingle(str);//	Analog	超快冷水温
                str = dt.Rows[I][03].ToString(); if (str.Length > 0) lst.Hpress_All_Water_press_1 = Convert.ToSingle(str);//	Analog	超快冷水压

                str = dt.Rows[I][04].ToString(); if (str.Length > 0) lst.Hpress1_Dn_flux_act = Convert.ToSingle(str);//	Analog	超快冷1#下集管流量
                str = dt.Rows[I][05].ToString(); if (str.Length > 0) lst.Hpress1_Up_flux_act = Convert.ToSingle(str);//	Analog	超快冷1#上集管流量
                str = dt.Rows[I][06].ToString(); if (str.Length > 0) lst.Hpress2_Dn_flux_act = Convert.ToSingle(str);//	Analog	超快冷2#下集管流量
                str = dt.Rows[I][07].ToString(); if (str.Length > 0) lst.Hpress2_Up_flux_act = Convert.ToSingle(str);//	Analog	超快冷2#上集管流量
                str = dt.Rows[I][08].ToString(); if (str.Length > 0) lst.Hpress3_Dn_flux_act = Convert.ToSingle(str);//	Analog	超快冷3#下集管流量
                str = dt.Rows[I][09].ToString(); if (str.Length > 0) lst.Hpress3_Up_flux_act = Convert.ToSingle(str);//	Analog	超快冷3#上集管流量
                str = dt.Rows[I][10].ToString(); if (str.Length > 0) lst.Hpress4_Dn_flux_act = Convert.ToSingle(str);//	Analog	超快冷4#下集管流量
                str = dt.Rows[I][11].ToString(); if (str.Length > 0) lst.Hpress4_Up_flux_act = Convert.ToSingle(str);//	Analog	超快冷4#上集管流量
                str = dt.Rows[I][12].ToString(); if (str.Length > 0) lst.Hpress5_Dn_flux_act = Convert.ToSingle(str);//	Analog	超快冷5#下集管流量
                str = dt.Rows[I][13].ToString(); if (str.Length > 0) lst.Hpress5_Up_flux_act = Convert.ToSingle(str);//	Analog	超快冷5#上集管流量

                str = dt.Rows[I][14].ToString(); if (str.Length > 0) lst.Hpres6_Up_flux_act = Convert.ToSingle(str);//	Analog	超快冷6#上集管流量
                str = dt.Rows[I][15].ToString(); if (str.Length > 0) lst.Hpress6_Dn_flux_act = Convert.ToSingle(str);//	Analog	超快冷6#下集管流量
                str = dt.Rows[I][16].ToString(); if (str.Length > 0) lst.Hpres7_Up_flux_act = Convert.ToSingle(str);//	Analog	超快冷7#上集管流量
                str = dt.Rows[I][17].ToString(); if (str.Length > 0) lst.Hpress7_Dn_flux_act = Convert.ToSingle(str);//	Analog	超快冷7#下集管流量
                str = dt.Rows[I][18].ToString(); if (str.Length > 0) lst.Hpres8_Up_flux_act = Convert.ToSingle(str);//	Analog	超快冷8#上集管流量
                str = dt.Rows[I][19].ToString(); if (str.Length > 0) lst.Hpres8_Dn_flux_act = Convert.ToSingle(str);//	Analog	超快冷8#下集管流量
                str = dt.Rows[I][20].ToString(); if (str.Length > 0) lst.Hpres9_Dn_flux_act = Convert.ToSingle(str);//	Analog	超快冷9#下集管流量
                str = dt.Rows[I][21].ToString(); if (str.Length > 0) lst.Hpres9_Up_flux_act = Convert.ToSingle(str);//	Analog	超快冷9#上集管流量
                str = dt.Rows[I][22].ToString(); if (str.Length > 0) lst.Hpres10_Dn_flux_act = Convert.ToSingle(str);//	Analog	超快冷10#下集管流量
                str = dt.Rows[I][23].ToString(); if (str.Length > 0) lst.Hpres10_Up_flux_act = Convert.ToSingle(str);//	Analog	超快冷10#上集管流量

                str = dt.Rows[I][24].ToString(); if (str.Length > 0) lst.Hpres11_Dn_flux_act = Convert.ToSingle(str);//	Analog	超快冷11#下集管流量
                str = dt.Rows[I][25].ToString(); if (str.Length > 0) lst.Hpres11_Up_flux_act = Convert.ToSingle(str);//	Analog	超快冷11#上集管流量
                str = dt.Rows[I][26].ToString(); if (str.Length > 0) lst.Hpres12_Dn_flux_act = Convert.ToSingle(str);//	Analog	超快冷12#下集管流量
                str = dt.Rows[I][27].ToString(); if (str.Length > 0) lst.Hpres12_Up_flux_act = Convert.ToSingle(str);//	Analog	超快冷12#上集管流量
                str = dt.Rows[I][28].ToString(); if (str.Length > 0) lst.Hpres13_Dn_flux_act = Convert.ToSingle(str);//	Analog	超快冷13#下集管流量
                str = dt.Rows[I][29].ToString(); if (str.Length > 0) lst.Hpres13_Up_flux_act = Convert.ToSingle(str);//	Analog	超快冷13#上集管流量
                str = dt.Rows[I][30].ToString(); if (str.Length > 0) lst.Hpres14_Dn_flux_act = Convert.ToSingle(str);//	Analog	超快冷14#下集管流量
                str = dt.Rows[I][31].ToString(); if (str.Length > 0) lst.Hpres14_Up_flux_act = Convert.ToSingle(str);//	Analog	超快冷14#上集管流量
                str = dt.Rows[I][32].ToString(); if (str.Length > 0) lst.Hpres15_Dn_flux_act = Convert.ToSingle(str);//	Analog	超快冷15#下集管流量
                str = dt.Rows[I][33].ToString(); if (str.Length > 0) lst.Hpres15_Up_flux_act = Convert.ToSingle(str);//	Analog	超快冷15#上集管流量

                str = dt.Rows[I][34].ToString(); if (str.Length > 0) lst.Hpres16_Dn_flux_act = Convert.ToSingle(str);//	Analog	超快冷16#下集管流量
                str = dt.Rows[I][35].ToString(); if (str.Length > 0) lst.Hpres16_Up_flux_act = Convert.ToSingle(str);//	Analog	超快冷16#上集管流量
                str = dt.Rows[I][36].ToString(); if (str.Length > 0) lst.Hpres17_Dn_flux_act = Convert.ToSingle(str);//	Analog	超快冷17#下集管流量
                str = dt.Rows[I][37].ToString(); if (str.Length > 0) lst.Hpres17_Up_flux_act = Convert.ToSingle(str);//	Analog	超快冷17#上集管流量
                str = dt.Rows[I][38].ToString(); if (str.Length > 0) lst.Hpres18_Dn_flux_act = Convert.ToSingle(str);//	Analog	超快冷18#下集管流量
                str = dt.Rows[I][39].ToString(); if (str.Length > 0) lst.Hpres18_Up_flux_act = Convert.ToSingle(str);//	Analog	超快冷18#上集管流量
                str = dt.Rows[I][40].ToString(); if (str.Length > 0) lst.Hpres19_Dn_flux_act = Convert.ToSingle(str);//	Analog	超快冷19#下集管流量
                str = dt.Rows[I][41].ToString(); if (str.Length > 0) lst.Hpres19_Up_flux_act = Convert.ToSingle(str);//	Analog	超快冷19#上集管流量
                str = dt.Rows[I][42].ToString(); if (str.Length > 0) lst.Hpres20_Dn_flux_act = Convert.ToSingle(str);//	Analog	超快冷20#下集管流量
                str = dt.Rows[I][43].ToString(); if (str.Length > 0) lst.Hpres20_Up_flux_act = Convert.ToSingle(str);//	Analog	超快冷20#上集管流量

                LST.Add(lst);
            }
            dt.Dispose();


            return LST;
        }

        //层流冷却
        public List<HisDB_CTC> GetHRM_HisDB_CTC(string CoilID)
        {
            List<HisDB_CTC> LST = new List<HisDB_CTC>();
            HisDB_CTC lst;
            string str;

            //获取起止时间
            string StartTime = "", EndTime = "";
            string strSQL = "SELECT * FROM HRM_MATTRACK_TIME"
                          + " WHERE mat_no='" + CoilID + "'"
                          + " AND Device_no='LY2250_CTC'";
            DataTable dtA = sqt.ReadDatatable_OraDB(strSQL);
            for (int RowIndex = 0; RowIndex < dtA.Rows.Count; RowIndex++)
            {
                StartTime = dtA.Rows[RowIndex]["start_time"].ToString();
                EndTime = dtA.Rows[RowIndex]["stop_time"].ToString();
            }
            if ((StartTime.Length <= 0) || (EndTime.Length <= 0)) return LST;

            //获取变量标签
            string[] tags = new string[12];            
            tags[00] = "LYQ2250.CTC.CT";//
            tags[01] = "LYQ2250.CTC.CT_LENGTH";//
            tags[02] = "LYQ2250.CTC.CT_SPEED";//
            tags[03] = "LYQ2250.CTC.CT_TARGET";//
            tags[04] = "LYQ2250.CTC.FDT";//
            tags[05] = "LYQ2250.CTC.FET";//
            tags[06] = "LYQ2250.CTC.MT";//
            tags[07] = "LYQ2250.CTC.MT_LENGTH";//
            tags[08] = "LYQ2250.CTC.MT_SPEED";//
            tags[09] = "LYQ2250.CTC.CT_ENABLE";// Discrete
            tags[10] = "LYQ2250.CTC.CT_ON";// Discrete
            tags[11] = "LYQ2250.CTC.MT_ON";// Discrete


            //获取历史数据
            System.Data.DataTable dt = sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(StartTime), Convert.ToDateTime(EndTime));

            for (int I = 0; I < dt.Rows.Count; I++)
            {
                lst = new HisDB_CTC();

                lst.DateAndTime = Convert.ToDateTime(dt.Rows[I][00]);
                                
                str = dt.Rows[I][01].ToString(); if (str.Length > 0) lst.CT = Convert.ToSingle(str);//
                str = dt.Rows[I][02].ToString(); if (str.Length > 0) lst.CT_LENGTH = Convert.ToSingle(str);//
                str = dt.Rows[I][03].ToString(); if (str.Length > 0) lst.CT_SPEED = Convert.ToSingle(str);//
                str = dt.Rows[I][04].ToString(); if (str.Length > 0) lst.CT_TARGET = Convert.ToSingle(str);//
                str = dt.Rows[I][05].ToString(); if (str.Length > 0) lst.FDT = Convert.ToSingle(str);//
                str = dt.Rows[I][06].ToString(); if (str.Length > 0) lst.FET = Convert.ToSingle(str);//
                str = dt.Rows[I][07].ToString(); if (str.Length > 0) lst.MT = Convert.ToSingle(str);//
                str = dt.Rows[I][08].ToString(); if (str.Length > 0) lst.MT_LENGTH = Convert.ToSingle(str);//
                str = dt.Rows[I][09].ToString(); if (str.Length > 0) lst.MT_SPEED = Convert.ToSingle(str);//
                lst.CT_ENABLE = Convert.ToBoolean(dt.Rows[I][10]);//
                lst.CT_ON = Convert.ToBoolean(dt.Rows[I][11]);//
                lst.MT_ON = Convert.ToBoolean(dt.Rows[I][12]);//
                
                LST.Add(lst);
            }
            dt.Dispose();


            return LST;
        }

        public void GetReportPdf_HF(string SlabID, ref iTextSharp.text.Document pdfDocument)
        {
            //转为竖直放置的页面
            pdfDocument.setPageSize(iTextSharp.text.PageSize.A4);

            //新开一个页面
            pdfDocument.newPage();
            //转为竖直放置的页面
            pdfDocument.setPageSize(iTextSharp.text.PageSize.A4);

            //标题
            if (null == FontCaption) DefinePdfFont();
            iTextSharp.text.Paragraph para = new iTextSharp.text.Paragraph(SlabID + "铸坯 加热炉数据追溯", FontCaption);
            iTextSharp.text.Chapter chapter = new iTextSharp.text.Chapter(para,21);
            pdfDocument.Add(chapter);
            
            FontHei.Size = 14;
            para = new iTextSharp.text.Paragraph("基本信息", FontHei);
            para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfDocument.Add(para);

            //以表形式来写入必须的信息
            iTextSharp.text.Table table = new iTextSharp.text.Table(6);
            table.WidthPercentage = 100; // 百分比
            table.Padding = 5;
            int[] headerwidths = { 10, 15, 10, 15, 10, 15 }; // 百分比
            table.setWidths(headerwidths);
            table.AutoFillEmptyCells = true;
            //设置边框颜色 
            table.BorderColor = new iTextSharp.text.Color(0, 0, 0);
            table.DefaultCellBorderColor = new iTextSharp.text.Color(0, 0, 255);
            iTextSharp.text.Color Color_DescCell = new iTextSharp.text.Color(System.Drawing.Color.LightGray);
            table.DefaultCellBackgroundColor = new iTextSharp.text.Color(System.Drawing.Color.White);
            table.DefaultVerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            table.DefaultHorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            FontKai.Size = 9; FontSong.Size = 9;
            iTextSharp.text.Cell cell;

            //获取基本信息
            List<HRM_BaseInfo_HF> LST = GetHRM_BaseInfo_HF(SlabID);
            if (LST.Count > 0)
            {
                //这里开始写基本信息
                cell = new iTextSharp.text.Cell(new Paragraph("班次", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].shift_no, FontSong)); table.addCell(cell); ;
                cell = new iTextSharp.text.Cell(new Paragraph("班别", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].group_no, FontSong)); table.addCell(cell); ;
                cell = new iTextSharp.text.Cell(new Paragraph("", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("", FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("铸坯号", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].slab_id, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("钢卷号", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].productno, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("钢种", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].steel_grade, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("坯长", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].slab_length, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("坯宽", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].slab_width, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("坯厚", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].slab_thick, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("坯重", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].slab_weight, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("进入温度", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].slab_charge_avg, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("", FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("加热炉号", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].fceno, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("进炉", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].charge_time, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("出炉", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].discharge_time, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("热循环段进", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].RecycleSectionArrivalTime, FontSong)); table.addCell(cell);                
                cell = new iTextSharp.text.Cell(new Paragraph("停留", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].in_recycle_time, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("出温", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("", FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("预热段进", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].PreHeatSectionArrivalTime, FontSong)); table.addCell(cell);               
                cell = new iTextSharp.text.Cell(new Paragraph("停留", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].in_pre_time, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("出口温度", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].hcheat_exit_avg, FontSong)); table.addCell(cell);


                cell = new iTextSharp.text.Cell(new Paragraph("加热1段进", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].FirstHeatSectionArrivalTime, FontSong)); table.addCell(cell);                
                cell = new iTextSharp.text.Cell(new Paragraph("停留", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].in_first_time, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("出口均温", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].fstheat_exit_avg, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("加热2段进", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].SecondHeatSectionArrivalTime, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("停留", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].in_sec_time, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("出口均温", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].secheat_exit_avg, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("均热段进", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].SoakSectionArrivalTime, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("停留", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].in_soak_time, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("出口温度", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("", FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("目标温度", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].target_distemp, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("ave_distemp", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].ave_distemp, FontSong)); table.addCell(cell);;
                cell = new iTextSharp.text.Cell(new Paragraph("surface_distemp", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].surface_distemp, FontSong)); table.addCell(cell);;

                cell = new iTextSharp.text.Cell(new Paragraph("center_distemp", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].center_distemp, FontSong)); table.addCell(cell);;
                cell = new iTextSharp.text.Cell(new Paragraph("measure_rmtemp", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].measure_rmtemp, FontSong)); table.addCell(cell);;                
                cell = new iTextSharp.text.Cell(new Paragraph("粗轧状态", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].rm_status, FontSong)); table.addCell(cell);;
                
                cell = new iTextSharp.text.Cell(new Paragraph("dsurface_distemp", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].dsurface_distemp, FontSong)); table.addCell(cell);;
                cell = new iTextSharp.text.Cell(new Paragraph("铸坯测量温度", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].slab_measure_temp, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("粗轧目标温度", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].target_rm_temp, FontSong)); table.addCell(cell); ;

            }
            else
            {
                FontKai.Size = 10.5F;
                para = new iTextSharp.text.Paragraph("没有数据", FontKai);
                para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                pdfDocument.Add(para);
                return;
            }
            //把表写入文档
            pdfDocument.Add(table);

            //历史数据
            GetReportPdf_HisDB_HF(SlabID, ref  pdfDocument);

        }
        //***** 2250热轧厂--加热炉*******//
        public void GetReportPdf_HisDB_HF(string SlabID, ref  iTextSharp.text.Document pdfDocument)
        {
            //定义
            ClsDuHisPic.StuDrawPicInfo stuDrawPicInfo = new ClsDuHisPic.StuDrawPicInfo();
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = false;
            stuDrawPicInfo.YAxisDisplayWhenDrawOnly1YAxis = "温度,℃";
            stuDrawPicInfo.IsDisplayRelativeTime = true;

            int YAxisNo = 1;
            stuDrawPicInfo.TagDescription[YAxisNo] = "上部A点";
            YAxisNo ++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "上部B点";
            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "上部平均";
            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "下部A点";
            YAxisNo ++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "下部B点";
            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "下部平均";

            //绘图的数据
            List<HisDB_HF> LST = GetHisDB_HF(SlabID);
                        
            stuDrawPicInfo.dt=new DataTable();
            stuDrawPicInfo.dt.Columns.Add("DateAndTime");
            stuDrawPicInfo.dt.Columns.Add("TopA");
            stuDrawPicInfo.dt.Columns.Add("TopB");
            stuDrawPicInfo.dt.Columns.Add("TopAvg");
            stuDrawPicInfo.dt.Columns.Add("BottomA");
            stuDrawPicInfo.dt.Columns.Add("BottomB");
            stuDrawPicInfo.dt.Columns.Add("BottomAvg");

            for (int RowIndex = 0; RowIndex < LST.Count; RowIndex++)
            {
                DataRow dr = stuDrawPicInfo.dt.NewRow();

                dr["DateAndTime"] = LST[RowIndex].DateAndTime;
                dr["TopA"] =LST[RowIndex].TopA;
                dr["TopB"] =LST[RowIndex].TopB;
                dr["TopAvg"] =LST[RowIndex].TopAvg;
                dr["BottomA"] =LST[RowIndex].BottomA;
                dr["BottomB"] =LST[RowIndex].BottomB;
                dr["BottomAvg"] = LST[RowIndex].BottomAvg;

                stuDrawPicInfo.dt.Rows.Add(dr);       
            }

            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(SlabID + "号铸坯 热轧均热过程", stuDrawPicInfo, ref pdfDocument);
        }
 

        //粗轧pdf报告
        public void GetReportPdf_RM(string SlabID, ref iTextSharp.text.Document pdfDocument)
        {   

            //新开一个页面
            pdfDocument.newPage();             

            //标题
            if (null == FontCaption) DefinePdfFont();
            iTextSharp.text.Paragraph para = new iTextSharp.text.Paragraph(SlabID + "铸坯 粗轧数据追溯", FontCaption);
            iTextSharp.text.Chapter chapter = new iTextSharp.text.Chapter(para, 22);
            pdfDocument.Add(chapter);

            //获取基本信息
            List<HRM_BaseInfo_RM> LST = GetHRM_BaseInfo_RM(SlabID);
            if (LST.Count <= 0)
            {
                FontKai.Size = 10.5F;
                para = new iTextSharp.text.Paragraph("没有查询到数据", FontKai);
                para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                pdfDocument.Add(para);
                return;
            }

            FontHei.Size = 14;
            para = new iTextSharp.text.Paragraph("基本信息", FontHei);
            para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfDocument.Add(para);

            //以表形式来写入必须的信息
            iTextSharp.text.Table table = new iTextSharp.text.Table(6);
            table.WidthPercentage = 100; // 百分比
            table.Padding = 5;
            int[] headerwidths = { 10, 15, 10, 15, 10, 15 }; // 百分比
            table.setWidths(headerwidths);
            table.AutoFillEmptyCells = true;
            //设置边框颜色 
            table.BorderColor = new iTextSharp.text.Color(0, 0, 0);
            table.DefaultCellBorderColor = new iTextSharp.text.Color(0, 0, 255);
            iTextSharp.text.Color Color_DescCell = new iTextSharp.text.Color(System.Drawing.Color.LightGray);
            table.DefaultCellBackgroundColor = new iTextSharp.text.Color(System.Drawing.Color.White);
            table.DefaultVerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            table.DefaultHorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            FontKai.Size = 9; FontSong.Size = 9;
            iTextSharp.text.Cell cell;
                       
            //这里开始写基本信息
            cell = new iTextSharp.text.Cell(new Paragraph("板坯号", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(LST[0].C_SLABID, FontSong)); table.addCell(cell); ;
            cell = new iTextSharp.text.Cell(new Paragraph("钢卷号", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(LST[0].C_COILID, FontSong)); table.addCell(cell); ;
            cell = new iTextSharp.text.Cell(new Paragraph("总道次", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(LST[0].I_RMTOTALPASS, FontKai)); table.addCell(cell);
    
                
            //把表写入文档
            pdfDocument.Add(table);

            //历史数据           
            GetReportPdf_HisDB_RM(SlabID,ref  pdfDocument);
           
        }

        private void GetReportPdf_HisDB_RM(string SlabID, ref  iTextSharp.text.Document pdfDocument)
        {

            //绘图的数据
            List<HRM_HisDB_RM> LST = GetHRM_HisDB_RM(SlabID);

            //定义绘图对象
            ClsDuHisPic.StuDrawPicInfo stuDrawPicInfo = new ClsDuHisPic.StuDrawPicInfo();

            //****** 第一图 ******//
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);
            
            //数据
            stuDrawPicInfo.dt = new DataTable();
            stuDrawPicInfo.dt.Columns.Add("DateAndTime");
            stuDrawPicInfo.dt.Columns.Add("Length");
            stuDrawPicInfo.dt.Columns.Add("RollForce");
            stuDrawPicInfo.dt.Columns.Add("DSGap");
            //stuDrawPicInfo.dt.Columns.Add("OSGap");
            
          
            for (int RowIndex = 0; RowIndex < LST.Count; RowIndex++)
            {
                DataRow dr = stuDrawPicInfo.dt.NewRow();

                dr["DateAndTime"] = LST[RowIndex].DateAndTime;
                dr["Length"] = LST[RowIndex].Length;
                dr["RollForce"] = LST[RowIndex].RollForce;
                dr["DSGap"] = LST[RowIndex].DSGap;
                //dr["OSGap"] = LST[RowIndex].OSGap;                
                stuDrawPicInfo.dt.Rows.Add(dr);
            }
           
            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = true;
            //stuDrawPicInfo.YAxisDisplayWhenDrawOnly1YAxis = "";
            stuDrawPicInfo.IsDisplayRelativeTime = true;

            int YAxisNo = 1;
            stuDrawPicInfo.TagDescription[YAxisNo] = "长度";
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.IsBaseZero[YAxisNo] = false;
            stuDrawPicInfo.TagUnit[YAxisNo] = "mm";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "轧制力";
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.IsBaseZero[YAxisNo] = false;
            stuDrawPicInfo.TagUnit[YAxisNo] = "KN";

            YAxisNo ++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "DS辊缝";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = false;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.IsBaseZero[YAxisNo] = false;
            stuDrawPicInfo.TagUnit[YAxisNo] = "mm";

            //YAxisNo++;
            //stuDrawPicInfo.TagDescription[YAxisNo] = "OS辊缝";
            //stuDrawPicInfo.YAxisIsLeft[YAxisNo] = false;
            //stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            //stuDrawPicInfo.IsBaseZero[YAxisNo] = false;
            //stuDrawPicInfo.TagUnit[YAxisNo] = "mm";
            

            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(SlabID + "号铸坯 热轧粗轧过程数据", stuDrawPicInfo, ref pdfDocument);

            //****** 第2图 ******//
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //数据
            stuDrawPicInfo.dt = new DataTable();
            stuDrawPicInfo.dt.Columns.Add("DateAndTime");
            stuDrawPicInfo.dt.Columns.Add("Length");
            stuDrawPicInfo.dt.Columns.Add("SpeedTop");
            //stuDrawPicInfo.dt.Columns.Add("SpeedBot");
            stuDrawPicInfo.dt.Columns.Add("TorqueTop");
            //stuDrawPicInfo.dt.Columns.Add("TorqueBot");


            for (int RowIndex = 0; RowIndex < LST.Count; RowIndex++)
            {
                DataRow dr = stuDrawPicInfo.dt.NewRow();

                dr["DateAndTime"] = LST[RowIndex].DateAndTime;
                dr["Length"] = LST[RowIndex].Length;
                dr["SpeedTop"] = LST[RowIndex].SpeedTop;
                //dr["SpeedBot"] = LST[RowIndex].SpeedBot;
                dr["TorqueTop"] = LST[RowIndex].TorqueTop;
                //dr["TorqueBot"] = LST[RowIndex].TorqueBot;
                
                stuDrawPicInfo.dt.Rows.Add(dr);
            }

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = true;
            //stuDrawPicInfo.YAxisDisplayWhenDrawOnly1YAxis = "";
            stuDrawPicInfo.IsDisplayRelativeTime = true;

            YAxisNo = 1;
            stuDrawPicInfo.TagDescription[YAxisNo] = "长度";
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.IsBaseZero[YAxisNo] = false;
            stuDrawPicInfo.TagUnit[YAxisNo] = "mm";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "上辊速度";
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.IsBaseZero[YAxisNo] = false;
            stuDrawPicInfo.TagUnit[YAxisNo] = "m/min";

            //YAxisNo++;
            //stuDrawPicInfo.TagDescription[YAxisNo] = "下辊速度";
            //stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            //stuDrawPicInfo.IsBaseZero[YAxisNo] = false;
            //stuDrawPicInfo.TagUnit[YAxisNo] = "m/min";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "上辊扭矩";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = false;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.IsBaseZero[YAxisNo] = false;
            stuDrawPicInfo.TagUnit[YAxisNo] = "KN.m";

            //YAxisNo++;
            //stuDrawPicInfo.TagDescription[YAxisNo] = "下辊扭矩";
            //stuDrawPicInfo.YAxisIsLeft[YAxisNo] = false;
            //stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            //stuDrawPicInfo.IsBaseZero[YAxisNo] = false;
            //stuDrawPicInfo.TagUnit[YAxisNo] = "KN.m";

            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(SlabID + "号铸坯 热轧粗轧过程数据2", stuDrawPicInfo, ref pdfDocument);
                    
        }

        //精轧pdf报告
        public void GetReportPdf_FR(string SlabID, ref iTextSharp.text.Document pdfDocument)
        {
            //新开一个页面
            pdfDocument.newPage();

            //标题
            if (null == FontCaption) DefinePdfFont();
            iTextSharp.text.Paragraph para = new iTextSharp.text.Paragraph(SlabID + "铸坯 精轧数据追溯", FontCaption);
            iTextSharp.text.Chapter chapter = new iTextSharp.text.Chapter(para, 23);
            pdfDocument.Add(chapter);

            //获取基本信息
            List< HRM_BaseInfo_FR> LST = GetHRM_BaseInfo_FR(SlabID);
            if (LST.Count <= 0)
            {
                FontKai.Size = 10.5F;
                para = new iTextSharp.text.Paragraph("没有查询到数据", FontKai);
                para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                pdfDocument.Add(para);
                return;
            }

            FontHei.Size = 14;
            para = new iTextSharp.text.Paragraph("基本信息", FontHei);
            para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfDocument.Add(para);

            //以表形式来写入必须的信息
            iTextSharp.text.Table table = new iTextSharp.text.Table(6);
            table.WidthPercentage = 100; // 百分比
            table.Padding = 5;
            int[] headerwidths = { 10, 15, 10, 15, 10, 15 }; // 百分比
            table.setWidths(headerwidths);
            table.AutoFillEmptyCells = true;
            //设置边框颜色 
            table.BorderColor = new iTextSharp.text.Color(0, 0, 0);
            table.DefaultCellBorderColor = new iTextSharp.text.Color(0, 0, 255);
            iTextSharp.text.Color Color_DescCell = new iTextSharp.text.Color(System.Drawing.Color.LightGray);
            table.DefaultCellBackgroundColor = new iTextSharp.text.Color(System.Drawing.Color.White);
            table.DefaultVerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            table.DefaultHorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            FontKai.Size = 9; FontSong.Size = 9;
            iTextSharp.text.Cell cell;

            //这里开始写基本信息
            cell = new iTextSharp.text.Cell(new Paragraph("板坯号", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(LST[0].Mat_NO, FontSong)); table.addCell(cell); ;
            cell = new iTextSharp.text.Cell(new Paragraph("钢卷号", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(LST[0].CoilID, FontSong)); table.addCell(cell); ;
            cell = new iTextSharp.text.Cell(new Paragraph("", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("", FontKai)); table.addCell(cell);


            //把表写入文档
            pdfDocument.Add(table);

            //历史数据           
            GetReportPdf_HisDB_FR(SlabID, "1", LST[0].EntryTime_FR1, LST[0].ExitTime_FR1, ref pdfDocument);
            GetReportPdf_HisDB_FR(SlabID, "2", LST[0].EntryTime_FR2, LST[0].ExitTime_FR2, ref pdfDocument);
            GetReportPdf_HisDB_FR(SlabID, "3", LST[0].EntryTime_FR3, LST[0].ExitTime_FR3, ref pdfDocument);
            GetReportPdf_HisDB_FR(SlabID, "4", LST[0].EntryTime_FR4, LST[0].ExitTime_FR4, ref pdfDocument);
            GetReportPdf_HisDB_FR(SlabID, "5", LST[0].EntryTime_FR5, LST[0].ExitTime_FR5, ref pdfDocument);
            GetReportPdf_HisDB_FR(SlabID, "6", LST[0].EntryTime_FR6, LST[0].ExitTime_FR6, ref pdfDocument);
            GetReportPdf_HisDB_FR(SlabID, "7", LST[0].EntryTime_FR7, LST[0].ExitTime_FR7, ref pdfDocument);
             
        }
        private void GetReportPdf_HisDB_FR(string SlabID, string FR_ID, string StartTime,string EndTime,ref  iTextSharp.text.Document pdfDocument)
        {   
            //绘图的数据
            List<HisDB_FR> LST = GetHRM_HisDB_FR(FR_ID,StartTime,EndTime);

            //定义绘图对象
            ClsDuHisPic.StuDrawPicInfo stuDrawPicInfo = new ClsDuHisPic.StuDrawPicInfo();

            //****** 第1图/共4图 ******//
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //数据
            stuDrawPicInfo.dt = new DataTable();
            stuDrawPicInfo.dt.Columns.Add("DateAndTime");
            stuDrawPicInfo.dt.Columns.Add("BendForce");
            stuDrawPicInfo.dt.Columns.Add("RollForce");
            stuDrawPicInfo.dt.Columns.Add("DSGap");
            stuDrawPicInfo.dt.Columns.Add("OSGap");


            for (int RowIndex = 0; RowIndex < LST.Count; RowIndex++)
            {
                DataRow dr = stuDrawPicInfo.dt.NewRow();

                dr["DateAndTime"] = LST[RowIndex].DateAndTime;
                dr["BendForce"] = LST[RowIndex].BendForce;
                dr["RollForce"] = LST[RowIndex].RollForce;
                dr["DSGap"] = LST[RowIndex].DSGap;
                dr["OSGap"] = LST[RowIndex].OSGap;

                stuDrawPicInfo.dt.Rows.Add(dr);
            }

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = true;
            //stuDrawPicInfo.YAxisDisplayWhenDrawOnly1YAxis = "";
            stuDrawPicInfo.IsDisplayRelativeTime = true;

            int YAxisNo = 1;
            stuDrawPicInfo.TagDescription[YAxisNo] = "弯曲力";
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.IsBaseZero[YAxisNo] = false;
            stuDrawPicInfo.TagUnit[YAxisNo] = "KN";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "轧制力";
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.IsBaseZero[YAxisNo] = false;
            stuDrawPicInfo.TagUnit[YAxisNo] = "KN";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "DS辊缝";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = false;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.IsBaseZero[YAxisNo] = false;
            stuDrawPicInfo.TagUnit[YAxisNo] = "mm";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "OS辊缝";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = false;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.IsBaseZero[YAxisNo] = false;
            stuDrawPicInfo.TagUnit[YAxisNo] = "mm";
            
            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(SlabID + "号铸坯 热轧厂 "+ FR_ID + "#精轧机过程数据1", stuDrawPicInfo, ref pdfDocument);
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //****** 第2图/共4图 ******//
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //数据
            stuDrawPicInfo.dt = new DataTable();
            stuDrawPicInfo.dt.Columns.Add("DateAndTime");
            stuDrawPicInfo.dt.Columns.Add("ShiftBot");
            stuDrawPicInfo.dt.Columns.Add("ShiftTop");
            stuDrawPicInfo.dt.Columns.Add("WaterFlow");
            stuDrawPicInfo.dt.Columns.Add("WRWaterFlow");

            for (int RowIndex = 0; RowIndex < LST.Count; RowIndex++)
            {
                DataRow dr = stuDrawPicInfo.dt.NewRow();

                dr["DateAndTime"] = LST[RowIndex].DateAndTime;
                dr["ShiftBot"] = LST[RowIndex].ShiftBot;
                dr["ShiftTop"] = LST[RowIndex].ShiftTop;
                dr["WaterFlow"] = LST[RowIndex].WaterFlow;
                dr["WRWaterFlow"] = LST[RowIndex].WRWaterFlow;

                stuDrawPicInfo.dt.Rows.Add(dr);
            }

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = true;
            //stuDrawPicInfo.YAxisDisplayWhenDrawOnly1YAxis = "";
            stuDrawPicInfo.IsDisplayRelativeTime = true;

            YAxisNo = 1;
            stuDrawPicInfo.TagDescription[YAxisNo] = "ShiftBot";
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.IsBaseZero[YAxisNo] = false;
            stuDrawPicInfo.TagUnit[YAxisNo] = "mm";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "ShiftTop";
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.IsBaseZero[YAxisNo] = false;
            stuDrawPicInfo.TagUnit[YAxisNo] = "mm";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "WaterFlow";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = false;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.IsBaseZero[YAxisNo] = false;
            stuDrawPicInfo.TagUnit[YAxisNo] = "m3/h";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "WRWaterFlow";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = false;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.IsBaseZero[YAxisNo] = false;
            stuDrawPicInfo.TagUnit[YAxisNo] = "m3/h";

            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(SlabID + "号铸坯 热轧厂 " + FR_ID + "#精轧机过程数据2", stuDrawPicInfo, ref pdfDocument);
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////


            //****** 第3图/共4图 ******//
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //数据
            stuDrawPicInfo.dt = new DataTable();
            stuDrawPicInfo.dt.Columns.Add("DateAndTime");
            stuDrawPicInfo.dt.Columns.Add("LENGTH");
            stuDrawPicInfo.dt.Columns.Add("OilFilm");
            stuDrawPicInfo.dt.Columns.Add("BS");
            stuDrawPicInfo.dt.Columns.Add("FS");
                       


            for (int RowIndex = 0; RowIndex < LST.Count; RowIndex++)
            {
                DataRow dr = stuDrawPicInfo.dt.NewRow();

                dr["DateAndTime"] = LST[RowIndex].DateAndTime;
                dr["LENGTH"] = LST[RowIndex].LENGTH;
                dr["OilFilm"] = LST[RowIndex].OilFilm;
                dr["BS"] = LST[RowIndex].BS;
                dr["FS"] = LST[RowIndex].FS;                

                stuDrawPicInfo.dt.Rows.Add(dr);
            }

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = true;
            //stuDrawPicInfo.YAxisDisplayWhenDrawOnly1YAxis = "";
            stuDrawPicInfo.IsDisplayRelativeTime = true;

            YAxisNo = 1;
            stuDrawPicInfo.TagDescription[YAxisNo] = "长度";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = false;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.IsBaseZero[YAxisNo] = false;
            stuDrawPicInfo.TagUnit[YAxisNo] = "mm";

            YAxisNo ++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "BS";
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.IsBaseZero[YAxisNo] = false;
            stuDrawPicInfo.TagUnit[YAxisNo] = "";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "FS";
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.IsBaseZero[YAxisNo] = false;
            stuDrawPicInfo.TagUnit[YAxisNo] = "";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "OilFilm";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = false;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.IsBaseZero[YAxisNo] = false;
            stuDrawPicInfo.TagUnit[YAxisNo] = "um";            

            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(SlabID + "号铸坯 热轧厂 " + FR_ID + "#精轧机过程数据3", stuDrawPicInfo, ref pdfDocument);
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //****** 第4图/共4图 ******//
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //数据
            stuDrawPicInfo.dt = new DataTable();
            stuDrawPicInfo.dt.Columns.Add("DateAndTime");
            stuDrawPicInfo.dt.Columns.Add("LENGTH");
            stuDrawPicInfo.dt.Columns.Add("Speed");
            stuDrawPicInfo.dt.Columns.Add("Tension");
            //stuDrawPicInfo.dt.Columns.Add("");

            for (int RowIndex = 0; RowIndex < LST.Count; RowIndex++)
            {
                DataRow dr = stuDrawPicInfo.dt.NewRow();

                dr["DateAndTime"] = LST[RowIndex].DateAndTime;
                dr["LENGTH"] = LST[RowIndex].LENGTH;
                dr["Speed"] = LST[RowIndex].Speed;
                dr["Tension"] = LST[RowIndex].Tension;
                //dr[""] = LST[RowIndex].;

                stuDrawPicInfo.dt.Rows.Add(dr);
            }

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = true;
            //stuDrawPicInfo.YAxisDisplayWhenDrawOnly1YAxis = "";
            stuDrawPicInfo.IsDisplayRelativeTime = true;

            YAxisNo = 1;
            stuDrawPicInfo.TagDescription[YAxisNo] = "LENGTH";
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.IsBaseZero[YAxisNo] = false;
            stuDrawPicInfo.TagUnit[YAxisNo] = "mm";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "Speed";
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.IsBaseZero[YAxisNo] = false;
            stuDrawPicInfo.TagUnit[YAxisNo] = "mm";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "Tension";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = false;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.IsBaseZero[YAxisNo] = false;
            stuDrawPicInfo.TagUnit[YAxisNo] = "m3/h";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = false;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.IsBaseZero[YAxisNo] = false;
            stuDrawPicInfo.TagUnit[YAxisNo] = "m3/h";

            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(SlabID + "号铸坯 热轧厂 " + FR_ID + "#精轧机过程数据4", stuDrawPicInfo, ref pdfDocument);
        }

        


        private void GetReportPdf_HisDB_UFC(string SlabID, ref  iTextSharp.text.Document pdfDocument)
        {
            //绘图的数据
            List<HisDB_UFC> LST = GetHRM_HisDB_UFC(SlabID);

            //定义绘图对象
            ClsDuHisPic.StuDrawPicInfo stuDrawPicInfo = new ClsDuHisPic.StuDrawPicInfo();

            //****** 第1图/共6图 ******//
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //数据
            stuDrawPicInfo.dt = new DataTable();
            stuDrawPicInfo.dt.Columns.Add("DateAndTime");
            stuDrawPicInfo.dt.Columns.Add("UFCTselect");
            stuDrawPicInfo.dt.Columns.Add("Water_temperature");
            stuDrawPicInfo.dt.Columns.Add("Hpress_All_Water_press_1");           

            for (int RowIndex = 0; RowIndex < LST.Count; RowIndex++)
            {
                DataRow dr = stuDrawPicInfo.dt.NewRow();

                dr["DateAndTime"] = LST[RowIndex].DateAndTime;
                dr["UFCTselect"] = LST[RowIndex].UFCTselect;
                dr["Water_temperature"] = LST[RowIndex].Water_temperature;
                dr["Hpress_All_Water_press_1"] = LST[RowIndex].Hpress_All_Water_press_1;
               
                stuDrawPicInfo.dt.Rows.Add(dr);
            }

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = true;
            //stuDrawPicInfo.YAxisDisplayWhenDrawOnly1YAxis = "";
            stuDrawPicInfo.IsDisplayRelativeTime = true;

            int YAxisNo = 1;
            stuDrawPicInfo.TagDescription[YAxisNo] = "出口水温";
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.IsBaseZero[YAxisNo] = false;
            stuDrawPicInfo.TagUnit[YAxisNo] = "KN";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "入口水温";
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.IsBaseZero[YAxisNo] = false;
            stuDrawPicInfo.TagUnit[YAxisNo] = "KN";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "水压";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = false;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.IsBaseZero[YAxisNo] = false;
            stuDrawPicInfo.TagUnit[YAxisNo] = "Mpa";
 

            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(SlabID + "号铸坯 热轧厂超快冷 过程数据1", stuDrawPicInfo, ref pdfDocument);
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //****** 第2图/共6图 ******//
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //数据
            stuDrawPicInfo.dt = new DataTable();
            stuDrawPicInfo.dt.Columns.Add("DateAndTime");
            stuDrawPicInfo.dt.Columns.Add("Hpress1_Up_flux_act");
            stuDrawPicInfo.dt.Columns.Add("Hpress1_Dn_flux_act");
            stuDrawPicInfo.dt.Columns.Add("Hpress2_Up_flux_act");
            stuDrawPicInfo.dt.Columns.Add("Hpress2_Dn_flux_act");
            stuDrawPicInfo.dt.Columns.Add("Hpress3_Up_flux_act");
            stuDrawPicInfo.dt.Columns.Add("Hpress3_Dn_flux_act");
            stuDrawPicInfo.dt.Columns.Add("Hpress4_Up_flux_act");
            stuDrawPicInfo.dt.Columns.Add("Hpress4_Dn_flux_act");

            for (int RowIndex = 0; RowIndex < LST.Count; RowIndex++)
            {
                DataRow dr = stuDrawPicInfo.dt.NewRow();

                dr["DateAndTime"] = LST[RowIndex].DateAndTime;
                dr["Hpress1_Up_flux_act"] = LST[RowIndex].Hpress1_Up_flux_act;
                dr["Hpress1_Dn_flux_act"] = LST[RowIndex].Hpress1_Dn_flux_act;
                dr["Hpress2_Up_flux_act"] = LST[RowIndex].Hpress2_Up_flux_act;
                dr["Hpress2_Dn_flux_act"] = LST[RowIndex].Hpress2_Dn_flux_act;
                dr["Hpress3_Up_flux_act"] = LST[RowIndex].Hpress3_Up_flux_act;
                dr["Hpress3_Dn_flux_act"] = LST[RowIndex].Hpress3_Dn_flux_act;
                dr["Hpress4_Up_flux_act"] = LST[RowIndex].Hpress4_Up_flux_act;
                dr["Hpress4_Dn_flux_act"] = LST[RowIndex].Hpress4_Dn_flux_act;

                stuDrawPicInfo.dt.Rows.Add(dr);
            }

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = false;
            stuDrawPicInfo.YAxisDisplayWhenDrawOnly1YAxis = "流量";
            stuDrawPicInfo.IsDisplayRelativeTime = true;

            YAxisNo = 1;
            stuDrawPicInfo.TagDescription[YAxisNo] = "1#上";            
            stuDrawPicInfo.IsBaseZero[YAxisNo] = true ;
            stuDrawPicInfo.TagUnit[YAxisNo] = "m3/h";

            YAxisNo++;stuDrawPicInfo.TagDescription[YAxisNo] = "1#下";
            YAxisNo++; stuDrawPicInfo.TagDescription[YAxisNo] = "2#上";
            YAxisNo++; stuDrawPicInfo.TagDescription[YAxisNo] = "2#下";
            YAxisNo++; stuDrawPicInfo.TagDescription[YAxisNo] = "3#上";
            YAxisNo++; stuDrawPicInfo.TagDescription[YAxisNo] = "3#下";
            YAxisNo++; stuDrawPicInfo.TagDescription[YAxisNo] = "4#上";
            YAxisNo++; stuDrawPicInfo.TagDescription[YAxisNo] = "4#下";

            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(SlabID + "号铸坯 热轧厂超快冷 过程数据3", stuDrawPicInfo, ref pdfDocument);
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //****** 第3图/共6图 ******//
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //数据
            stuDrawPicInfo.dt = new DataTable();
            stuDrawPicInfo.dt.Columns.Add("DateAndTime");
            stuDrawPicInfo.dt.Columns.Add("Hpress5_Up_flux_act");
            stuDrawPicInfo.dt.Columns.Add("Hpress5_Dn_flux_act");
            stuDrawPicInfo.dt.Columns.Add("Hpress6_Up_flux_act");
            stuDrawPicInfo.dt.Columns.Add("Hpress6_Dn_flux_act");
            stuDrawPicInfo.dt.Columns.Add("Hpress7_Up_flux_act");
            stuDrawPicInfo.dt.Columns.Add("Hpress7_Dn_flux_act");
            stuDrawPicInfo.dt.Columns.Add("Hpress8_Up_flux_act");
            stuDrawPicInfo.dt.Columns.Add("Hpress8_Dn_flux_act");

            for (int RowIndex = 0; RowIndex < LST.Count; RowIndex++)
            {
                DataRow dr = stuDrawPicInfo.dt.NewRow();

                dr["DateAndTime"] = LST[RowIndex].DateAndTime;
                dr["Hpress5_Up_flux_act"] = LST[RowIndex].Hpress5_Up_flux_act;
                dr["Hpress5_Dn_flux_act"] = LST[RowIndex].Hpress5_Dn_flux_act;
                dr["Hpress6_Up_flux_act"] = LST[RowIndex].Hpres6_Up_flux_act;
                dr["Hpress6_Dn_flux_act"] = LST[RowIndex].Hpress6_Dn_flux_act;
                dr["Hpress7_Up_flux_act"] = LST[RowIndex].Hpres7_Up_flux_act;
                dr["Hpress7_Dn_flux_act"] = LST[RowIndex].Hpress7_Dn_flux_act;
                dr["Hpress8_Up_flux_act"] = LST[RowIndex].Hpres8_Up_flux_act;
                dr["Hpress8_Dn_flux_act"] = LST[RowIndex].Hpres8_Dn_flux_act;

                stuDrawPicInfo.dt.Rows.Add(dr);
            }

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = false;
            stuDrawPicInfo.YAxisDisplayWhenDrawOnly1YAxis = "流量";
            stuDrawPicInfo.IsDisplayRelativeTime = true;

            YAxisNo = 1;
            stuDrawPicInfo.TagDescription[YAxisNo] = "5#上";
            stuDrawPicInfo.IsBaseZero[YAxisNo] = true;
            stuDrawPicInfo.TagUnit[YAxisNo] = "m3/h";

            YAxisNo++; stuDrawPicInfo.TagDescription[YAxisNo] = "5#下";
            YAxisNo++; stuDrawPicInfo.TagDescription[YAxisNo] = "6#上";
            YAxisNo++; stuDrawPicInfo.TagDescription[YAxisNo] = "6#下";
            YAxisNo++; stuDrawPicInfo.TagDescription[YAxisNo] = "7#上";
            YAxisNo++; stuDrawPicInfo.TagDescription[YAxisNo] = "7#下";
            YAxisNo++; stuDrawPicInfo.TagDescription[YAxisNo] = "8#上";
            YAxisNo++; stuDrawPicInfo.TagDescription[YAxisNo] = "8#下";

            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(SlabID + "号铸坯 热轧厂超快冷 过程数据3", stuDrawPicInfo, ref pdfDocument);

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //****** 第4图/共6图 ******//
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //数据
            stuDrawPicInfo.dt = new DataTable();
            stuDrawPicInfo.dt.Columns.Add("DateAndTime");
            stuDrawPicInfo.dt.Columns.Add("Hpress9_Up_flux_act");
            stuDrawPicInfo.dt.Columns.Add("Hpress9_Dn_flux_act");
            stuDrawPicInfo.dt.Columns.Add("Hpress10_Up_flux_act");
            stuDrawPicInfo.dt.Columns.Add("Hpress10_Dn_flux_act");
            stuDrawPicInfo.dt.Columns.Add("Hpress11_Up_flux_act");
            stuDrawPicInfo.dt.Columns.Add("Hpress11_Dn_flux_act");
            stuDrawPicInfo.dt.Columns.Add("Hpress12_Up_flux_act");
            stuDrawPicInfo.dt.Columns.Add("Hpress12_Dn_flux_act");

            for (int RowIndex = 0; RowIndex < LST.Count; RowIndex++)
            {
                DataRow dr = stuDrawPicInfo.dt.NewRow();

                dr["DateAndTime"] = LST[RowIndex].DateAndTime;
                dr["Hpress9_Up_flux_act"] = LST[RowIndex].Hpres9_Up_flux_act;
                dr["Hpress9_Dn_flux_act"] = LST[RowIndex].Hpres9_Dn_flux_act;
                dr["Hpress10_Up_flux_act"] = LST[RowIndex].Hpres10_Up_flux_act;
                dr["Hpress10_Dn_flux_act"] = LST[RowIndex].Hpres10_Dn_flux_act;
                dr["Hpress11_Up_flux_act"] = LST[RowIndex].Hpres11_Up_flux_act;
                dr["Hpress11_Dn_flux_act"] = LST[RowIndex].Hpres11_Dn_flux_act;
                dr["Hpress12_Up_flux_act"] = LST[RowIndex].Hpres12_Up_flux_act;
                dr["Hpress12_Dn_flux_act"] = LST[RowIndex].Hpres12_Dn_flux_act;

                stuDrawPicInfo.dt.Rows.Add(dr);
            }

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = false;
            stuDrawPicInfo.YAxisDisplayWhenDrawOnly1YAxis = "流量";
            stuDrawPicInfo.IsDisplayRelativeTime = true;

            YAxisNo = 1;
            stuDrawPicInfo.TagDescription[YAxisNo] = "9#上";
            stuDrawPicInfo.IsBaseZero[YAxisNo] = true;
            stuDrawPicInfo.TagUnit[YAxisNo] = "m3/h";

            YAxisNo++; stuDrawPicInfo.TagDescription[YAxisNo] = "9#下";
            YAxisNo++; stuDrawPicInfo.TagDescription[YAxisNo] = "10#上";
            YAxisNo++; stuDrawPicInfo.TagDescription[YAxisNo] = "10#下";
            YAxisNo++; stuDrawPicInfo.TagDescription[YAxisNo] = "11#上";
            YAxisNo++; stuDrawPicInfo.TagDescription[YAxisNo] = "11#下";
            YAxisNo++; stuDrawPicInfo.TagDescription[YAxisNo] = "12#上";
            YAxisNo++; stuDrawPicInfo.TagDescription[YAxisNo] = "12#下";

            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(SlabID + "号铸坯 热轧厂超快冷 过程数据4", stuDrawPicInfo, ref pdfDocument);

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //****** 第5图/共6图 ******//
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //数据
            stuDrawPicInfo.dt = new DataTable();
            stuDrawPicInfo.dt.Columns.Add("DateAndTime");
            stuDrawPicInfo.dt.Columns.Add("Hpress13_Up_flux_act");
            stuDrawPicInfo.dt.Columns.Add("Hpress13_Dn_flux_act");
            stuDrawPicInfo.dt.Columns.Add("Hpress14_Up_flux_act");
            stuDrawPicInfo.dt.Columns.Add("Hpress14_Dn_flux_act");
            stuDrawPicInfo.dt.Columns.Add("Hpress15_Up_flux_act");
            stuDrawPicInfo.dt.Columns.Add("Hpress15_Dn_flux_act");
            stuDrawPicInfo.dt.Columns.Add("Hpress16_Up_flux_act");
            stuDrawPicInfo.dt.Columns.Add("Hpress16_Dn_flux_act");

            for (int RowIndex = 0; RowIndex < LST.Count; RowIndex++)
            {
                DataRow dr = stuDrawPicInfo.dt.NewRow();

                dr["DateAndTime"] = LST[RowIndex].DateAndTime;
                dr["Hpress13_Up_flux_act"] = LST[RowIndex].Hpres13_Up_flux_act;
                dr["Hpress13_Dn_flux_act"] = LST[RowIndex].Hpres13_Dn_flux_act;
                dr["Hpress14_Up_flux_act"] = LST[RowIndex].Hpres14_Up_flux_act;
                dr["Hpress14_Dn_flux_act"] = LST[RowIndex].Hpres14_Dn_flux_act;
                dr["Hpress15_Up_flux_act"] = LST[RowIndex].Hpres15_Up_flux_act;
                dr["Hpress15_Dn_flux_act"] = LST[RowIndex].Hpres15_Dn_flux_act;
                dr["Hpress16_Up_flux_act"] = LST[RowIndex].Hpres16_Up_flux_act;
                dr["Hpress16_Dn_flux_act"] = LST[RowIndex].Hpres16_Dn_flux_act;

                stuDrawPicInfo.dt.Rows.Add(dr);
            }

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = false;
            stuDrawPicInfo.YAxisDisplayWhenDrawOnly1YAxis = "流量";
            stuDrawPicInfo.IsDisplayRelativeTime = true;

            YAxisNo = 1;
            stuDrawPicInfo.TagDescription[YAxisNo] = "13#上";
            stuDrawPicInfo.IsBaseZero[YAxisNo] = true;
            stuDrawPicInfo.TagUnit[YAxisNo] = "m3/h";

            YAxisNo++; stuDrawPicInfo.TagDescription[YAxisNo] = "13#下";
            YAxisNo++; stuDrawPicInfo.TagDescription[YAxisNo] = "14#上";
            YAxisNo++; stuDrawPicInfo.TagDescription[YAxisNo] = "14#下";
            YAxisNo++; stuDrawPicInfo.TagDescription[YAxisNo] = "15#上";
            YAxisNo++; stuDrawPicInfo.TagDescription[YAxisNo] = "15#下";
            YAxisNo++; stuDrawPicInfo.TagDescription[YAxisNo] = "16#上";
            YAxisNo++; stuDrawPicInfo.TagDescription[YAxisNo] = "16#下";

            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(SlabID + "号铸坯 热轧厂超快冷 过程数据5", stuDrawPicInfo, ref pdfDocument);

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //****** 第6图/共6图 ******//
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //数据
            stuDrawPicInfo.dt = new DataTable();
            stuDrawPicInfo.dt.Columns.Add("DateAndTime");
            stuDrawPicInfo.dt.Columns.Add("Hpress17_Up_flux_act");
            stuDrawPicInfo.dt.Columns.Add("Hpress17_Dn_flux_act");
            stuDrawPicInfo.dt.Columns.Add("Hpress18_Up_flux_act");
            stuDrawPicInfo.dt.Columns.Add("Hpress18_Dn_flux_act");
            stuDrawPicInfo.dt.Columns.Add("Hpress19_Up_flux_act");
            stuDrawPicInfo.dt.Columns.Add("Hpress19_Dn_flux_act");
            stuDrawPicInfo.dt.Columns.Add("Hpress20_Up_flux_act");
            stuDrawPicInfo.dt.Columns.Add("Hpress20_Dn_flux_act");

            for (int RowIndex = 0; RowIndex < LST.Count; RowIndex++)
            {
                DataRow dr = stuDrawPicInfo.dt.NewRow();

                dr["DateAndTime"] = LST[RowIndex].DateAndTime;
                dr["Hpress17_Up_flux_act"] = LST[RowIndex].Hpres17_Up_flux_act;
                dr["Hpress17_Dn_flux_act"] = LST[RowIndex].Hpres17_Dn_flux_act;
                dr["Hpress18_Up_flux_act"] = LST[RowIndex].Hpres18_Up_flux_act;
                dr["Hpress18_Dn_flux_act"] = LST[RowIndex].Hpres18_Dn_flux_act;
                dr["Hpress19_Up_flux_act"] = LST[RowIndex].Hpres19_Up_flux_act;
                dr["Hpress19_Dn_flux_act"] = LST[RowIndex].Hpres19_Dn_flux_act;
                dr["Hpress20_Up_flux_act"] = LST[RowIndex].Hpres20_Up_flux_act;
                dr["Hpress20_Dn_flux_act"] = LST[RowIndex].Hpres20_Dn_flux_act;

                stuDrawPicInfo.dt.Rows.Add(dr);
            }

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = false;
            stuDrawPicInfo.YAxisDisplayWhenDrawOnly1YAxis = "流量";
            stuDrawPicInfo.IsDisplayRelativeTime = true;

            YAxisNo = 1;
            stuDrawPicInfo.TagDescription[YAxisNo] = "17#上";
            stuDrawPicInfo.IsBaseZero[YAxisNo] = true;
            stuDrawPicInfo.TagUnit[YAxisNo] = "m3/h";

            YAxisNo++; stuDrawPicInfo.TagDescription[YAxisNo] = "17#下";
            YAxisNo++; stuDrawPicInfo.TagDescription[YAxisNo] = "18#上";
            YAxisNo++; stuDrawPicInfo.TagDescription[YAxisNo] = "18#下";
            YAxisNo++; stuDrawPicInfo.TagDescription[YAxisNo] = "19#上";
            YAxisNo++; stuDrawPicInfo.TagDescription[YAxisNo] = "19#下";
            YAxisNo++; stuDrawPicInfo.TagDescription[YAxisNo] = "20#上";
            YAxisNo++; stuDrawPicInfo.TagDescription[YAxisNo] = "20#下";

            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(SlabID + "号铸坯 热轧厂超快冷 过程数据5", stuDrawPicInfo, ref pdfDocument);
        }

        //卷取
        public void GetReportPdf_CTC(string SlabID, ref iTextSharp.text.Document pdfDocument)
        {

            //新开一个页面
            pdfDocument.newPage();

            //标题
            if (null == FontCaption) DefinePdfFont();
            iTextSharp.text.Paragraph para = new iTextSharp.text.Paragraph(SlabID + "铸坯 卷取数据追溯", FontCaption);
            iTextSharp.text.Chapter chapter = new iTextSharp.text.Chapter(para, 25);
            pdfDocument.Add(chapter);

            //获取基本信息
            List<HRM_BaseInfo_CTC> LST = GetHRM_BaseInfo_CTC(SlabID);
            if (LST.Count <= 0)
            {
                FontKai.Size = 10.5F;
                para = new iTextSharp.text.Paragraph("没有查询到数据", FontKai);
                para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                pdfDocument.Add(para);
                return;
            }

            FontHei.Size = 14;
            para = new iTextSharp.text.Paragraph("基本信息", FontHei);
            para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfDocument.Add(para);

            //以表形式来写入必须的信息
            iTextSharp.text.Table table = new iTextSharp.text.Table(6);
            table.WidthPercentage = 100; // 百分比
            table.Padding = 5;
            int[] headerwidths = { 10, 15, 10, 15, 10, 15 }; // 百分比
            table.setWidths(headerwidths);
            table.AutoFillEmptyCells = true;
            //设置边框颜色 
            table.BorderColor = new iTextSharp.text.Color(0, 0, 0);
            table.DefaultCellBorderColor = new iTextSharp.text.Color(0, 0, 255);
            iTextSharp.text.Color Color_DescCell = new iTextSharp.text.Color(System.Drawing.Color.LightGray);
            table.DefaultCellBackgroundColor = new iTextSharp.text.Color(System.Drawing.Color.White);
            table.DefaultVerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
            table.DefaultHorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
            FontKai.Size = 9; FontSong.Size = 9;
            iTextSharp.text.Cell cell;

            //这里开始写基本信息
            cell = new iTextSharp.text.Cell(new Paragraph("板坯号", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(LST[0].SLAB_ID, FontSong)); table.addCell(cell); ;
            cell = new iTextSharp.text.Cell(new Paragraph("钢卷号", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(LST[0].COIL_ID, FontSong)); table.addCell(cell); ;
            cell = new iTextSharp.text.Cell(new Paragraph("", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("", FontKai)); table.addCell(cell);


            //把表写入文档
            pdfDocument.Add(table);

            //历史数据           
            GetReportPdf_HisDB_CTC(SlabID, ref pdfDocument);

        }

        private  void GetReportPdf_HisDB_CTC(string SlabID, ref  iTextSharp.text.Document pdfDocument)
        {
            //绘图的数据
            List<HisDB_CTC> LST = GetHRM_HisDB_CTC(SlabID);

            //定义绘图对象
            ClsDuHisPic.StuDrawPicInfo stuDrawPicInfo = new ClsDuHisPic.StuDrawPicInfo();

            //****** 第1图/共3图 ******//
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //数据
            stuDrawPicInfo.dt = new DataTable();
            stuDrawPicInfo.dt.Columns.Add("DateAndTime");
            stuDrawPicInfo.dt.Columns.Add("CT");
            stuDrawPicInfo.dt.Columns.Add("CT_LENGTH");
            stuDrawPicInfo.dt.Columns.Add("CT_SPEED");           

            for (int RowIndex = 0; RowIndex < LST.Count; RowIndex++)
            {
                DataRow dr = stuDrawPicInfo.dt.NewRow();

                dr["DateAndTime"] = LST[RowIndex].DateAndTime;
                dr["CT"] = LST[RowIndex].CT;
                dr["CT_LENGTH"] = LST[RowIndex].CT_LENGTH;
                dr["CT_SPEED"] = LST[RowIndex].CT_SPEED;
              
                stuDrawPicInfo.dt.Rows.Add(dr);
            }

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = true;
            //stuDrawPicInfo.YAxisDisplayWhenDrawOnly1YAxis = "";
            stuDrawPicInfo.IsDisplayRelativeTime = true;

            int YAxisNo = 1;
            stuDrawPicInfo.TagDescription[YAxisNo] = "CT";
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.IsBaseZero[YAxisNo] = false;
            stuDrawPicInfo.TagUnit[YAxisNo] = "";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "CT_LENGTH";
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.IsBaseZero[YAxisNo] = false;
            stuDrawPicInfo.TagUnit[YAxisNo] = "mm";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "CT_SPEED";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = false;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.IsBaseZero[YAxisNo] = false;
            stuDrawPicInfo.TagUnit[YAxisNo] = "mm/min";

            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(SlabID + "号铸坯 热轧厂 卷取 过程数据1", stuDrawPicInfo, ref pdfDocument);
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //****** 第2图/共3图 ******//
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //数据
            stuDrawPicInfo.dt = new DataTable();
            stuDrawPicInfo.dt.Columns.Add("DateAndTime");
            stuDrawPicInfo.dt.Columns.Add("MT");
            stuDrawPicInfo.dt.Columns.Add("MT_LENGTH");
            stuDrawPicInfo.dt.Columns.Add("MT_SPEED");

            for (int RowIndex = 0; RowIndex < LST.Count; RowIndex++)
            {
                DataRow dr = stuDrawPicInfo.dt.NewRow();

                dr["DateAndTime"] = LST[RowIndex].DateAndTime;
                dr["MT"] = LST[RowIndex].MT;
                dr["MT_LENGTH"] = LST[RowIndex].MT_LENGTH;
                dr["MT_SPEED"] = LST[RowIndex].MT_SPEED;

                stuDrawPicInfo.dt.Rows.Add(dr);
            }

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = true;
            //stuDrawPicInfo.YAxisDisplayWhenDrawOnly1YAxis = "";
            stuDrawPicInfo.IsDisplayRelativeTime = true;

            YAxisNo = 1;
            stuDrawPicInfo.TagDescription[YAxisNo] = "MT";
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.IsBaseZero[YAxisNo] = false;
            stuDrawPicInfo.TagUnit[YAxisNo] = "";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "MT_LENGTH";
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.IsBaseZero[YAxisNo] = false;
            stuDrawPicInfo.TagUnit[YAxisNo] = "mm";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "MT_SPEED";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = false;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.IsBaseZero[YAxisNo] = false;
            stuDrawPicInfo.TagUnit[YAxisNo] = "mm/min";

            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(SlabID + "号铸坯 热轧厂 卷取 过程数据2", stuDrawPicInfo, ref pdfDocument);
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //****** 第3图/共3图 ******//
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //数据
            stuDrawPicInfo.dt = new DataTable();
            stuDrawPicInfo.dt.Columns.Add("DateAndTime");
            stuDrawPicInfo.dt.Columns.Add("FDT");
            stuDrawPicInfo.dt.Columns.Add("FET");

            for (int RowIndex = 0; RowIndex < LST.Count; RowIndex++)
            {
                DataRow dr = stuDrawPicInfo.dt.NewRow();

                dr["DateAndTime"] = LST[RowIndex].DateAndTime;
                dr["FDT"] = LST[RowIndex].FDT;
                dr["FET"] = LST[RowIndex].FET;
               
                stuDrawPicInfo.dt.Rows.Add(dr);
            }

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = true;
            //stuDrawPicInfo.YAxisDisplayWhenDrawOnly1YAxis = "";
            stuDrawPicInfo.IsDisplayRelativeTime = true;

            YAxisNo = 1;
            stuDrawPicInfo.TagDescription[YAxisNo] = "FDT";
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.IsBaseZero[YAxisNo] = false;
            stuDrawPicInfo.TagUnit[YAxisNo] = "";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "FET";
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.IsBaseZero[YAxisNo] = false;
            stuDrawPicInfo.TagUnit[YAxisNo] = "mm";


            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(SlabID + "号铸坯 热轧厂 卷取 过程数据3", stuDrawPicInfo, ref pdfDocument);
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }

        private void DefinePdfFont()
        {
            //定义字体
            iTextSharp.text.pdf.BaseFont bfHei = iTextSharp.text.pdf.BaseFont.createFont(@"C:\Windows\Fonts\SIMHEI.TTF", iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);
            FontHei = new iTextSharp.text.Font(bfHei, 32, 1);
            iTextSharp.text.pdf.BaseFont bfKai = iTextSharp.text.pdf.BaseFont.createFont(@"C:\Windows\Fonts\simkai.ttf", iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);
            FontKai = new iTextSharp.text.Font(bfKai, 32, 1);
            iTextSharp.text.pdf.BaseFont bfSun = iTextSharp.text.pdf.BaseFont.createFont(@"C:\Windows\Fonts\SIMSUN.TTC,1", iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);
            FontSong = new iTextSharp.text.Font(bfSun, 32, 1);
            FontSong10 = new iTextSharp.text.Font(bfSun, 10, 1);
            //图的标题
            FontCaption = new iTextSharp.text.Font(bfKai, 12, 1);
        }


        public List<HRM_HisDB_RM> GetHRM_HisDB_RM(string SlabID)
        {
            //获取炉次对应的板坯号信息
            List<HRM_HisDB_RM> LST = new List<HRM_HisDB_RM>();
            HRM_HisDB_RM lst;
            string str = "";

            //获取起止时间
            string strSQL = "SELECT * FROM HRM_MATTRACK_TIME"
                          + " WHERE mat_no='" + SlabID + "'"
                          + " AND Device_no='LY2250_RM'";
            DataTable dtA = sqt.ReadDatatable_OraDB(strSQL);
            if (dtA.Rows.Count <=0 )  return LST;


            DateTime StartTime = DateTime.Now ;
            DateTime EndTime = DateTime.Now;
            for (int RowIndex = 0; RowIndex < dtA.Rows.Count; RowIndex++)
            {
                StartTime = Convert.ToDateTime(dtA.Rows[0]["start_time"]);
                EndTime = Convert.ToDateTime(dtA.Rows[0]["stop_time"]);
            } 

            //数据标签
            string[] tags = new string[13];         
            tags[0] = "LYQ2250.R1.DSGap";//	Analog
            tags[1] = "LYQ2250.R1.Length";//Analog
            tags[2] = "LYQ2250.R1.OSGap";//Analog
            tags[3] = "LYQ2250.R1.Pass";//Analog
            tags[4] = "LYQ2250.R1.RollForce";//	Analog
            tags[5] = "LYQ2250.R1.SpeedBot";//	Analog
            tags[6] = "LYQ2250.R1.SpeedTop";//	Analog
            tags[7] = "LYQ2250.R1.TorqueBot";//	Analog
            tags[8] = "LYQ2250.R1.TorqueTop";//扭矩
            tags[9] = "LYQ2250.R1.BarLength";//	Analog
            tags[10] = "LYQ2250.R1.BarThick";//	Analog
            tags[11] = "LYQ2250.R1.BarWidth";//	Analog
            tags[12] = "LYQ2250.R1.BendForce";//	Analog

            //获取历史数据
            System.Data.DataTable dt = sqt.ReadDatatable_HisDB(tags,StartTime, EndTime);
            for (int RowIndex = 0; RowIndex < dt.Rows.Count; RowIndex++)
            {
                lst = new HRM_HisDB_RM();

                lst.DateAndTime =Convert.ToDateTime(dt.Rows[RowIndex][0]);

                str = dt.Rows[RowIndex][1].ToString(); if (str.Length > 0) lst.DSGap=Convert.ToSingle( str);
                str = dt.Rows[RowIndex][2].ToString(); if (str.Length > 0) lst.Length=Convert.ToSingle( str);
                str = dt.Rows[RowIndex][3].ToString(); if (str.Length > 0) lst.OSGap=Convert.ToSingle( str);
                str = dt.Rows[RowIndex][4].ToString(); if (str.Length > 0) lst.Pass=Convert.ToSingle( str);
                str = dt.Rows[RowIndex][5].ToString(); if (str.Length > 0) lst.RollForce=Convert.ToSingle( str);
                str = dt.Rows[RowIndex][6].ToString(); if (str.Length > 0) lst.SpeedBot=Convert.ToSingle( str);
                str = dt.Rows[RowIndex][7].ToString(); if (str.Length > 0) lst.SpeedTop = Convert.ToSingle(str);                
                str = dt.Rows[RowIndex][8].ToString(); if (str.Length > 0) lst.TorqueBot = Convert.ToSingle(str);
                str = dt.Rows[RowIndex][9].ToString(); if (str.Length > 0) lst.TorqueTop= Convert.ToSingle(str);

                str = dt.Rows[RowIndex][10].ToString(); if (str.Length > 0) lst.BarLength = Convert.ToSingle(str);
                str = dt.Rows[RowIndex][11].ToString(); if (str.Length > 0) lst.BarThick = Convert.ToSingle(str);
                str = dt.Rows[RowIndex][12].ToString(); if (str.Length > 0) lst.BarWidth = Convert.ToSingle(str);
                str = dt.Rows[RowIndex][13].ToString(); if (str.Length > 0) lst.BendForce = Convert.ToSingle(str);

                LST.Add(lst);
            }
            return LST;
        }

        //卷取
        public List<HRM_HisDB_DC> GetHRM_HisDB_DC(string DC_ID,string StartTime,string EndTime)
        {
            List<HRM_HisDB_DC> LST = new List<HRM_HisDB_DC>();
            HRM_HisDB_DC lst;
            string str;
             

            //获取变量标签
            string[] tags = new string[18];
            tags[00] = "LYQ2250.DC" + DC_ID + ".COIL_DIAMETER";//		 
            tags[01] = "LYQ2250.DC" + DC_ID + ".COIL_WIDTH";//	 
            tags[02] = "LYQ2250.DC" + DC_ID + ".COILING_TENSION"; 
            tags[03] = "LYQ2250.DC" + DC_ID + ".LENGTH";//		 
            tags[04] = "LYQ2250.DC" + DC_ID + ".LENGTH_B";//	 
            tags[05] = "LYQ2250.DC" + DC_ID + ".LENGTH_C";// 
            tags[06] = "LYQ2250.DC" + DC_ID + ".MANDREL_SPEED";// 
            tags[07] = "LYQ2250.DC" + DC_ID + ".MANDREL_TORQUE";// 
            tags[08] = "LYQ2250.DC" + DC_ID + ".MD_SPEED_SET";// 	
            tags[09] = "LYQ2250.DC" + DC_ID + ".MD_TENSION";// 
            tags[10] = "LYQ2250.DC" + DC_ID + ".MD_TENSION_SET";// 	
            tags[11] = "LYQ2250.DC" + DC_ID + ".MD_TORQUE";// 
            tags[12] = "LYQ2250.DC" + DC_ID + ".PR_LEAD_RATIO";// 	
            tags[13] = "LYQ2250.DC" + DC_ID + ".STRIP_SPEED";// 
            tags[14] = "LYQ2250.DC" + DC_ID + ".THICK_SET";// 
            tags[15] = "LYQ2250.DC" + DC_ID + ".WRAPER_ROLL1_SPEED";// 
            tags[16] = "LYQ2250.DC" + DC_ID + ".WRAPER_ROLL2_SPEED";// 
            tags[17] = "LYQ2250.DC" + DC_ID + ".WRAPER_ROLL3_SPEED";// 	
            

            //获取历史数据
            System.Data.DataTable dt = sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(StartTime), Convert.ToDateTime(EndTime));

            for (int I = 0; I < dt.Rows.Count; I++)
            {
                lst = new HRM_HisDB_DC();

                lst.DateAndTime = Convert.ToDateTime(dt.Rows[I][00]);

                str = dt.Rows[I][01].ToString(); if (str.Length > 0) lst.COIL_DIAMETER = Convert.ToSingle(str);//		 
                str = dt.Rows[I][02].ToString(); if (str.Length > 0) lst.COIL_WIDTH = Convert.ToSingle(str);//	 
                str = dt.Rows[I][03].ToString(); if (str.Length > 0) lst.COILING_TENSION = Convert.ToSingle(str);
                str = dt.Rows[I][04].ToString(); if (str.Length > 0) lst.LENGTH = Convert.ToSingle(str);//		 
                str = dt.Rows[I][05].ToString(); if (str.Length > 0) lst.LENGTH_B = Convert.ToSingle(str);//	 
                str = dt.Rows[I][06].ToString(); if (str.Length > 0) lst.LENGTH_C = Convert.ToSingle(str);// 
                str = dt.Rows[I][07].ToString(); if (str.Length > 0) lst.MANDREL_SPEED = Convert.ToSingle(str);// 
                str = dt.Rows[I][08].ToString(); if (str.Length > 0) lst.MANDREL_TORQUE = Convert.ToSingle(str);// 
                str = dt.Rows[I][09].ToString(); if (str.Length > 0) lst.MD_SPEED_SET = Convert.ToSingle(str);// 	
                str = dt.Rows[I][10].ToString(); if (str.Length > 0) lst.MD_TENSION = Convert.ToSingle(str);// 
                str = dt.Rows[I][11].ToString(); if (str.Length > 0) lst.MD_TENSION_SET = Convert.ToSingle(str);// 	
                str = dt.Rows[I][12].ToString(); if (str.Length > 0) lst.MD_TORQUE = Convert.ToSingle(str);// 
                str = dt.Rows[I][13].ToString(); if (str.Length > 0) lst.PR_LEAD_RATIO = Convert.ToSingle(str);// 	
                str = dt.Rows[I][14].ToString(); if (str.Length > 0) lst.STRIP_SPEED = Convert.ToSingle(str);// 
                str = dt.Rows[I][15].ToString(); if (str.Length > 0) lst.THICK_SET = Convert.ToSingle(str);// 
                str = dt.Rows[I][16].ToString(); if (str.Length > 0) lst.WRAPER_ROLL1_SPEED = Convert.ToSingle(str);// 
                str = dt.Rows[I][17].ToString(); if (str.Length > 0) lst.WRAPER_ROLL2_SPEED = Convert.ToSingle(str);// 
                str = dt.Rows[I][18].ToString(); if (str.Length > 0) lst.WRAPER_ROLL3_SPEED = Convert.ToSingle(str);// 	

                LST.Add(lst);
            }
            dt.Dispose();


            return LST;
        }

    }
}
