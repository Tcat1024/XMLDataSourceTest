using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QtDataTrace.Interfaces;
using System.Data;
using System.Data.OleDb;
using iTextSharp.text;
using iTextSharp.text.pdf;
using DuHisPic;

namespace QtDataTrace.Access
{
   public class SingleQtTableLYCRM
    {
        //对于起初编辑的类的引用
        SingleQtTable sqt=new SingleQtTable();

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

      /// <summary>
      ///获取钢卷的物料谱系
      /// </summary>
      /// <param name="CoilID"></param>
      /// <returns></returns>
        public List<Mat_Pedigree> GetCRM_CoilInfo_PEDIGREE(string CoilID)
        { 
            List<Mat_Pedigree> LST = new List<Mat_Pedigree>();
            Mat_Pedigree lst;
            string str;

            //往前找
            string CoilID_Pre = CoilID; 
            do
            {
                //作为其他工序的出口料
                string strSQL = " SELECT * FROM CRM_COIL_TRACK_VIEW"
                              + " WHERE out_mat_id='" + CoilID_Pre + "'";
                DataTable dt = sqt.ReadDatatable_OraDB(strSQL);

                if (dt.Rows.Count <= 0)
                    break;
                else
                {
                    lst = new Mat_Pedigree();

                    str = dt.Rows[0]["OUT_MAT_ID"].ToString(); if (str.Trim().Length > 0) lst.OUT_MAT_ID = str;
                    str = dt.Rows[0]["IN_MAT_ID1"].ToString(); if (str.Trim().Length > 0) lst.IN_MAT_ID1 = str;
                    str = dt.Rows[0]["IN_MAT_ID2"].ToString(); if (str.Trim().Length > 0) lst.IN_MAT_ID2 = str;
                    str = dt.Rows[0]["DEVICE_NO"].ToString(); if (str.Trim().Length > 0) lst.DEVICE_NO = str;
                    str = dt.Rows[0]["PROCESS_CODE"].ToString(); if (str.Trim().Length > 0) lst.PROCESS_CODE = str;
                    str = dt.Rows[0]["START_TIME"].ToString(); if (str.Trim().Length > 0) lst.START_TIME = Convert.ToDateTime(str);
                    str = dt.Rows[0]["STOP_TIME"].ToString(); if (str.Trim().Length > 0) lst.STOP_TIME = Convert.ToDateTime(str);
                    str = dt.Rows[0]["START_LOC"].ToString(); if (str.Trim().Length > 0) lst.START_LOC = str;
                    str = dt.Rows[0]["MAT1_LENTH"].ToString(); if (str.Trim().Length > 0) lst.MAT1_LENTH = str;
                    str = dt.Rows[0]["MAT2_LENGTH"].ToString(); if (str.Trim().Length > 0) lst.MAT2_LENGTH = str;

                    LST.Add(lst);

                    //如果不幸的，出口料和入口料代号相同，就会先入死循环
                    if (LST.Count > 10) break;

                    //然后把出料作为入料，再次查找
                    CoilID_Pre = lst.IN_MAT_ID1;
                }
            } while (true);

            //往后找
            string CoilID_Next = CoilID;           
            do
            {
                //作为其他工序的入口料
                string strSQL = " SELECT * FROM CRM_COIL_TRACK_VIEW"
                              + " WHERE in_mat_id1='" + CoilID_Next + "'"
                              + " OR in_mat_id2='" + CoilID_Next + "'";
                DataTable dt = sqt.ReadDatatable_OraDB(strSQL);

                if (dt.Rows.Count <= 0)
                   break;
                else
                {
                    lst = new Mat_Pedigree();

                    str = dt.Rows[0]["OUT_MAT_ID"].ToString(); if (str.Trim().Length > 0) lst.OUT_MAT_ID = str;
                    str = dt.Rows[0]["IN_MAT_ID1"].ToString(); if (str.Trim().Length > 0) lst.IN_MAT_ID1 = str;
                    str = dt.Rows[0]["IN_MAT_ID2"].ToString(); if (str.Trim().Length > 0) lst.IN_MAT_ID2 = str;
                    str = dt.Rows[0]["DEVICE_NO"].ToString(); if (str.Trim().Length > 0) lst.DEVICE_NO = str;
                    str = dt.Rows[0]["PROCESS_CODE"].ToString(); if (str.Trim().Length > 0) lst.PROCESS_CODE = str;
                    str = dt.Rows[0]["START_TIME"].ToString(); if (str.Trim().Length > 0) lst.START_TIME = Convert.ToDateTime(str);
                    str = dt.Rows[0]["STOP_TIME"].ToString(); if (str.Trim().Length > 0) lst.STOP_TIME = Convert.ToDateTime(str);
                    str = dt.Rows[0]["START_LOC"].ToString(); if (str.Trim().Length > 0) lst.START_LOC = str;
                    str = dt.Rows[0]["MAT1_LENTH"].ToString(); if (str.Trim().Length > 0) lst.MAT1_LENTH = str;
                    str = dt.Rows[0]["MAT2_LENGTH"].ToString(); if (str.Trim().Length > 0) lst.MAT2_LENGTH = str;

                    LST.Add(lst);

                    //如果不幸的，出口料和入口料代号相同，就会先入死循环
                    if (LST.Count > 10) break;

                    //然后把出料作为入料，再次查找
                    CoilID_Next = lst.OUT_MAT_ID;
                }
            } while (true);                       


            //按照进入日期排序
            //按照时长排序
            LST.Sort(delegate(Mat_Pedigree a, Mat_Pedigree b) { return a.START_TIME.CompareTo(b.START_TIME); });

            return LST;
        }


        public List<CRM_CoilInfo_AF> GetCRM_CoilInfo_AF(string CoilID)
        { //获取钢卷在罩式退火炉内的信息
            List<CRM_CoilInfo_AF> LST = new List<CRM_CoilInfo_AF>();
            CRM_CoilInfo_AF lst; 
            
            string strSQL = "SELECT * FROM CRM_AF_INFO WHERE in_mat='"+ CoilID +"'";
            DataTable dt = sqt.ReadDatatable_OraDB(strSQL);

            for (int RowIndex = 0; RowIndex < dt.Rows.Count; RowIndex++)
            {
                lst = new CRM_CoilInfo_AF();

                lst.in_mat = dt.Rows[RowIndex]["in_mat"].ToString(); //
                lst.out_mat = dt.Rows[RowIndex]["out_mat"].ToString(); //   VARCHAR2(20),
                lst.stack_id = dt.Rows[RowIndex]["stack_id"].ToString(); //;//堆垛号      NUMBER(12),
                lst.coil_pos = dt.Rows[RowIndex]["coil_pos"].ToString(); // ;//堆垛在第几层，从下往上数 
                lst.on_base = dt.Rows[RowIndex]["on_base"].ToString(); // ;//在第几号罩式退火炉 

                lst.annealed = dt.Rows[RowIndex]["annealed"].ToString(); //    DATE,
                lst.base_id = dt.Rows[RowIndex]["base_id"].ToString(); //       NUMBER(6),
                lst.cycle = dt.Rows[RowIndex]["cycle"].ToString(); //          NUMBER(6),
                lst.hhood_id = dt.Rows[RowIndex]["hhood_id"].ToString(); //   NUMBER(6),
                lst.chood_id = dt.Rows[RowIndex]["chood_id"].ToString(); //   NUMBER(6),

                lst.icover_id = dt.Rows[RowIndex]["icover_id"].ToString(); //  NUMBER(6),
                lst.height = dt.Rows[RowIndex]["height"].ToString(); // NUMBER(8),
                lst.weight = dt.Rows[RowIndex]["weight"].ToString(); // NUMBER(8),
                lst.width = dt.Rows[RowIndex]["width"].ToString(); //   NUMBER(8),
                lst.outerdiam = dt.Rows[RowIndex]["outerdiam"].ToString(); //  NUMBER(8),
                lst.innerdiam = dt.Rows[RowIndex]["innerdiam"].ToString(); // NUMBER(8),
                lst.thickness = dt.Rows[RowIndex]["thickness"].ToString(); //  NUMBER(8),
                lst.priority = dt.Rows[RowIndex]["priority"].ToString(); //  NUMBER(3),
                lst.comment_count = dt.Rows[RowIndex]["comment_count"].ToString(); //  NUMBER(3),
                lst.transport_id = dt.Rows[RowIndex]["transport_id"].ToString(); //;// NUMBER(6)
                
                LST.Add(lst);
            }

            //已入库，但还没有处理时，
            if (LST.Count <= 0)
            {
                lst = new CRM_CoilInfo_AF();
                lst.in_mat = CoilID;
                lst.comment_count = "已到站，但未处理";
                lst.base_id = "";
                LST.Add(lst);
            }

            return LST;
        }

        public List<CRM_CoilInfo_PLTCM> GetCRM_CoilInfo_PLTCM(string CoilID)
        { //Pickling line 酸轧线

            List<CRM_CoilInfo_PLTCM> LST = new List<CRM_CoilInfo_PLTCM>();
            
            //一些基本的生产信息，从三级表过来的
            string strSQL = "SELECT * FROM CRM_PLTCM_REPORT"
                         + " WHERE in_mat_no_1='" + CoilID + "'"
                         + " OR in_mat_no_2='" + CoilID + "'";
            DataTable dt = sqt.ReadDatatable_OraDB(strSQL);
            if (dt.Rows.Count <= 0)
            {
                //如果没有数据，则返回空的列表
                return LST;
            }

            
            CRM_CoilInfo_PLTCM lst = new CRM_CoilInfo_PLTCM();
            CRM_CoilInfo_PLTCM_ini(ref lst);

            //入口物料，一行
            lst.IN_MAT_LIST = dt.Rows[0]["in_mat_no_1"].ToString();
            string in_mat_no_2 = dt.Rows[0]["in_mat_no_2"].ToString();
            if (in_mat_no_2.Trim().Length > 0)
            {
                lst.IN_MAT_LIST = lst.IN_MAT_LIST + "," + in_mat_no_2;
            }

            //出口物料，多行 
            lst.OUT_MAT_LIST = dt.Rows[0]["out_mat_no"].ToString();
            for (int RowsIndex = 1; RowsIndex < dt.Rows.Count; RowsIndex++)
            {
                lst.OUT_MAT_LIST = lst.OUT_MAT_LIST + "," + dt.Rows[RowsIndex]["out_mat_no"].ToString();
            }

            //钢种
            lst.SteelGrade = dt.Rows[0]["ST_NO"].ToString();
            //生产时间
            lst.START_TIME = dt.Rows[0]["START_TIME"].ToString();
            lst.STOP_TIME = dt.Rows[0]["STOP_TIME"].ToString();
                        
            
            //需要的拆解信息，从殷工自己提取的PLTCM_MESSAGE中获取，这里CRM_PLTCM_INFO已经经过拆解和迁移
            //获得钢卷通过各个监测点的时间
            strSQL = "SELECT * FROM CRM_PLTCM_INFO"
                   + " WHERE coil_id='" + CoilID + "'";
            dt = sqt.ReadDatatable_OraDB(strSQL);

            for (int RowIndex = 0; RowIndex < dt.Rows.Count; RowIndex++)
            {
                string EquiID = dt.Rows[RowIndex]["equi_id"].ToString();               
                switch (EquiID)
                {
                    //这里边的IF条件，是因为有许多重复的消息


                    //步进式送卷机，进入到拆卷系统
                    case "CV1 ":
                        if( lst.Conveyor01_Time.Length<=0) lst.Conveyor01_Time=dt.Rows[RowIndex]["start_time"].ToString();
                        break;
                    case "CV2 ":
                        if( lst.Conveyor02_Time.Length<=0) lst.Conveyor02_Time=dt.Rows[RowIndex]["start_time"].ToString();
                        break;
                    case "CV3 ":
                        if( lst.Conveyor03_Time.Length<=0) lst.Conveyor03_Time=dt.Rows[RowIndex]["start_time"].ToString();
                        break;
                    case "CV4 ":
                        if( lst.Conveyor04_Time.Length<=0) lst.Conveyor04_Time=dt.Rows[RowIndex]["start_time"].ToString();
                        break;
                    case "CV5 ":
                        if( lst.Conveyor05_Time.Length<=0) lst.Conveyor05_Time=dt.Rows[RowIndex]["start_time"].ToString();
                        break;
                    case "CV6 ":
                        if( lst.Conveyor06_Time.Length<=0) lst.Conveyor06_Time=dt.Rows[RowIndex]["start_time"].ToString();
                        break;
                    case "CV7 ":
                        if( lst.Conveyor07_Time.Length<=0) lst.Conveyor07_Time=dt.Rows[RowIndex]["start_time"].ToString();
                        break;
                    case "CV8 ":
                        if( lst.Conveyor08_Time.Length<=0) lst.Conveyor08_Time=dt.Rows[RowIndex]["start_time"].ToString();
                        break;
                    case "CV9 ":
                        if( lst.Conveyor09_Time.Length<=0) lst.Conveyor09_Time=dt.Rows[RowIndex]["start_time"].ToString();
                        break;

                    //传送
                    case "CT1 ": 
                        if (lst.CoilTransfer.Length<=0)
                        {lst.CoilTransfer="1";
                        lst.CoilTransTime=dt.Rows[RowIndex]["start_time"].ToString();}
                        break;
                    case "CT2 ":if (lst.CoilTransfer.Length<=0)
                        {
                        lst.CoilTransfer="2";
                        lst.CoilTransTime=dt.Rows[RowIndex]["start_time"].ToString();
                        }
                        break;

                     case  "ECC1"://Coil cars 钢卷台车
                        if (lst.CoilCar.Length<=0)
                        {
                        lst.CoilCar="1";
                        lst.CoilCarsTime=dt.Rows[RowIndex]["start_time"].ToString();
                        }
                        break;
                     case  "ECC2"://Coil cars 钢卷台车
                        if (lst.CoilCar.Length<=0)
                        {   lst.CoilCar="2";
                            lst.CoilCarsTime=dt.Rows[RowIndex]["start_time"].ToString();
                        }
                        break;

                    //开卷
                    case "POR1": 
                        if( lst.DeCoiler.Length<=0)
                        {lst.DeCoiler="1";
                        lst.DeCoilTime=dt.Rows[RowIndex]["start_time"].ToString();}
                        break;                         
                    case "POR2":
                        if( lst.DeCoiler.Length<=0)
                        {lst.DeCoiler="2";
                        lst.DeCoilTime=dt.Rows[RowIndex]["start_time"].ToString();}
                        break;


                    case "WX01"://Welder 焊接机
                        if( lst.WelderTime.Length<=0) lst.WelderTime=dt.Rows[RowIndex]["start_time"].ToString();
                        break;
                  
                    case "WX02"://EntryLooperTime;//入口活套进入时间
                        if( lst.EntryLooperTime_IN.Length<=0) lst.EntryLooperTime_IN=dt.Rows[RowIndex]["start_time"].ToString();
                        break;

                        //以下点不跟踪
                        //WPDX3 Entry looper 12.5% length
                        //WPDX4 Entry looper 25% length
                        //WPDX5 Entry looper 37.5% length
                        //WPDX6 Entry looper 50% length
                        //WPDX7 Entry looper 62.5% length
                        //WPDX8 Entry looper 75% length
                        //WPDX9 Entry looper 87.5% length

                    case "WX10"://EntryLooperTime;//入口活套退出时间
                        if( lst.EntryLooperTime_OUT.Length<=0) lst.EntryLooperTime_OUT=dt.Rows[RowIndex]["start_time"].ToString();
                        break;

                    case "WX11":// WPD1 张紧辊1# 
                        if( lst.WPD1Time.Length<=0) lst.WPD1Time=dt.Rows[RowIndex]["start_time"].ToString();
                        break;

                    case "WX12":// T/L位置 张力平整机
                        if( lst.TensionLevelerTime.Length<=0) lst.TensionLevelerTime=dt.Rows[RowIndex]["start_time"].ToString();
                        break;

                    case "WX13"://PL entry position 进入酸洗池
                         if( lst.PL_EntryTime.Length<=0) lst.PL_EntryTime=dt.Rows[RowIndex]["start_time"].ToString();
                        break;

                    case "WX14"://酸洗池退出时间 
                        if( lst.PL_ExitTime.Length<=0) lst.PL_ExitTime=dt.Rows[RowIndex]["start_time"].ToString();
                        break;

                    case "WX15"://#1 Delivery looper entry position 出口活套进入时间
                        if( lst.ExitLooper01Time_IN.Length<=0) lst.ExitLooper01Time_IN=dt.Rows[RowIndex]["start_time"].ToString();                        
                        break;
                        
                     //以下点不跟踪
                     //WPDX16 #1 Delivery looper 25% length
                     //WPDX17 #1 Delivery looper 50% length
                     //WPDX18 #1 Delivery looper 75% length

                    case "WX19"://#1 Delivery looper exit position 1#出口活套退出时间
                        if( lst.ExitLooper01Time_OUT.Length<=0) lst.ExitLooper01Time_OUT=dt.Rows[RowIndex]["start_time"].ToString();
                        //同时也是2#张紧辊的进入时间
                        if( lst.WPD2Time.Length<=0) lst.WPD2Time=dt.Rows[RowIndex]["start_time"].ToString();
                        break;

                    case "WX20"://Notcher Entry 月牙剪
                        if( lst.NotcherEntryTime.Length<=0) lst.NotcherEntryTime=dt.Rows[RowIndex]["start_time"].ToString();
                        //同时也是3#张紧辊的进入时间
                        if( lst.WPD3Time.Length<=0) lst.WPD3Time=dt.Rows[RowIndex]["start_time"].ToString();
                        break;

                    case "WX21": //#2 Delivery looper entry position 2#出口活套进入时间
                        if( lst.ExitLooper02Time_IN.Length<=0) lst.ExitLooper02Time_IN=dt.Rows[RowIndex]["start_time"].ToString();                        
                        break;

                        //以下点不跟踪
                        //WPDX22 #2 Delivery looper 25% length
                        //WPDX23 #2 Delivery looper 50% length
                        //WPDX24 #2 Delivery looper 75% length
                        

                    case "WX25": //#2 Delivery looper Exit position 2#出口活套退出时间
                        if( lst.ExitLooper02Time_OUT.Length<=0) lst.ExitLooper02Time_OUT=dt.Rows[RowIndex]["start_time"].ToString();                        
                        //同时也是4#张紧辊的进入时间
                        if( lst.WPD4Time.Length<=0) lst.WPD4Time=dt.Rows[RowIndex]["start_time"].ToString();
                        break;

                    case "WX26"://TCM entry进入轧机实际
                         if( lst.TCM_EntryTime.Length<=0) lst.TCM_EntryTime=dt.Rows[RowIndex]["start_time"].ToString();                        
                        //同时也是5#张紧辊的进入时间
                        if( lst.WPD5Time.Length<=0) lst.WPD5Time=dt.Rows[RowIndex]["start_time"].ToString();
                        break;
                }
            }
            
            lst.SHIFT_ID = "1";
            lst.CREW_ID = "1";

            LST.Add(lst);
            return LST;
        }
        private void CRM_CoilInfo_PLTCM_ini(ref CRM_CoilInfo_PLTCM lst)
       {
            lst.SHIFT_ID="";         lst.CREW_ID="";

      
         lst.Conveyor01_Time="";         lst.Conveyor02_Time="";         lst.Conveyor03_Time="";
         lst.Conveyor04_Time="";         lst.Conveyor05_Time="";         lst.Conveyor06_Time="";
         lst.Conveyor07_Time="";         lst.Conveyor08_Time="";         lst.Conveyor09_Time="";

         lst.CoilTransfer="";          lst.CoilTransTime="";
         lst.CoilCar="";         lst.CoilCarsTime="";//

         lst.DeCoiler="";          lst.DeCoilTime="";

         lst.WelderTime="";

         lst.EntryLooperTime_IN="";         lst.EntryLooperTime_OUT="";

         lst.WPD1Time="";

         lst.TensionLevelerTime="";

         lst.PL_EntryTime="";//酸洗池进入时间
         lst.PL_ExitTime="";//酸洗池退出时间 

         lst.ExitLooper01Time_IN="";//出口活套进入时间
         lst.ExitLooper01Time_OUT="";//出口活套进入时间

         lst.WPD2Time="";//2#张紧辊

         lst.NotcherEntryTime ="";//月牙剪

         lst.WPD3Time="";//3#张紧辊

         lst.ExitLooper02Time_IN="";         lst.ExitLooper02Time_OUT="";
         lst.WPD4Time="";//4#张紧辊
         lst.TCM_EntryTime="";
         lst.WPD5Time="";//4#张紧辊

       }


