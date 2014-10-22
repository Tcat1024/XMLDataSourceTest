using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QtDataTrace.Interfaces
{
    class CRMCoilInfo
    {
    }
    public class Mat_Pedigree
    { 
        public string OUT_MAT_ID ;        
        public string IN_MAT_ID1;     
        public string IN_MAT_ID2;       
        public string DEVICE_NO ;
        public string STEEL_GRADE;
        public string ORDER_ID;
        public string PROCESS_CODE ;       
        public DateTime START_TIME;
        public DateTime STOP_TIME;       
        public string START_LOC;      
        public string MAT1_LENTH ;
        public string MAT2_LENGTH; 

    }
    public class CRMCoilTrack
    {
        private string coilID;
        public string CoilID
        {
            get { return coilID; }
            set { coilID = value; }
        }

        private string thicknessIn;
        public string ThicknessIn
        {
            get { return thicknessIn; }
            set { thicknessIn = value; }
        }
        private string thicknessOut;
        public string ThicknessOut
        {
            get { return thicknessOut; }
            set { thicknessOut = value; }
        }
        private string widthIn;
        public string WidthIn
        {
            get { return widthIn; }
            set { widthIn = value; }
        }
        private string widthOut;
        public string WidthOut
        {
            get { return widthOut; }
            set { widthOut = value; }
        }
                
        //工序名称 
        private string pROCESS_CODE;
        public string PROCESS_CODE
        {
            get { return pROCESS_CODE; }
            set { pROCESS_CODE = value; }
        }

        
        //生产时间
        private string sTART_TIME;
        public string START_TIME
        {
            get { return sTART_TIME; }
            set { sTART_TIME = value; }
        }
        private string sTOP_TIME;
        public string STOP_TIME
        {
            get { return sTOP_TIME; }
            set { sTOP_TIME = value; }
        }

        //钢种
        private string steelGrade;
        public string SteelGrade
        {
            get { return steelGrade; }
            set { steelGrade = value; }
        }

        //入口物料号列表
        private string iN_MAT_LIST;
        public string IN_MAT_LIST
        {
            get { return iN_MAT_LIST; }
            set { iN_MAT_LIST = value; }
        }

        //出口物料号列表
        private string oUT_MAT_LIST;
        public string OUT_MAT_LIST
        {
            get { return oUT_MAT_LIST; }
            set { oUT_MAT_LIST = value; }
        }
    }

    public class CRM_STEELGRADE
    {
        //钢种
        //private string steelGrade;
        public string SteelGrade;
        //{
        //    get { return steelGrade; }
        //    set { steelGrade = value; }
        //}
    }
     public class CRM_CoilInfo_PLTCM
     { //Pickling line 酸轧线
         
         
         public string SHIFT_ID;
         public string CREW_ID;

         //生产时间
         private string sTART_TIME;
         public string START_TIME
         {
             get { return sTART_TIME; }
             set { sTART_TIME = value; }
         }
         private string sTOP_TIME;
         public string STOP_TIME
         {
             get { return sTOP_TIME; }
             set { sTOP_TIME = value; }
         }

         //钢种
         private string steelGrade;
         public string SteelGrade
         {
             get { return steelGrade; }
             set { steelGrade = value; }
         }

         //入口物料号列表
         private string iN_MAT_LIST;
         public string IN_MAT_LIST
         {
             get { return iN_MAT_LIST; }
             set { iN_MAT_LIST = value; }
         }

         //出口物料号列表
         private string oUT_MAT_LIST;
         public string OUT_MAT_LIST
         {
             get { return oUT_MAT_LIST; }
             set { oUT_MAT_LIST = value; }
         }

         //步进式送卷机
         public string Conveyor01_Time;//Coil Turner;Entry Conveyor (W/B)
         public string Conveyor02_Time;//Coil Turner;Entry Conveyor (W/B)
         public string Conveyor03_Time;//Coil Turner;Entry Conveyor (W/B)
         public string Conveyor04_Time;//Coil Turner;Entry Conveyor (W/B)
         public string Conveyor05_Time;//Coil Turner;Entry Conveyor (W/B)
         public string Conveyor06_Time;//Coil Turner;Entry Conveyor (W/B)
         public string Conveyor07_Time;//Coil Turner;Entry Conveyor (W/B)
         public string Conveyor08_Time;//Coil Turner;Entry Conveyor (W/B)
         public string Conveyor09_Time;//Coil Turner;Entry Conveyor (W/B)

         public string CoilTransfer; //送卷机 CT1,CT2
         public string CoilTransTime;
         
         public string CoilCar;//钢卷车
         public string CoilCarsTime;//

         public string DeCoiler; //拆卷机,对应POR1,POR@
         public string DeCoilTime;

         public string WelderTime;//焊接时间

         public string EntryLooperTime_IN;//入口活套进入时间
         public string EntryLooperTime_OUT;//入口活套进入时间

         public string WPD1Time;//1#张紧辊

         public string TensionLevelerTime;//张力平整机

         public string PL_EntryTime;//酸洗池进入时间
         public string PL_ExitTime;//酸洗池退出时间 

         public string ExitLooper01Time_IN;//出口活套进入时间
         public string ExitLooper01Time_OUT;//出口活套进入时间

         public string WPD2Time;//2#张紧辊

         public string NotcherEntryTime ;//月牙剪

         public string WPD3Time;//3#张紧辊

         public string ExitLooper02Time_IN;//出口活套进入时间
         public string ExitLooper02Time_OUT;//出口活套进入时间

         public string WPD4Time;//4#张紧辊

         public string TCM_EntryTime;//进入轧机时间

         public string WPD5Time;//4#张紧辊

                                 
     }

     public class CRM_KeyEvents_PLTCM
     { //Pickling line 酸轧线

         public string KeyEventsName;
         public string DateAndTime;
         public float Duration;
         public string Description;

     }

        public class CRM_CoilInfo_CSL
        {//CrossSplitLine 横切线
            public string IN_MAT_ID_1;//
            public string IN_MAT_ID_2;//
            public string OUT_MAT_ID;//
            public string STEEL_GRADE;//
            public string PROD_TIME_START;//
            public string PROD_TIME_END;//
            public string SHIFT_ID;//
            public string CREW_ID;//
            public string PLAN_ID;//
            public string FINAL_COIL;//
            public string WORK_KIND_CODE;//
            public string ORDER_NO;//
            public string SURFACE;//
            public string SPEC_CHANGE_FLAG;//
            public string EXIT_LEN_TARGET;//
            public string EXIT_WIDTH_TARGET;//
            public string EXIT_THICK_TARGET;//
            public string EXIT_LEN;//
            public string EXIT_WIDTH;//
            public string EXIT_THICK;//
            public string ACT_NET_WEIGHT;//
            public string ACT_GROSS_WEIGHT;//
            public string SHEET_NUM;//
            public string SHEET_NUM_COIL_1;//
            public string MAT_WEIGHT_COIL_1;//
            public string SHEET_NUM_COIL_2;//
            public string MAT_WEIGHT_COIL_2;//
            public string HEAD_SCRAP_CODE;//
            public string HEAD_SCRAP_LEN;//
            public string HEAD_SCRAP_WEIGHT;//
            public string TAIL_SCRAP_CODE;//
            public string TAIL_SCRAP_LEN;//
            public string TAIL_SCRAP_WEIGHT;//
            public string TRIM_FLAG;//
            public string TRIM_WIDTH;//
            public string FLAG_WITH_LENGHT_CHANGE;//
            public string PILE_MACHINE;//
            public string RMND_FLAG;//
            public string PACK_TYPE;//

        }

        public class CRM_CoilInfo_RTL
        {//SlitterLine  纵切线
            public string COIL_ID;
            public string SHIFT_ID;
            public string CREW_ID;

            public string START_TIME;
            public string END_TIME;
        }
     
        public class CRM_CoilInfo_RCL
        {//Re Coiling Line重卷线
            public string IN_MAT_ID_1;//
            public string IN_MAT_ID_2;//
            public string OUT_MAT_ID;//
            public string EXITMATERIALID;//
            public string EXITMATERIALOD;//
            public string ORDER_NUMBER;//
            public string PRODUCT_TIME_START;//
            public string PRODUCT_TIME_END;//
            public string FINAL_COIL_FLAG;//
            public string REPAIR_FLAG;//
            public string REPAIR_COUNT;//
            public string PRODUCT_PLAN_NUMBER;//
            public string WORK_KIND_CODE;//
            public string STEEL_GRADE;//
            public string SG_SIGN;//
            public string SURFACE_ACCURACY;//
            public string SPEC_CHANGE_FLAG;//
            public string AIM_LENGTH;//
            public string IN_LENGTH_1;//
            public string IN_LENGTH_2;//
            public string OUT_LENGTH;//
            public string AIM_WIDTH;//
            public string IN_WIDTH_1;//
            public string IN_WIDTH_2;//
            public string OUT_WIDTH;//
            public string AIM_THICKNESS;//
            public string IN_THICKNESS_1;//
            public string IN_THICKNESS_2;//
            public string OUT_THICKNESS;//
            public string IN_WEIGHT_1;//
            public string IN_WEIGHT_2;//
            public string OUT_CAL_WEIGHT;//
            public string OUT_WEIGHT;//
            public string PRODUCTION_TIME_ELAPSED;//
            public string SHIFT_ID;//
            public string CREW_ID;//
            public string STOP_TIME;//
            public string STOP_NUMBER;//
            public string SURFACE_GROUP;//
            public string SURFACE_PROTECT;//
            public string SURFACE_DECIDE;//
            public string SURFACE_DECIDE_MAKER;//
            public string SCRAP_CODE_HEAD;//
            public string SCRAP_LENGTH_HEAD;//
            public string SCRAP_WEIGHT_HEAD;//
            public string SCRAP_CODE_TAIL;//
            public string SCRAP_LENGTH_TAIL;//
            public string SCRAP_WEIGHT_TAIL;//
            public string TRIM_FLAG;//
            public string TRIM_WIDTH;//
            public string OIL_TYPE_CODE;//
            public string OIL_QUANTITY;//
            public string STRIP_NUMBER_PACK;//

        }

        public class CRM_CoilInfo_SPM
        {//平整
          
            public string SHIFT_ID;
            public string CREW_ID;

            public string entry_mat_no;
            public string exit_mat_no;
            public string product_plan_no;
            public string order_no;
            public string st_grade;
            public string prod_time_start;
            public string prod_time_end;           
            public string order_thick;
            public string entry_mat_thick;
            public string exit_mat_thick;
            public string order_width;
            public string entry_mat_width;
            public string exit_mat_width;
            public string order_wt_unit_aim;
            public string entry_mat_wt;
            public string exit_mat_wt;
            public string entry_mat_len;
            public string exit_mat_len;
            public string entry_mat_inner_dia;
            public string exit_mat_inner_dia;
            public string entry_mat_outer_dia;
            public string exit_mat_outer_dia;
            public string elpow_cons;
            public string steam_cons;
            public string ctapwater_cons;
            public string demwater_cons;
            public string coolwater_cons;
            public string oil_consumption;


        }

    public class CRM_CoilInfo_AF
    {
        public string in_mat ;//        VARCHAR2(32),
        public string out_mat  ;//      VARCHAR2(20),
        public string stack_id;//堆垛号      NUMBER(12),
        public string coil_pos ;//堆垛在第几层，从下往上数 
        public string on_base ;//在第几号罩式退火炉 
        public string annealed  ;//     DATE,
        public string base_id ;//       NUMBER(6),
        public string  cycle  ;//        NUMBER(6),
        public string  hhood_id   ;//    NUMBER(6),
        public string  chood_id   ;//    NUMBER(6),
        public string  icover_id  ;//    NUMBER(6),
        public string  height   ;//      NUMBER(8),
        public string  weight  ;//       NUMBER(8),
        public string  width   ;//       NUMBER(8),
        public string  outerdiam  ;//    NUMBER(8),
        public string  innerdiam ;//     NUMBER(8),
        public string  thickness ;//     NUMBER(8),
        public string priority  ;//     NUMBER(3),
        public string  comment_count ;// NUMBER(3),
        public string transport_id;// NUMBER(6)       
        
    }

    public class CRM_HisDB_AF
    {

        public DateTime DateAndTime;
        public float Duration;

        //罩式炉的历史数据
        //形如 LYQCRM.AF_BASE_01.ATM_EXCHANGE_SET_RATE
        //public float	 	ATM_EXCHANGE_SET_RATE	;//	气氛置换体积设定
        //public float	 	TOTAL_HEAT_TIME	;//	加热时间总记
        public float	 	CTL_TEMP_ACT	;//	实际内罩温度
        //public float	 	CTL_TEMP_SET	;//	内罩温度设定值
        //public float	 	FLOW_STATE	;//	吹扫阶段
        public float	 	H2_FLOW_RATE_ACT	;//	实际氢气流量
        //public float	 	H2_FLOW_RATE_SET	;//	SPAC_H2FLOW
        //public float	 	HEAT_TIME	;//	加热时间
        //public float	 	AIR_COOLING_TIME	;//	快冷实际时间
        //public float	 	HH_COOLING_TIME	;//	带加热罩冷却时间设定
        //public float	 	HH_ID	;//	加热罩编号
        public float	 	HH_TEMP_ACT	;//	实际加热罩温度
        //public float	 	HH_TEMP_SET	;//	加热罩温度值设定值
        public float	 	N2_FLOW_RATE_ACT	;//	实际氮气流量
        //public float	 	N2_FLOW_RATE_SET	;//	SPAC_N2FLOW
        public float	 	O2_ACT	;//	氧含量
        //public float	 	PREV_FLOW_ACT_TIME	;//	预吹扫实际时间
        //public float	 	PROC_STATE	;//	过程阶段
        //public float	 	RAPID_COOLING_END_TEMP	;//	出炉温度设定
        public float	 	RAPID_COOLING_START_TEMP	;//	快冷开始温度
        //public float	 	RAPID_COOLING_TEMP	;//	喷淋冷却温度设定
        //public float	 	RAPID_COOLING_TIME	;//	快冷时间
        public float	 	RECIRC_RPM_ACT	;//	循环风机实际转速
       // public float	 	RECIRC_RPM_SET	;//	循环风机设定转速
        //public float	 	BASE_STATE	;//	炉台状态

    }

    public class CRM_HisDB_SPM
    { //平整线（注意在镀锌线后又光整机，功能相同）

        public DateTime DateAndTime;

        public double ELG1_ELG_ACT;//
        public double MILL_Speed_Act;//
        public double RBC1A_VAL_ACT;//
        public double Strip_Thickness;//
        

        public double Enbri_Bot_Tension;//
        public double Enbri_Top_Tension;//
        public double Exbri_Bot_Tension;//
        public double Exbri_Top_Tension;//
        public double POR_Tension;//
        public double TER_Tension;//

        public double RGC01_TLT_ACT;//
        public double RGC01_TRF_ACT;//
        public double RGC01_ELG_REF;//
        public double RGC01_StripWidth;//        
                
        public bool RBC1A_CTL_ON;//

    }



    public class CRM_HisDB_PL_BR
    { //酸洗

        public DateTime DateAndTime;
        public float Duration;

        public double BR1_Tension;//LYQCRM.PL_BR1.Tension BR张力
        public double BR2_Tension;//LYQCRM.PL_BR2.Tension BR张力
        public double BR3_Tension;//LYQCRM.PL_BR3.Tension BR张力
        public double DLP1_Tension;//LYQCRM.PL_DLP1.Tension 出口活套张力
        public double DLP2_Tension;//LYQCRM.PL_DLP2.Tension 出口活套张力
        public double ELP_Tension;//LYQCRM.PL_ELP.Tension 出口活套张力DLP1,DLP2,

        }

    public class CRM_HisDB_PL_PC
    { //酸洗

        public DateTime DateAndTime;
        public float Duration;
        //LYQCRM.PL_PC1 到 LYQCRM.PL_PC3
        public double 	AcidFlow	;//	LYQCRM.PL_PC1.AcidFlow	酸循环罐酸加酸流量
        //public double AcidFlowSet	;//	LYQCRM.PL_PC1.AcidFlowSet	酸循环罐酸加酸流量设定
        public double 	AddAcidFlow	;//	LYQCRM.PL_PC1.AddAcidFlow	槽加新酸流量
        public double 	FECL2	;//	LYQCRM.PL_PC1.FECL2	酸循环罐FECL2
        public double 	WaterFlow	;//	LYQCRM.PL_PC1.WaterFlow	酸循环罐酸加水流量
        //public double HCLSet	;//	LYQCRM.PL_PC1.HCLSet	酸循环罐HCL设定
        public double 	Level	;//	LYQCRM.PL_PC1.Level	酸循环罐液位
        //public double LevelSet	;//	LYQCRM.PL_PC1.LevelSet	酸循环罐液位设定
        public double 	Temperature	;//	LYQCRM.PL_PC1.Temperature	酸循环罐温度
        public double 	HCL	;//	LYQCRM.PL_PC1.HCL	酸循环罐HCL

    }

    public class CRM_HisDB_PL_PH
    { //酸洗

        public DateTime DateAndTime;
        public float Duration;
        //LYQCRM.PL_PH1 到 LYQCRM.PL_PH3

        //public double 	EntTemp	;//	LYQCRM.PL_PH1.EntTemp	槽石墨加热器入口温度
        ////public double 	EntTempSet	;//	LYQCRM.PL_PH1.EntTempSet	槽石墨加热器入口温度设定
        ////public double 	TemperatureSet	;//	LYQCRM.PL_PH1.TemperatureSet	槽石墨加热器出口温度设定
        //public double 	Temperature	;//	LYQCRM.PL_PH1.Temperature	槽石墨加热器出口温度
        //public double 	PHValue	;//	LYQCRM.PL_PH1.PHValue	槽石墨加热器冷凝水PH值

        public double PH1_EntTemp;//	LYQCRM.PL_PH1.EntTemp	槽石墨加热器入口温度        
        public double PH1_Temperature;//	LYQCRM.PL_PH1.Temperature	槽石墨加热器出口温度
        public double PH1_PHValue;//	LYQCRM.PL_PH1.PHValue	槽石墨加热器冷凝水PH值

        public double PH2_EntTemp;//	LYQCRM.PL_PH2.EntTemp	槽石墨加热器入口温度        
        public double PH2_Temperature;//	LYQCRM.PL_PH2.Temperature	槽石墨加热器出口温度
        public double PH2_PHValue;//	LYQCRM.PL_PH2.PHValue	槽石墨加热器冷凝水PH值

        public double PH3_EntTemp;//	LYQCRM.PL_PH3.EntTemp	槽石墨加热器入口温度        
        public double PH3_Temperature;//	LYQCRM.PL_PH3.Temperature	槽石墨加热器出口温度
        public double PH3_PHValue;//	LYQCRM.PL_PH3.PHValue	槽石墨加热器冷凝水PH值

        }

    public class CRM_HisDB_PL_PR2
    { //酸洗

        public DateTime DateAndTime;
        public float Duration;
        //LYQCRM.PL_PR1 --LYQCRM.PL_PR2
        public double 	Intermesh1	;//	直头机2#辊插入深度	LYQCRM.PL_PR1.Intermesh1
        public double 	Intermesh1Set	;//	直头机2#辊插入深度设定	LYQCRM.PL_PR1.Intermesh1Set
        public double 	Intermesh3Set	;//	直头机6#辊插入深度设定	LYQCRM.PL_PR1.Intermesh3Set
        public double 	Intermesh2Set	;//	直头机4#辊插入深度设定	LYQCRM.PL_PR1.Intermesh2Set
        public double 	Intermesh3;//	直头机6#辊插入深度	LYQCRM.PL_PR1.Intermesh3
        public double 	Intermesh2;//	直头机4#辊插入深度	LYQCRM.PL_PR1.Intermesh2

        }

    public class CRM_HisDB_PL_PT
    { //酸洗

        public DateTime DateAndTime;
        public float Duration;
        //LYQCRM.PL_PT1  --- 》 LYQCRM.PL_PT3
        public double 	Conductivity	;//	酸罐酸液电导率	LYQCRM.PL_PT1.Conductivity
        public double 	Density	;//	酸液浓度	LYQCRM.PL_PT1.Density
        public double 	DensityRatio	;//	酸罐酸液比重	LYQCRM.PL_PT1.DensityRatio
        public double 	EntAcidFlow	;//	酸槽入口酸流量	LYQCRM.PL_PT1.EntAcidFlow
        public double 	WasterAcidFlow	;//	废酸流量	LYQCRM.PL_PT1.WasterAcidFlow
        public double 	ExtAcidFlow	;//	酸槽出口酸流量	LYQCRM.PL_PT1.ExtAcidFlow
        //public double 	ExtAcidFlowSet	;//	酸槽出口酸流量设定	LYQCRM.PL_PT1.ExtAcidFlowSet
        public double 	InhibitorFlow	;//	缓蚀剂流量	LYQCRM.PL_PT1.InhibitorFlow
        public double 	Temperature	;//	酸液温度	LYQCRM.PL_PT1.Temperature
        //public double 	EntAcidFlowSet	;//	酸槽入口酸流量设定	LYQCRM.PL_PT1.EntAcidFlowSet

        }

    public class CRM_HisDB_PL_RT
    { //酸洗

        public DateTime DateAndTime;
        public float Duration;
        //LYQCRM.PL_RT4就一个        
        public double 	PH	;//	漂洗槽PH值	LYQCRM.PL_RT4.PH
        //public double 	TemperatureSet	;//	漂洗槽温度	LYQCRM.PL_RT4.TemperatureSet
        public double 	Temperature	;//	漂洗槽温度	LYQCRM.PL_RT4.Temperature
        //public double 	WaterFlowSet	;//	漂洗槽加水流量设定	LYQCRM.PL_RT4.WaterFlowSet
        public double 	WaterFlow	;//	漂洗槽加水流量	LYQCRM.PL_RT4.WaterFlow
        
        }

    public class CRM_HisDB_PL_SC2
    { //酸洗

        public DateTime DateAndTime;
        public float Duration;
        //LYQCRM.PL_SC1---LYQCRM.PL_SC2;
        public double	DsKGap	;//	碎边剪的传动侧间隙量	LYQCRM.PL_SC1.DsKGap
        //public double	WsKGapSet	;//	碎边剪工作侧刀片间隙量设定	LYQCRM.PL_SC1.WsKGapSet
        public double	WsKGap	;//	碎边剪工作侧刀片间隙量	LYQCRM.PL_SC1.WsKGap
        //public double	DsKGapSet	;//	碎边剪的传动侧间隙量设定	LYQCRM.PL_SC1.DsKGapSet

        }

    public class CRM_HisDB_PL_SHR
    { //酸洗

        public DateTime DateAndTime;
        public float Duration;
        //LYQCRM.PL_SHR
        public double	DownCutHeadLth	;//	下通道剪切头长度	LYQCRM.PL_SHR.DownCutHeadLth
        public double	UpCutTailLth	;//	上通道剪切尾长度	LYQCRM.PL_SHR.UpCutTailLth
        public double	UpCutHeadLth	;//	上通道剪切头长度	LYQCRM.PL_SHR.UpCutHeadLth
        public double	DownCutTailLth	;//	下通道剪切尾长度	LYQCRM.PL_SHR.DownCutTailLth

        }

    public class CRM_HisDB_PL_SM2
    { //酸洗

        public DateTime DateAndTime;
        public float Duration;
        //LYQCRM.PL_SM1--LYQCRM.PL_SM2
        public double 	DsKGap	;//	圆盘剪传动侧刀片间隙量	LYQCRM.PL_SM1.DsKGap
        //public double 	DsKGapSet	;//	圆盘剪传动侧刀片间隙量设定	LYQCRM.PL_SM1.DsKGapSet
        public double 	DsKLap	;//	圆盘剪传动侧刀片重叠量	LYQCRM.PL_SM1.DsKLap
        //public double 	WsKLapSet	;//	圆盘剪工作侧刀片重叠量设定	LYQCRM.PL_SM1.WsKLapSet
        public double 	WsKGap	;//	圆盘剪工作侧刀片间隙量	LYQCRM.PL_SM1.WsKGap
        //public double 	WsKGapSet	;//	圆盘剪工作侧刀片间隙量设定	LYQCRM.PL_SM1.WsKGapSet
        public double 	WsKLap	;//	圆盘剪工作侧刀片重叠量	LYQCRM.PL_SM1.WsKLap
        //public double 	DsKLapSet	;//	圆盘剪传动侧刀片重叠量设定	LYQCRM.PL_SM1.DsKLapSet

        }

    public class CRM_HisDB_PL_ST
    { //酸洗

        public DateTime DateAndTime;
        public float Duration;
        //LYQCRM.PL_ST 
         public double Speed;//圆盘剪的速度
         public double WidthSet;//圆盘剪设定宽度
         public double Width;//圆盘剪实际宽度

        }

    public class CRM_HisDB_PL_TL
    { //酸洗

        public DateTime DateAndTime;
        public float Duration;
        //LYQCRM.PL_TL
        public double	Elongation	;//	拉矫机延伸率	LYQCRM.PL_TL.Elongation
        //public double 	ElongationSet	;//	拉矫机延伸率设定	LYQCRM.PL_TL.ElongationSet
        public double 	Intermesh1	;//		LYQCRM.PL_TL.Intermesh1
        //public double 	Intermesh1Set	;//	拉矫机1#辊插入深度设定	LYQCRM.PL_TL.Intermesh1Set
        public double 	Use	;//	拉矫机投入	LYQCRM.PL_TL.Use
        //public double 	Intermesh2Set	;//	拉矫机2#辊插入深度设定	LYQCRM.PL_TL.Intermesh2Set
        public double 	Intermesh3	;//	拉矫机3#辊插入深度	LYQCRM.PL_TL.Intermesh3
        //public double 	Intermesh3Set	;//	拉矫机3#辊插入深度设定	LYQCRM.PL_TL.Intermesh3Set
        public double 	Tension	;//	拉矫机张力	LYQCRM.PL_TL.Tension
        public double 	Intermesh2	;//	拉矫机2#辊插入深度	LYQCRM.PL_TL.Intermesh2

        }

    public class CRM_HisDB_PL_TRK
    { //酸洗

        public DateTime DateAndTime;
        public float Duration;
        //LYQCRM.PL_TRK
        public double ARP_ON;//	ARP_ON	ARP正常
        public double DsKNo;//	DsKNo	圆盘剪工作侧在用刀片
        public double EntSpeed;//	EntSpeed	酸洗入口段速度
        public double ExtSpeed;//	ExtSpeed	酸洗出口段速度
        public double POR1_DIA;//	POR1_DIA	
        public double POR1_DIA_SET;//	POR1_DIA_SET	
        public double POR1_R_LENGTH;//	POR1_R_LENGTH	
        public double POR1_TensionON;//	POR1_TensionON	1#开卷机建张
        public double POR1_THICK;//	POR1_THICK	
        public double POR1_WIDTH;//	POR1_WIDTH	
        public double POR2_DIA;//	POR2_DIA	
        public double POR2_DIA_SET;//	POR2_DIA_SET	
        public double POR2_R_LENGTH;//	POR2_R_LENGTH	
        public double POR2_TensionON;//	POR2_TensionON	2#开卷机建张
        public double POR2_THICK;//	POR2_THICK	
        public double POR2_WIDTH;//	POR2_WIDTH	
        public double ProSpeed;//	ProSpeed	酸洗工艺段速度
        public double ST_WPE;//	ST_WPE	焊缝到圆盘剪的长度(END)
        public double ST_WPT;//	ST_WPT	焊缝到圆盘剪的长度(TOP)
        public double Thick1;//	Thick1	
        public double Thick2;//	Thick2	
        public double Thick3;//	Thick3	
        public double Thick4;//	Thick4	
        public double Thick5;//	Thick5	
        public double Thick6;//	Thick6	
        public double Thick7;//	Thick7	
        public double Thick8;//	Thick8	
        public double TL_WPE;//	TL_WPE	焊缝到拉矫机的长度（END）
        public double TL_WPT;//	TL_WPT	焊缝到拉矫机的长度（TOP）
        public double Welding;//	Welding	焊接
        public double Width1;//	Width1	
        public double Width2;//	Width2	
        public double Width3;//	Width3	
        public double Width4;//	Width4	
        public double Width5;//	Width5	
        public double Width6;//	Width6	
        public double Width7;//	Width7	
        public double Width8;//	Width8	
        public double WsKNo;//	WsKNo	圆盘剪传动侧在用刀片
        public double HCoilNo1;//	HCoilNo1	
        public double HCoilNo2;//	HCoilNo2	
        public double HCoilNo3;//	HCoilNo3	
        public double HCoilNo4;//	HCoilNo4	
        public double HCoilNo5;//	HCoilNo5	
        public double HCoilNo6;//	HCoilNo6	
        public double HCoilNo7;//	HCoilNo7	
        public double HCoilNo8;//	HCoilNo8	
        public double STCOILNO;//	STCOILNO	
        public double SteelGrade1;//	SteelGrade1	
        public double SteelGrade2;//	SteelGrade2	
        public double SteelGrade3;//	SteelGrade3	
        public double SteelGrade4;//	SteelGrade4	
        public double SteelGrade5;//	SteelGrade5	
        public double SteelGrade6;//	SteelGrade6	
        public double SteelGrade7;//	SteelGrade7	
        public double SteelGrade8;//	SteelGrade8	
        public double Zone1;//	Zone1	
        public double Zone2;//	Zone2	
        public double Zone3;//	Zone3	
        public double Zone4;//	Zone4	
        public double Zone5;//	Zone5	
        public double Zone6;//	Zone6	
        public double Zone7;//	Zone7	
        public double Zone8;//	Zone8

        }

    public class CRM_HisDB_PL_UTI
    { //酸洗

        public DateTime DateAndTime;
        public float Duration;
        //LYQCRM.PL_UTI
        public double CompAirFlow;//	CompAirFlow	压缩空气流量
        public double CompAirPress;//	CompAirPress	压缩空气压力
        public double CoolWatFlow;//	CoolWatFlow	冷却水流量
        public double SteamFlow;//	SteamFlow	蒸汽流量
        public double SteamPress;//	SteamPress	蒸汽压力
        public double SteamTemp;//	SteamTemp	蒸汽温度


    }

    public class CRM_HisDB_TCM_ASC
    {
        public DateTime DateAndTime;
        public float Duration;

        public double a;//	板形系数a
        //public double aSet;//	板形系数a设定值
        public double b;//	板形系数b
        //public double bSet;//	板形系数b设定值
        public double c;//	板形系数c
        //public double cSet;//	板形系数c设定值
        public double Gain;//	ASC增益

        public double Iunit1;//	板形仪1区平直度I-unit
        //public double Iunit1Set;//	板形1区平直度设定
        public double Iunit2;//	板形仪2区平直度I-unit
        //public double Iunit2Set;//	板形2区平直度设定
        public double Iunit3;//	板形仪3区平直度I-unit
        //public double Iunit3Set;//	板形3区平直度设定
        public double Iunit4;//	板形仪4区平直度I-unit
        //public double Iunit4Set;//	板形4区平直度设定
        public double Iunit5;//	板形仪5区平直度I-unit
        //public double Iunit5Set;//	板形5区平直度设定
        public double Iunit6;//	板形仪6区平直度I-unit
        //public double Iunit6Set;//	板形6区平直度设定
        public double Iunit7;//	板形仪7区平直度I-unit
        //public double Iunit7Set;//	板形7区平直度设定
        public double Iunit8;//	板形仪8区平直度I-unit
        //public double Iunit8Set;//	板形8区平直度设定
        public double Iunit9;//	板形仪9区平直度I-unit
        //public double Iunit9Set;//	板形9区平直度设定
        
        public double Iunit10;//	板形仪10区平直度I-unit
        //public double Iunit10Set;//	板形10区平直度设定
        public double Iunit11;//	板形仪11区平直度I-unit
        //public double Iunit11Set;//	板形11区平直度设定
        public double Iunit12;//	板形仪12区平直度I-unit
        //public double Iunit12Set;//	板形12区平直度设定
        public double Iunit13;//	板形仪13区平直度I-unit
        //public double Iunit13Set;//	板形13区平直度设定
        public double Iunit14;//	板形仪14区平直度I-unit
        //public double Iunit14Set;//	板形14区平直度设定
        public double Iunit15;//	板形仪15区平直度I-unit
        //public double Iunit15Set;//	板形15区平直度设定
        public double Iunit16;//	板形仪16区平直度I-unit
        //public double Iunit16Set;//	板形16区平直度设定
        public double Iunit17;//	板形仪17区平直度I-unit
        //public double Iunit17Set;//	板形17区平直度设定
        public double Iunit18;//	板形仪18区平直度I-unit
        //public double Iunit18Set;//	板形18区平直度设定
        public double Iunit19;//	板形仪19区平直度I-unit
        //public double Iunit19Set;//	板形19区平直度设定
        
        

        public double Iunit20;//	板形仪20区平直度I-unit
        //public double Iunit20Set;//	板形20区平直度设定
        public double Iunit21;//	板形仪21区平直度I-unit
        //public double Iunit21Set;//	板形21区平直度设定
        public double Iunit22;//	板形仪22区平直度I-unit
        //public double Iunit22Set;//	板形22区平直度设定
        public double Iunit23;//	板形仪23区平直度I-unit
        //public double Iunit23Set;//	板形23区平直度设定
        public double Iunit24;//	板形仪24区平直度I-unit
        //public double Iunit24Set;//	板形24区平直度设定
        public double Iunit25;//	板形仪25区平直度I-unit
        //public double Iunit25Set;//	板形25区平直度设定
        public double Iunit26;//	板形仪26区平直度I-unit
        //public double Iunit26Set;//	板形26区平直度设定
        public double Iunit27;//	板形仪27区平直度I-unit
        //public double Iunit27Set;//	板形27区平直度设定
        public double Iunit28;//	板形仪28区平直度I-unit
        //public double Iunit28Set;//	板形28区平直度设定
        public double Iunit29;//	板形仪29区平直度I-unit
        //public double Iunit29Set;//	板形29区平直度设定
               
        public double Iunit30;//	板形仪30区平直度I-unit
        //public double Iunit30Set;//	板形30区平直度设定
        public double Iunit31;//	板形仪31区平直度I-unit
        //public double Iunit31Set;//	板形31区平直度设定
        
        public double Mode;//	ASC模式
    }
   
    public class CRM_TCM_F01
    {
        public DateTime DateAndTime;
        public float Duration;

        public double Reduction;//	总压下分配
        public double Tension;//	0#轧机入口张力
        public double TensionDif;//	0#轧机入口张力差（WS-DS）
        public double TensionSet;//	0#轧机入口张力设定
        public double ThickDif;//	0#轧机入口厚度偏差
        public double ThickSet;//	0#轧机入口厚度设定
        public double UTension;//	0#轧机入口单位张力

    }
    public class CRM_HisDB_TCM_F
    {
        public DateTime DateAndTime;
        public float Duration;

        public double Current;//	轧机主电机电流
        public double DSGap	;//	轧机传动侧辊缝(mm)
        public double FGCGap	;//	机架FGC辊缝
        public double Fslip	;//	机架前滑
        public double IMRBender	;//	中间辊弯辊力
        public double IMRBenderSet	;//	中间辊弯辊力设定
        public double IMRbShift	;//	机架中间辊下辊串辊量
        public double IMRtShift	;//	机架中间辊上辊串辊量
        public double Leveling	;//	轧机倾斜
        public double Reduction	;//	机架压下分配量
        public double RFDif	;//	轧机轧制压力差（WS-DS）
        public double RForceSet;//	轧机轧制力设定
        public double RollForce;//	轧机轧制压力
        public double Speed;//轧机速度
        public double SpeedSet;//	轧机速度设定
        public double Tension;//	轧机间张力
        public double TensionDif;//	轧机间张力差（WS-DS）
        public double TensionSet;//	轧机间张力设定
        public double ThickDif;//	轧机出口厚度偏差
        public double ThickSet;//	轧机出口厚度设定
        public double Torque;//	轧机扭矩
        public double UTension;//	轧机单位张力
        public double WRBender;//	工作辊弯辊力
        public double WSGap;//	轧机工作侧辊缝(mm)


     
    }

    public class CRM_TCM_TR
    {
        public DateTime DateAndTime;
        public float Duration;

        public double COILDia;//	卷取机钢卷直径
        public double Current;//	卷取机电流
        public double ATRFault;//	4#机架出口ATR故障
        public double ATROn;//	4#机架出口ATR投入
        public double ATRSelect;//	4#机架出口ATR选择
        public double SATR;//	4#机架出口ATR S-ATR

    }

    public class CRM_TCM_TRK
    {
        public DateTime DateAndTime;
        public float Duration;

        public double ConstantRF;//	恒轧制力模式
        public double CP_F0;//	剪切点到0#轧机的长度
        public double DP_FO;//	月牙点到0#轧机的长度
        public double Mateaial_Thk;//	来料厚度
        public double Material_Width;//	来料宽度
        public double MaxSpeed;//	最大线速度
        public double Product_Thk;//	产品厚度
        public double TCMEn_Speed;//	轧制入口速度
        public double TCMEx_Speed;//	轧制出口速度
        public double WindLth;//	卷取长度
        public double WP_F0;//	焊缝到0#轧机的长度
        public double YP;//	屈服强度

    }

    /// <summary>
    /// 镀锌线
    /// </summary>
    public class CRM_HisDB_CGL
    {
        public DateTime DateAndTime;
        public float Duration;

        //LYQCRM.CGL_ENT.
        public double Electrolyte_conductance_QI119_Len4;//	uwei
        public double Electrolyte_TemLen4;//	
        public double Electrolyte_TempTIC_103_Len4;//	
        public double ELTension_Len3;//	
        public double Entry_Act_Speed_Len1;//	入口速度
        public double Hot_water_brush_TempTIC_101_Len4;//	
        public double POR_Tension_Len2;//	开卷机张力

        //LYQCRM.CGL_EXIT
        public double Exit_Act_Speed_Len35;//LYQCRM.CGL_EXIT	出口段速度
        public double Steel_Down_coating_oil_Len33;//LYQCRM.CGL_EXIT	带钢下表面涂油量
        public double Steel_Ucoating_oil_Len33;//LYQCRM.CGL_EXIT	带钢上表面涂油量
        public double TR_1_Tension_Len36;//LYQCRM.CGL_EXIT	1#卷取机张力
        public double TR_2_Tension_Len37;//LYQCRM.CGL_EXIT	2#卷取机张力
        public double XLTension_Len34;//LYQCRM.CGL_EXIT	出口活套张力

    }

    public class CRM_HisDB_CGL_FUR
    {
        public DateTime DateAndTime;
        public float Duration;
        public double ATM_dew_point_69_Len24;//LYQCRM.CGL_FUR	ATM段露点69
        public double ATM_section_H2_in_percent_Len24;//LYQCRM.CGL_FUR	ATM段氢气含量
        public double ATM_section_O2_in_percent_Len24;//LYQCRM.CGL_FUR	ATM段氧含量
        public double ATM_section_O2_PPM_Len24;//LYQCRM.CGL_FUR	ATM段氧含量
        public double Coke_gas_pressure_Len6;//LYQCRM.CGL_FUR	焦炉煤气压力 PI222
        public double FI305_Len12;//LYQCRM.CGL_FUR	FI-305
        public double FI522_Len24;//LYQCRM.CGL_FUR	FI-522
        public double FI542_Len24;//LYQCRM.CGL_FUR	FI-542
        public double FIQ512_Len24;//LYQCRM.CGL_FUR	FIQ-512(FQ-512)
        public double FIQ522_Len24;//LYQCRM.CGL_FUR	FIQ-522
        public double FIQ542_Len24;//LYQCRM.CGL_FUR	FIQ542
        public double FQ305_Len12;//LYQCRM.CGL_FUR	FQ-305
        public double Furnace_Tension_Len5;//LYQCRM.CGL_FUR	退火炉张力
        public double Gas_value_CH4_Len6;//LYQCRM.CGL_FUR	煤气热值 CH4
        public double Gas_value_CO_Len6;//LYQCRM.CGL_FUR	煤气热值 CO
        public double Gas_value_CO2_Len6;//LYQCRM.CGL_FUR	煤气热值 CO2
        public double Gas_value_H2_Len6;//LYQCRM.CGL_FUR	煤气热值 H2
        public double Gas_value_O2_N2_Len6;//LYQCRM.CGL_FUR	煤气热值 O2+N2
        public double JCF_1_blower_speed_Len20;//LYQCRM.CGL_FUR	JCF段1#风机转速
        public double JCF_2_blower_speed_Len20;//LYQCRM.CGL_FUR	JCF段2#风机转速
        public double JCF_3_blower_speed_Len20;//LYQCRM.CGL_FUR	JCF段3#风机转速
        public double JCF_4_blower_speed_Len21;//LYQCRM.CGL_FUR	JCF段4#风机转速
        public double JCF_5_blower_speed_Len22;//LYQCRM.CGL_FUR	JCF段5#风机转速
        public double JCF_6_blower_speed_Len23;//LYQCRM.CGL_FUR	JCF段6#风机转速
        public double JCF_dew_point_67_Len22;//LYQCRM.CGL_FUR	JCF段露点67
        public double JCF_section_H2_in_percent_Len22;//LYQCRM.CGL_FUR	JCF段氢气含量
        public double JCF_section_O2_in_percent_Len22;//LYQCRM.CGL_FUR	JCF段氧含量
        public double JCF_section_O2_PPM_Len22;//LYQCRM.CGL_FUR	JCF段氧含量-PPM
        public double JCF_Zone_1_Temp_TIA411_1_Len20;//LYQCRM.CGL_FUR	JCF段1区炉温
        public double JCF_Zone_2_Temp_TIA411_2_Len21;//LYQCRM.CGL_FUR	JCF段2区炉温
        public double JCF_Zone_3_Temp_TIA411_3_Len22;//LYQCRM.CGL_FUR	JCF段3区炉温
        public double Mixed_gas_pressure_Len6;//LYQCRM.CGL_FUR	混合煤气压力
        public double NOF_entry_O2_QI268B_Len6;//LYQCRM.CGL_FUR	NOF段入口氧含量
        public double NOF_exhaust_gas_Temp_Len11;//LYQCRM.CGL_FUR	NOF段废气温度
        public double NOF_Fur_Pre_Regul_PV_PIC212_Len6;//LYQCRM.CGL_FUR	NOF段炉压调节阀开口度
        public double NOF_furnace_pressure_PIC212_Len6;//LYQCRM.CGL_FUR	NOF段炉压
        public double NOF_gas_flow_FI222_Len6;//LYQCRM.CGL_FUR	NOF段煤气流量
        public double NOF_heat_exchanger_Temp_Len11;//LYQCRM.CGL_FUR	NOF段热交换器温度
        public double NOF_main_air_pressure_PIC245_Len6;//LYQCRM.CGL_FUR	NOF段主空气压力
        public double NOF_main_gas_pressure_PIC226_Len6;//LYQCRM.CGL_FUR	NOF段主燃烧煤气压力
        public double NOF_total_gas_flow_FQ222_Len6;//LYQCRM.CGL_FUR	NOF段煤气总流量
        public double NOF_total_gas_pressure_PI222_Len6;//LYQCRM.CGL_FUR	NOF段总管煤气压力
        public double NOF_va_actual_speed_Len6;//LYQCRM.CGL_FUR	风机实际转速
        public double NOF_ZONE_0_air_flow_FIC223_0_Len6;//LYQCRM.CGL_FUR	NOF段0区空气流量
        public double NOF_ZONE_0_air_gas_Len6;//LYQCRM.CGL_FUR	NOF段0区空煤比
        public double NOF_ZONE_0_Temp_TIC215_0_Len6;//LYQCRM.CGL_FUR	NOF段0区煤气流量
        public double NOF_ZONE_1_air_gas_Len7;//LYQCRM.CGL_FUR	NOF段1区空煤比
        public double NOF_ZONE_1_CO_Len10;//LYQCRM.CGL_FUR	NOF段1区CO值
        public double NOF_ZONE_1_CO2_Len10;//LYQCRM.CGL_FUR	NOF段1区CO2值
        public double NOF_ZONE_1_Gas_flow_FIC223_1_Len7;//LYQCRM.CGL_FUR	NOF段1区煤气流量
        public double NOF_ZONE_1_Temp_TIC215_1_Len7;//LYQCRM.CGL_FUR	NOF段1区炉温
        public double NOF_ZONE_2_air_gas_Len8;//LYQCRM.CGL_FUR	NOF段2区空煤比
        public double NOF_ZONE_2_CO_Len10;//LYQCRM.CGL_FUR	NOF段2区CO值
        public double NOF_ZONE_2_CO2_Len10;//LYQCRM.CGL_FUR	NOF段2区CO2值
        public double NOF_ZONE_2_Gas_flow_FIC223_2_Len8;//LYQCRM.CGL_FUR	NOF段2区煤气流量
        public double NOF_ZONE_2_Temp_TIC215_2_Len8;//LYQCRM.CGL_FUR	NOF段2区炉温
        public double NOF_ZONE_3_air_gas_Len9;//LYQCRM.CGL_FUR	NOF段3区空煤比
        public double NOF_ZONE_3_CO_Len10;//LYQCRM.CGL_FUR	NOF段3区CO值
        public double NOF_ZONE_3_CO2_Len10;//LYQCRM.CGL_FUR	NOF段3区CO2值
        public double NOF_ZONE_3_Gas_flow_FIC223_3_Len9;//LYQCRM.CGL_FUR	NOF段3区煤气流量
        public double NOF_ZONE_3_Temp_TIC215_3_Len9;//LYQCRM.CGL_FUR	NOF段3区炉温
        public double NOF_ZONE_4_air_gas_Len10;//LYQCRM.CGL_FUR	NOF段4区空煤比
        public double NOF_ZONE_4_CO_Len10;//LYQCRM.CGL_FUR	NOF段4区CO值
        public double NOF_ZONE_4_CO2_Len10;//LYQCRM.CGL_FUR	NOF段4区CO2值
        public double NOF_ZONE_4_Gas_flow_FIC223_4_Len10;//LYQCRM.CGL_FUR	NOF段4区煤气流量
        public double NOF_ZONE_4_Temp_TIC215_4_Len10;//LYQCRM.CGL_FUR	NOF段4区炉温
        public double NOF_Zone_striTemp_TIA201_Len11;//LYQCRM.CGL_FUR	NOF段板温
        public double PH_furnace_TempTIC_210_Len6;//LYQCRM.CGL_FUR	PH段炉温
        public double PIC325_Len12;//LYQCRM.CGL_FUR	PIC325
        public double PIC345_Len12;//LYQCRM.CGL_FUR	PIC345
        public double PIC355_Len12;//LYQCRM.CGL_FUR	PIC355
        public double Pretect_gas_ratio_Len24;//LYQCRM.CGL_FUR	保护气体比值69
        public double protect_gas_H2_flow_FIc512_Len24;//LYQCRM.CGL_FUR	保护气体氢气流量69
        public double protect_gas_N2_flow_FIc523_Len24;//LYQCRM.CGL_FUR	保护气体氮气流量69
        public double RTF_dew_point_62_Len17;//LYQCRM.CGL_FUR	RTF段露点62
        public double RTF_furnace_pressure_PIA312_Len17;//LYQCRM.CGL_FUR	RTF段炉压
        public double RTF_gas_pressure_PIA306_Len12;//LYQCRM.CGL_FUR	RTF段煤气压力
        public double RTF_section_H2_in_percent_Len17;//LYQCRM.CGL_FUR	RTF段氢气含量
        public double RTF_section_O2_in_percent_Len17;//LYQCRM.CGL_FUR	RTF段氧含量
        public double RTF_section_O2_PPM_Len17;//LYQCRM.CGL_FUR	RTF段氧含量
        public double RTF_striTemp_TIA301_Len19;//LYQCRM.CGL_FUR	RTF板温
        public double RTF_Zone_1_Air_flow_FIC342_1_Len12;//LYQCRM.CGL_FUR	RTF段1区空气流量
        public double RTF_Zone_1_Air_Gas_Len18;//LYQCRM.CGL_FUR	RTF段1区空煤比
        public double RTF_Zone_1_Gas_flow_FIC322_1_Len12;//LYQCRM.CGL_FUR	RTF段1区煤气流量
        public double RTF_Zone_1_Temp_TIC310_1_Len12;//LYQCRM.CGL_FUR	RTF段1区炉温
        public double RTF_Zone_2_Air_flow_FIC342_2_Len13;//LYQCRM.CGL_FUR	RTF段2区空气流量
        public double RTF_Zone_2_Air_Gas_Len18;//LYQCRM.CGL_FUR	RTF段2区空煤比
        public double RTF_Zone_2_Gas_flow_FIC322_2_Len13;//LYQCRM.CGL_FUR	RTF段2区煤气流量
        public double RTF_Zone_2_Temp_TIC3102_Len13;//LYQCRM.CGL_FUR	RTF段2区炉温
        public double RTF_Zone_3_Air_flow_FIC342_3_Len14;//LYQCRM.CGL_FUR	RTF段3区空气流量
        public double RTF_Zone_3_Air_Gas_Len18;//LYQCRM.CGL_FUR	RTF段3区空煤比
        public double RTF_Zone_3_Gas_flow_FIC322_3_Len14;//LYQCRM.CGL_FUR	RTF段3区煤气流量
        public double RTF_Zone_3_Temp_TIC310_3_Len14;//LYQCRM.CGL_FUR	RTF段3区炉温
        public double RTF_Zone_4_Air_flow_FIC342_4_Len15;//LYQCRM.CGL_FUR	RTF段4区空气流量
        public double RTF_Zone_4_Air_Gas_Len18;//LYQCRM.CGL_FUR	RTF段4区空煤比
        public double RTF_Zone_4_Gas_flow_FIC322_4_Len15;//LYQCRM.CGL_FUR	RTF段4区煤气流量
        public double RTF_Zone_4_Temp_TIC310_4_Len15;//LYQCRM.CGL_FUR	RTF段4区炉温
        public double RTF_Zone_5_Air_flow_FIC342_5_Len16;//LYQCRM.CGL_FUR	RTF段5区空气流量
        public double RTF_Zone_5_Air_Gas_Len18;//LYQCRM.CGL_FUR	RTF段5区空煤比
        public double RTF_Zone_5_Gas_flow_FIC322_5_Len16;//LYQCRM.CGL_FUR	RTF段5区煤气流量
        public double RTF_Zone_5_Temp_TIC310_5_Len16;//LYQCRM.CGL_FUR	RTF段5区炉温
        public double RTF_Zone_6_Air_flow_FIC342_6_Len17;//LYQCRM.CGL_FUR	RTF段6区空气流量
        public double RTF_Zone_6_Air_Gas_Len18;//LYQCRM.CGL_FUR	RTF段6区空煤比
        public double RTF_Zone_6_Gas_flow_FIC322_6_Len17;//LYQCRM.CGL_FUR	RTF段6区煤气流量
        public double RTF_Zone_6_Temp_TIC310_6_Len17;//LYQCRM.CGL_FUR	RTF段6区炉温
        public double RTF_Zone_7_Air_flow_FIC342_7_Len18;//LYQCRM.CGL_FUR	RTF段7区空气流量
        public double RTF_Zone_7_Air_Gas_Len18;//LYQCRM.CGL_FUR	RTF段7区空煤比
        public double RTF_Zone_7_Gas_flow_FIC322_7_Len18;//LYQCRM.CGL_FUR	RTF段7区煤气流量
        public double RTF_Zone_7_Temp_TIC310_7_Len18;//LYQCRM.CGL_FUR	RTF段7区炉温
        public double TDS_dew_point_69_Len24;//LYQCRM.CGL_FUR	TDS段露点69
        public double TDS_furnace_pressure_PIA_414_Len24;//LYQCRM.CGL_FUR	TDS段炉压
        public double TDS_section_H2_in_percent_Len24;//LYQCRM.CGL_FUR	TDS段氢气含量
        public double TDS_section_O2_in_percent_Len24;//LYQCRM.CGL_FUR	TDS段氧含量
        public double TDS_section_O2_PPM_Len24;//LYQCRM.CGL_FUR	TDS段氧含量
        public double TDS_striTemp_TIA501_Len24;//LYQCRM.CGL_FUR	TDS段板温
            }
    public class CRM_HisDB_CGL_LEN
    {
        public DateTime DateAndTime;
        public float Duration;
        public double Length_1;//LYQCRM.CGL_LEN	长度1
        public double Length_2;//LYQCRM.CGL_LEN	长度2
        public double Length_3;//LYQCRM.CGL_LEN	长度3
        public double Length_4;//LYQCRM.CGL_LEN	长度4
        public double Length_5;//LYQCRM.CGL_LEN	长度5
        public double Length_6;//LYQCRM.CGL_LEN	长度6
        public double Length_7;//LYQCRM.CGL_LEN	长度7
        public double Length_8;//LYQCRM.CGL_LEN	长度8
        public double Length_9;//LYQCRM.CGL_LEN	长度9

        public double Length_10;//LYQCRM.CGL_LEN	长度10
        public double Length_11;//LYQCRM.CGL_LEN	长度11
        public double Length_12;//LYQCRM.CGL_LEN	长度12
        public double Length_13;//LYQCRM.CGL_LEN	长度13
        public double Length_14;//LYQCRM.CGL_LEN	长度14
        public double Length_15;//LYQCRM.CGL_LEN	长度15
        public double Length_16;//LYQCRM.CGL_LEN	长度16
        public double Length_17;//LYQCRM.CGL_LEN	长度17
        public double Length_18;//LYQCRM.CGL_LEN	长度18
        public double Length_19;//LYQCRM.CGL_LEN	长度19
        
        public double Length_20;//LYQCRM.CGL_LEN	长度20
        public double Length_21;//LYQCRM.CGL_LEN	长度21
        public double Length_22;//LYQCRM.CGL_LEN	长度22
        public double Length_23;//LYQCRM.CGL_LEN	长度23
        public double Length_24;//LYQCRM.CGL_LEN	长度24
        public double Length_25;//LYQCRM.CGL_LEN	长度25
        public double Length_26;//LYQCRM.CGL_LEN	长度26
        public double Length_27;//LYQCRM.CGL_LEN	长度27
        public double Length_28;//LYQCRM.CGL_LEN	长度28
        public double Length_29;//LYQCRM.CGL_LEN	长度29
        
        public double Length_30;//LYQCRM.CGL_LEN	长度30
        public double Length_31;//LYQCRM.CGL_LEN	长度31
        public double Length_32;//LYQCRM.CGL_LEN	长度32
        public double Length_33;//LYQCRM.CGL_LEN	长度33
        public double Length_34;//LYQCRM.CGL_LEN	长度34
        public double Length_35;//LYQCRM.CGL_LEN	长度35
        public double Length_36;//LYQCRM.CGL_LEN	长度36
        public double Length_37;//LYQCRM.CGL_LEN	长度37

        public double PA_hot_blow_temp_TIC718_Len33;//LYQCRM.CGL_PA 钝化热风干燥温度

    }
    public class CRM_HisDB_CGL_SPM
    {
        public DateTime DateAndTime;
        public float Duration;
        public double Process_Act_Speed_Len30;//LYQCRM.CGL_SPM	中央段速度
        public double SPM_Anti_crease_height_Len31;//LYQCRM.CGL_SPM	光整机防皱辊高度
        public double SPM_Anti_quiver_height_Len31;//LYQCRM.CGL_SPM	光整机防颤辊高度
        public double SPM_bending_force_Len31;//LYQCRM.CGL_SPM	光整机弯辊力
        public double SPM_DS_force_Len31;//LYQCRM.CGL_SPM	光整机工作侧轧制力
        public double SPM_DS_gap_Len31;//LYQCRM.CGL_SPM	光整机工作侧辊缝值
        public double SPM_elongation_Len31;//LYQCRM.CGL_SPM	光整机延伸率
        public double SPM_entry_Tension_Len31;//LYQCRM.CGL_SPM	光整机入口张力
        public double SPM_exit_Tension_Len31;//LYQCRM.CGL_SPM	光整机出口张力
        public double SPM_rolling_force_Len31;//LYQCRM.CGL_SPM	光整机轧制力
        public double SPM_WS_force_Len31;//LYQCRM.CGL_SPM	光整机工作侧轧制力
        public double SPM_WS_gap_Len31;//LYQCRM.CGL_SPM	光整机工作侧辊缝值
    }
    public class CRM_HisDB_CGL_TLV
    {
        public DateTime DateAndTime;
        public float Duration;
        public double TLV_C_warPush_dowm_distance_Len32;//LYQCRM.CGL_TLV	拉矫机C翘单元压下量
        public double TLV_Elongation_Len32;//LYQCRM.CGL_TLV	拉矫机延伸率
        public double TLV_elongation_Push_dowm_distance_Len32;//LYQCRM.CGL_TLV	拉矫机延伸单元压下量
        public double TLV_L_warPush_dowm_distance_Len32;//LYQCRM.CGL_TLV	拉矫机L翘单元压下量
        public double TLV_Tension_Len32;//LYQCRM.CGL_TLV	拉矫机张力
    }
    public class CRM_HisDB_CGL_UF
    {
        public DateTime DateAndTime;
        public float Duration;

        public double UF_down_coating_roller_speed_Len33;//LYQCRM.CGL_UF	耐指纹下涂敷辊速度
        public double UF_down_extact_material_roller_speed_Len33;//LYQCRM.CGL_UF	耐指纹下提料辊速度
        public double UF_down_roller_ds_pressure_Len33;//LYQCRM.CGL_UF	耐指纹下辊传动侧辊间压力
        public double UF_down_roller_ws_pressure_Len33;//LYQCRM.CGL_UF	耐指纹下辊工作侧辊间压力
        public double UF_heat_exit_steel_temp_Len33;//LYQCRM.CGL_UF	耐指纹加热器出口带钢温度
        public double UF_power_Len33;//LYQCRM.CGL_UF	耐指纹炉功率
        public double UF_temperature_Len33;//LYQCRM.CGL_UF	耐指纹板温
        public double UF_Tension_Len33;//LYQCRM.CGL_UF	耐指纹段张力
        public double UF_ucoating_roller_speed_Len33;//LYQCRM.CGL_UF	耐指纹上涂敷辊速度
        public double UF_uextact_material_roller_speed_Len33;//LYQCRM.CGL_UF	v
        public double UF_uroller_ds_pressure_Len33;//LYQCRM.CGL_UF	耐指纹上辊传动侧辊间压力
        public double UF_uroller_ws_pressure_Len33;//LYQCRM.CGL_UF	耐指纹上辊工作侧辊间压力

    }
    public class CRM_HisDB_CGL_ZIN
    {
        public DateTime DateAndTime;
        public float Duration;

        public double Act_thick_1TR_Len30;//LYQCRM.CGL_ZIN	成品带钢厚度(1#TR)
        public double Act_thick_2TR_Len30;//LYQCRM.CGL_ZIN	成品带钢厚度(2#TR)
        public double Back_main_Nozzle_DS_gaLen25;//LYQCRM.CGL_ZIN	后气刀传动侧距离
        public double Back_main_Nozzle_pressure_Len25;//LYQCRM.CGL_ZIN	后气刀压力
        public double Back_main_Nozzle_WS_gaLen25;//LYQCRM.CGL_ZIN	后气刀工作侧距离
        public double Back_staroll_DS_position_Len25;//LYQCRM.CGL_ZIN	后稳定辊传动侧位置
        public double Back_staroll_WS_position_Len25;//LYQCRM.CGL_ZIN	后稳定辊工作侧位置
        public double Back_sub_Nozzle_DS_gaLen25;//LYQCRM.CGL_ZIN	后辅助喷嘴传动侧距离
        public double Back_sub_Nozzle_pressure_Len25;//LYQCRM.CGL_ZIN	后辅助喷气刀压力
        public double Back_sub_Nozzle_WS_gaLen25;//LYQCRM.CGL_ZIN	后辅助喷嘴工作侧距离
        public double BC_Act_coating_weight_Len30;//LYQCRM.CGL_ZIN	实际镀层下面中
        public double BDW_Act_coating_weight_Len30;//LYQCRM.CGL_ZIN	实际镀层下面右
        public double BOTTOM_AVERAGE_Len30;//LYQCRM.CGL_ZIN	实际镀层重量下表面
        public double BW_Act_coating_weight_Len30;//LYQCRM.CGL_ZIN	实际镀层下面左
        public double COATING_WEIGHT_Len30;//LYQCRM.CGL_ZIN	计划镀层重量
        public double FC_Act_coating_weight_Len30;//LYQCRM.CGL_ZIN	实际镀层上面中
        public double FD_Act_coating_weight_Len30;//LYQCRM.CGL_ZIN	实际镀层上面右
        public double Front_main_Nozzle_DS_gaLen25;//LYQCRM.CGL_ZIN	前气刀传动侧距离
        public double Front_main_Nozzle_pressure_Len25;//LYQCRM.CGL_ZIN	前气刀压力
        public double Front_main_Nozzle_WS_gaLen25;//LYQCRM.CGL_ZIN	前气刀工作侧距离
        public double Front_sub_Nozzle_DS_gaLen25;//LYQCRM.CGL_ZIN	前辅助喷嘴传动侧距离
        public double Front_sub_Nozzle_pressure_Len25;//LYQCRM.CGL_ZIN	前辅助喷气刀压力
        public double Front_sub_Nozzle_WS_gaLen25;//LYQCRM.CGL_ZIN	前辅助喷嘴工作侧距离
        public double FW_Act_coating_weight_Len30;//LYQCRM.CGL_ZIN	实际镀层上面左
        public double GI_AZINC_POWER_Len25;//LYQCRM.CGL_ZIN	GI锌锅A感应器功率
        public double GI_BZINC_POWER_Len25;//LYQCRM.CGL_ZIN	GI锌锅B感应器功率
        public double GI_mainzinc_POW_Len25;//LYQCRM.CGL_ZIN	GL主锅感应器功率
        public double GI_mainzinc_temLen25;//LYQCRM.CGL_ZIN	GL主锌锅温度
        public double GI_YUzinc_POW_Len25;//LYQCRM.CGL_ZIN	GL预熔锅感应器功率
        public double GI_yuzinc_temLen25;//LYQCRM.CGL_ZIN	GL预熔锅温度
        public double GI_zinc_pot_temLen25;//LYQCRM.CGL_ZIN	GI锌锅温度
        public double Main_Nozzle_DS_position_Len25;//LYQCRM.CGL_ZIN	气刀传动侧高度
        public double Main_Nozzle_WS_position_Len25;//LYQCRM.CGL_ZIN	气刀工作侧高度
        public double Nozzle_bleeding_valve_pressure_Len25;//LYQCRM.CGL_ZIN	气刀放散阀压力
        public double Nozzle_bleeding_valve_pv_out_Len25;//LYQCRM.CGL_ZIN	气刀放散阀开度
        public double QTK_water_Temp_Len29;//LYQCRM.CGL_ZIN	水淬槽水温
        public double Quick_cool_1_blower_speed_Len26;//LYQCRM.CGL_ZIN	快冷风机1#转速4
        public double Quick_cool_2_blower_speed_Len26;//LYQCRM.CGL_ZIN	快冷风机2#转速5
        public double TOAVERAGE_Len30;//LYQCRM.CGL_ZIN	实际镀层重量上表面
        public double VA_AJC_01_speed_Len26;//LYQCRM.CGL_ZIN	1#AJC-1#冷却风机转速
        public double VA_AJC_02_speed_Len27;//LYQCRM.CGL_ZIN	1#AJC-2#冷却风机转速
        public double VA_AJC_03_speed_Len28;//LYQCRM.CGL_ZIN	1#AJC-3#冷却风机转速
        public double Zinc_Tension_Len25;//LYQCRM.CGL_ZIN	锌锅段张力

    }

    public class CRM_CoilInfo_CGL
    {
        public string PCOIL_SID;
        public string OUT_MAT;
        public string IN_MAT;
        public string COIL_WEIGHT;
        public string PROD_DAY;
        public string SHIFT_NO;
        public string SHIFT_CREW;
        public string PROD_START;
        public string PROD_END;
        public string PROD_DURATION;
        public string LENGTH;
        public string WIDTH;
        public string THICKNESS;
        public string WEIGHT;
        public string SCRAP_WEIGTH;
        public string STEEL_GRADE;
        public string PLATE_WEIGHT_UPPER;
        public string PLATE_WEIGHT_LOWER;
        public string PLATE_WEIGHT_TOTAL;
        public string SPM_ELONGATION_AVG;
        public string TLEV_ELONGATION_AVG;
        public string HOLD_FLAG;
        public string DIAM_INNER;
        public string HEAT_CYCLE_CODE;
        public string OIL_TYPE_CODE;
        public string SURF_GROUP;
        public string SURF_TREAT;
        public string E_THICKNESS;
        public string E_WIDTH;

    }
}