        public List<CRM_KeyEvents_PLTCM> GetCRM_KeyEvents_PLTCM(string CoilID)
        {
            //
            List<CRM_KeyEvents_PLTCM> LST = new List<CRM_KeyEvents_PLTCM>();
            CRM_KeyEvents_PLTCM lst;            
            
            //由于在事件表中，有很多重复的事件，因此需要先使用过滤的过程来读取
             List<CRM_CoilInfo_PLTCM>  LST_Info=GetCRM_CoilInfo_PLTCM(CoilID);

            //碰上了没有数据的钢卷号，返回空
             if (LST_Info.Count <= 0) return LST;
             
            //TCM_EntryTime为进入酸洗池的时间，有时候检测不到，为空，这时候会出错。
            DateTime TCM_EntryTime_out;
            if (! DateTime.TryParse(LST_Info[0].TCM_EntryTime, out TCM_EntryTime_out))
            {//如果转换不成功，采用前道工序的时间
                if (!DateTime.TryParse(LST_Info[0].TensionLevelerTime, out TCM_EntryTime_out))
                {
                    LST_Info[0].TCM_EntryTime = LST_Info[0].TensionLevelerTime;
                }
                else
                {
                    if (!DateTime.TryParse(LST_Info[0].WPD1Time, out TCM_EntryTime_out))
                    {
                        LST_Info[0].TCM_EntryTime = LST_Info[0].WPD1Time;
                    }
                    else
                    {
                        return LST;
                    }
                }
            }


            //查找下一个钢卷到达轧机前的时间
            DateTime TCM_EntryTime=Convert.ToDateTime(LST_Info[0].TCM_EntryTime);
            string strNextTCM_EntryTime = TCM_EntryTime.AddHours(2).ToString();
            string strSQL = "SELECT mat_no  FROM CRM_MATTRACK_TIME"
                           + " WHERE start_time>to_date('" + LST_Info[0].TCM_EntryTime + "','yyyy-mm-dd hh24:mi:ss')"
                           + " AND start_time<to_date('" + strNextTCM_EntryTime + "','yyyy-mm-dd hh24:mi:ss')"
                           + " AND device_no='LYCRM_TCM'"                          
                           + " ORDER BY start_time";
            DataTable dt = sqt.ReadDatatable_OraDB(strSQL);
            
            string NextCoilID;
            List<CRM_CoilInfo_PLTCM> NextLST_Info = new List<CRM_CoilInfo_PLTCM>();
            if (dt.Rows.Count > 0)
            {
                NextCoilID = dt.Rows[0]["mat_no"].ToString();
                NextLST_Info = GetCRM_CoilInfo_PLTCM(NextCoilID);
            }
            else
            {
                CRM_CoilInfo_PLTCM Nextlst_Info = new CRM_CoilInfo_PLTCM();
                NextLST_Info.Add(Nextlst_Info);
                NextLST_Info[0].TCM_EntryTime = TCM_EntryTime.AddHours(0.1).ToString();
            }
            dt.Dispose();

            
           
            //然后再逐一的进行分解
            string strStartDateTime="";
            
            if (LST_Info[0].Conveyor01_Time.Length>0) 
            { 
                lst = new CRM_KeyEvents_PLTCM(); 
                CRM_KeyEvents_PLTCM_ini(ref lst);
                lst.KeyEventsName = "Conveyor01_Time";
                lst.DateAndTime=LST_Info[0].Conveyor01_Time;
                lst.Description="到达步进式送卷机1#台";                  
                LST.Add(lst);
                if (strStartDateTime.Length <= 0) strStartDateTime = lst.DateAndTime;
            }

            if (LST_Info[0].Conveyor02_Time.Length > 0)
            {
                lst = new CRM_KeyEvents_PLTCM();
                CRM_KeyEvents_PLTCM_ini(ref lst);
                lst.KeyEventsName = "Conveyor02_Time";
                lst.DateAndTime = LST_Info[0].Conveyor02_Time;
                lst.Description = "到达步进式送卷机2#台";
                LST.Add(lst);
                if (strStartDateTime.Length <= 0) strStartDateTime = lst.DateAndTime;
            }

            if (LST_Info[0].Conveyor03_Time.Length > 0)
            {
                lst = new CRM_KeyEvents_PLTCM();
                CRM_KeyEvents_PLTCM_ini(ref lst);
                lst.KeyEventsName = "Conveyor03_Time";
                lst.DateAndTime = LST_Info[0].Conveyor03_Time;
                lst.Description = "到达步进式送卷机3#台";
                LST.Add(lst);
                if (strStartDateTime.Length <= 0) strStartDateTime = lst.DateAndTime;
            }

            if (LST_Info[0].Conveyor04_Time.Length > 0)
            {
                lst = new CRM_KeyEvents_PLTCM();
                CRM_KeyEvents_PLTCM_ini(ref lst);
                lst.KeyEventsName = "Conveyor04_Time";
                lst.DateAndTime = LST_Info[0].Conveyor04_Time;
                lst.Description = "到达步进式送卷机4#台";
                LST.Add(lst);
                if (strStartDateTime.Length <= 0) strStartDateTime = lst.DateAndTime;
            }

            if (LST_Info[0].Conveyor05_Time.Length > 0)
            {
                lst = new CRM_KeyEvents_PLTCM();
                CRM_KeyEvents_PLTCM_ini(ref lst);
                lst.KeyEventsName = "Conveyor05_Time";
                lst.DateAndTime = LST_Info[0].Conveyor05_Time;
                lst.Description = "到达步进式送卷机5#台";
                LST.Add(lst);
                if (strStartDateTime.Length <= 0) strStartDateTime = lst.DateAndTime;
            }
 
            if (LST_Info[0].Conveyor06_Time.Length>0) 
              { 
                  lst = new CRM_KeyEvents_PLTCM(); 
                  CRM_KeyEvents_PLTCM_ini(ref lst);
                  lst.KeyEventsName = "Conveyor06_Time";
                  lst.DateAndTime=LST_Info[0].Conveyor06_Time;
                  lst.Description="到达步进式送卷机6#台";
                LST.Add(lst);
                if (strStartDateTime.Length <= 0) strStartDateTime = lst.DateAndTime;
              } 
            if (LST_Info[0].Conveyor07_Time.Length>0) 
              { 
                  lst = new CRM_KeyEvents_PLTCM(); 
                  CRM_KeyEvents_PLTCM_ini(ref lst);
                  lst.KeyEventsName = "Conveyor07_Time";
                  lst.DateAndTime=LST_Info[0].Conveyor07_Time;
                  lst.Description="到达步进式送卷机7#台";
                LST.Add(lst);
                if (strStartDateTime.Length <= 0) strStartDateTime = lst.DateAndTime;
              } 
            if (LST_Info[0].Conveyor08_Time.Length>0) 
              { 
                  lst = new CRM_KeyEvents_PLTCM(); 
                  CRM_KeyEvents_PLTCM_ini(ref lst);
                  lst.KeyEventsName = "Conveyor08_Time";
                  lst.DateAndTime=LST_Info[0].Conveyor08_Time;
                  lst.Description="到达步进式送卷机8#台";
                LST.Add(lst);
                if (strStartDateTime.Length <= 0) strStartDateTime = lst.DateAndTime;
              } 
            if (LST_Info[0].Conveyor09_Time.Length>0) 
              { 
                  lst = new CRM_KeyEvents_PLTCM(); 
                  CRM_KeyEvents_PLTCM_ini(ref lst);
                  lst.KeyEventsName = "Conveyor09_Time";
                  lst.DateAndTime=LST_Info[0].Conveyor09_Time;
                  lst.Description="到达步进式送卷机9#台";
                LST.Add(lst);
                if (strStartDateTime.Length <= 0) strStartDateTime = lst.DateAndTime;
              }
            

         if (LST_Info[0].CoilTransTime.Length>0) 
              { 
                  lst = new CRM_KeyEvents_PLTCM(); 
                  CRM_KeyEvents_PLTCM_ini(ref lst);
                  lst.KeyEventsName = "CoilTransTime";
                  lst.DateAndTime=LST_Info[0].CoilTransTime;
                  lst.Description="到达"+LST_Info[0].CoilTransfer+"#开卷机台";
             LST.Add(lst);
              }  

             if (LST_Info[0].CoilCarsTime.Length>0) 
              { 
                  lst = new CRM_KeyEvents_PLTCM(); 
                  CRM_KeyEvents_PLTCM_ini(ref lst);
                  lst.KeyEventsName = "CoilCarsTime";
                  lst.DateAndTime=LST_Info[0].CoilCarsTime;
                  lst.Description="到达"+LST_Info[0].CoilCar+"#钢卷提升台";
                 LST.Add(lst);
              } 
       
            if (LST_Info[0].DeCoilTime.Length>0) 
              { 
                  lst = new CRM_KeyEvents_PLTCM(); 
                  CRM_KeyEvents_PLTCM_ini(ref lst);
                  lst.KeyEventsName = "DeCoilTime";
                  lst.DateAndTime=LST_Info[0].DeCoilTime;
                  lst.Description="到达"+LST_Info[0].DeCoiler+"#拆卷机";
                LST.Add(lst);
              } 

        
             if (LST_Info[0].WelderTime.Length>0) 
              { 
                  lst = new CRM_KeyEvents_PLTCM(); 
                  CRM_KeyEvents_PLTCM_ini(ref lst);
                  lst.KeyEventsName = "WelderTime";
                  lst.DateAndTime=LST_Info[0].WelderTime;
                  lst.Description="焊接时间";
                 LST.Add(lst);
              } 
        
             if (LST_Info[0].EntryLooperTime_IN.Length>0) 
              { 
                  lst = new CRM_KeyEvents_PLTCM(); 
                  CRM_KeyEvents_PLTCM_ini(ref lst);
                  lst.KeyEventsName = "EntryLooperTime_IN";
                  lst.DateAndTime=LST_Info[0].EntryLooperTime_IN;
                  lst.Description="入口活套进入时间";
                 LST.Add(lst);
              } 

            if (LST_Info[0].EntryLooperTime_OUT.Length>0) 
              { 
                  lst = new CRM_KeyEvents_PLTCM(); 
                  CRM_KeyEvents_PLTCM_ini(ref lst);
                  lst.KeyEventsName = "EntryLooperTime_OUT";
                  lst.DateAndTime=LST_Info[0].EntryLooperTime_OUT;
                  lst.Description="入口活套进入时间";
                LST.Add(lst);
              }
           
            if (LST_Info[0].WPD1Time.Length>0) 
              { 
                  lst = new CRM_KeyEvents_PLTCM(); 
                  CRM_KeyEvents_PLTCM_ini(ref lst);
                  lst.KeyEventsName = "WPD1Time";
                  lst.DateAndTime=LST_Info[0].WPD1Time;
                  lst.Description="1#张紧辊";
                LST.Add(lst);
              } 

            if (LST_Info[0].TensionLevelerTime.Length>0) 
              { 
                  lst = new CRM_KeyEvents_PLTCM(); 
                  CRM_KeyEvents_PLTCM_ini(ref lst);
                  lst.KeyEventsName = "TensionLevelerTime";
                  lst.DateAndTime=LST_Info[0].TensionLevelerTime;
                  lst.Description="张力平整机";
                LST.Add(lst);
              }
            
            if (LST_Info[0].PL_EntryTime.Length>0) 
              { 
                  lst = new CRM_KeyEvents_PLTCM(); 
                  CRM_KeyEvents_PLTCM_ini(ref lst);
                  lst.KeyEventsName = "PL_EntryTime";
                  lst.DateAndTime=LST_Info[0].PL_EntryTime;
                  lst.Description="进入酸洗池";
                LST.Add(lst);
              } 

            if (LST_Info[0].PL_ExitTime.Length>0) 
              { 
                  lst = new CRM_KeyEvents_PLTCM(); 
                  CRM_KeyEvents_PLTCM_ini(ref lst);
                  lst.KeyEventsName = "PL_ExitTime";
                  lst.DateAndTime=LST_Info[0].PL_ExitTime;
                  lst.Description="离开酸洗池";
                LST.Add(lst);
              } 
         
            if (LST_Info[0].ExitLooper01Time_IN.Length>0) 
            { 
                lst = new CRM_KeyEvents_PLTCM(); 
                CRM_KeyEvents_PLTCM_ini(ref lst);
                lst.KeyEventsName = "ExitLooper01Time_IN";
                lst.DateAndTime=LST_Info[0].ExitLooper01Time_IN;
                lst.Description="进入1#出口活套";
                LST.Add(lst);
            } 

            if (LST_Info[0].ExitLooper01Time_OUT.Length>0) 
            { 
                lst = new CRM_KeyEvents_PLTCM(); 
                CRM_KeyEvents_PLTCM_ini(ref lst);
                lst.KeyEventsName = "ExitLooper01Time_OUT";
                lst.DateAndTime=LST_Info[0].ExitLooper01Time_OUT;
                lst.Description="离开1#出口活套";
                LST.Add(lst);
            } 
            
            if (LST_Info[0].WPD2Time.Length>0) 
            { 
                lst = new CRM_KeyEvents_PLTCM(); 
                CRM_KeyEvents_PLTCM_ini(ref lst);
                lst.KeyEventsName = "WPD2Time";
                lst.DateAndTime=LST_Info[0].WPD2Time;
                lst.Description="2#张紧辊";
                LST.Add(lst);
            }
            
            if (LST_Info[0].NotcherEntryTime.Length>0) 
            { 
                lst = new CRM_KeyEvents_PLTCM(); 
                CRM_KeyEvents_PLTCM_ini(ref lst);
                lst.KeyEventsName = "NotcherEntryTime";
                lst.DateAndTime=LST_Info[0].NotcherEntryTime;
                lst.Description="月牙剪";
                LST.Add(lst);
            } 
 
            if (LST_Info[0].WPD3Time.Length>0) 
            { 
                lst = new CRM_KeyEvents_PLTCM(); 
                CRM_KeyEvents_PLTCM_ini(ref lst);
                lst.KeyEventsName = "WPD3Time";
                lst.DateAndTime=LST_Info[0].WPD3Time;
                lst.Description="3#张紧辊";
                LST.Add(lst);
            } 
 
             if (LST_Info[0].ExitLooper02Time_IN.Length>0) 
            { 
                lst = new CRM_KeyEvents_PLTCM(); 
                CRM_KeyEvents_PLTCM_ini(ref lst);
                lst.KeyEventsName = "ExitLooper02Time_IN";
                lst.DateAndTime=LST_Info[0].ExitLooper02Time_IN;
                lst.Description="进入2#出口活套";
                LST.Add(lst);
            } 

                if (LST_Info[0].ExitLooper02Time_OUT.Length>0) 
                { 
                    lst = new CRM_KeyEvents_PLTCM(); 
                    CRM_KeyEvents_PLTCM_ini(ref lst);
                    lst.KeyEventsName = "ExitLooper02Time_OUT";
                    lst.DateAndTime=LST_Info[0].ExitLooper02Time_OUT;
                    lst.Description="离开2#出口活套";
                    LST.Add(lst);
                } 

            if (LST_Info[0].WPD4Time.Length>0) 
            { 
                    lst = new CRM_KeyEvents_PLTCM(); 
                    CRM_KeyEvents_PLTCM_ini(ref lst);
                    lst.KeyEventsName = "WPD4Time";
                    lst.DateAndTime=LST_Info[0].WPD4Time;
                    lst.Description="4#张紧辊";
                    LST.Add(lst);
            }

            if (LST_Info[0].WPD5Time.Length > 0)
            {
                  lst = new CRM_KeyEvents_PLTCM();
                  CRM_KeyEvents_PLTCM_ini(ref lst);
                  lst.KeyEventsName = "WPD5Time";
                  lst.DateAndTime = LST_Info[0].WPD5Time;
                  lst.Description = "5#张紧辊";
                  LST.Add(lst);
            }
            
            if (LST_Info[0].TCM_EntryTime.Length>0) 
            { 
                lst = new CRM_KeyEvents_PLTCM(); 
                CRM_KeyEvents_PLTCM_ini(ref lst);
                lst.KeyEventsName = "COIL_HEAD_ENTRY_CRM";
                lst.DateAndTime=LST_Info[0].TCM_EntryTime;
                lst.Description = "卷头到1#轧机";
                LST.Add(lst);
            }
            
            if (NextLST_Info[0].TCM_EntryTime.Length > 0) 
            { 
                lst = new CRM_KeyEvents_PLTCM(); 
                CRM_KeyEvents_PLTCM_ini(ref lst);
                lst.KeyEventsName = "COIL_TAIL_ENTRY_CRM";
                lst.DateAndTime = NextLST_Info[0].TCM_EntryTime;
                lst.Description="卷尾到1#轧机";
                LST.Add(lst);
            }
           
            //计算时长 
            DateTime StartDateTime = Convert.ToDateTime(strStartDateTime);
            DateTime cDateTime = new DateTime();
            TimeSpan ts = new TimeSpan();
            for (int I = 0; I < LST.Count; I++)
            {
                if (DateTime.TryParse(LST[I].DateAndTime, out cDateTime))
                {
                    ts = cDateTime - StartDateTime;
                    LST[I].Duration = float.Parse((ts.TotalSeconds / 60.0).ToString("#0.00"));
                }
            }

            //按照时长排序
            LST.Sort(delegate(CRM_KeyEvents_PLTCM a, CRM_KeyEvents_PLTCM b) { return a.Duration.CompareTo(b.Duration); });

            return LST;
        }

       private void CRM_KeyEvents_PLTCM_ini(ref CRM_KeyEvents_PLTCM lst)
       {
            lst.DateAndTime="";
            lst.Duration = 0;
            lst.Description = "";
       }

        public List<CRM_CoilInfo_CSL> GetCRM_CoilInfo_CSL(string CoilID)
       { //CrossSplitLine横切线
           List<CRM_CoilInfo_CSL> LST = new List<CRM_CoilInfo_CSL>();
           CRM_CoilInfo_CSL lst;

           string strSQL = " SELECT * FROM CRM_CSL_INFO"
                         + " WHERE in_mat_ID_1='" + CoilID + "'"
                         + " OR    in_mat_ID_2='" + CoilID + "'";
           DataTable dt = sqt.ReadDatatable_OraDB(strSQL);

           //如果没有查找到数据，返回空
           if (dt.Rows.Count <= 0) return LST;

           lst = new CRM_CoilInfo_CSL();

           int RowIndex = 0;           
           lst.IN_MAT_ID_1 = dt.Rows[RowIndex]["IN_MAT_ID_1"].ToString();
           lst.IN_MAT_ID_2 = dt.Rows[RowIndex]["IN_MAT_ID_2"].ToString();
           lst.OUT_MAT_ID = dt.Rows[RowIndex]["OUT_MAT_ID"].ToString();
           lst.STEEL_GRADE = dt.Rows[RowIndex]["STEEL_GRADE"].ToString();
           lst.PROD_TIME_START = dt.Rows[RowIndex]["PROD_TIME_START"].ToString();
           lst.PROD_TIME_END = dt.Rows[RowIndex]["PROD_TIME_END"].ToString();
           lst.SHIFT_ID = dt.Rows[RowIndex]["SHIFT_ID"].ToString();
           lst.CREW_ID = dt.Rows[RowIndex]["CREW_ID"].ToString();
           lst.PLAN_ID = dt.Rows[RowIndex]["PLAN_ID"].ToString();
           lst.FINAL_COIL = dt.Rows[RowIndex]["FINAL_COIL"].ToString();
           lst.WORK_KIND_CODE = dt.Rows[RowIndex]["WORK_KIND_CODE"].ToString();
           lst.ORDER_NO = dt.Rows[RowIndex]["ORDER_NO"].ToString();
           lst.SURFACE = dt.Rows[RowIndex]["SURFACE"].ToString();
           lst.SPEC_CHANGE_FLAG = dt.Rows[RowIndex]["SPEC_CHANGE_FLAG"].ToString();
           lst.EXIT_LEN_TARGET = dt.Rows[RowIndex]["EXIT_LEN_TARGET"].ToString();
           lst.EXIT_WIDTH_TARGET = dt.Rows[RowIndex]["EXIT_WIDTH_TARGET"].ToString();
           lst.EXIT_THICK_TARGET = dt.Rows[RowIndex]["EXIT_THICK_TARGET"].ToString();
           lst.EXIT_LEN = dt.Rows[RowIndex]["EXIT_LEN"].ToString();
           lst.EXIT_WIDTH = dt.Rows[RowIndex]["EXIT_WIDTH"].ToString();
           lst.EXIT_THICK = dt.Rows[RowIndex]["EXIT_THICK"].ToString();
           lst.ACT_NET_WEIGHT = dt.Rows[RowIndex]["ACT_NET_WEIGHT"].ToString();
           lst.ACT_GROSS_WEIGHT = dt.Rows[RowIndex]["ACT_GROSS_WEIGHT"].ToString();
           lst.SHEET_NUM = dt.Rows[RowIndex]["SHEET_NUM"].ToString();
           lst.SHEET_NUM_COIL_1 = dt.Rows[RowIndex]["SHEET_NUM_COIL_1"].ToString();
           lst.MAT_WEIGHT_COIL_1 = dt.Rows[RowIndex]["MAT_WEIGHT_COIL_1"].ToString();
           lst.SHEET_NUM_COIL_2 = dt.Rows[RowIndex]["SHEET_NUM_COIL_2"].ToString();
           lst.MAT_WEIGHT_COIL_2 = dt.Rows[RowIndex]["MAT_WEIGHT_COIL_2"].ToString();
           lst.HEAD_SCRAP_CODE = dt.Rows[RowIndex]["HEAD_SCRAP_CODE"].ToString();
           lst.HEAD_SCRAP_LEN = dt.Rows[RowIndex]["HEAD_SCRAP_LEN"].ToString();
           lst.HEAD_SCRAP_WEIGHT = dt.Rows[RowIndex]["HEAD_SCRAP_WEIGHT"].ToString();
           lst.TAIL_SCRAP_CODE = dt.Rows[RowIndex]["TAIL_SCRAP_CODE"].ToString();
           lst.TAIL_SCRAP_LEN = dt.Rows[RowIndex]["TAIL_SCRAP_LEN"].ToString();
           lst.TAIL_SCRAP_WEIGHT = dt.Rows[RowIndex]["TAIL_SCRAP_WEIGHT"].ToString();
           lst.TRIM_FLAG = dt.Rows[RowIndex]["TRIM_FLAG"].ToString();
           lst.TRIM_WIDTH = dt.Rows[RowIndex]["TRIM_WIDTH"].ToString();
           lst.FLAG_WITH_LENGHT_CHANGE = dt.Rows[RowIndex]["FLAG_WITH_LENGHT_CHANGE"].ToString();
           lst.PILE_MACHINE = dt.Rows[RowIndex]["PILE_MACHINE"].ToString();
           lst.RMND_FLAG = dt.Rows[RowIndex]["RMND_FLAG"].ToString();
           lst.PACK_TYPE = dt.Rows[RowIndex]["PACK_TYPE"].ToString();
        
            LST.Add(lst);
            return LST;
        }

        public List<CRM_CoilInfo_RTL> GetCRM_CoilInfo_RTL(string CoilID)
        { //SlitterLine  纵切线
            List<CRM_CoilInfo_RTL> LST = new List<CRM_CoilInfo_RTL>();
            CRM_CoilInfo_RTL lst = new CRM_CoilInfo_RTL();

            lst.COIL_ID = CoilID;
            lst.SHIFT_ID = "1";
            lst.CREW_ID = "1";

            LST.Add(lst);
            return LST;
        }

        public List<CRM_CoilInfo_RCL> GetCRM_CoilInfo_RCL(string CoilID)
        { //ReCoiling 重卷线
            List<CRM_CoilInfo_RCL> LST = new List<CRM_CoilInfo_RCL>();
            CRM_CoilInfo_RCL lst ;

            string strSQL = " SELECT * FROM CRM_RCL_INFO"
                          + " WHERE in_mat_ID_1='" + CoilID + "'"
                          + " OR  in_mat_ID_2='" + CoilID + "'";
            DataTable dt = sqt.ReadDatatable_OraDB(strSQL);

            //如果没有查找到数据，返回空
            if(dt.Rows.Count<=0 ) return LST;
 
            lst = new CRM_CoilInfo_RCL();

            int RowIndex = 0;
            lst.IN_MAT_ID_1 = dt.Rows[RowIndex]["IN_MAT_ID_1"].ToString();
            lst.IN_MAT_ID_2 = dt.Rows[RowIndex]["IN_MAT_ID_2"].ToString();
            lst.OUT_MAT_ID = dt.Rows[RowIndex]["OUT_MAT_ID"].ToString();
            lst.EXITMATERIALID = dt.Rows[RowIndex]["EXITMATERIALID"].ToString();
            lst.EXITMATERIALOD = dt.Rows[RowIndex]["EXITMATERIALOD"].ToString();
            lst.ORDER_NUMBER = dt.Rows[RowIndex]["ORDER_NUMBER"].ToString();
            lst.PRODUCT_TIME_START = dt.Rows[RowIndex]["PRODUCT_TIME_START"].ToString();
            lst.PRODUCT_TIME_END = dt.Rows[RowIndex]["PRODUCT_TIME_END"].ToString();
            lst.FINAL_COIL_FLAG = dt.Rows[RowIndex]["FINAL_COIL_FLAG"].ToString();
            lst.REPAIR_FLAG = dt.Rows[RowIndex]["REPAIR_FLAG"].ToString();
            lst.REPAIR_COUNT = dt.Rows[RowIndex]["REPAIR_COUNT"].ToString();
            lst.PRODUCT_PLAN_NUMBER = dt.Rows[RowIndex]["PRODUCT_PLAN_NUMBER"].ToString();
            lst.WORK_KIND_CODE = dt.Rows[RowIndex]["WORK_KIND_CODE"].ToString();
            lst.STEEL_GRADE = dt.Rows[RowIndex]["STEEL_GRADE"].ToString();
            lst.SG_SIGN = dt.Rows[RowIndex]["SG_SIGN"].ToString();
            lst.SURFACE_ACCURACY = dt.Rows[RowIndex]["SURFACE_ACCURACY"].ToString();
            lst.SPEC_CHANGE_FLAG = dt.Rows[RowIndex]["SPEC_CHANGE_FLAG"].ToString();
            lst.AIM_LENGTH = dt.Rows[RowIndex]["AIM_LENGTH"].ToString();
            lst.IN_LENGTH_1 = dt.Rows[RowIndex]["IN_LENGTH_1"].ToString();
            lst.IN_LENGTH_2 = dt.Rows[RowIndex]["IN_LENGTH_2"].ToString();
            lst.OUT_LENGTH = dt.Rows[RowIndex]["OUT_LENGTH"].ToString();
            lst.AIM_WIDTH = dt.Rows[RowIndex]["AIM_WIDTH"].ToString();
            lst.IN_WIDTH_1 = dt.Rows[RowIndex]["IN_WIDTH_1"].ToString();
            lst.IN_WIDTH_2 = dt.Rows[RowIndex]["IN_WIDTH_2"].ToString();
            lst.OUT_WIDTH = dt.Rows[RowIndex]["OUT_WIDTH"].ToString();
            lst.AIM_THICKNESS = dt.Rows[RowIndex]["AIM_THICKNESS"].ToString();
            lst.IN_THICKNESS_1 = dt.Rows[RowIndex]["IN_THICKNESS_1"].ToString();
            lst.IN_THICKNESS_2 = dt.Rows[RowIndex]["IN_THICKNESS_2"].ToString();
            lst.OUT_THICKNESS = dt.Rows[RowIndex]["OUT_THICKNESS"].ToString();
            lst.IN_WEIGHT_1 = dt.Rows[RowIndex]["IN_WEIGHT_1"].ToString();
            lst.IN_WEIGHT_2 = dt.Rows[RowIndex]["IN_WEIGHT_2"].ToString();
            lst.OUT_CAL_WEIGHT = dt.Rows[RowIndex]["OUT_CAL_WEIGHT"].ToString();
            lst.OUT_WEIGHT = dt.Rows[RowIndex]["OUT_WEIGHT"].ToString();
            lst.PRODUCTION_TIME_ELAPSED = dt.Rows[RowIndex]["PRODUCTION_TIME_ELAPSED"].ToString();
            lst.SHIFT_ID = dt.Rows[RowIndex]["SHIFT_ID"].ToString();
            lst.CREW_ID = dt.Rows[RowIndex]["CREW_ID"].ToString();
            lst.STOP_TIME = dt.Rows[RowIndex]["STOP_TIME"].ToString();
            lst.STOP_NUMBER = dt.Rows[RowIndex]["STOP_NUMBER"].ToString();
            lst.SURFACE_GROUP = dt.Rows[RowIndex]["SURFACE_GROUP"].ToString();
            lst.SURFACE_PROTECT = dt.Rows[RowIndex]["SURFACE_PROTECT"].ToString();
            lst.SURFACE_DECIDE = dt.Rows[RowIndex]["SURFACE_DECIDE"].ToString();
            lst.SURFACE_DECIDE_MAKER = dt.Rows[RowIndex]["SURFACE_DECIDE_MAKER"].ToString();

            lst.SCRAP_CODE_HEAD = dt.Rows[RowIndex]["SCRAP_CODE_HEAD"].ToString();
            lst.SCRAP_LENGTH_HEAD = dt.Rows[RowIndex]["SCRAP_LENGTH_HEAD"].ToString();
            lst.SCRAP_WEIGHT_HEAD = dt.Rows[RowIndex]["SCRAP_WEIGHT_HEAD"].ToString();
            
            lst.SCRAP_CODE_TAIL = dt.Rows[RowIndex]["SCRAP_CODE_TAIL"].ToString();
            lst.SCRAP_LENGTH_TAIL = dt.Rows[RowIndex]["SCRAP_LENGTH_TAIL"].ToString();
            lst.SCRAP_WEIGHT_TAIL = dt.Rows[RowIndex]["SCRAP_WEIGHT_TAIL"].ToString();

            lst.TRIM_FLAG = dt.Rows[RowIndex]["TRIM_FLAG"].ToString();
            lst.TRIM_WIDTH = dt.Rows[RowIndex]["TRIM_WIDTH"].ToString();
            lst.OIL_TYPE_CODE = dt.Rows[RowIndex]["OIL_TYPE_CODE"].ToString();
            lst.OIL_QUANTITY = dt.Rows[RowIndex]["OIL_QUANTITY"].ToString();
            lst.STRIP_NUMBER_PACK = dt.Rows[RowIndex]["STRIP_NUMBER_PACK"].ToString();
            
            LST.Add(lst);
            return LST;
        }

        public List<CRM_CoilInfo_SPM> GetCRM_CoilInfo_SPM(string CoilID)
        { //平整机基本信息	"

            List<CRM_CoilInfo_SPM> LST = new List<CRM_CoilInfo_SPM>();
           

            string strSQL = " SELECT * FROM CRM_SPM_INFO"
                          + " WHERE entry_mat_no='" + CoilID + "'";
            DataTable dt = sqt.ReadDatatable_OraDB(strSQL);

            //如果没有查找到数据，返回空
            if (dt.Rows.Count <= 0) return LST;

            for (int RowIndex = 0; RowIndex < dt.Rows.Count; RowIndex++)
            {
                CRM_CoilInfo_SPM lst = new CRM_CoilInfo_SPM();
                               
                lst.SHIFT_ID = dt.Rows[RowIndex]["prod_shift_no"].ToString();  
                lst.CREW_ID =dt.Rows[RowIndex]["prod_shift_group"].ToString(); 
                                
               lst.entry_mat_no = dt.Rows[RowIndex]["entry_mat_no"].ToString();
               lst.exit_mat_no = dt.Rows[RowIndex]["exit_mat_no"].ToString();

               lst.product_plan_no = dt.Rows[RowIndex]["product_plan_no"].ToString();
               lst.order_no = dt.Rows[RowIndex]["order_no"].ToString();

               lst.st_grade = dt.Rows[RowIndex]["st_grade"].ToString();
               lst.prod_time_start = dt.Rows[RowIndex]["prod_time_start"].ToString();
               lst.prod_time_end = dt.Rows[RowIndex]["prod_time_end"].ToString();

               lst.order_thick = dt.Rows[RowIndex]["order_thick"].ToString();
               lst.entry_mat_thick = dt.Rows[RowIndex]["entry_mat_thick"].ToString();
               lst.exit_mat_thick = dt.Rows[RowIndex]["exit_mat_thick"].ToString();

               lst.order_width = dt.Rows[RowIndex]["order_width"].ToString();
               lst.entry_mat_width = dt.Rows[RowIndex]["entry_mat_width"].ToString();
               lst.exit_mat_width = dt.Rows[RowIndex]["exit_mat_width"].ToString();

               lst.order_wt_unit_aim = dt.Rows[RowIndex]["order_wt_unit_aim"].ToString();
               lst.entry_mat_wt = dt.Rows[RowIndex]["entry_mat_wt"].ToString();
               lst.exit_mat_wt = dt.Rows[RowIndex]["exit_mat_wt"].ToString();

               lst.entry_mat_len = dt.Rows[RowIndex]["entry_mat_len"].ToString();
               lst.exit_mat_len = dt.Rows[RowIndex]["exit_mat_len"].ToString();

               lst.entry_mat_inner_dia = dt.Rows[RowIndex]["entry_mat_inner_dia"].ToString();
               lst.exit_mat_inner_dia = dt.Rows[RowIndex]["exit_mat_inner_dia"].ToString();

               lst.entry_mat_outer_dia = dt.Rows[RowIndex]["entry_mat_outer_dia"].ToString();
               lst.exit_mat_outer_dia = dt.Rows[RowIndex]["exit_mat_outer_dia"].ToString();

               lst.elpow_cons = dt.Rows[RowIndex]["elpow_cons"].ToString();
               lst.steam_cons = dt.Rows[RowIndex]["steam_cons"].ToString();
               lst.ctapwater_cons = dt.Rows[RowIndex]["ctapwater_cons"].ToString();
               lst.demwater_cons = dt.Rows[RowIndex]["demwater_cons"].ToString();
               lst.coolwater_cons = dt.Rows[RowIndex]["coolwater_cons"].ToString();
               lst.oil_consumption = dt.Rows[RowIndex]["oil_consumption"].ToString();

                LST.Add(lst);
            }
            return LST;
        }
        
       /// <summary>
       /// 获取某个工序在某个时间段的物料代号列表
       /// </summary>
        public List<CRMCoilTrack> GetCRMCoilTrack(string strStartTime, string strEndTime, string WorkProcess, string SteelGrade, string WorkerTeam)
        {
            List<CRMCoilTrack> LST = new List<CRMCoilTrack>();

            //检查时间范围的设置
            sqt.CheckDateRange(strStartTime,ref strEndTime,7);

            ///等待全部的物料谱系表建立后，才能统一到PROCESS_MAT_PEDIGREE中去

            string strSQL = "";
            if (WorkProcess == "PLTCM")
            {//酸轧线
                strSQL = "SELECT * FROM CRM_PLTCM_REPORT"
                       + " WHERE start_time>=to_date('" + strStartTime + "','yyyy-mm-dd')"
                       + " AND start_time<=to_date('" + strEndTime + "','yyyy-mm-dd')";

                if (SteelGrade.Length > 0)
                    strSQL = strSQL + " AND ST_NO='" + SteelGrade + "'";

                //依照产品生产时间排序
                strSQL = strSQL + " ORDER BY in_mat_no_1";
            }

            if (WorkProcess == "AF")
            {//罩式炉
                strSQL = "SELECT * FROM CRM_AF_INFO"
                       + " WHERE on_base>=to_date('" + strStartTime + "','yyyy-mm-dd')"
                       + " AND on_base<=to_date('" + strEndTime + "','yyyy-mm-dd')";

                //if (SteelGrade.Length > 0)
                    //strSQL = strSQL + " AND ST_NO='" + SteelGrade + "'";

                //依照产品生产时间排序
                strSQL = strSQL + " ORDER BY in_mat";
            }

            if (WorkProcess == "CGL")
            {//镀锌线
                strSQL = "SELECT * FROM CRM_CGL_INFO"
                       + " WHERE prod_start>=to_date('" + strStartTime + "','yyyy-mm-dd')"
                       + " AND prod_start<=to_date('" + strEndTime + "','yyyy-mm-dd')";

                //if (SteelGrade.Length > 0)
                //strSQL = strSQL + " AND ST_NO='" + SteelGrade + "'";

                //依照产品生产时间排序
                strSQL = strSQL + " ORDER BY in_mat";
            }

            if (WorkProcess == "SPM")
            {//平整线
                strSQL = "SELECT * FROM CRM_SPM_INFO"
                       + " WHERE prod_time_start>=to_date('" + strStartTime + "','yyyy-mm-dd')"
                       + " AND prod_time_start<=to_date('" + strEndTime + "','yyyy-mm-dd')";

                if (SteelGrade.Length > 0)
                    strSQL = strSQL + " AND st_grade='" + SteelGrade + "'";

                //依照产品生产时间排序
                strSQL = strSQL + " ORDER BY entry_mat_no";
            }

            if (WorkProcess == "RCL")
            {//重卷线
                strSQL = "SELECT * FROM CRM_RCL_INFO"
                       + " WHERE PRODUCT_TIME_START >=to_date('" + strStartTime + "','yyyy-mm-dd')"
                       + " AND PRODUCT_TIME_START <=to_date('" + strEndTime + "','yyyy-mm-dd')";

                if (SteelGrade.Length > 0)
                    strSQL = strSQL + " AND STEEL_GRADE ='" + SteelGrade + "'";

                //依照产品生产时间排序
                strSQL = strSQL + " ORDER BY in_mat_Id_1";
            }

            if (WorkProcess == "CSL")
            {//横切线
                strSQL = "SELECT * FROM CRM_CSL_INFO"
                       + " WHERE PROD_TIME_START >=to_date('" + strStartTime + "','yyyy-mm-dd')"
                       + " AND   PROD_TIME_START <=to_date('" + strEndTime + "','yyyy-mm-dd')";

                if (SteelGrade.Length > 0)
                    strSQL = strSQL + " AND STEEL_GRADE ='" + SteelGrade + "'";

                //依照产品生产时间排序
                strSQL = strSQL + " ORDER BY in_mat_id_1";
            }


            //如果没有在这些工序中，返回空
            if (strSQL == "") return LST;

            //读出数据
            DataTable dt = sqt.ReadDatatable_OraDB(strSQL);
            
            //逐行写入列表
            for (int RowIndex = 0; RowIndex < dt.Rows.Count; RowIndex++)
            {
                CRMCoilTrack lst = new CRMCoilTrack();

                ///等待全部的物料谱系表建立后，才能统一到PROCESS_MAT_PEDIGREE中去
                if (WorkProcess == "PLTCM")
                {//酸轧线
                    lst.IN_MAT_LIST = dt.Rows[RowIndex]["in_mat_no_1"].ToString();
                    string in_mat_no_2 = dt.Rows[RowIndex]["in_mat_no_2"].ToString();
                    if (in_mat_no_2.Trim().Length > 0)
                    {
                        lst.IN_MAT_LIST = lst.IN_MAT_LIST + "," + in_mat_no_2;
                    }

                    lst.OUT_MAT_LIST = dt.Rows[RowIndex]["out_mat_no"].ToString();

                    //钢种
                    lst.SteelGrade = dt.Rows[RowIndex]["ST_NO"].ToString();

                    lst.START_TIME = dt.Rows[RowIndex]["START_TIME"].ToString();
                    lst.STOP_TIME = dt.Rows[RowIndex]["STOP_TIME"].ToString();
                }

                if (WorkProcess == "AF")
                {//罩式炉
                    lst.IN_MAT_LIST = dt.Rows[RowIndex]["in_mat"].ToString();                    
                    lst.OUT_MAT_LIST = dt.Rows[RowIndex]["out_mat"].ToString();

                    //钢种
                    lst.SteelGrade = "";

                    lst.START_TIME = dt.Rows[RowIndex]["on_base"].ToString();
                    lst.STOP_TIME = dt.Rows[RowIndex]["annealed"].ToString();
                }

                if (WorkProcess == "CGL")
                {//镀锌线
                    lst.IN_MAT_LIST = dt.Rows[RowIndex]["in_mat"].ToString();
                    lst.OUT_MAT_LIST = dt.Rows[RowIndex]["out_mat"].ToString();

                    //钢种
                    lst.SteelGrade = "";

                    lst.START_TIME = dt.Rows[RowIndex]["prod_start"].ToString();
                    lst.STOP_TIME = dt.Rows[RowIndex]["prod_end"].ToString();
                }


                if (WorkProcess == "SPM")
                {//平整线
                    lst.IN_MAT_LIST = dt.Rows[RowIndex]["entry_mat_no"].ToString();


                    lst.OUT_MAT_LIST = dt.Rows[RowIndex]["exit_mat_no"].ToString();

                    //钢种
                    lst.SteelGrade = dt.Rows[RowIndex]["ST_GRADE"].ToString();

                    lst.START_TIME = dt.Rows[RowIndex]["prod_time_start"].ToString();
                    lst.STOP_TIME = dt.Rows[RowIndex]["prod_time_end"].ToString();
                }

                if (WorkProcess == "RCL")
                {//平整线
                    lst.IN_MAT_LIST = dt.Rows[RowIndex]["in_mat_id_1"].ToString();
                    string in_mat_id_2 = dt.Rows[RowIndex]["in_mat_id_2"].ToString();
                    if (in_mat_id_2.Trim().Length > 0)
                    {
                        lst.IN_MAT_LIST = lst.IN_MAT_LIST + "," + in_mat_id_2;
                    }

                    lst.OUT_MAT_LIST = dt.Rows[RowIndex]["out_mat_id"].ToString();

                    //钢种
                    lst.SteelGrade = dt.Rows[RowIndex]["STeel_GRADE"].ToString();

                    lst.START_TIME = dt.Rows[RowIndex]["PRODUCT_TIME_START"].ToString();
                    lst.STOP_TIME = dt.Rows[RowIndex]["PRODUCT_TIME_end"].ToString();
                }

                if (WorkProcess == "CSL")
                {//横切线
                    lst.IN_MAT_LIST = dt.Rows[RowIndex]["in_mat_id_1"].ToString();
                    string in_mat_id_2 = dt.Rows[RowIndex]["in_mat_id_2"].ToString();
                    if (in_mat_id_2.Trim().Length > 0) lst.IN_MAT_LIST = lst.IN_MAT_LIST + "," + in_mat_id_2;

                    lst.OUT_MAT_LIST = dt.Rows[RowIndex]["out_mat_id"].ToString();

                    //钢种
                    lst.SteelGrade = dt.Rows[RowIndex]["STeel_GRADE"].ToString();

                    lst.START_TIME = dt.Rows[RowIndex]["PROD_TIME_START"].ToString();
                    lst.STOP_TIME = dt.Rows[RowIndex]["PROD_TIME_end"].ToString();
                }

                //
                LST.Add(lst);

            }
            return LST;
        }

        public List<String> GetCRM_STEELGRADE()
        {//从bof_heat中获取钢种列表信息

            List<String> list = new List<string>();

            string strSQL = "SELECT DISTINCT steel_grade FROM bof_heat WHERE steel_grade IS NOT NULL ORDER BY steel_grade";
            DataTable dt = sqt.ReadDatatable_OraDB(strSQL);
            for (int RowIndex = 0; RowIndex < dt.Rows.Count; RowIndex++)
            {
                string str = dt.Rows[RowIndex][0].ToString().Trim();
                if (str.Length > 0) list.Add(str);
            }
            return list;
        }

       /// <summary>
       /// 获取某个物料的生产历程
       /// </summary>
        public string GetMat(string CoilID,string Process,string InOrOut)
        { 
            string ProcessList="";

            string strSQL = "SELECT ";
            //从哪个工序开始查找


            //是入口还是出口的物料

            return ProcessList;
        }

        public List<CRMCoilTrack> GetCRMCoilTrack(string CoilID)
        {//获取指定炉号列表信息
            List<CRMCoilTrack> LST = new List<CRMCoilTrack>();

            //
            string strSQL = "SELECT * FROM CRM_PLTCM_REPORT"
                          + " WHERE mat_no='" + CoilID + "'";

            DataTable dt = sqt.ReadDatatable_OraDB(strSQL);
                       

            return LST;
        }
               

        //获取钢卷在罩式炉内的历史数据
        public List<CRM_HisDB_AF> GetCRM_HisDB_AF(string AFID, string StartDateTime, string EndDateTime)
        {
            //获取结晶器的冷却历程数据
            List<CRM_HisDB_AF> LST = new List<CRM_HisDB_AF>();
            CRM_HisDB_AF lst = new CRM_HisDB_AF();


            object obj = new object();
            string[] tags = new string[6];
            if (1 == AFID.Length) AFID = "0" + AFID;
            tags[00] = "LYQCRM.AF_BASE_" + AFID + ".CTL_TEMP_ACT";//	实际内罩温度         
            tags[01] = "LYQCRM.AF_BASE_" + AFID + ".HH_TEMP_ACT";//	实际加热罩温度        
            tags[02] = "LYQCRM.AF_BASE_" + AFID + ".H2_FLOW_RATE_ACT";//实际氢气流量       
            tags[03] = "LYQCRM.AF_BASE_" + AFID + ".N2_FLOW_RATE_ACT";//实际氮气流量             
            tags[04] = "LYQCRM.AF_BASE_" + AFID + ".O2_ACT";//氧含量                     
            tags[05] = "LYQCRM.AF_BASE_" + AFID + ".RECIRC_RPM_ACT";//	循环风机实际转速
                    

            //获取历史数据
            System.Data.DataTable dt = sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(StartDateTime), Convert.ToDateTime(EndDateTime));//,60000
            //起始时间
            DateTime TimeStart = Convert.ToDateTime(StartDateTime);

            for (int I = 0; I < dt.Rows.Count; I++)
            {
                lst = new CRM_HisDB_AF();

                lst.DateAndTime = Convert.ToDateTime(dt.Rows[I][00]);
                TimeSpan ts = Convert.ToDateTime(lst.DateAndTime) - TimeStart;
                lst.Duration = (float)ts.TotalMilliseconds / 60000;

                //dt.Rows[I][00]是时间列

                obj = dt.Rows[I][01]; if (obj.ToString().Length > 0) lst.CTL_TEMP_ACT = Convert.ToSingle(obj);         
                obj = dt.Rows[I][02]; if (obj.ToString().Length > 0) lst.HH_TEMP_ACT = Convert.ToSingle(obj);        
                obj = dt.Rows[I][03]; if (obj.ToString().Length > 0) lst.H2_FLOW_RATE_ACT = Convert.ToSingle(obj);       
                obj = dt.Rows[I][04]; if (obj.ToString().Length > 0) lst.N2_FLOW_RATE_ACT = Convert.ToSingle(obj);             
                obj = dt.Rows[I][05]; if (obj.ToString().Length > 0) lst.O2_ACT = Convert.ToSingle(obj);
                obj = dt.Rows[I][06]; if (obj.ToString().Length > 0) lst.RECIRC_RPM_ACT = Convert.ToSingle(obj);
                LST.Add(lst);
            }
            dt.Dispose();

            return LST;
        }

        //获取罩式退火炉AF的pdf报告
        public void GetReportPdf_AF(string CoilID, ref  iTextSharp.text.Document pdfDocument)
        {
            //新开一个页面
            pdfDocument.newPage();

            if (null == FontHei) DefinePdfFont();
            FontHei.Size = 16;
            iTextSharp.text.Paragraph para = new iTextSharp.text.Paragraph(CoilID + "钢卷 罩式退火炉数据追踪",FontHei);
            para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfDocument.Add(para);


            //基本信息了
            pdfDocument.Add(new Paragraph(" "));//空行

            FontHei.Size = 14;
            para = new iTextSharp.text.Paragraph("炉次基本信息", FontHei);
            para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfDocument.Add(para);

            //以表形式来写入必须的信息
            iTextSharp.text.Table table = new iTextSharp.text.Table(6);
            table.WidthPercentage = 100; // 百分比
            table.Padding = 5;
            int[] headerwidths = { 5, 10, 5, 10, 5, 10}; // 百分比
            table.setWidths(headerwidths);
            table.AutoFillEmptyCells = true;
            //设置边框颜色 
            table.BorderColor = new iTextSharp.text.Color(0, 0, 0);
            table.DefaultCellBorderColor = new iTextSharp.text.Color(0, 0, 255);
            iTextSharp.text.Color Color_DescCell = new iTextSharp.text.Color(System.Drawing.Color.LightGray);
            table.DefaultCellBackgroundColor = new iTextSharp.text.Color(System.Drawing.Color.White);

            table.DefaultVerticalAlignment = Element.ALIGN_MIDDLE;
            table.DefaultHorizontalAlignment = Element.ALIGN_CENTER;

            FontKai.Size = 9; FontSong.Size = 9;
            iTextSharp.text.Cell cell;


            //获取基本信息
            List<CRM_CoilInfo_AF>LST= GetCRM_CoilInfo_AF(CoilID);
            if (LST.Count == 0)
            {
                para = new iTextSharp.text.Paragraph("没有找到数据", FontSong);
                para.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
                pdfDocument.Add(para);

                return;
            }

            //这里开始写基本信息
            cell = new iTextSharp.text.Cell(new Paragraph("钢卷号", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(CoilID, FontSong)); table.addCell(cell);

            cell = new iTextSharp.text.Cell(new Paragraph("基座号", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(LST[0].base_id, FontSong)); table.addCell(cell);

            cell = new iTextSharp.text.Cell(new Paragraph("", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("", FontSong)); table.addCell(cell);

            cell = new iTextSharp.text.Cell(new Paragraph("进入", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(LST[0].on_base, FontSong)); table.addCell(cell);

            cell = new iTextSharp.text.Cell(new Paragraph("出炉", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(LST[0].annealed, FontSong)); table.addCell(cell);

            //把表写入文档
            pdfDocument.Add(table);

            //加热数据
            GetReportPdf_HisDB_AF_Heat(CoilID, LST[0].base_id, LST[0].on_base, LST[0].annealed, ref pdfDocument);
            //气体流量数据
            GetReportPdf_HisDB_AF_GasFlux(CoilID, LST[0].base_id, LST[0].on_base, LST[0].annealed, ref pdfDocument);
            //氧气含量，风扇转速
            GetReportPdf_HisDB_AF_OxyRota(CoilID, LST[0].base_id, LST[0].on_base, LST[0].annealed, ref pdfDocument);
            
        }


        //获取 酸洗 的pdf报告
        public void GetReportPdf_PL(string CoilID, ref  iTextSharp.text.Document pdfDocument)
        {
            //竖向的A4纸，若为PageSize.A4.rotate()则是横向的。
            pdfDocument.setPageSize(PageSize.A4);

            //新开一个页面
            pdfDocument.newPage();

            //如果没有定义过字体，则去定义一番
            if (null == FontHei) DefinePdfFont();

            iTextSharp.text.Paragraph para = new iTextSharp.text.Paragraph(CoilID + "钢卷 酸洗数据追踪", FontHei);
            para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfDocument.Add(para);


            //基本信息了
            pdfDocument.Add(new Paragraph(" "));//空行

            FontHei.Size = 14;
            para = new iTextSharp.text.Paragraph("钢卷基本信息", FontHei);
            para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfDocument.Add(para);

            //以表形式来写入必须的信息
            iTextSharp.text.Table table = new iTextSharp.text.Table(10);
            table.WidthPercentage = 100; // 百分比
            table.Padding = 5;
            int[] headerwidths = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 }; // 百分比
            table.setWidths(headerwidths);
            table.AutoFillEmptyCells = true;
            //设置边框颜色 
            table.BorderColor = new iTextSharp.text.Color(0, 0, 0);
            table.DefaultCellBorderColor = new iTextSharp.text.Color(0, 0, 255);
            iTextSharp.text.Color Color_DescCell = new iTextSharp.text.Color(System.Drawing.Color.LightGray);
            table.DefaultCellBackgroundColor = new iTextSharp.text.Color(System.Drawing.Color.White);

            table.DefaultVerticalAlignment = Element.ALIGN_MIDDLE;
            table.DefaultHorizontalAlignment = Element.ALIGN_CENTER;

            FontKai.Size = 9; FontSong.Size = 9;
            iTextSharp.text.Cell cell;


            //获取基本信息
            List<CRMCoilTrack> lst = GetCRMCoilTrack(CoilID);
            if (lst.Count > 0)
            {
                //这里开始写基本信息
                cell = new iTextSharp.text.Cell(new Paragraph("钢卷号", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(CoilID, FontSong)); table.addCell(cell);
            }

            //把表写入文档
            pdfDocument.Add(table);
            
              //获取 酸洗 的pdf报告
            GetReportPdf_PL_KeyEvents(CoilID, ref pdfDocument);
           
        }

        //获取 镀锌线 的pdf报告
        public void GetReportPdf_CGL(string CoilID, ref  iTextSharp.text.Document pdfDocument)
        {
            //竖向的A4纸，若为PageSize.A4.rotate()则是横向的。
            pdfDocument.setPageSize(PageSize.A4);

            //新开一个页面
            pdfDocument.newPage();

            //如果没有定义过字体，则去定义一番
            if (null == FontHei) DefinePdfFont();
                         
            //标题
            FontHei.Size = 16;
            iTextSharp.text.Paragraph para = new iTextSharp.text.Paragraph(CoilID + "钢卷 镀锌工艺数据追踪", FontHei);           
            iTextSharp.text.Chapter chapter = new iTextSharp.text.Chapter(para,0);
            pdfDocument.Add(chapter);
            
            FontHei.Size = 14;
            para = new iTextSharp.text.Paragraph("钢卷基本信息", FontHei);
            para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfDocument.Add(para);

            //以表形式来写入必须的信息
            iTextSharp.text.Table table = new iTextSharp.text.Table(6);
            table.WidthPercentage = 100; // 百分比
            table.Padding = 2;
            int[] headerwidths = { 5, 10, 5, 10, 5, 10 }; // 百分比
            table.setWidths(headerwidths);
            table.AutoFillEmptyCells = true;
            //设置边框颜色 
            table.BorderColor = new iTextSharp.text.Color(0, 0, 0);
            table.DefaultCellBorderColor = new iTextSharp.text.Color(0, 0, 255);
            iTextSharp.text.Color Color_DescCell = new iTextSharp.text.Color(System.Drawing.Color.LightGray);
            table.DefaultCellBackgroundColor = new iTextSharp.text.Color(System.Drawing.Color.White);

            table.DefaultVerticalAlignment = Element.ALIGN_MIDDLE;
            table.DefaultHorizontalAlignment = Element.ALIGN_CENTER;

            FontKai.Size = 9; FontSong.Size = 9;
            iTextSharp.text.Cell cell;

            //获取基本信息
            List<CRM_CoilInfo_CGL> LST = GetCRM_CoilInfo_CGL(CoilID);
            if (LST.Count > 0)
            {
                //这里开始写基本信息
                cell = new iTextSharp.text.Cell(new Paragraph("班次", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].SHIFT_NO, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("班别", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].SHIFT_CREW, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("班长", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("", FontSong)); table.addCell(cell);
                
                cell = new iTextSharp.text.Cell(new Paragraph("PCOIL_SID", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].PCOIL_SID, FontSong)); table.addCell(cell);                               
                cell = new iTextSharp.text.Cell(new Paragraph("MAT_IDENT", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].OUT_MAT, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("EMAT_IDENT", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].IN_MAT, FontSong)); table.addCell(cell);
                
                cell = new iTextSharp.text.Cell(new Paragraph("钢种", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].STEEL_GRADE, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("生产日期", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].PROD_DAY, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("", FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("开始", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].PROD_START, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("结束", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].PROD_END, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("用时", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].PROD_DURATION, FontSong)); table.addCell(cell);
                
                cell = new iTextSharp.text.Cell(new Paragraph("卷重", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].COIL_WEIGHT, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("WEIGHT", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].WEIGHT, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("废钢重", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].SCRAP_WEIGTH, FontSong)); table.addCell(cell);
                                
                cell = new iTextSharp.text.Cell(new Paragraph("长度", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].LENGTH, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("宽度", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].WIDTH, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("厚度", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].THICKNESS, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("计划长度", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("", FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("计划宽度", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].E_WIDTH, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("计划厚度", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].E_THICKNESS, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("镀层上限", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].PLATE_WEIGHT_UPPER, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("镀层下限", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].PLATE_WEIGHT_LOWER, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("镀层总重", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].PLATE_WEIGHT_TOTAL, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("内径", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].DIAM_INNER, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("外径", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("", FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("", FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("光整延伸率", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].SPM_ELONGATION_AVG, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("TLEV_延伸率", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].TLEV_ELONGATION_AVG, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("HOLD_FLAG", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].HOLD_FLAG, FontSong)); table.addCell(cell);
                              
                cell = new iTextSharp.text.Cell(new Paragraph("热处理", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].HEAT_CYCLE_CODE, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("OIL_TYPE_CODE", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].OIL_TYPE_CODE, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("", FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("表面级别", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].SURF_GROUP, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("表面处理", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(LST[0].SURF_TREAT, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("", FontSong)); table.addCell(cell);
                

            }

            //把表写入文档
            pdfDocument.Add(table);


            //厚度
            GetReportPdf_HisDB_CGL_thick(CoilID, LST[0].PROD_START, LST[0].PROD_END, ref pdfDocument);
            //镀层重量 
            GetReportPdf_HisDB_CGL_CoatingWeight(CoilID, LST[0].PROD_START, LST[0].PROD_END, ref pdfDocument);
            //感应器功率
            GetReportPdf_HisDB_CGL_GI_POWER(CoilID, LST[0].PROD_START, LST[0].PROD_END, ref pdfDocument);
            //锌锅温度
            GetReportPdf_HisDB_CGL_GI_Tempture(CoilID, LST[0].PROD_START, LST[0].PROD_END, ref pdfDocument);
            //张力 *******//
            GetReportPdf_HisDB_CGL_GI_Tension(CoilID, LST[0].PROD_START, LST[0].PROD_END, ref pdfDocument);

            //光整机
            GetReportPdf_HisDB_CGL_SPM_ParaA(CoilID, LST[0].PROD_START, LST[0].PROD_END, ref pdfDocument);
            GetReportPdf_HisDB_CGL_SPM_ParaA(CoilID, LST[0].PROD_START, LST[0].PROD_END, ref pdfDocument);
        }


        //***** 冷轧厂--镀锌线--厚度 *******//
        public void GetReportPdf_HisDB_CGL_thick(string CoilID,  string StartTime, string EndTime, ref  iTextSharp.text.Document pdfDocument)
        {
            //新页面
            //pdfDocument.newPage();

            //定义
            ClsDuHisPic.StuDrawPicInfo stuDrawPicInfo = new ClsDuHisPic.StuDrawPicInfo();
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = false ;
            stuDrawPicInfo.YAxisDisplayWhenDrawOnly1YAxis = "带钢厚度,mm";
            stuDrawPicInfo.IsDisplayRelativeTime = true;

            int YAxisNo = 1;
            stuDrawPicInfo.TagDescription[YAxisNo] = "1#TR";
            stuDrawPicInfo.TagUnit[YAxisNo] = "mm";
            stuDrawPicInfo.TagDecimalPlaces[YAxisNo] = 2;
            stuDrawPicInfo.TagFormat[YAxisNo] = "#0.00";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = true;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = false;            
            stuDrawPicInfo.TagMin[YAxisNo] = 0;
            stuDrawPicInfo.TagMax[YAxisNo] = 3;

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "2#TR";
            stuDrawPicInfo.TagUnit[YAxisNo] = "mm";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = false;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.TagMin[YAxisNo] = 0;
            stuDrawPicInfo.TagMax[YAxisNo] = 1000;

            //绘图的数据
            string[] tags = new string[2];
            tags[00] = "LYQCRM.CGL_ZIN.Act_thick_1TR_Len30";//LYQCRM.CGL_ZIN	成品带钢厚度(1#TR)
            tags[01] = "LYQCRM.CGL_ZIN.Act_thick_2TR_Len30";//LYQCRM.CGL_ZIN	成品带钢厚度(2#TR)

            //获取历史数据
            stuDrawPicInfo.dt = sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(StartTime), Convert.ToDateTime(EndTime));
            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(CoilID + "号钢卷 镀锌线 钢带厚度", stuDrawPicInfo, ref pdfDocument);             
        }

        //***** 冷轧厂--镀锌线--镀层重量 *******//
        public void GetReportPdf_HisDB_CGL_CoatingWeight(string CoilID, string StartTime, string EndTime, ref  iTextSharp.text.Document pdfDocument)
        {
            //新页面
            //pdfDocument.newPage();

            //定义
            ClsDuHisPic.StuDrawPicInfo stuDrawPicInfo = new ClsDuHisPic.StuDrawPicInfo();
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = false;
            stuDrawPicInfo.YAxisDisplayWhenDrawOnly1YAxis = "镀层重量";
            stuDrawPicInfo.IsDisplayRelativeTime = true;

            int YAxisNo = 1;
            stuDrawPicInfo.TagDescription[YAxisNo] = "上表面左";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "上表面中";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "上表面右";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "下表面左";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "下表面中";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "下表面右";          

            //绘图的数据
            string[] tags = new string[6];                        
            tags[00] = "LYQCRM.CGL_ZIN.FW_Act_coating_weight_Len30";//LYQCRM.CGL_ZIN	实际镀层上面左
            tags[01] = "LYQCRM.CGL_ZIN.FC_Act_coating_weight_Len30";//LYQCRM.CGL_ZIN	实际镀层上面中
            tags[02] = "LYQCRM.CGL_ZIN.FD_Act_coating_weight_Len30";//LYQCRM.CGL_ZIN	实际镀层上面右
            tags[03] = "LYQCRM.CGL_ZIN.BW_Act_coating_weight_Len30";//LYQCRM.CGL_ZIN	实际镀层下面左 
            tags[04] = "LYQCRM.CGL_ZIN.BC_Act_coating_weight_Len30";//LYQCRM.CGL_ZIN	实际镀层下面中
            tags[05] = "LYQCRM.CGL_ZIN.BDW_Act_coating_weight_Len30";//LYQCRM.CGL_ZIN   实际镀层下面右

            //获取历史数据
            stuDrawPicInfo.dt = sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(StartTime), Convert.ToDateTime(EndTime));
            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(CoilID + "号钢卷 镀锌线 镀层重量", stuDrawPicInfo, ref pdfDocument); 
        }

        //***** 冷轧厂--镀锌线--锌锅功率 *******//
        public void GetReportPdf_HisDB_CGL_GI_POWER(string CoilID, string StartTime, string EndTime, ref  iTextSharp.text.Document pdfDocument)
        {
            //定义
            ClsDuHisPic.StuDrawPicInfo stuDrawPicInfo = new ClsDuHisPic.StuDrawPicInfo();
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = false;
            stuDrawPicInfo.YAxisDisplayWhenDrawOnly1YAxis = "感应器功率";
            stuDrawPicInfo.IsDisplayRelativeTime = true;

            int YAxisNo = 1;
            stuDrawPicInfo.TagDescription[YAxisNo] = "预熔锅";

            YAxisNo ++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "主锅";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "锌锅A";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "锌锅B";

            //绘图的数据
            string[] tags = new string[4];
            tags[00] = "LYQCRM.CGL_ZIN.GI_YUzinc_POW_Len25";//LYQCRM.CGL_ZIN	GL预熔锅感应器功率
            tags[01] = "LYQCRM.CGL_ZIN.GI_mainzinc_POW_Len25";//LYQCRM.CGL_ZIN	GL主锅感应器功率
            tags[02] = "LYQCRM.CGL_ZIN.GI_AZINC_POWER_Len25";//LYQCRM.CGL_ZIN	GI锌锅A感应器功率
            tags[03] = "LYQCRM.CGL_ZIN.GI_BZINC_POWER_Len25";//LYQCRM.CGL_ZIN	GI锌锅B感应器功率
            
            //获取历史数据
            stuDrawPicInfo.dt = sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(StartTime), Convert.ToDateTime(EndTime));
            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(CoilID + "号钢卷 镀锌线 感应器功率", stuDrawPicInfo, ref pdfDocument); 
        }

        //***** 冷轧厂--镀锌线--锌锅温度 *******//
        public void GetReportPdf_HisDB_CGL_GI_Tempture(string CoilID, string StartTime, string EndTime, ref  iTextSharp.text.Document pdfDocument)
        {
            //定义
            ClsDuHisPic.StuDrawPicInfo stuDrawPicInfo = new ClsDuHisPic.StuDrawPicInfo();
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = false;
            stuDrawPicInfo.YAxisDisplayWhenDrawOnly1YAxis = "锌锅温度,℃";
            stuDrawPicInfo.IsDisplayRelativeTime = true;

            int YAxisNo = 1;
            stuDrawPicInfo.TagDescription[YAxisNo] = "预熔锅";
            stuDrawPicInfo.AutoSetRang[YAxisNo] = false;
            stuDrawPicInfo.TagMin[YAxisNo]=400;
            stuDrawPicInfo.TagMax[YAxisNo] = 700;

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "主锅";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "锌锅A";
                       

            //绘图的数据
            string[] tags = new string[3];
            tags[00] = "LYQCRM.CGL_ZIN.GI_yuzinc_temLen25";//LYQCRM.CGL_ZIN	GL预熔锅温度
            tags[01] = "LYQCRM.CGL_ZIN.GI_mainzinc_temLen25";//LYQCRM.CGL_ZIN	GL主锌锅温度
            tags[02] = "LYQCRM.CGL_ZIN.GI_zinc_pot_temLen25";//LYQCRM.CGL_ZIN	GI锌锅温度

            //获取历史数据
            stuDrawPicInfo.dt = sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(StartTime), Convert.ToDateTime(EndTime));
            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(CoilID +"钢卷 镀锌线 锌锅温度", stuDrawPicInfo, ref pdfDocument);
        }

        //***** 冷轧厂--镀锌线--张力 *******//
        public void GetReportPdf_HisDB_CGL_GI_Tension(string CoilID, string StartTime, string EndTime, ref  iTextSharp.text.Document pdfDocument)
        {
            //定义
            ClsDuHisPic.StuDrawPicInfo stuDrawPicInfo = new ClsDuHisPic.StuDrawPicInfo();
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = false;
            stuDrawPicInfo.YAxisDisplayWhenDrawOnly1YAxis = "锌锅段张力,";
            stuDrawPicInfo.IsDisplayRelativeTime = true;

            int YAxisNo = 1;
            stuDrawPicInfo.TagDescription[YAxisNo] = "锌锅段张力";

            //绘图的数据
            string[] tags = new string[3];           
            tags[00] = "LYQCRM.CGL_ZIN.Zinc_Tension_Len25";//LYQCRM.CGL_ZIN	锌锅段张力

            //获取历史数据
            stuDrawPicInfo.dt = sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(StartTime), Convert.ToDateTime(EndTime));

            sqt.DrawstuDrawPicInfo2pdfDocument(CoilID + "钢卷 镀锌线 锌锅段张力", stuDrawPicInfo, ref pdfDocument);
        }


        //***** 冷轧厂--镀锌线--光整机 *******//
        public void GetReportPdf_HisDB_CGL_SPM_ParaA(string CoilID, string StartTime, string EndTime, ref  iTextSharp.text.Document pdfDocument)
        {
            //定义
            ClsDuHisPic.StuDrawPicInfo stuDrawPicInfo = new ClsDuHisPic.StuDrawPicInfo();
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = true;
            //stuDrawPicInfo.YAxisDisplayWhenDrawOnly1YAxis = "锌锅段张力,";
            stuDrawPicInfo.IsDisplayRelativeTime = true;

            int YAxisNo = 1;
            stuDrawPicInfo.TagDescription[YAxisNo] = "中央段速度";
            stuDrawPicInfo.TagUnit[YAxisNo] = "";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = true;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "延伸率";
            stuDrawPicInfo.TagUnit[YAxisNo] = "";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = true;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "防皱辊高度";
            stuDrawPicInfo.TagUnit[YAxisNo] = "";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = true;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "防颤辊高度";
            stuDrawPicInfo.TagUnit[YAxisNo] = "";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = true;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "DS工作侧辊缝值";
            stuDrawPicInfo.TagUnit[YAxisNo] = "";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = true;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "WS工作侧辊缝值";
            stuDrawPicInfo.TagUnit[YAxisNo] = "";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = true;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;

            //绘图的数据
            string[] tags = new string[6];
            
            tags[00] = "LYQCRM.CGL_SPM.Process_Act_Speed_Len30";    //LYQCRM.CGL_SPM	中央段速度
            tags[01] = "LYQCRM.CGL_SPM.SPM_elongation_Len31";   //LYQCRM.CGL_SPM	光整机延伸率

            tags[02] = "LYQCRM.CGL_SPM.SPM_Anti_crease_height_Len31";   //LYQCRM.CGL_SPM	光整机防皱辊高度
            tags[03] = "LYQCRM.CGL_SPM.SPM_Anti_quiver_height_Len31";   //LYQCRM.CGL_SPM	光整机防颤辊高度

            tags[04] = "LYQCRM.CGL_SPM.SPM_DS_gap_Len31";   //LYQCRM.CGL_SPM	光整机工作侧辊缝值
            tags[05] = "LYQCRM.CGL_SPM.SPM_WS_gap_Len31";   //LYQCRM.CGL_SPM	光整机工作侧辊缝值


            //获取历史数据
            stuDrawPicInfo.dt = sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(StartTime), Convert.ToDateTime(EndTime));

            sqt.DrawstuDrawPicInfo2pdfDocument(CoilID + "钢卷 镀锌线 光整机参数", stuDrawPicInfo, ref pdfDocument);
        }

        //***** 冷轧厂--镀锌线--光整机 *******//
        public void GetReportPdf_HisDB_CGL_SPM_ParaB(string CoilID, string StartTime, string EndTime, ref  iTextSharp.text.Document pdfDocument)
        {
            //定义
            ClsDuHisPic.StuDrawPicInfo stuDrawPicInfo = new ClsDuHisPic.StuDrawPicInfo();
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = false ;
            stuDrawPicInfo.YAxisDisplayWhenDrawOnly1YAxis = "锌锅段张力,";
            stuDrawPicInfo.IsDisplayRelativeTime = true;

            int YAxisNo = 1;
            stuDrawPicInfo.TagDescription[YAxisNo] = "弯辊力";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "DS工作侧轧制力";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "入口张力";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "出口张力";    

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "轧制力";
            stuDrawPicInfo.TagUnit[YAxisNo] = "";   

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "WS工作侧轧制力";
        

            //绘图的数据
            string[] tags = new string[6];
            tags[00] = "LYQCRM.CGL_SPM.SPM_bending_force_Len31";    //LYQCRM.CGL_SPM	光整机弯辊力
            tags[01] = "LYQCRM.CGL_SPM.SPM_DS_force_Len31"; //LYQCRM.CGL_SPM	光整机工作侧轧制力            
            tags[02] = "LYQCRM.CGL_SPM.SPM_entry_Tension_Len31";    //LYQCRM.CGL_SPM	光整机入口张力
            tags[03] = "LYQCRM.CGL_SPM.SPM_exit_Tension_Len31"; //LYQCRM.CGL_SPM	光整机出口张力
            tags[04] = "LYQCRM.CGL_SPM.SPM_rolling_force_Len31";    //LYQCRM.CGL_SPM	光整机轧制力
            tags[05] = "LYQCRM.CGL_SPM.SPM_WS_force_Len31"; //LYQCRM.CGL_SPM	光整机工作侧轧制力

            //获取历史数据
            stuDrawPicInfo.dt = sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(StartTime), Convert.ToDateTime(EndTime));

            sqt.DrawstuDrawPicInfo2pdfDocument(CoilID + "钢卷 镀锌线 光整机参数", stuDrawPicInfo, ref pdfDocument);
        }


        //获取 酸洗 的pdf报告
        public void GetReportPdf_PL_KeyEvents(string CoilID, ref  iTextSharp.text.Document pdfDocument)
        {

            FontHei.Size = 14;
            iTextSharp.text.Paragraph para = new iTextSharp.text.Paragraph(CoilID + "钢卷 酸洗过程关键事件表", FontHei);
            para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfDocument.Add(para);

            //以表形式来写入必须的信息
            iTextSharp.text.Table table = new iTextSharp.text.Table(3);
            table.WidthPercentage = 100; // 百分比
            table.Padding = 3;
            int[] HeaderWidths = { 30,30,40}; // 百分比
            table.setWidths(HeaderWidths);
            table.AutoFillEmptyCells = true;
            //设置边框颜色 
            table.BorderColor = new iTextSharp.text.Color(0, 255, 255);
            table.DefaultCellBorderColor = new iTextSharp.text.Color(255, 0, 255);

            table.DefaultVerticalAlignment = Element.ALIGN_MIDDLE;
            table.DefaultHorizontalAlignment = Element.ALIGN_CENTER;

            //这里开始写基本信息
            //**标题行
            table.DefaultCellBackgroundColor = new iTextSharp.text.Color(System.Drawing.Color.LightGray);
            iTextSharp.text.Cell cell = new iTextSharp.text.Cell(new Paragraph("时间", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("时间", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("时长,min", FontSong)); table.addCell(cell);            
            cell = new iTextSharp.text.Cell(new Paragraph("事件", FontSong)); table.addCell(cell);
            
            // 表格头行结束
            table.endHeaders();

            //数据行
            table.DefaultCellBackgroundColor = new iTextSharp.text.Color(System.Drawing.Color.White);
            //读出数据
            List<CRM_KeyEvents_PLTCM> lst = GetCRM_KeyEvents_PLTCM(CoilID);

            //酸洗槽进入的时间
            DateTime PL_PC_IN = DateTime.Now, PL_PC_OUT = DateTime.Now;
			//轧机到、出时间,非常的不准
            DateTime TCM_IN = DateTime.Now, TCM_OUT = DateTime.Now;

            for (int I = 0; I < lst.Count; I++)
            {
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].DateAndTime, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].Duration.ToString("#0.0"), FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].Description, FontSong)); table.addCell(cell);

                //进出酸洗池的时间
                if ("PL_EntryTime" == lst[I].KeyEventsName) PL_PC_IN =Convert.ToDateTime( lst[I].DateAndTime);
                if ("PL_ExitTime" == lst[I].KeyEventsName)  PL_PC_OUT =Convert.ToDateTime( lst[I].DateAndTime);

                //进出 轧机的时间	
                if ("COIL_HEAD_ENTRY_CRM" == lst[I].KeyEventsName)  TCM_IN = Convert.ToDateTime(lst[I].DateAndTime);
                if ("COIL_TAIL_ENTRY_CRM" == lst[I].KeyEventsName) TCM_OUT = Convert.ToDateTime(lst[I].DateAndTime);
            }

            //把表写入文档
            pdfDocument.Add(table);

            //***** 冷轧厂--酸洗PL--酸槽数据*******//
            GetReportPdf_HisDB_PL_PT(CoilID, "1", PL_PC_IN, PL_PC_OUT, ref   pdfDocument);
            GetReportPdf_HisDB_PL_PT(CoilID, "2", PL_PC_IN, PL_PC_OUT, ref   pdfDocument);
            GetReportPdf_HisDB_PL_PT(CoilID, "3", PL_PC_IN, PL_PC_OUT, ref   pdfDocument);

            //循环酸罐
            GetReportPdf_HisDB_PL_PC(CoilID, "1", PL_PC_IN, PL_PC_OUT, ref   pdfDocument);
            GetReportPdf_HisDB_PL_PC(CoilID, "2", PL_PC_IN, PL_PC_OUT, ref   pdfDocument);
            GetReportPdf_HisDB_PL_PC(CoilID, "3", PL_PC_IN, PL_PC_OUT, ref   pdfDocument);

            //漂洗槽
            GetReportPdf_HisDB_PL_RT(CoilID, PL_PC_IN, PL_PC_OUT, ref  pdfDocument);

            //冷轧版型参数设置
            GetReportPdf_HisDB_TCM_ASC_ParaSet(CoilID, TCM_IN, TCM_OUT, ref   pdfDocument);
            //平直度
            GetReportPdf_HisDB_TCM_ASC_Iunit(CoilID, 1, 5, TCM_IN, TCM_OUT, ref   pdfDocument);
            GetReportPdf_HisDB_TCM_ASC_Iunit(CoilID, 6, 10, TCM_IN, TCM_OUT, ref   pdfDocument);
            GetReportPdf_HisDB_TCM_ASC_Iunit(CoilID, 11, 15, TCM_IN, TCM_OUT, ref   pdfDocument);
            GetReportPdf_HisDB_TCM_ASC_Iunit(CoilID, 16, 20, TCM_IN, TCM_OUT, ref   pdfDocument);
            GetReportPdf_HisDB_TCM_ASC_Iunit(CoilID, 21, 25, TCM_IN, TCM_OUT, ref   pdfDocument);
            GetReportPdf_HisDB_TCM_ASC_Iunit(CoilID, 26, 31, TCM_IN, TCM_OUT, ref   pdfDocument);
         
            //***** 冷轧厂--冷轧TCM--精轧机--辊缝 *******//
            for (int I = 0; I <= 4; I++)
            {
                GetReportPdf_HisDB_TCM_FR_Gap(CoilID, I.ToString(), TCM_IN, TCM_OUT, ref   pdfDocument);
                GetReportPdf_HisDB_TCM_FR_Para(CoilID, I.ToString(), TCM_IN, TCM_OUT, ref   pdfDocument);
                GetReportPdf_HisDB_TCM_FR_RollingForce(CoilID, I.ToString(), TCM_IN, TCM_OUT, ref   pdfDocument);
                GetReportPdf_HisDB_TCM_FR_Tension(CoilID, I.ToString(), TCM_IN, TCM_OUT, ref   pdfDocument);
            }

        }

        //***** 冷轧厂--酸洗PL--酸槽数据*******//
        public void GetReportPdf_HisDB_PL_PT(string CoilID,string PL_PT_ID, DateTime StartTime, DateTime EndTime, ref  iTextSharp.text.Document pdfDocument)
        {            

            //定义
            ClsDuHisPic.StuDrawPicInfo stuDrawPicInfo = new ClsDuHisPic.StuDrawPicInfo();
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);


            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = true;

            int YAxisNo = 1;
            stuDrawPicInfo.TagDescription[YAxisNo] = "酸罐酸液电导率";
            stuDrawPicInfo.TagUnit[YAxisNo] = "ms/cm";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = true;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = false ;
            stuDrawPicInfo.TagMin[YAxisNo] = 400;
            stuDrawPicInfo.TagMax[YAxisNo] = 1000;

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "酸液浓度";
            stuDrawPicInfo.TagUnit[YAxisNo] = "g/cm3";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = true;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = false ;
            stuDrawPicInfo.TagMin[YAxisNo] = 1000;
            stuDrawPicInfo.TagMax[YAxisNo] = 1500;


            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "酸槽酸流量";
            stuDrawPicInfo.TagUnit[YAxisNo] = "l/min";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = false;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = false ;
            stuDrawPicInfo.TagMin[YAxisNo] = 0;
            stuDrawPicInfo.TagMax[YAxisNo] = 500;

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "废酸流量";
            stuDrawPicInfo.TagUnit[YAxisNo] = "l/min";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = false;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = false;
            stuDrawPicInfo.TagMin[YAxisNo] = 0;
            stuDrawPicInfo.TagMax[YAxisNo] = 500;

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "缓蚀剂流量";
            stuDrawPicInfo.TagUnit[YAxisNo] = "l/min";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = false;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.TagMin[YAxisNo] = 0;
            stuDrawPicInfo.TagMax[YAxisNo] = 1000;

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "酸液温度";
            stuDrawPicInfo.TagUnit[YAxisNo] = "℃";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = true;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = false ;
            stuDrawPicInfo.TagMin[YAxisNo] = 400;
            stuDrawPicInfo.TagMax[YAxisNo] = 1000;

            //绘图的数据
            string[] tags = new string[6];
            tags[00] = "LYQCRM.PL_PT" + PL_PT_ID + ".Conductivity";//	酸罐酸液电导率	LYQCRM.PL_PT1.Conductivity
            tags[01] = "LYQCRM.PL_PT" + PL_PT_ID + ".Density";//	    酸液浓度	LYQCRM.PL_PT1.Density            
            tags[02] = "LYQCRM.PL_PT" + PL_PT_ID + ".EntAcidFlow";//	酸槽酸流量	LYQCRM.PL_PT1.EntAcidFlow
            tags[03] = "LYQCRM.PL_PT" + PL_PT_ID + ".WasterAcidFlow";//	废酸流量	LYQCRM.PL_PT1.WasterAcidFlow            
            tags[04] = "LYQCRM.PL_PT" + PL_PT_ID + ".InhibitorFlow";//	缓蚀剂流量	LYQCRM.PL_PT1.InhibitorFlow
            tags[05] = "LYQCRM.PL_PT" + PL_PT_ID + ".Temperature";//	酸液温度	LYQCRM.PL_PT1.Temperature      


            //获取历史数据
            System.Data.DataTable dt = sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(StartTime), Convert.ToDateTime(EndTime));
            //绘图
            sqt.DrawstuDrawPicInfo2pdfDocument(CoilID + "号钢卷在" + PL_PT_ID + "号清洗酸槽数据", stuDrawPicInfo, ref pdfDocument);             
        }


        //***** 冷轧厂--酸洗PL--循环酸槽数据*******//
        public void GetReportPdf_HisDB_PL_PC(string CoilID, string PL_PC_ID, DateTime StartTime, DateTime EndTime, ref  iTextSharp.text.Document pdfDocument)
        {

            //定义
            ClsDuHisPic.StuDrawPicInfo stuDrawPicInfo = new ClsDuHisPic.StuDrawPicInfo();
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);


            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = true;

            int YAxisNo = 1;
            stuDrawPicInfo.TagDescription[YAxisNo] = "FeCl2";
            stuDrawPicInfo.TagUnit[YAxisNo] = "%";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = true;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = false ;
            stuDrawPicInfo.TagMin[YAxisNo] = 0;
            stuDrawPicInfo.TagMax[YAxisNo] = 300;

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "HCl";
            stuDrawPicInfo.TagUnit[YAxisNo] = "%";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = true;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = false;
            stuDrawPicInfo.TagMin[YAxisNo] = 40;
            stuDrawPicInfo.TagMax[YAxisNo] = 100;

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "液位";
            stuDrawPicInfo.TagUnit[YAxisNo] = "mm";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = false ;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = false ;
            stuDrawPicInfo.TagMin[YAxisNo] = 0;
            stuDrawPicInfo.TagMax[YAxisNo] = 100;


            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "温度";
            stuDrawPicInfo.TagUnit[YAxisNo] = "℃";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = false;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = false ;
            stuDrawPicInfo.TagMin[YAxisNo] = 30;
            stuDrawPicInfo.TagMax[YAxisNo] = 60;

          
                        

            //绘图的数据
            string[] tags = new string[4];           
            tags[00] = "LYQCRM.PL_PC" + PL_PC_ID + ".FECL2";//	酸循环罐FECL2             
            tags[01] = "LYQCRM.PL_PC" + PL_PC_ID + ".HCL";//	酸循环罐HCL
            tags[02] = "LYQCRM.PL_PC" + PL_PC_ID + ".Level";//	酸循环罐液位       
            tags[03] = "LYQCRM.PL_PC" + PL_PC_ID + ".Temperature";//	酸循环罐温度
            


            //获取历史数据
            System.Data.DataTable dt = sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(StartTime), Convert.ToDateTime(EndTime));
            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(CoilID + "号钢卷在" + PL_PC_ID + "号循环酸槽数据", stuDrawPicInfo, ref pdfDocument);             
        }


        //***** 冷轧厂--酸洗PL--漂洗槽数据*******//
        public void GetReportPdf_HisDB_PL_RT(string CoilID,  DateTime StartTime, DateTime EndTime, ref  iTextSharp.text.Document pdfDocument)
        {

            //定义
            ClsDuHisPic.StuDrawPicInfo stuDrawPicInfo = new ClsDuHisPic.StuDrawPicInfo();
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);


            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = true;

            int YAxisNo = 1;
            stuDrawPicInfo.TagDescription[YAxisNo] = "PH值";
            stuDrawPicInfo.TagUnit[YAxisNo] = "/";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = true;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = false;
            stuDrawPicInfo.TagMin[YAxisNo] =0;
            stuDrawPicInfo.TagMax[YAxisNo] = 100;

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "温度";
            stuDrawPicInfo.TagUnit[YAxisNo] = "℃";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = true;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = false;
            stuDrawPicInfo.TagMin[YAxisNo] = 0;
            stuDrawPicInfo.TagMax[YAxisNo] = 100;


            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "加水流量";
            stuDrawPicInfo.TagUnit[YAxisNo] = "l/min";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = false;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = false;
            stuDrawPicInfo.TagMin[YAxisNo] = 0;
            stuDrawPicInfo.TagMax[YAxisNo] = 200;
             
            //绘图的数据
            string[] tags = new string[3];
            tags[00] = "LYQCRM.PL_RT4.PH";//	漂洗槽PH值        
            tags[01] = "LYQCRM.PL_RT4.Temperature";//漂洗槽温度
            tags[02] = "LYQCRM.PL_RT4.WaterFlow";//漂洗槽加水流量	


            //获取历史数据
            System.Data.DataTable dt = sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(StartTime), Convert.ToDateTime(EndTime));
            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(CoilID + "号钢卷在漂洗酸槽数据", stuDrawPicInfo, ref pdfDocument);
        }


        //***** 冷轧厂--冷轧TCM--版型设定参数*******//
        public void GetReportPdf_HisDB_TCM_ASC_ParaSet(string CoilID, DateTime StartTime, DateTime EndTime, ref  iTextSharp.text.Document pdfDocument)
        {
            
            //定义
            ClsDuHisPic.StuDrawPicInfo stuDrawPicInfo = new ClsDuHisPic.StuDrawPicInfo();
                       
            
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);


            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = true;

            int YAxisNo = 1;
            stuDrawPicInfo.TagDescription[YAxisNo] = "板形系数a";
            stuDrawPicInfo.TagUnit[YAxisNo] = "/";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = true;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.TagMin[YAxisNo] = 0;
            stuDrawPicInfo.TagMax[YAxisNo] = 100;

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "板形系数b";
            stuDrawPicInfo.TagUnit[YAxisNo] = "/";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = true;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.TagMin[YAxisNo] = 0;
            stuDrawPicInfo.TagMax[YAxisNo] = 100;


            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "板形系数c";
            stuDrawPicInfo.TagUnit[YAxisNo] = "/";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = false;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.TagMin[YAxisNo] = 0;
            stuDrawPicInfo.TagMax[YAxisNo] = 200;

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "ASC增益";
            stuDrawPicInfo.TagUnit[YAxisNo] = "/";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = false;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.TagMin[YAxisNo] = 0;
            stuDrawPicInfo.TagMax[YAxisNo] = 200;

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "ASC模式";
            stuDrawPicInfo.TagUnit[YAxisNo] = "/";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = false;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            stuDrawPicInfo.TagMin[YAxisNo] = 0;
            stuDrawPicInfo.TagMax[YAxisNo] = 200;

            //绘图的数据
            string[] tags = new string[5];
            tags[00] = "LYQCRM.TCM_ASC.a";//	板形系数a        
            tags[01] = "LYQCRM.TCM_ASC.b";//	板形系数b       
            tags[02] = "LYQCRM.TCM_ASC.c";//	板形系数c       
            tags[03] = "LYQCRM.TCM_ASC.Gain";//	ASC增益
            tags[04] = "LYQCRM.TCM_ASC.Mode";//	ASC模式 


            //获取历史数据
            System.Data.DataTable dt = sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(StartTime), Convert.ToDateTime(EndTime));
            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(CoilID + "号钢卷 冷轧版型参数", stuDrawPicInfo, ref pdfDocument);
        }


        //***** 冷轧厂--冷轧TCM--平直度*******//
        public void GetReportPdf_HisDB_TCM_ASC_Iunit(string CoilID, int StartUnit, int StopUnit, DateTime StartTime, DateTime EndTime, ref  iTextSharp.text.Document pdfDocument)
        {

            //定义
            ClsDuHisPic.StuDrawPicInfo stuDrawPicInfo = new ClsDuHisPic.StuDrawPicInfo();

            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = false;
            stuDrawPicInfo.YAxisDisplayWhenDrawOnly1YAxis = "平直度,um";
            
            for (int I = StartUnit; I <= StopUnit; I++)
            {
                int YAxisNo = I - StartUnit+1;
                stuDrawPicInfo.TagDescription[YAxisNo] =I.ToString ()+ "区平直度";
                stuDrawPicInfo.TagUnit[YAxisNo] = "/";
                stuDrawPicInfo.YAxisIsLeft[YAxisNo] = true;
                stuDrawPicInfo.AutoSetRang[YAxisNo] = true;
            }       

            //绘图的数据
            string[] tags = new string[StopUnit - StartUnit+1];
            for (int I=StartUnit;I<=StopUnit;I++)
            {
                tags[I - StartUnit] = "LYQCRM.TCM_ASC.Iunit" + I.ToString();//	板形仪I区平直度I-unit        
            }              

            //获取历史数据
            stuDrawPicInfo.dt = sqt.ReadDatatable_HisDB(tags, StartTime, EndTime);
            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(CoilID + "号钢卷 " + StartUnit.ToString() + "-" + StopUnit.ToString() + "区版型平直度", stuDrawPicInfo, ref pdfDocument);

        }

        //***** 冷轧厂--冷轧TCM--精轧机--辊缝 *******//
        public void GetReportPdf_HisDB_TCM_FR_Gap(string CoilID, string FR_ID, DateTime StartTime, DateTime EndTime, ref  iTextSharp.text.Document pdfDocument)
        {

            //定义
            ClsDuHisPic.StuDrawPicInfo stuDrawPicInfo = new ClsDuHisPic.StuDrawPicInfo();

            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = false;
            stuDrawPicInfo.YAxisDisplayWhenDrawOnly1YAxis = "辊缝,mm";
                      
            int YAxisNo = 1;
            stuDrawPicInfo.TagDescription[YAxisNo] = "轧机传动侧辊缝";
            stuDrawPicInfo.TagUnit[YAxisNo] = "mm";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = true;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;

            YAxisNo ++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "机架FGC辊缝";
            stuDrawPicInfo.TagUnit[YAxisNo] = "mm";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "中间辊下辊串辊量";
            stuDrawPicInfo.TagUnit[YAxisNo] = "mm";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "中间辊上辊串辊量";
            stuDrawPicInfo.TagUnit[YAxisNo] = "mm";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "工作侧辊缝";
            stuDrawPicInfo.TagUnit[YAxisNo] = "mm";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "出口厚度偏差";
            stuDrawPicInfo.TagUnit[YAxisNo] = "mm";

            //绘图的数据
            string[] tags = new string[6];
            tags[00] = "LYQCRM.TCM_F" + FR_ID + ".DSGap";//	轧机传动侧辊缝(mm)           
            tags[01] = "LYQCRM.TCM_F" + FR_ID + ".FGCGap";//
            tags[02] = "LYQCRM.TCM_F" + FR_ID + ".IMRbShift"  ;//
            tags[03] = "LYQCRM.TCM_F" + FR_ID + ".IMRtShift" ;//
            tags[04] = "LYQCRM.TCM_F" + FR_ID + ".WSGap";// "工作侧辊缝";
            tags[05] = "LYQCRM.TCM_F" + FR_ID + ".ThickDif"; //"出口厚度偏差";
								     	 

            //获取历史数据
            stuDrawPicInfo.dt = sqt.ReadDatatable_HisDB(tags, StartTime, EndTime);
            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(CoilID + "号钢卷 " + FR_ID + "号轧机 辊缝数据", stuDrawPicInfo, ref pdfDocument);
        }

        //***** 冷轧厂--冷轧TCM--精轧机--轧制力 *******//
        public void GetReportPdf_HisDB_TCM_FR_RollingForce(string CoilID, string FR_ID, DateTime StartTime, DateTime EndTime, ref  iTextSharp.text.Document pdfDocument)
        {

            //定义
            ClsDuHisPic.StuDrawPicInfo stuDrawPicInfo = new ClsDuHisPic.StuDrawPicInfo();

            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = false;
            stuDrawPicInfo.YAxisDisplayWhenDrawOnly1YAxis = "轧制力,KN";

            int YAxisNo = 1;
            stuDrawPicInfo.TagDescription[YAxisNo] = "轧制压力";
            stuDrawPicInfo.TagUnit[YAxisNo] = "KN";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = true;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "轧制压力差（WS-DS）";
            stuDrawPicInfo.TagUnit[YAxisNo] = "KN";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "中间辊弯辊力";
            stuDrawPicInfo.TagUnit[YAxisNo] = "KN";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "压下分配量";
            stuDrawPicInfo.TagUnit[YAxisNo] = "KN";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "工作辊弯辊力";
            stuDrawPicInfo.TagUnit[YAxisNo] = "KN";

            //绘图的数据
            string[] tags = new string[5];
            tags[00] = "LYQCRM.TCM_F" + FR_ID + ".RollForce";// 轧制压力          
            tags[01] = "LYQCRM.TCM_F" + FR_ID + ".RFDif" ;// 轧制压力差（WS-DS）
            tags[02] = "LYQCRM.TCM_F" + FR_ID + ".IMRBender";//  中间辊弯辊力"
            tags[03] = "LYQCRM.TCM_F" + FR_ID + ".Reduction";//  压下分配量
            tags[04] = "LYQCRM.TCM_F" + FR_ID + ".WRBender";//  工作辊弯辊力
            

            //获取历史数据
            stuDrawPicInfo.dt = sqt.ReadDatatable_HisDB(tags, StartTime, EndTime);
            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(CoilID + "号钢卷 " + FR_ID + "号轧机 轧制力数据", stuDrawPicInfo, ref pdfDocument);
        }


        //***** 冷轧厂--冷轧TCM--精轧机--轧机间张力 *******//
        public void GetReportPdf_HisDB_TCM_FR_Tension(string CoilID, string FR_ID, DateTime StartTime, DateTime EndTime, ref  iTextSharp.text.Document pdfDocument)
        {

            //定义
            ClsDuHisPic.StuDrawPicInfo stuDrawPicInfo = new ClsDuHisPic.StuDrawPicInfo();

            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = false;
            stuDrawPicInfo.YAxisDisplayWhenDrawOnly1YAxis = "张力,KN";

            int YAxisNo = 1;
            stuDrawPicInfo.TagDescription[YAxisNo] = "轧机间张力";
            stuDrawPicInfo.TagUnit[YAxisNo] = "KN";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = true;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "轧机间张力差（WS-DS）";
            stuDrawPicInfo.TagUnit[YAxisNo] = "KN";

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "轧机单位张力";
            stuDrawPicInfo.TagUnit[YAxisNo] = "KN";
 

            //绘图的数据
            string[] tags = new string[3];
            tags[00] = "LYQCRM.TCM_F" + FR_ID + ".Tension";//  轧机间张力
            tags[01] = "LYQCRM.TCM_F" + FR_ID + ".TensionDif" ;//  轧机间张力差（WS-DS）
            tags[02] = "LYQCRM.TCM_F" + FR_ID + ".UTension";//轧机单位张力
            

            //获取历史数据
            stuDrawPicInfo.dt = sqt.ReadDatatable_HisDB(tags, StartTime, EndTime);
            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(CoilID + "号钢卷 " + FR_ID + "号轧机 张力数据", stuDrawPicInfo, ref pdfDocument); 
        }

        //***** 冷轧厂--冷轧TCM--精轧机--轧制参数 *******//
        public void GetReportPdf_HisDB_TCM_FR_Para(string CoilID, string FR_ID, DateTime StartTime, DateTime EndTime, ref  iTextSharp.text.Document pdfDocument)
        {

            //定义
            ClsDuHisPic.StuDrawPicInfo stuDrawPicInfo = new ClsDuHisPic.StuDrawPicInfo();

            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = true;
            
            int YAxisNo = 1;
            stuDrawPicInfo.TagDescription[YAxisNo] = "主电机电流";
            stuDrawPicInfo.TagUnit[YAxisNo] = "A";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = true;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "机架前滑";
            stuDrawPicInfo.TagUnit[YAxisNo] = "mm";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = true;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "轧机倾斜";
            stuDrawPicInfo.TagUnit[YAxisNo] = "rad";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = true;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "轧机扭矩";
            stuDrawPicInfo.TagUnit[YAxisNo] = "N.M";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = false ;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;

            YAxisNo++;
            stuDrawPicInfo.TagDescription[YAxisNo] = "轧机速度";
            stuDrawPicInfo.TagUnit[YAxisNo] = "m/min";
            stuDrawPicInfo.YAxisIsLeft[YAxisNo] = false;
            stuDrawPicInfo.AutoSetRang[YAxisNo] = true;

            //绘图的数据
            string[] tags = new string[5];
            tags[00] = "LYQCRM.TCM_F" + FR_ID + ".Current" ;//"主电机电流        
            tags[01] = "LYQCRM.TCM_F" + FR_ID + ".Fslip" ;//   "机架前滑 
            tags[02] = "LYQCRM.TCM_F" + FR_ID + ".Leveling"; //轧机倾斜"
            tags[03] = "LYQCRM.TCM_F" + FR_ID + ".Torque";//轧机扭矩 
            tags[04] = "LYQCRM.TCM_F" + FR_ID + ".Speed";//轧机速度
            

            //获取历史数据
            stuDrawPicInfo.dt = sqt.ReadDatatable_HisDB(tags, StartTime, EndTime);
            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(CoilID + "号钢卷 " + FR_ID + "号轧机 轧制参数", stuDrawPicInfo, ref pdfDocument);
        }


        //***** 冷轧厂--罩式退火炉AF--加热数据*******//
        public void GetReportPdf_HisDB_AF_Heat(string CoilID, string BaseID, string StartTime, string EndTime, ref  iTextSharp.text.Document pdfDocument)
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

            stuDrawPicInfo.TagDescription[1] = "内罩温度";
            stuDrawPicInfo.TagUnit[1] = "℃";
            stuDrawPicInfo.YAxisIsLeft[1] = true;
            stuDrawPicInfo.AutoSetRang[1] = false;
            stuDrawPicInfo.YSN[1] = 10;
            stuDrawPicInfo.TagMin[1] = 0;
            stuDrawPicInfo.TagMax[1] = 1000;

            stuDrawPicInfo.TagDescription[2] = "加热罩温度";
            stuDrawPicInfo.TagUnit[2] = "℃";
            stuDrawPicInfo.YAxisIsLeft[2] = true;
            stuDrawPicInfo.AutoSetRang[2] = false;
            stuDrawPicInfo.YSN[2] = 10;
            stuDrawPicInfo.TagMin[2] = 0;
            stuDrawPicInfo.TagMax[2] = 1000;

            //绘图的数据
            string[] tags = new string[2];
            tags[00] = "LYQCRM.AF_BASE_" + BaseID + ".CTL_TEMP_ACT";//	实际内罩温度
            tags[01] = "LYQCRM.AF_BASE_" + BaseID + ".HH_TEMP_ACT";//	实际加热罩温度


            //获取历史数据
            stuDrawPicInfo.dt = sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(StartTime), Convert.ToDateTime(EndTime));
            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(CoilID + "号钢卷在" + BaseID + "号罩式退火炉中温度变化", stuDrawPicInfo, ref pdfDocument);
        }

        //***** 冷轧厂--罩式退火炉AF--[O],转速*******//
        public void GetReportPdf_HisDB_AF_OxyRota(string CoilID, string BaseID, string StartTime, string EndTime, ref  iTextSharp.text.Document pdfDocument)
        {
            //定义
            ClsDuHisPic.StuDrawPicInfo stuDrawPicInfo = new ClsDuHisPic.StuDrawPicInfo();
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = true;
            //stuDrawPicInfo.YAxisDisplayWhenDrawOnly1YAxis = "温度,℃";
            stuDrawPicInfo.IsDisplayRelativeTime = true;

            stuDrawPicInfo.TagDescription[1] = "氧气O2含量";
            stuDrawPicInfo.TagUnit[1] = "ppm";
            stuDrawPicInfo.YAxisIsLeft[1] = true;
            stuDrawPicInfo.AutoSetRang[1] = true;            
            stuDrawPicInfo.TagMin[1] = 0;
            stuDrawPicInfo.TagMax[1] = 1000;

            stuDrawPicInfo.TagDescription[2] = "风机转速";
            stuDrawPicInfo.TagUnit[2] = "rad/min";
            stuDrawPicInfo.YAxisIsLeft[2] = false;
            stuDrawPicInfo.AutoSetRang[2] = true;            
            stuDrawPicInfo.TagMin[2] = 0;
            stuDrawPicInfo.TagMax[2] = 1000;

            //绘图的数据
            string[] tags = new string[2];
            tags[00] = "LYQCRM.AF_BASE_" + BaseID + ".O2_ACT";//氧气O2含量
            tags[01] = "LYQCRM.AF_BASE_" + BaseID + ".RECIRC_RPM_ACT";//风机转速


            //获取历史数据
            stuDrawPicInfo.dt = sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(StartTime), Convert.ToDateTime(EndTime));
            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(CoilID + "号钢卷在" + BaseID + "号罩式退火炉 氧气含量与风扇转速", stuDrawPicInfo, ref pdfDocument);
        }

        //***** 冷轧厂--罩式退火炉AF--气氛*******//
        public void GetReportPdf_HisDB_AF_GasFlux(string CoilID, string BaseID, string StartTime, string EndTime, ref  iTextSharp.text.Document pdfDocument)
        {
            //新页面
            //pdfDocument.newPage();

            //定义
            ClsDuHisPic.StuDrawPicInfo stuDrawPicInfo = new ClsDuHisPic.StuDrawPicInfo();
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = true;
            //stuDrawPicInfo.YAxisDisplayWhenDrawOnly1YAxis = "温度,℃";
            stuDrawPicInfo.IsDisplayRelativeTime = true;

            stuDrawPicInfo.TagDescription[1] = "氢气H2流量";
            stuDrawPicInfo.TagUnit[1] = "m3/h";
            stuDrawPicInfo.YAxisIsLeft[1] = true;
            stuDrawPicInfo.AutoSetRang[1] = true;
            stuDrawPicInfo.TagMin[1] = 0;
            stuDrawPicInfo.TagMax[1] = 1000;

            stuDrawPicInfo.TagDescription[2] = "氮气N2流量";
            stuDrawPicInfo.TagUnit[2] = "m3/h";
            stuDrawPicInfo.YAxisIsLeft[2] = false;
            stuDrawPicInfo.AutoSetRang[2] = true;
            stuDrawPicInfo.TagMin[2] = 0;
            stuDrawPicInfo.TagMax[2] = 1000;

            //绘图的数据
            string[] tags = new string[2];
            tags[00] = "LYQCRM.AF_BASE_" + BaseID + ".H2_FLOW_RATE_ACT";//氢气H2流量
            tags[01] = "LYQCRM.AF_BASE_" + BaseID + ".N2_FLOW_RATE_ACT";//氮气N2流量


            //获取历史数据
            stuDrawPicInfo.dt = sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(StartTime), Convert.ToDateTime(EndTime));
            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(CoilID + "号钢卷在" + BaseID + "号罩式退火炉 气体流量", stuDrawPicInfo, ref pdfDocument);
        }

       public List<CRM_HisDB_PL_BR> GetCRM_HisDB_PL_BR(string StartTime,string EndTime)
       {
           //获取结晶器的冷却历程数据
            List<CRM_HisDB_PL_BR> LST = new List<CRM_HisDB_PL_BR>();
            CRM_HisDB_PL_BR lst = new CRM_HisDB_PL_BR();


            object obj = new object();
            string[] tags = new string[6];             
            tags[00] = "LYQCRM.PL_BR1.Tension";// BR张力
            tags[01] = "LYQCRM.PL_BR2.Tension";//BR张力
            tags[02] = "LYQCRM.PL_BR3.Tension";//BR张力
            tags[03] = "LYQCRM.PL_DLP1.Tension";//出口活套张力
            tags[04] = "LYQCRM.PL_DLP2.Tension";//出口活套张力
            tags[05] = "LYQCRM.PL_ELP.Tension";//出口活套张力DLP1,DLP2,


            //获取历史数据
            System.Data.DataTable dt = sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(StartTime), Convert.ToDateTime(EndTime));
            //起始时间
            DateTime TimeStart = Convert.ToDateTime(StartTime);

            for (int I = 0; I < dt.Rows.Count; I++)
            {
                lst = new CRM_HisDB_PL_BR();

                lst.DateAndTime = Convert.ToDateTime(dt.Rows[I][00]);
                TimeSpan ts = Convert.ToDateTime(lst.DateAndTime) - TimeStart;
                lst.Duration = (float)ts.TotalMilliseconds / 60000;

                //dt.Rows[I][00]是时间列

                obj = dt.Rows[I][01]; if (obj.ToString().Length > 0) lst.BR1_Tension = Convert.ToSingle(obj);         
                obj = dt.Rows[I][02]; if (obj.ToString().Length > 0) lst.BR2_Tension = Convert.ToSingle(obj);        
                obj = dt.Rows[I][03]; if (obj.ToString().Length > 0) lst.BR3_Tension = Convert.ToSingle(obj);       
                obj = dt.Rows[I][04]; if (obj.ToString().Length > 0) lst.DLP1_Tension = Convert.ToSingle(obj);             
                obj = dt.Rows[I][05]; if (obj.ToString().Length > 0) lst.DLP2_Tension = Convert.ToSingle(obj);
                obj = dt.Rows[I][06]; if (obj.ToString().Length > 0) lst.ELP_Tension = Convert.ToSingle(obj);
                LST.Add(lst);
            }
            dt.Dispose();

            return LST;
       }


       public List<CRM_HisDB_PL_PC> GetCRM_HisDB_PL_PC( string PL_PC_ID ,string StartTime, string EndTime)
       {//冷轧厂酸洗-酸循环罐
           List<CRM_HisDB_PL_PC> LST = new List<CRM_HisDB_PL_PC>();
           CRM_HisDB_PL_PC lst;


            object obj = new object();
            string[] tags = new string[7];
            tags[00] = "LYQCRM.PL_PC" + PL_PC_ID + ".AcidFlow";// 酸循环罐酸加酸流量
            tags[01] = "LYQCRM.PL_PC" + PL_PC_ID + ".AddAcidFlow";//槽加新酸流量	
            tags[02] = "LYQCRM.PL_PC" + PL_PC_ID + ".FECL2";//	酸循环罐FECL2
            tags[03] = "LYQCRM.PL_PC" + PL_PC_ID + ".WaterFlow";//	酸循环罐酸加水流量       
            tags[04] = "LYQCRM.PL_PC" + PL_PC_ID + ".Level";//	酸循环罐液位       
            tags[05] = "LYQCRM.PL_PC" + PL_PC_ID + ".Temperature";//	酸循环罐温度
            tags[06] = "LYQCRM.PL_PC" + PL_PC_ID + ".HCL";//	酸循环罐HCL

           //获取历史数据
           System.Data.DataTable dt = sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(StartTime), Convert.ToDateTime(EndTime));
           //起始时间
           DateTime TimeStart = Convert.ToDateTime(StartTime);

           for (int I = 0; I < dt.Rows.Count; I++)
           {
               lst = new CRM_HisDB_PL_PC();

               lst.DateAndTime = Convert.ToDateTime(dt.Rows[I][00]);
               TimeSpan ts = Convert.ToDateTime(lst.DateAndTime) - TimeStart;
               lst.Duration = (float)ts.TotalMilliseconds / 60000;

               //dt.Rows[I][00]是时间列

               obj = dt.Rows[I][01]; if (obj.ToString().Length > 0) lst.AcidFlow = Convert.ToSingle(obj);
               obj = dt.Rows[I][02]; if (obj.ToString().Length > 0) lst.AddAcidFlow = Convert.ToSingle(obj);
               obj = dt.Rows[I][03]; if (obj.ToString().Length > 0) lst.FECL2 = Convert.ToSingle(obj);
               obj = dt.Rows[I][04]; if (obj.ToString().Length > 0) lst.WaterFlow = Convert.ToSingle(obj);
               obj = dt.Rows[I][05]; if (obj.ToString().Length > 0) lst.Temperature = Convert.ToSingle(obj);
               obj = dt.Rows[I][06]; if (obj.ToString().Length > 0) lst.HCL = Convert.ToSingle(obj);
               LST.Add(lst);
           }
           dt.Dispose();

           return LST;
       }

       public List<CRM_HisDB_PL_PT> GetCRM_HisDB_PL_PT(string PL_PT_ID, string StartTime, string EndTime)
       {//冷轧厂酸洗-清洗酸罐
           List<CRM_HisDB_PL_PT> LST = new List<CRM_HisDB_PL_PT>();
           CRM_HisDB_PL_PT lst;


            object obj = new object();
            string[] tags = new string[8];         
            tags[00] = "LYQCRM.PL_PT" + PL_PT_ID + ".Conductivity";//	酸罐酸液电导率	LYQCRM.PL_PT1.Conductivity
            tags[01] = "LYQCRM.PL_PT" + PL_PT_ID + ".Density";//	酸液浓度	LYQCRM.PL_PT1.Density
            tags[02] = "LYQCRM.PL_PT" + PL_PT_ID + ".DensityRatio";//	酸罐酸液比重	LYQCRM.PL_PT1.DensityRatio
            tags[03] = "LYQCRM.PL_PT" + PL_PT_ID + ".EntAcidFlow";//	酸槽入口酸流量	LYQCRM.PL_PT1.EntAcidFlow
            tags[04] = "LYQCRM.PL_PT" + PL_PT_ID + ".WasterAcidFlow";//	废酸流量	LYQCRM.PL_PT1.WasterAcidFlow
            tags[05] = "LYQCRM.PL_PT" + PL_PT_ID + ".ExtAcidFlow";//	酸槽出口酸流量	LYQCRM.PL_PT1.ExtAcidFlow        
            tags[06] = "LYQCRM.PL_PT" + PL_PT_ID + ".InhibitorFlow";//	缓蚀剂流量	LYQCRM.PL_PT1.InhibitorFlow
            tags[07] = "LYQCRM.PL_PT" + PL_PT_ID + ".Temperature";//	酸液温度	LYQCRM.PL_PT1.Temperature
        
           //获取历史数据
           System.Data.DataTable dt = sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(StartTime), Convert.ToDateTime(EndTime));
           //起始时间
           DateTime TimeStart = Convert.ToDateTime(StartTime);

           for (int I = 0; I < dt.Rows.Count; I++)
           {
               lst = new CRM_HisDB_PL_PT();

               lst.DateAndTime = Convert.ToDateTime(dt.Rows[I][00]);
               TimeSpan ts = Convert.ToDateTime(lst.DateAndTime) - TimeStart;
               lst.Duration = (float)ts.TotalMilliseconds / 60000;

               //dt.Rows[I][00]是时间列
               obj = dt.Rows[I][01]; if (obj.ToString().Length > 0) lst.Conductivity = Convert.ToSingle(obj);//	酸罐酸液电导率	LYQCRM.PL_PT1.Conductivity
               obj = dt.Rows[I][02]; if (obj.ToString().Length > 0) lst.Density = Convert.ToSingle(obj);//	酸液浓度	LYQCRM.PL_PT1.Density
               obj = dt.Rows[I][03]; if (obj.ToString().Length > 0) lst.DensityRatio = Convert.ToSingle(obj);//	酸罐酸液比重	LYQCRM.PL_PT1.DensityRatio
               obj = dt.Rows[I][04]; if (obj.ToString().Length > 0) lst.EntAcidFlow = Convert.ToSingle(obj);//	酸槽入口酸流量	LYQCRM.PL_PT1.EntAcidFlow
               obj = dt.Rows[I][05]; if (obj.ToString().Length > 0) lst.WasterAcidFlow = Convert.ToSingle(obj);//	废酸流量	LYQCRM.PL_PT1.WasterAcidFlow
               obj = dt.Rows[I][06]; if (obj.ToString().Length > 0) lst.ExtAcidFlow = Convert.ToSingle(obj);//	酸槽出口酸流量	LYQCRM.PL_PT1.ExtAcidFlow        
               obj = dt.Rows[I][07]; if (obj.ToString().Length > 0) lst.InhibitorFlow = Convert.ToSingle(obj);//	缓蚀剂流量	LYQCRM.PL_PT1.InhibitorFlow
               obj = dt.Rows[I][08]; if (obj.ToString().Length > 0) lst.Temperature = Convert.ToSingle(obj);//	酸液温度	LYQCRM.PL_PT1.Temperature 
               
               LST.Add(lst);
           }
           dt.Dispose();

           return LST;
       }

       public List<CRM_HisDB_PL_RT> GetCRM_HisDB_PL_RT(string StartTime, string EndTime)
       {//冷轧厂酸洗-漂洗槽
           List<CRM_HisDB_PL_RT> LST = new List<CRM_HisDB_PL_RT>();
           CRM_HisDB_PL_RT lst;


           object obj = new object();
           string[] tags = new string[3];               
           tags[00] = "LYQCRM.PL_RT4.PH";//	漂洗槽PH值        
           tags[01] = "LYQCRM.PL_RT4.Temperature";//漂洗槽温度
           tags[02] = "LYQCRM.PL_RT4.WaterFlow";//漂洗槽加水流量	

           //获取历史数据
           System.Data.DataTable dt = sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(StartTime), Convert.ToDateTime(EndTime));
           //起始时间
           DateTime TimeStart = Convert.ToDateTime(StartTime);

           for (int I = 0; I < dt.Rows.Count; I++)
           {
               lst = new CRM_HisDB_PL_RT();

               lst.DateAndTime = Convert.ToDateTime(dt.Rows[I][00]);
               TimeSpan ts = Convert.ToDateTime(lst.DateAndTime) - TimeStart;
               lst.Duration = (float)ts.TotalMilliseconds / 60000;

               //dt.Rows[I][00]是时间列
               obj = dt.Rows[I][01]; if (obj.ToString().Length > 0) lst.PH = Convert.ToSingle(obj);//	漂洗槽PH值        
               obj = dt.Rows[I][02]; if (obj.ToString().Length > 0) lst.Temperature = Convert.ToSingle(obj);//漂洗槽温度
               obj = dt.Rows[I][03]; if (obj.ToString().Length > 0) lst.WaterFlow = Convert.ToSingle(obj);//漂洗槽加水流量

               LST.Add(lst);
           }
           dt.Dispose();

           return LST;
       }

       public List<CRM_HisDB_PL_PH> GetCRM_HisDB_PL_PH(string StartTime, string EndTime)
       {//冷轧厂酸洗-石墨槽
           List<CRM_HisDB_PL_PH> LST = new List<CRM_HisDB_PL_PH>();
           CRM_HisDB_PL_PH lst;


            object obj = new object();
            string[] tags = new string[9];           
            tags[00] = "LYQCRM.PL_PH1.EntTemp";//	槽石墨加热器入口温度        
            tags[01] = "LYQCRM.PL_PH1.Temperature";//	槽石墨加热器出口温度
            tags[02] = "LYQCRM.PL_PH1.PHValue";//	槽石墨加热器冷凝水PH值

            tags[03] = "LYQCRM.PL_PH2.EntTemp";//	槽石墨加热器入口温度        
            tags[04] = "LYQCRM.PL_PH2.Temperature";//	槽石墨加热器出口温度
            tags[05] = "LYQCRM.PL_PH2.PHValue";//	槽石墨加热器冷凝水PH值

            tags[06] = "LYQCRM.PL_PH3.EntTemp";//	槽石墨加热器入口温度        
            tags[07] = "LYQCRM.PL_PH3.Temperature";//	槽石墨加热器出口温度
            tags[08] = "LYQCRM.PL_PH3.PHValue";//	槽石墨加热器冷凝水PH值


           //获取历史数据
           System.Data.DataTable dt = sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(StartTime), Convert.ToDateTime(EndTime));
           //起始时间
           DateTime TimeStart = Convert.ToDateTime(StartTime);

           for (int I = 0; I < dt.Rows.Count; I++)
           {
               lst = new CRM_HisDB_PL_PH();

               lst.DateAndTime = Convert.ToDateTime(dt.Rows[I][00]);
               TimeSpan ts = Convert.ToDateTime(lst.DateAndTime) - TimeStart;
               lst.Duration = (float)ts.TotalMilliseconds / 60000;

               //dt.Rows[I][00]是时间列
                              
               obj = dt.Rows[I][01]; if (obj.ToString().Length > 0) lst.PH1_EntTemp=Convert.ToDouble(obj);//	槽石墨加热器入口温度        
               obj = dt.Rows[I][02]; if (obj.ToString().Length > 0) lst.PH1_Temperature=Convert.ToDouble(obj);//	槽石墨加热器出口温度
               obj = dt.Rows[I][03]; if (obj.ToString().Length > 0) lst.PH1_PHValue=Convert.ToDouble(obj);//	槽石墨加热器冷凝水PH值

               obj = dt.Rows[I][04]; if (obj.ToString().Length > 0) lst.PH2_EntTemp=Convert.ToDouble(obj);//	槽石墨加热器入口温度        
               obj = dt.Rows[I][05]; if (obj.ToString().Length > 0) lst.PH2_Temperature=Convert.ToDouble(obj);//	槽石墨加热器出口温度
               obj = dt.Rows[I][06]; if (obj.ToString().Length > 0) lst.PH2_PHValue=Convert.ToDouble(obj);//	槽石墨加热器冷凝水PH值

               obj = dt.Rows[I][07]; if (obj.ToString().Length > 0) lst.PH3_EntTemp=Convert.ToDouble(obj);//	槽石墨加热器入口温度        
               obj = dt.Rows[I][08]; if (obj.ToString().Length > 0) lst.PH3_Temperature=Convert.ToDouble(obj);//	槽石墨加热器出口温度
               obj = dt.Rows[I][09]; if (obj.ToString().Length > 0) lst.PH3_PHValue = Convert.ToDouble(obj);//	槽石墨加热器冷凝水PH值

               LST.Add(lst);
           }
           dt.Dispose();

           return LST;
       }


       public List<CRM_HisDB_CGL_ZIN> GetCRM_HisDB_CGL_ZIN(string StartTime, string EndTime)
       {//冷轧厂-镀锌线-锌锅
           List<CRM_HisDB_CGL_ZIN> LST = new List<CRM_HisDB_CGL_ZIN>();
           CRM_HisDB_CGL_ZIN lst;


           object obj = new object();
           string[] tags = new string[20];             
          
            tags[00] = "LYQCRM.CGL_ZIN.Act_thick_1TR_Len30";//LYQCRM.CGL_ZIN	成品带钢厚度(1#TR)
            tags[01] = "LYQCRM.CGL_ZIN.Act_thick_2TR_Len30";//LYQCRM.CGL_ZIN	成品带钢厚度(2#TR)
                      
           tags[02] = "LYQCRM.CGL_ZIN.COATING_WEIGHT_Len30";//LYQCRM.CGL_ZIN	计划镀层重量 
           tags[03] = "LYQCRM.CGL_ZIN.TOAVERAGE_Len30";//LYQCRM.CGL_ZIN	实际镀层重量上表面
           tags[04] = "LYQCRM.CGL_ZIN.BOTTOM_AVERAGE_Len30";//LYQCRM.CGL_ZIN	实际镀层重量下表面
            
            
           tags[05] = "LYQCRM.CGL_ZIN.FW_Act_coating_weight_Len30";//LYQCRM.CGL_ZIN	实际镀层上面左
           tags[06] = "LYQCRM.CGL_ZIN.FC_Act_coating_weight_Len30";//LYQCRM.CGL_ZIN	实际镀层上面中
           tags[07] = "LYQCRM.CGL_ZIN.FD_Act_coating_weight_Len30";//LYQCRM.CGL_ZIN	实际镀层上面右
           tags[08] = "LYQCRM.CGL_ZIN.BW_Act_coating_weight_Len30";//LYQCRM.CGL_ZIN	实际镀层下面左 
           tags[09] = "LYQCRM.CGL_ZIN.BC_Act_coating_weight_Len30";//LYQCRM.CGL_ZIN	实际镀层下面中
           tags[10] = "LYQCRM.CGL_ZIN.BDW_Act_coating_weight_Len30";//LYQCRM.CGL_ZIN 实际镀层下面右
                       
            
            tags[11] = "LYQCRM.CGL_ZIN.GI_AZINC_POWER_Len25";//LYQCRM.CGL_ZIN	GI锌锅A感应器功率
            tags[12] = "LYQCRM.CGL_ZIN.GI_BZINC_POWER_Len25";//LYQCRM.CGL_ZIN	GI锌锅B感应器功率
            tags[13] = "LYQCRM.CGL_ZIN.GI_mainzinc_POW_Len25";//LYQCRM.CGL_ZIN	GL主锅感应器功率
            tags[14] = "LYQCRM.CGL_ZIN.GI_YUzinc_POW_Len25";//LYQCRM.CGL_ZIN	GL预熔锅感应器功率

            tags[15] = "LYQCRM.CGL_ZIN.GI_mainzinc_temLen25";//LYQCRM.CGL_ZIN	GL主锌锅温度            
            tags[16] = "LYQCRM.CGL_ZIN.GI_yuzinc_temLen25";//LYQCRM.CGL_ZIN	GL预熔锅温度
            tags[17] = "LYQCRM.CGL_ZIN.GI_zinc_pot_temLen25";//LYQCRM.CGL_ZIN	GI锌锅温度
            tags[18] = "LYQCRM.CGL_ZIN.QTK_water_Temp_Len29";//LYQCRM.CGL_ZIN	水淬槽水温
            tags[19] = "LYQCRM.CGL_ZIN.Zinc_Tension_Len25";//LYQCRM.CGL_ZIN	锌锅段张力

            //tags[] = "LYQCRM.CGL_ZIN.Front_main_Nozzle_DS_gaLen25";//LYQCRM.CGL_ZIN	前气刀传动侧距离
            //tags[] = "LYQCRM.CGL_ZIN.Front_main_Nozzle_pressure_Len25";//LYQCRM.CGL_ZIN	前气刀压力
            //tags[] = "LYQCRM.CGL_ZIN.Front_main_Nozzle_WS_gaLen25";//LYQCRM.CGL_ZIN	前气刀工作侧距离
            //tags[] = "LYQCRM.CGL_ZIN.Front_sub_Nozzle_DS_gaLen25";//LYQCRM.CGL_ZIN	前辅助喷嘴传动侧距离
            //tags[] = "LYQCRM.CGL_ZIN.Front_sub_Nozzle_pressure_Len25";//LYQCRM.CGL_ZIN	前辅助喷气刀压力
            //tags[] = "LYQCRM.CGL_ZIN.Front_sub_Nozzle_WS_gaLen25";//LYQCRM.CGL_ZIN	前辅助喷嘴工作侧距离

            //tags[] = "LYQCRM.CGL_ZIN.Back_main_Nozzle_DS_gaLen25";//LYQCRM.CGL_ZIN	后气刀传动侧距离
            //tags[] = "LYQCRM.CGL_ZIN.Back_main_Nozzle_pressure_Len25";//LYQCRM.CGL_ZIN	后气刀压力
            //tags[] = "LYQCRM.CGL_ZIN.Back_main_Nozzle_WS_gaLen25";//LYQCRM.CGL_ZIN	后气刀工作侧距离
            //tags[] = "LYQCRM.CGL_ZIN.Back_staroll_DS_position_Len25";//LYQCRM.CGL_ZIN	后稳定辊传动侧位置
            //tags[] = "LYQCRM.CGL_ZIN.Back_staroll_WS_position_Len25";//LYQCRM.CGL_ZIN	后稳定辊工作侧位置
            //tags[] = "LYQCRM.CGL_ZIN.Back_sub_Nozzle_DS_gaLen25";//LYQCRM.CGL_ZIN	后辅助喷嘴传动侧距离
            //tags[] = "LYQCRM.CGL_ZIN.Back_sub_Nozzle_pressure_Len25";//LYQCRM.CGL_ZIN	后辅助喷气刀压力
            //tags[] = "LYQCRM.CGL_ZIN.Back_sub_Nozzle_WS_gaLen25";//LYQCRM.CGL_ZIN	后辅助喷嘴工作侧距离
           
            //tags[] = "LYQCRM.CGL_ZIN.Main_Nozzle_DS_position_Len25";//LYQCRM.CGL_ZIN	气刀传动侧高度
            //tags[] = "LYQCRM.CGL_ZIN.Main_Nozzle_WS_position_Len25";//LYQCRM.CGL_ZIN	气刀工作侧高度
            //tags[] = "LYQCRM.CGL_ZIN.Nozzle_bleeding_valve_pressure_Len25";//LYQCRM.CGL_ZIN	气刀放散阀压力
            //tags[] = "LYQCRM.CGL_ZIN.Nozzle_bleeding_valve_pv_out_Len25";//LYQCRM.CGL_ZIN	气刀放散阀开度
            
            //tags[] = "LYQCRM.CGL_ZIN.Quick_cool_1_blower_speed_Len26";//LYQCRM.CGL_ZIN	快冷风机1#转速4
            //tags[] = "LYQCRM.CGL_ZIN.Quick_cool_2_blower_speed_Len26";//LYQCRM.CGL_ZIN	快冷风机2#转速5
            
            //tags[] = "LYQCRM.CGL_ZIN.VA_AJC_01_speed_Len26";//LYQCRM.CGL_ZIN	1#AJC-1#冷却风机转速
            //tags[] = "LYQCRM.CGL_ZIN.VA_AJC_02_speed_Len27";//LYQCRM.CGL_ZIN	1#AJC-2#冷却风机转速
            //tags[] = "LYQCRM.CGL_ZIN.VA_AJC_03_speed_Len28";//LYQCRM.CGL_ZIN	1#AJC-3#冷却风机转速
            



           //获取历史数据
           System.Data.DataTable dt = sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(StartTime), Convert.ToDateTime(EndTime));
           //起始时间
           DateTime TimeStart = Convert.ToDateTime(StartTime);

           for (int I = 0; I < dt.Rows.Count; I++)
           {
               lst = new CRM_HisDB_CGL_ZIN();

               lst.DateAndTime = Convert.ToDateTime(dt.Rows[I][00]);
               TimeSpan ts = Convert.ToDateTime(lst.DateAndTime) - TimeStart;
               lst.Duration = (float)ts.TotalMilliseconds / 60000;

               //dt.Rows[I][00]是时间列

               obj = dt.Rows[I][01]; if (obj.ToString().Length > 0) lst.Act_thick_1TR_Len30 = Convert.ToDouble(obj);//LYQCRM.CGL_ZIN	成品带钢厚度(1#TR)
               obj = dt.Rows[I][02]; if (obj.ToString().Length > 0) lst.Act_thick_2TR_Len30 = Convert.ToDouble(obj);//LYQCRM.CGL_ZIN	成品带钢厚度(2#TR)

               obj = dt.Rows[I][03]; if (obj.ToString().Length > 0) lst.COATING_WEIGHT_Len30 = Convert.ToDouble(obj);//LYQCRM.CGL_ZIN	计划镀层重量 
               obj = dt.Rows[I][04]; if (obj.ToString().Length > 0) lst.TOAVERAGE_Len30 = Convert.ToDouble(obj);//LYQCRM.CGL_ZIN	实际镀层重量上表面
               obj = dt.Rows[I][05]; if (obj.ToString().Length > 0) lst.BOTTOM_AVERAGE_Len30 = Convert.ToDouble(obj);//LYQCRM.CGL_ZIN	实际镀层重量下表面


               obj = dt.Rows[I][06]; if (obj.ToString().Length > 0) lst.FW_Act_coating_weight_Len30 = Convert.ToDouble(obj);//LYQCRM.CGL_ZIN	实际镀层上面左
               obj = dt.Rows[I][07]; if (obj.ToString().Length > 0) lst.FC_Act_coating_weight_Len30 = Convert.ToDouble(obj);//LYQCRM.CGL_ZIN	实际镀层上面中
               obj = dt.Rows[I][08]; if (obj.ToString().Length > 0) lst.FD_Act_coating_weight_Len30 = Convert.ToDouble(obj);//LYQCRM.CGL_ZIN	实际镀层上面右
               obj = dt.Rows[I][09]; if (obj.ToString().Length > 0) lst.BW_Act_coating_weight_Len30 = Convert.ToDouble(obj);//LYQCRM.CGL_ZIN	实际镀层下面左 
               obj = dt.Rows[I][10]; if (obj.ToString().Length > 0) lst.BC_Act_coating_weight_Len30 = Convert.ToDouble(obj);//LYQCRM.CGL_ZIN	实际镀层下面中
               obj = dt.Rows[I][11]; if (obj.ToString().Length > 0) lst.BDW_Act_coating_weight_Len30 = Convert.ToDouble(obj);//LYQCRM.CGL_ZIN	实际镀层下面右

               obj = dt.Rows[I][12]; if (obj.ToString().Length > 0) lst.GI_AZINC_POWER_Len25 = Convert.ToDouble(obj);//LYQCRM.CGL_ZIN	GI锌锅A感应器功率
               obj = dt.Rows[I][13]; if (obj.ToString().Length > 0) lst.GI_BZINC_POWER_Len25 = Convert.ToDouble(obj);//LYQCRM.CGL_ZIN	GI锌锅B感应器功率
               obj = dt.Rows[I][14]; if (obj.ToString().Length > 0) lst.GI_mainzinc_POW_Len25 = Convert.ToDouble(obj);//LYQCRM.CGL_ZIN	GL主锅感应器功率
               obj = dt.Rows[I][15]; if (obj.ToString().Length > 0) lst.GI_YUzinc_POW_Len25 = Convert.ToDouble(obj);//LYQCRM.CGL_ZIN	GL预熔锅感应器功率

               obj = dt.Rows[I][16]; if (obj.ToString().Length > 0) lst.GI_mainzinc_temLen25 = Convert.ToDouble(obj);//LYQCRM.CGL_ZIN	GL主锌锅温度            
               obj = dt.Rows[I][17]; if (obj.ToString().Length > 0) lst.GI_yuzinc_temLen25 = Convert.ToDouble(obj);//LYQCRM.CGL_ZIN	GL预熔锅温度
               obj = dt.Rows[I][18]; if (obj.ToString().Length > 0) lst.GI_zinc_pot_temLen25 = Convert.ToDouble(obj);//LYQCRM.CGL_ZIN	GI锌锅温度
               obj = dt.Rows[I][19]; if (obj.ToString().Length > 0) lst.QTK_water_Temp_Len29 = Convert.ToDouble(obj);//LYQCRM.CGL_ZIN	水淬槽水温
               obj = dt.Rows[I][20]; if (obj.ToString().Length > 0) lst.Zinc_Tension_Len25 = Convert.ToDouble(obj);//LYQCRM.CGL_ZIN	锌锅段张力

               LST.Add(lst);
           }
           dt.Dispose();

           return LST;
       }

       public List<CRM_HisDB_CGL_SPM> GetCRM_HisDB_CGL_SPM(string StartTime, string EndTime)
       {//冷轧厂-镀锌线-光整机
            List<CRM_HisDB_CGL_SPM> LST = new List<CRM_HisDB_CGL_SPM>();
            CRM_HisDB_CGL_SPM lst;
           
            object obj = new object();
            string[] tags = new string[12];             
          
            tags[00] = "LYQCRM.CGL_SPM.Process_Act_Speed_Len30";    //LYQCRM.CGL_SPM	中央段速度
            tags[06] = "LYQCRM.CGL_SPM.SPM_elongation_Len31";   //LYQCRM.CGL_SPM	光整机延伸率

            tags[01] = "LYQCRM.CGL_SPM.SPM_Anti_crease_height_Len31";   //LYQCRM.CGL_SPM	光整机防皱辊高度
            tags[02] = "LYQCRM.CGL_SPM.SPM_Anti_quiver_height_Len31";   //LYQCRM.CGL_SPM	光整机防颤辊高度

            tags[05] = "LYQCRM.CGL_SPM.SPM_DS_gap_Len31";   //LYQCRM.CGL_SPM	光整机工作侧辊缝值
            tags[11] = "LYQCRM.CGL_SPM.SPM_WS_gap_Len31";   //LYQCRM.CGL_SPM	光整机工作侧辊缝值

            tags[03] = "LYQCRM.CGL_SPM.SPM_bending_force_Len31";    //LYQCRM.CGL_SPM	光整机弯辊力
            tags[04] = "LYQCRM.CGL_SPM.SPM_DS_force_Len31"; //LYQCRM.CGL_SPM	光整机工作侧轧制力            
            tags[07] = "LYQCRM.CGL_SPM.SPM_entry_Tension_Len31";    //LYQCRM.CGL_SPM	光整机入口张力
            tags[08] = "LYQCRM.CGL_SPM.SPM_exit_Tension_Len31"; //LYQCRM.CGL_SPM	光整机出口张力
            tags[09] = "LYQCRM.CGL_SPM.SPM_rolling_force_Len31";    //LYQCRM.CGL_SPM	光整机轧制力
            tags[10] = "LYQCRM.CGL_SPM.SPM_WS_force_Len31"; //LYQCRM.CGL_SPM	光整机工作侧轧制力
                    
                       

            //获取历史数据
            System.Data.DataTable dt = sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(StartTime), Convert.ToDateTime(EndTime));
            //起始时间
            DateTime TimeStart = Convert.ToDateTime(StartTime);

            for (int I = 0; I < dt.Rows.Count; I++)
            {
                lst = new CRM_HisDB_CGL_SPM();

                lst.DateAndTime = Convert.ToDateTime(dt.Rows[I][00]);
                TimeSpan ts = Convert.ToDateTime(lst.DateAndTime) - TimeStart;
                lst.Duration = (float)ts.TotalMilliseconds / 60000;

                //dt.Rows[I][00]是时间列

                obj = dt.Rows[I][01]; if (obj.ToString().Length > 0) lst.Process_Act_Speed_Len30 = Convert.ToDouble(obj);//LYQCRM.CGL_SPM	中央段速度
                obj = dt.Rows[I][02]; if (obj.ToString().Length > 0) lst.SPM_Anti_crease_height_Len31 = Convert.ToDouble(obj);//LYQCRM.CGL_SPM	光整机防皱辊高度
                obj = dt.Rows[I][03]; if (obj.ToString().Length > 0) lst.SPM_Anti_quiver_height_Len31 = Convert.ToDouble(obj);//LYQCRM.CGL_SPM	光整机防颤辊高度
                obj = dt.Rows[I][04]; if (obj.ToString().Length > 0) lst.SPM_bending_force_Len31 = Convert.ToDouble(obj);//LYQCRM.CGL_SPM	光整机弯辊力
                obj = dt.Rows[I][05]; if (obj.ToString().Length > 0) lst.SPM_DS_force_Len31 = Convert.ToDouble(obj);//LYQCRM.CGL_SPM	光整机工作侧轧制力
                obj = dt.Rows[I][06]; if (obj.ToString().Length > 0) lst.SPM_DS_gap_Len31 = Convert.ToDouble(obj);//LYQCRM.CGL_SPM	光整机工作侧辊缝值
                obj = dt.Rows[I][07]; if (obj.ToString().Length > 0) lst.SPM_elongation_Len31 = Convert.ToDouble(obj);//LYQCRM.CGL_SPM	光整机延伸率
                obj = dt.Rows[I][08]; if (obj.ToString().Length > 0) lst.SPM_entry_Tension_Len31 = Convert.ToDouble(obj);//LYQCRM.CGL_SPM	光整机入口张力
                obj = dt.Rows[I][09]; if (obj.ToString().Length > 0) lst.SPM_exit_Tension_Len31 = Convert.ToDouble(obj);//LYQCRM.CGL_SPM	光整机出口张力
                obj = dt.Rows[I][10]; if (obj.ToString().Length > 0) lst.SPM_rolling_force_Len31 = Convert.ToDouble(obj);//LYQCRM.CGL_SPM	光整机轧制力
                obj = dt.Rows[I][11]; if (obj.ToString().Length > 0) lst.SPM_WS_force_Len31 = Convert.ToDouble(obj);//LYQCRM.CGL_SPM	光整机工作侧轧制力
                obj = dt.Rows[I][12]; if (obj.ToString().Length > 0) lst.SPM_WS_gap_Len31 = Convert.ToDouble(obj);//LYQCRM.CGL_SPM	光整机工作侧辊缝值

                LST.Add(lst);
           }
           dt.Dispose();

           return LST;
   }

       public List<CRM_HisDB_TCM_ASC> GetCRM_HisDB_TCM_ASC(string StartTime, string EndTime)
       {//冷轧厂-自动板型仪
           List<CRM_HisDB_TCM_ASC> LST = new List<CRM_HisDB_TCM_ASC>();
           CRM_HisDB_TCM_ASC lst;


            object obj = new object();
            string[] tags = new string[36];
            tags[00] = "LYQCRM.TCM_ASC.a";//	板形系数a        
            tags[01] = "LYQCRM.TCM_ASC.b";//	板形系数b       
            tags[02] = "LYQCRM.TCM_ASC.c";//	板形系数c       
            tags[03] = "LYQCRM.TCM_ASC.Gain";//	ASC增益
            tags[04] = "LYQCRM.TCM_ASC.Mode";//	ASC模式 

            tags[05] = "LYQCRM.TCM_ASC.Iunit1";//	板形仪1区平直度I-unit        
            tags[06] = "LYQCRM.TCM_ASC.Iunit2";//	板形仪2区平直度I-unit        
            tags[07] = "LYQCRM.TCM_ASC.Iunit3";//	板形仪3区平直度I-unit       
            tags[08] = "LYQCRM.TCM_ASC.Iunit4";//	板形仪4区平直度I-unit        
            tags[09] = "LYQCRM.TCM_ASC.Iunit5";//	板形仪5区平直度I-unit
        
            tags[10] = "LYQCRM.TCM_ASC.Iunit6";//	板形仪6区平直度I-unit        
            tags[11] = "LYQCRM.TCM_ASC.Iunit7";//	板形仪7区平直度I-unit      
            tags[12] = "LYQCRM.TCM_ASC.Iunit8";//	板形仪8区平直度I-unit        
            tags[13] = "LYQCRM.TCM_ASC.Iunit9";//	板形仪9区平直度I-unit        
            tags[14] = "LYQCRM.TCM_ASC.Iunit10";//	板形仪10区平直度I-unit
        
            tags[15] = "LYQCRM.TCM_ASC.Iunit11";//	板形仪11区平直度I-unit       
            tags[16] = "LYQCRM.TCM_ASC.Iunit12";//	板形仪12区平直度I-unit        
            tags[17] = "LYQCRM.TCM_ASC.Iunit13";//	板形仪13区平直度I-unit       
            tags[18] = "LYQCRM.TCM_ASC.Iunit14";//	板形仪14区平直度I-unit        
            tags[19] = "LYQCRM.TCM_ASC.Iunit15";//	板形仪15区平直度I-unit
        
            tags[20] = "LYQCRM.TCM_ASC.Iunit16";//	板形仪16区平直度I-unit        
            tags[21] = "LYQCRM.TCM_ASC.Iunit17";//	板形仪17区平直度I-unit        
            tags[22] = "LYQCRM.TCM_ASC.Iunit18";//	板形仪18区平直度I-unit        
            tags[23] = "LYQCRM.TCM_ASC.Iunit19";//	板形仪19区平直度I-unit
            tags[24] = "LYQCRM.TCM_ASC.Iunit20";//	板形仪20区平直度I-unit
        
            tags[25] = "LYQCRM.TCM_ASC.Iunit21";//	板形仪21区平直度I-unit       
            tags[26] = "LYQCRM.TCM_ASC.Iunit22";//	板形仪22区平直度I-unit        
            tags[27] = "LYQCRM.TCM_ASC.Iunit23";//	板形仪23区平直度I-unit        
            tags[28] = "LYQCRM.TCM_ASC.Iunit24";//	板形仪24区平直度I-unit        
            tags[29] = "LYQCRM.TCM_ASC.Iunit25";//	板形仪25区平直度I-unit
        
            tags[30] = "LYQCRM.TCM_ASC.Iunit26";//	板形仪26区平直度I-unit        
            tags[31] = "LYQCRM.TCM_ASC.Iunit27";//	板形仪27区平直度I-unit        
            tags[32] = "LYQCRM.TCM_ASC.Iunit28";//	板形仪28区平直度I-unit       
            tags[33] = "LYQCRM.TCM_ASC.Iunit29";//	板形仪29区平直度I-unit               
            tags[34] = "LYQCRM.TCM_ASC.Iunit30";//	板形仪30区平直度I-unit
        
            tags[35] = "LYQCRM.TCM_ASC.Iunit31";//	板形仪31区平直度I-unit
                


           //获取历史数据
           System.Data.DataTable dt = sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(StartTime), Convert.ToDateTime(EndTime));
           //起始时间
           DateTime TimeStart = Convert.ToDateTime(StartTime);

           for (int I = 0; I < dt.Rows.Count; I++)
           {
               lst = new CRM_HisDB_TCM_ASC();

               lst.DateAndTime = Convert.ToDateTime(dt.Rows[I][00]);
               TimeSpan ts = Convert.ToDateTime(lst.DateAndTime) - TimeStart;
               lst.Duration = (float)ts.TotalMilliseconds / 60000;

               //dt.Rows[I][00]是时间列

               obj = dt.Rows[I][01]; if (obj.ToString().Length > 0) lst.a = Convert.ToDouble(obj);//	板形系数a        
               obj = dt.Rows[I][02]; if (obj.ToString().Length > 0) lst.b = Convert.ToDouble(obj);//	板形系数b       
               obj = dt.Rows[I][03]; if (obj.ToString().Length > 0) lst.c = Convert.ToDouble(obj);//	板形系数c       
               obj = dt.Rows[I][04]; if (obj.ToString().Length > 0) lst.Gain = Convert.ToDouble(obj);//	ASC增益
               obj = dt.Rows[I][05]; if (obj.ToString().Length > 0) lst.Mode = Convert.ToDouble(obj);//	ASC模式 

               obj = dt.Rows[I][06]; if (obj.ToString().Length > 0) lst.Iunit1 = Convert.ToDouble(obj);//	板形仪1区平直度I-unit        
               obj = dt.Rows[I][07]; if (obj.ToString().Length > 0) lst.Iunit2 = Convert.ToDouble(obj);//	板形仪2区平直度I-unit        
               obj = dt.Rows[I][08]; if (obj.ToString().Length > 0) lst.Iunit3 = Convert.ToDouble(obj);//	板形仪3区平直度I-unit       
               obj = dt.Rows[I][09]; if (obj.ToString().Length > 0) lst.Iunit4 = Convert.ToDouble(obj);//	板形仪4区平直度I-unit        
               obj = dt.Rows[I][10]; if (obj.ToString().Length > 0) lst.Iunit5 = Convert.ToDouble(obj);//	板形仪5区平直度I-unit

                obj = dt.Rows[I][11]; if (obj.ToString().Length > 0) lst.Iunit6 = Convert.ToDouble(obj);//	板形仪6区平直度I-unit        
               obj = dt.Rows[I][12]; if (obj.ToString().Length > 0) lst.Iunit7 = Convert.ToDouble(obj);//	板形仪7区平直度I-unit      
               obj = dt.Rows[I][13]; if (obj.ToString().Length > 0) lst.Iunit8 = Convert.ToDouble(obj);//	板形仪8区平直度I-unit        
               obj = dt.Rows[I][14]; if (obj.ToString().Length > 0) lst.Iunit9 = Convert.ToDouble(obj);//	板形仪9区平直度I-unit        
               obj = dt.Rows[I][15]; if (obj.ToString().Length > 0) lst.Iunit10 = Convert.ToDouble(obj);//	板形仪10区平直度I-unit

               obj = dt.Rows[I][16]; if (obj.ToString().Length > 0) lst.Iunit11 = Convert.ToDouble(obj);//	板形仪11区平直度I-unit       
               obj = dt.Rows[I][17]; if (obj.ToString().Length > 0) lst.Iunit12 = Convert.ToDouble(obj);//	板形仪12区平直度I-unit        
               obj = dt.Rows[I][18]; if (obj.ToString().Length > 0) lst.Iunit13 = Convert.ToDouble(obj);//	板形仪13区平直度I-unit       
               obj = dt.Rows[I][19]; if (obj.ToString().Length > 0) lst.Iunit14 = Convert.ToDouble(obj);//	板形仪14区平直度I-unit        
               obj = dt.Rows[I][20]; if (obj.ToString().Length > 0) lst.Iunit15 = Convert.ToDouble(obj);//	板形仪15区平直度I-unit

               obj = dt.Rows[I][21]; if (obj.ToString().Length > 0) lst.Iunit16 = Convert.ToDouble(obj);//	板形仪16区平直度I-unit        
               obj = dt.Rows[I][22]; if (obj.ToString().Length > 0) lst.Iunit17 = Convert.ToDouble(obj);//	板形仪17区平直度I-unit        
               obj = dt.Rows[I][23]; if (obj.ToString().Length > 0) lst.Iunit18 = Convert.ToDouble(obj);//	板形仪18区平直度I-unit        
               obj = dt.Rows[I][24]; if (obj.ToString().Length > 0) lst.Iunit19 = Convert.ToDouble(obj);//	板形仪19区平直度I-unit
               obj = dt.Rows[I][25]; if (obj.ToString().Length > 0) lst.Iunit20 = Convert.ToDouble(obj);//	板形仪20区平直度I-unit

               obj = dt.Rows[I][26]; if (obj.ToString().Length > 0) lst.Iunit21 = Convert.ToDouble(obj);//	板形仪21区平直度I-unit       
               obj = dt.Rows[I][27]; if (obj.ToString().Length > 0) lst.Iunit22 = Convert.ToDouble(obj);//	板形仪22区平直度I-unit        
               obj = dt.Rows[I][28]; if (obj.ToString().Length > 0) lst.Iunit23 = Convert.ToDouble(obj);//	板形仪23区平直度I-unit        
               obj = dt.Rows[I][29]; if (obj.ToString().Length > 0) lst.Iunit24 = Convert.ToDouble(obj);//	板形仪24区平直度I-unit        
               obj = dt.Rows[I][30]; if (obj.ToString().Length > 0) lst.Iunit25 = Convert.ToDouble(obj);//	板形仪25区平直度I-unit

               obj = dt.Rows[I][31]; if (obj.ToString().Length > 0) lst.Iunit26 = Convert.ToDouble(obj);//	板形仪26区平直度I-unit        
               obj = dt.Rows[I][32]; if (obj.ToString().Length > 0) lst.Iunit27 = Convert.ToDouble(obj);//	板形仪27区平直度I-unit        
               obj = dt.Rows[I][33]; if (obj.ToString().Length > 0) lst.Iunit28 = Convert.ToDouble(obj);//	板形仪28区平直度I-unit       
               obj = dt.Rows[I][34]; if (obj.ToString().Length > 0) lst.Iunit29 = Convert.ToDouble(obj);//	板形仪29区平直度I-unit               
               obj = dt.Rows[I][35]; if (obj.ToString().Length > 0) lst.Iunit30 = Convert.ToDouble(obj);//	板形仪30区平直度I-unit

               obj = dt.Rows[I][36]; if (obj.ToString().Length > 0) lst.Iunit31 = Convert.ToDouble(obj);//	板形仪31区平直度I-unit

               LST.Add(lst);
           }
           dt.Dispose();

           return LST;
       }

       public List<CRM_HisDB_TCM_F> GetCRM_HisDB_TCM_F(string F,string StartTime, string EndTime)
       {//冷轧厂 1-4的精轧机组数据
           List<CRM_HisDB_TCM_F> LST = new List<CRM_HisDB_TCM_F>();
           CRM_HisDB_TCM_F lst;


            object obj = new object();
            string[] tags = new string[24];            
            tags[00] = "LYQCRM.TCM_F"+ F +".Current";//	轧机主电机电流
            tags[01] = "LYQCRM.TCM_F"+ F +".DSGap";//	轧机传动侧辊缝(mm)
            tags[02] = "LYQCRM.TCM_F"+ F +".FGCGap";//	机架FGC辊缝
            tags[03] = "LYQCRM.TCM_F"+ F +".Fslip";//	机架前滑
            tags[04] = "LYQCRM.TCM_F"+ F +".IMRBender";//	中间辊弯辊力
            tags[05] = "LYQCRM.TCM_F"+ F +".IMRBenderSet";//	中间辊弯辊力设定
            tags[06] = "LYQCRM.TCM_F"+ F +".IMRbShift";//	机架中间辊下辊串辊量
            tags[07] = "LYQCRM.TCM_F"+ F +".IMRtShift";//	机架中间辊上辊串辊量
            tags[08] = "LYQCRM.TCM_F"+ F +".Leveling";//	轧机倾斜
            tags[09] = "LYQCRM.TCM_F"+ F +".Reduction";//	机架压下分配量

            tags[10] = "LYQCRM.TCM_F"+ F +".RFDif";//	轧机轧制压力差（WS-DS）
            tags[11] = "LYQCRM.TCM_F"+ F +".RForceSet";//	轧机轧制力设定
            tags[12] = "LYQCRM.TCM_F"+ F +".RollForce";//	轧机轧制压力
            tags[13] = "LYQCRM.TCM_F"+ F +".Speed";//轧机速度
            tags[14] = "LYQCRM.TCM_F"+ F +".SpeedSet";//	轧机速度设定
            tags[15] = "LYQCRM.TCM_F"+ F +".Tension";//	轧机间张力
            tags[16] = "LYQCRM.TCM_F"+ F +".TensionDif";//	轧机间张力差（WS-DS）
            tags[17] = "LYQCRM.TCM_F"+ F +".TensionSet";//	轧机间张力设定
            tags[18] = "LYQCRM.TCM_F"+ F +".ThickDif";//	轧机出口厚度偏差
            tags[19] = "LYQCRM.TCM_F"+ F +".ThickSet";//	轧机出口厚度设定

            tags[20] = "LYQCRM.TCM_F"+ F +".Torque";//轧机扭矩
            tags[21] = "LYQCRM.TCM_F"+ F +".UTension";//轧机单位张力
            tags[22] = "LYQCRM.TCM_F"+ F +".WRBender";//工作辊弯辊力
            tags[23] = "LYQCRM.TCM_F"+ F +".WSGap";//轧机工作侧辊缝(mm)	

           //获取历史数据
           System.Data.DataTable dt = sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(StartTime), Convert.ToDateTime(EndTime));
           //起始时间
           DateTime TimeStart = Convert.ToDateTime(StartTime);

           for (int I = 0; I < dt.Rows.Count; I++)
           {
               lst = new CRM_HisDB_TCM_F();

               lst.DateAndTime = Convert.ToDateTime(dt.Rows[I][00]);
               TimeSpan ts = Convert.ToDateTime(lst.DateAndTime) - TimeStart;
               lst.Duration = (float)ts.TotalMilliseconds / 60000;

               //dt.Rows[I][00]是时间列
                obj = dt.Rows[I][01]; if (obj.ToString().Length > 0) lst.Current = Convert.ToDouble(obj);//	轧机主电机电流
                obj = dt.Rows[I][02]; if (obj.ToString().Length > 0) lst.DSGap = Convert.ToDouble(obj);//	轧机传动侧辊缝(mm)
                obj = dt.Rows[I][03]; if (obj.ToString().Length > 0) lst.FGCGap = Convert.ToDouble(obj);//	机架FGC辊缝
                obj = dt.Rows[I][04]; if (obj.ToString().Length > 0) lst.Fslip = Convert.ToDouble(obj);//	机架前滑
                obj = dt.Rows[I][05]; if (obj.ToString().Length > 0) lst.IMRBender = Convert.ToDouble(obj);//	中间辊弯辊力
                obj = dt.Rows[I][06]; if (obj.ToString().Length > 0) lst.IMRBenderSet = Convert.ToDouble(obj);//	中间辊弯辊力设定
                obj = dt.Rows[I][07]; if (obj.ToString().Length > 0) lst.IMRbShift = Convert.ToDouble(obj);//	机架中间辊下辊串辊量
                obj = dt.Rows[I][08]; if (obj.ToString().Length > 0) lst.IMRtShift = Convert.ToDouble(obj);//	机架中间辊上辊串辊量
                obj = dt.Rows[I][09]; if (obj.ToString().Length > 0) lst.Leveling = Convert.ToDouble(obj);//	轧机倾斜

                obj = dt.Rows[I][10]; if (obj.ToString().Length > 0) lst.Reduction = Convert.ToDouble(obj);//	机架压下分配量
                obj = dt.Rows[I][11]; if (obj.ToString().Length > 0) lst.RFDif = Convert.ToDouble(obj);//	轧机轧制压力差（WS-DS）
                obj = dt.Rows[I][12]; if (obj.ToString().Length > 0) lst.RForceSet = Convert.ToDouble(obj);//	轧机轧制力设定
                obj = dt.Rows[I][13]; if (obj.ToString().Length > 0) lst.RollForce = Convert.ToDouble(obj);//	轧机轧制压力
                obj = dt.Rows[I][14]; if (obj.ToString().Length > 0) lst.Speed = Convert.ToDouble(obj);//轧机速度
                obj = dt.Rows[I][15]; if (obj.ToString().Length > 0) lst.SpeedSet = Convert.ToDouble(obj);//	轧机速度设定
                obj = dt.Rows[I][16]; if (obj.ToString().Length > 0) lst.Tension = Convert.ToDouble(obj);//	轧机间张力
                obj = dt.Rows[I][17]; if (obj.ToString().Length > 0) lst.TensionDif = Convert.ToDouble(obj);//	轧机间张力差（WS-DS）
                obj = dt.Rows[I][18]; if (obj.ToString().Length > 0) lst.TensionSet = Convert.ToDouble(obj);//	轧机间张力设定
                obj = dt.Rows[I][19]; if (obj.ToString().Length > 0) lst.ThickDif = Convert.ToDouble(obj);//	轧机出口厚度偏差

                obj = dt.Rows[I][20]; if (obj.ToString().Length > 0) lst.ThickSet = Convert.ToDouble(obj);//	轧机出口厚度设定
                obj = dt.Rows[I][21]; if (obj.ToString().Length > 0) lst.Torque = Convert.ToDouble(obj);//轧机扭矩
                obj = dt.Rows[I][22]; if (obj.ToString().Length > 0) lst.UTension = Convert.ToDouble(obj);//轧机单位张力
                obj = dt.Rows[I][23]; if (obj.ToString().Length > 0) lst.WRBender = Convert.ToDouble(obj);//工作辊弯辊力
                obj = dt.Rows[I][24]; if (obj.ToString().Length > 0) lst.WSGap = Convert.ToDouble(obj);//轧机工作侧辊缝(mm)	

               LST.Add(lst);
           }
           dt.Dispose();

           return LST;
       }

       /// <summary>
       ///平整线 
       /// </summary>
       /// <param name="F"></param>
       /// <param name="StartTime"></param>
       /// <param name="EndTime"></param>
       /// <returns></returns>
       public List<CRM_HisDB_SPM> GetCRM_HisDB_SPM(string StartTime, string EndTime)
       {
           List<CRM_HisDB_SPM> LST = new List<CRM_HisDB_SPM>();
           CRM_HisDB_SPM lst;


            object obj = new object();
            string[] tags = new string[14];           
            tags[00] = "LYQCRM.SPM.ELG1_ELG_ACT";//
            tags[01] = "LYQCRM.SPM.Enbri_Bot_Tension";//
            tags[02] = "LYQCRM.SPM.Enbri_Top_Tension";//
            tags[03] = "LYQCRM.SPM.Exbri_Bot_Tension";//
            tags[04] = "LYQCRM.SPM.Exbri_Top_Tension";//
            tags[05] = "LYQCRM.SPM.MILL_Speed_Act";//
            tags[06] = "LYQCRM.SPM.POR_Tension";//
            tags[07] = "LYQCRM.SPM.RBC1A_VAL_ACT";//
            tags[08] = "LYQCRM.SPM.RGC01_ELG_REF";//
            tags[09] = "LYQCRM.SPM.RGC01_StripWidth";//
            tags[10] = "LYQCRM.SPM.RGC01_TLT_ACT";//
            tags[11] = "LYQCRM.SPM.RGC01_TRF_ACT";//
            tags[12] = "LYQCRM.SPM.Strip_Thickness";//
            tags[13] = "LYQCRM.SPM.TER_Tension";//
        

           //获取历史数据
           System.Data.DataTable dt = sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(StartTime), Convert.ToDateTime(EndTime));
           //起始时间
           DateTime TimeStart = Convert.ToDateTime(StartTime);

           for (int I = 0; I < dt.Rows.Count; I++)
           {
               lst = new CRM_HisDB_SPM();

               lst.DateAndTime = Convert.ToDateTime(dt.Rows[I][00]);
               
               //dt.Rows[I][00]是时间列               
                obj = dt.Rows[I][01]; if (obj.ToString().Length > 0) lst.ELG1_ELG_ACT = Convert.ToDouble(obj);//
                obj = dt.Rows[I][02]; if (obj.ToString().Length > 0) lst.Enbri_Bot_Tension = Convert.ToDouble(obj);//
                obj = dt.Rows[I][03]; if (obj.ToString().Length > 0) lst.Enbri_Top_Tension = Convert.ToDouble(obj);//
                obj = dt.Rows[I][04]; if (obj.ToString().Length > 0) lst.Exbri_Bot_Tension = Convert.ToDouble(obj);//
                obj = dt.Rows[I][05]; if (obj.ToString().Length > 0) lst.Exbri_Top_Tension = Convert.ToDouble(obj);//
                obj = dt.Rows[I][06]; if (obj.ToString().Length > 0) lst.MILL_Speed_Act = Convert.ToDouble(obj);//
                obj = dt.Rows[I][07]; if (obj.ToString().Length > 0) lst.POR_Tension = Convert.ToDouble(obj);//
                obj = dt.Rows[I][08]; if (obj.ToString().Length > 0) lst.RBC1A_VAL_ACT = Convert.ToDouble(obj);//
                obj = dt.Rows[I][09]; if (obj.ToString().Length > 0) lst.RGC01_ELG_REF = Convert.ToDouble(obj);//
                obj = dt.Rows[I][10]; if (obj.ToString().Length > 0) lst.RGC01_StripWidth = Convert.ToDouble(obj);//
                obj = dt.Rows[I][11]; if (obj.ToString().Length > 0) lst.RGC01_TLT_ACT = Convert.ToDouble(obj);//
                obj = dt.Rows[I][12]; if (obj.ToString().Length > 0) lst.RGC01_TRF_ACT = Convert.ToDouble(obj);//
                obj = dt.Rows[I][13]; if (obj.ToString().Length > 0) lst.Strip_Thickness = Convert.ToDouble(obj);//
                obj = dt.Rows[I][14]; if (obj.ToString().Length > 0) lst.TER_Tension = Convert.ToDouble(obj);//
         
               LST.Add(lst);
           }
           dt.Dispose();

           return LST;
       }


       //获取钢卷在镀锌线CGL中的基本信息
       public List<CRM_CoilInfo_CGL> GetCRM_CoilInfo_CGL(string CoilID)
       {
           List<CRM_CoilInfo_CGL> LST = new List<CRM_CoilInfo_CGL>();
           CRM_CoilInfo_CGL lst = new CRM_CoilInfo_CGL();

           string strSQL = "SELECT * FROM  crm_cgl_info WHERE in_mat='" + CoilID + "'";
           DataTable dt = sqt.ReadDatatable_OraDB(strSQL);

           for (int RowIndex = 0; RowIndex < dt.Rows.Count; RowIndex++)
           {
                lst = new CRM_CoilInfo_CGL();
                              
                lst.PCOIL_SID= dt.Rows[RowIndex]["PCOIL_SID"].ToString(); 
                lst.OUT_MAT= dt.Rows[RowIndex]["out_mat"].ToString(); 
                lst.IN_MAT= dt.Rows[RowIndex]["in_mat"].ToString(); 
                lst.COIL_WEIGHT= dt.Rows[RowIndex]["COIL_WEIGHT"].ToString(); 
                lst.PROD_DAY= dt.Rows[RowIndex]["PROD_DAY"].ToString(); 
                lst.SHIFT_NO= dt.Rows[RowIndex]["SHIFT_NO"].ToString(); 
                lst.SHIFT_CREW= dt.Rows[RowIndex]["SHIFT_CREW"].ToString(); 
                lst.PROD_START= dt.Rows[RowIndex]["PROD_START"].ToString(); 
                lst.PROD_END= dt.Rows[RowIndex]["PROD_END"].ToString(); 
                lst.PROD_DURATION= dt.Rows[RowIndex]["PROD_DURATION"].ToString(); 
                lst.LENGTH= dt.Rows[RowIndex]["LENGTH"].ToString(); 
                lst.WIDTH= dt.Rows[RowIndex]["WIDTH"].ToString(); 
                lst.THICKNESS= dt.Rows[RowIndex]["THICKNESS"].ToString(); 
                lst.WEIGHT= dt.Rows[RowIndex]["WEIGHT"].ToString(); 
                lst.SCRAP_WEIGTH= dt.Rows[RowIndex]["SCRAP_WEIGTH"].ToString(); 
                lst.STEEL_GRADE= dt.Rows[RowIndex]["STEEL_GRADE"].ToString(); 
                lst.PLATE_WEIGHT_UPPER= dt.Rows[RowIndex]["PLATE_WEIGHT_UPPER"].ToString(); 
                lst.PLATE_WEIGHT_LOWER= dt.Rows[RowIndex]["PLATE_WEIGHT_LOWER"].ToString(); 
                lst.PLATE_WEIGHT_TOTAL= dt.Rows[RowIndex]["PLATE_WEIGHT_TOTAL"].ToString(); 
                lst.SPM_ELONGATION_AVG= dt.Rows[RowIndex]["SPM_ELONGATION_AVG"].ToString(); 
                lst.TLEV_ELONGATION_AVG= dt.Rows[RowIndex]["TLEV_ELONGATION_AVG"].ToString(); 
                lst.HOLD_FLAG= dt.Rows[RowIndex]["HOLD_FLAG"].ToString(); 
                lst.DIAM_INNER= dt.Rows[RowIndex]["DIAM_INNER"].ToString(); 
                lst.HEAT_CYCLE_CODE= dt.Rows[RowIndex]["HEAT_CYCLE_CODE"].ToString(); 
                lst.OIL_TYPE_CODE= dt.Rows[RowIndex]["OIL_TYPE_CODE"].ToString(); 
                lst.SURF_GROUP= dt.Rows[RowIndex]["SURF_GROUP"].ToString(); 
                lst.SURF_TREAT= dt.Rows[RowIndex]["SURF_TREAT"].ToString(); 
                lst.E_THICKNESS= dt.Rows[RowIndex]["E_THICKNESS"].ToString();
                lst.E_WIDTH = dt.Rows[RowIndex]["E_WIDTH"].ToString(); 
                 

               LST.Add(lst);
           }

           LST.Add(lst);
           return LST;
       }

       private void DefinePdfFont()
       { 
           //定义字体
            iTextSharp.text.pdf.BaseFont bfHei = iTextSharp.text.pdf.BaseFont.createFont(@"C:\Windows\Fonts\SIMHEI.TTF", iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);
            FontHei = new iTextSharp.text.Font(bfHei, 32,1);
            iTextSharp.text.pdf.BaseFont bfKai = iTextSharp.text.pdf.BaseFont.createFont(@"C:\Windows\Fonts\simkai.ttf", iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);
            FontKai = new iTextSharp.text.Font(bfKai, 32, 1);
            iTextSharp.text.pdf.BaseFont bfSun = iTextSharp.text.pdf.BaseFont.createFont(@"C:\Windows\Fonts\SIMSUN.TTC,1", iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);
            FontSong = new iTextSharp.text.Font(bfSun, 32,1);
            FontSong10 = new iTextSharp.text.Font(bfSun, 10, 1);
           //图的标题
            FontCaption = new iTextSharp.text.Font(bfKai, 12, 1);            
       }

    }
}
