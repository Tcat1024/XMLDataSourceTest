using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QtDataTrace.Interfaces
{
    class LYQ2250
    {
    }
    public class LY2250SlabNoInfo
    {
        public string slab_id;
	    public string productno;
	    public string steel_grade;	
	    public string fceno;
        public string charge_time;
    }
    public class HRM_BaseInfo_HF
    {
        public string slab_id;
        public string planno;
        public string productno;
        public string fceno;
        public string fcerow;
        public string fcenorow;
        public string charge_time;
        public string discharge_time;
        public string slab_width;
        public string slab_length;
        public string slab_thick;
        public string slab_weight;
        public string slab_plan_weight;
        public string steel_grade;
        public string in_recycle_time;
        public string RecycleSectionArrivalTime;
        public string RecycleSectionLeaveTime;
        public string in_pre_time;
        public string PreHeatSectionArrivalTime;
        public string PreHeatSectionLeaveTime;
        public string in_first_time;
        public string FirstHeatSectionArrivalTime;
        public string FirstHeatSectionLeaveTime;
        public string in_sec_time;
        public string SecondHeatSectionArrivalTime;
        public string SecondHeatSectionLeaveTime;
        public string in_soak_time;
        public string SoakSectionArrivalTime;
        public string SoakSectionLeaveTime;
        public string target_distemp;
        public string ave_distemp;
        public string surface_distemp;
        public string center_distemp;
        public string measure_rmtemp;
        public string calc_distemp;
        public string rm_status;
        public string time_stamp;
        public string slab_charge_avg;
        public string hcheat_exit_avg;
        public string preheat_exit_avg;
        public string fstheat_exit_avg;
        public string secheat_exit_avg;
        public string shift_no;
        public string group_no;
        public string steelgrade;
        public string dsurface_distemp;
        public string slab_measure_temp;
        public string target_rm_temp;
        public string real_width;
    }

    [Serializable]
    public class HisDB_HF
    {
        private DateTime dateAndTime;

        public DateTime DateAndTime
        {
            get { return dateAndTime; }
            set { dateAndTime = value; }
        }

        private float duration;

        public float Duration
        {
            get { return duration; }
            set { duration = value; }
        }
        private float topA;//	预热段上部炉温A

        public float TopA
        {
            get { return topA; }
            set { topA = value; }
        }
        private float topB;//	预热段上部炉温B

        public float TopB
        {
            get { return topB; }
            set { topB = value; }
        }
        private float topAvg;//	预热段上部炉温平均

        public float TopAvg
        {
            get { return topAvg; }
            set { topAvg = value; }
        }
        private float bottomA;//	预热段下部炉温A

        public float BottomA
        {
            get { return bottomA; }
            set { bottomA = value; }
        }
        private float bottomB;//	预热段下部炉温B        

        public float BottomB
        {
            get { return bottomB; }
            set { bottomB = value; }
        }
        private float bottomAvg;//	预热段下部炉温平均

        public float BottomAvg
        {
            get { return bottomAvg; }
            set { bottomAvg = value; }
        }
    }

    public class HRM_BaseInfo_RM
    {
        public string C_PIECENAME;//
        public string C_HISTORYKEYTM;//
        public string GT_HISTORYKEYTM;//
        public string C_SLABID;//
        public string C_COILID;//
        public string I_RSUCOURSE;//
        public string I_ESUCOURSE;//
        public string I_AWCCOURSE;//
        public string F_HSBSLABTHK;// --hsb 除磷箱
        public string F_HSBSLABWID;//
        public string F_HSBSLABLEN;//
        public string B_USEHSBHEADER1;//--hsb 除磷箱中1号喷头是否使用
        public string B_USEHSBHEADER2;//
        public string F_HSBTRANSFERSPD;//
        public string F_R1SLABTHK;// --板坯厚度
        public string F_R1SLABWID;//
        public string F_R1SLABLEN;//
        public string F_RMDELTHK;//  --DEL出口
        public string F_RMDELTHKCOLD;//
        public string F_RMDELWID;//
        public string F_RMDELWIDCOLD;// --出口宽度冷尺
        public string F_RDWWIDTARG;// --目标
        public string F_RMDELLEN;//
        public string F_RMDELLENCOLD;//
        public string F_RMDELTEMP;//
        public string F_TRANSFERBARRDTCALC;//--轧件出口温度计算值
        public string F_R1FINDELTHK;// --最终厚度
        public string F_R1FINDELWID;//
        public string I_RMTOTALPASS;//--初轧区总道次数
        public string I_R1TOTALPASS;//--只有1个初轧机，因此等于RM
        public string I_MAPINDEX;//
        public string I_DESCPTNNO;//
        public string B_CBUSEMODE;//--热卷箱使用模式
        public string B_PLATEROLLINGMODE;//--轧板模式
        public string F_TRANSFERBARWID;//
        public string I_FAMILY;// --钢种族
        public string F_FFAWCWIDTARG;// --前馈AWC宽度目标
        public string F_FFAWCINFLCOEF;//--前馈AWC可调系数
        public string F_E1NECKCPSGAPOPENVAL;//--立辊E1缩颈辊缝开度
        public string F_E1NECKCPSSTARTLEN;//--立辊E1缩颈起始位置
        public string F_E1NECKCPSLEN;//--立辊E1缩颈长度
        public string I_TPRFFAWCMODE;//--锥度
        public string B_ISFE1SOFTDMY;//
        public string F_FE1REDUCT;// --FE1精轧前立辊压下量
        public string F_FE1THRDSPD;//--穿带速度
        public string F_FE1RUNSPD;//--运行速度
        public string F_FE1FRC;//--摩擦系数
        public string F_FE1TORQ;//--转矩
        public string F_FE1PWR;//--功率
        public string F_FE1CURRRATIO;//--电流比率
        public string F_FE1GAP;//--辊缝
        public string F_FEF1TENS;//--张力
        public string F_FE1NECKCPSGAPOPENVAL;//--FE1精轧前立辊缩颈辊缝开度
        public string F_FE1NECKCPSLEN;//
        public string F_FE1NECKCPSSTARTLEN;//
        public string F_FE1NECKCPSWID;//---FE1精轧前立辊缩颈补偿宽度
        public string F_FE1LLTCDFRCDDEFORM;//--
        public string F_FE1LLTCDFRCDFTENS;//--
        public string F_FE1LLTCDTORQDDEFORM;//
        public string F_FE1LLTCDTORQDFTENS;//
        public string F_RMENTTBLSCANHEADTRIM;// --入口辊道扫描头部
        public string F_RMENTTBLSCANTAILTRIM;// --尾部
        public string F_RMDELTBLSCANHEADTRIM;// --出口
        public string F_RMDELTBLSCANTAILTRIM;//
        public string F_FMDELWID;//--精轧出口宽度
        public string F_FMDELTBLSCANHEADTRIM;//--精轧出口头
        public string F_FMDELTBLSCANTAILTRIM;//
    }

    public class HRM_BaseInfo_FR
    {
        public string CoilID;
        public string Mat_NO;
        
        public string EntryTime_FR1;
        public string ExitTime_FR1;

        public string EntryTime_FR2;
        public string ExitTime_FR2;

        public string EntryTime_FR3;
        public string ExitTime_FR3;

        public string EntryTime_FR4;
        public string ExitTime_FR4;

        public string EntryTime_FR5;
        public string ExitTime_FR5;

        public string EntryTime_FR6;
        public string ExitTime_FR6;

        public string EntryTime_FR7;
        public string ExitTime_FR7;

    }
        
    public class HRM_BaseInfo_CTC
    {//层冷基础数据        

        public string PIECENAME;
        public string HISTORYKEYTM;
        public string SLAB_ID;
        public string COIL_ID;
        public string COURSE;
        public string STATUS;
        public string AIR_COOL_TM;
        public string SPRAY_BOT_FBK;
        public string SPRAY_TOP_FBK;
        public string SPRAY_BOT_FFWD;
        public string SPRAY_TOP_FFWD;
        public string FDEL_THK_TARG;
        public string FDT_TEMP_TARG;
        public string FDEL_WIDTH_TARG;
        public string GRT_IDX;
        public string LEN_HEAD;
        public string LEN_TAIL;
        public string SPRAY_PAT;
        public string IS_STEP_COOL;
        public string TEMP_OFFSET_HEAD;
        public string TEMP_OFFSET_TAIL;
        public string CT_RANGE_SEL;
        public string CT_TEMP_PRED;
        public string MT_TEMP_PRED;
        public string CT_TEMP_TARG;
        public string MT_TEMP_TARG;
        public string ROT_SPRAY_STS_TOP;
        public string ROT_SPRAY_STS_BOT;
        public string ROT_SPRAY_STS_CROSS;
        public string DRY_HEAD_LEN;



    }

    public class HRM_BaseInfo_DC
    {// 卷取 基础数据
        public string SlabID;
        public string CoilID;

    }

    public class HisDB_CTC
    {
        public DateTime DateAndTime;
        public float Duration;
        public float CT;//
        public float CT_LENGTH;//
        public float CT_SPEED;//
        public float CT_TARGET;//
        public float FDT;//
        public float FET;//
        public float MT;//
        public float MT_LENGTH;//
        public float MT_SPEED;//
        public bool  CT_ENABLE;// Discrete
        public bool CT_ON;// Discrete
        public bool MT_ON;// Discrete

    }

    public class HRM_HisDB_DC
    {
        //LYQ2250.DC1--LYQ2250.DC3

        public DateTime DateAndTime;
        public float Duration;
        public float COIL_DIAMETER;//		Analog	LYQ2250.DC1.COIL_DIAMETER
        public float COIL_WIDTH;//		Analog	
        public float COILING_TENSION;//		Analog	
        public float LENGTH;//		Analog	
        public float LENGTH_B;//		Analog	
        public float LENGTH_C;//		Analog	
        public float MANDREL_SPEED;//		Analog	
        public float MANDREL_TORQUE;//		Analog	
        public float MD_SPEED_SET;//		Analog	
        public float MD_TENSION;//		Analog	
        public float MD_TENSION_SET;//		Analog	
        public float MD_TORQUE;//		Analog	
        public float PR_LEAD_RATIO;//		Analog	
        public float STRIP_SPEED;//		Analog	
        public float THICK_SET;//		Analog	
        public float WRAPER_ROLL1_SPEED;//		Analog	
        public float WRAPER_ROLL2_SPEED;//		Analog	
        public float WRAPER_ROLL3_SPEED;//		Analog	
        public float ON;//		Discrete	
        public float SELECTED;//		Discrete	
        public float UNLOAD;//		Discrete	
        public float DCZ;//	
    }

    //精轧机
    public class HisDB_FR
    {
        //LYQ2250.F1---LYQ2250.F7
        public DateTime DateAndTime;
        public float Duration;
        public float BendForce;//
        public float BendForceSet;//
        public float BS;//
        public float DraftSet;//
        public float DSGap;//
        public float FS;//
        public float GapSet;//
        public float LENGTH;//
        public float OilFilm;//
        public float OSGap;//
        public float RollForce;//
        public float RollForceSet;//
        public float ShiftBot;//
        public float ShiftSet;//
        public float ShiftTop;//
        public float Speed;//
        public float SpeedSet;//
        public float Tension;//
        public float TensionSet;//
        public float Torque;//
        public float WaterFlow;//
        public float WRWaterFlow;//
        public Boolean AGC_ON;//
        public Boolean ON;//
        public Boolean WaterOn;//

    }

    public class HisDB_FE
    {
        //LYQ2250.FE 
        public DateTime DateAndTime;
        public float Duration;
     
        public float	FET	;//		Analog
        public float	FET_LENGTH	;//		Analog
        public float	FSB_BW_Press	;//		Analog
        public float	FSB_FW_Press	;//		Analog
        public bool	    FET_ON	;//		Discrete
        public string	FMEZ_ID	;//		String
        public string	FSB_ID	;//		String

    }
    public class HisDB_FX
    {
        //LYQ2250.FX 
        public DateTime DateAndTime;
        public float Duration;

        public float CROWN100;//	Analog
        public float CROWN100_SET;//	Analog
        public float CROWN25;//	Analog
        public float CROWN25_SET;//	Analog
        public float CROWN40;//	Analog
        public float CROWN40_SET;//	Analog
        public float CROWN50;//	Analog
        public float CROWN50_SET;//	Analog
        public float FDT;//	Analog
        public float FDT_LENGTH;//	Analog
        public float FDT_ON;//	Analog
        public float FDT_TARGET;//	Analog
        public float FDT1;//	Analog
        public float FDT2;//	Analog
        public float FLATNESS;//	Analog
        public float FLT_LENGTH;//	Analog
        public float FLT_SPEED;//	Analog
        public float MFG_LENGTH;//	Analog
        public float MFG_SPEED;//	Analog
        public float THK_DEV;//	Analog
        public float THK_DS_DIFF;//	Analog
        public float THK_LENGTH;//	Analog
        public float THK_OS_DIFF;//	Analog
        public float THK_TARGET;//	Analog
        public float WEDGE;//	Analog
        public float WID_DEV_FLT;//	Analog
        public float WID_DEV_MFG;//	Analog
        public float WIDTH_FLT;//	Analog
        public float WIDTH_MFG;//	Analog
        public float WIDTH_WG;//	Analog
        public bool FDT_SEL;//	Discrete
        public bool FLT_ON;//	Discrete
        public bool MFG_ON;//	Discrete
        public bool THK_ON;//	Discrete
        public bool WID_FLT_SEL;//	Discrete
        public bool WID_MFG_SEL;//	Discrete
        public string FMZ_ID;//	String
        public string ROTZ_ID;//	String
    }

    public class HRM_HisDB_RM
    {
        //LYQ2250.R1
        public DateTime DateAndTime;
        public float BarLength;//	Analog
        public float BarThick;//	Analog
        public float BarWidth;//	Analog
        public float BendForce;//	Analog
        public float DSGap;//	Analog
        public float Length;//	Analog
        public float OSGap;//	Analog
        public float Pass;//	Analog
        public float RollForce;//	Analog
        public float SpeedBot;//	Analog
        public float SpeedTop;//	Analog
        public float TorqueBot;//	Analog
        public float TorqueTop;//	Analog
    }

    public class HisDB_R1
    {
        //LYQ2250.R1 
        public DateTime DateAndTime;
        public float Duration;

        public float BarLength;//	Analog
        public float BarThick;//	Analog
        public float BarWidth;//	Analog
        public float BendForce;//	Analog
        public float DLEN1;//	Analog
        public float DLEN2;//	Analog
        public float DLEN3;//	Analog
        public float DLEN4;//	Analog
        public float DLEN5;//	Analog
        public float DLEN6;//	Analog
        public float DLEN7;//	Analog
        public float DLEN8;//	Analog
        public float DLEN9;//	Analog
        public float DSGap;//	Analog
        public float DTHK1;//	Analog
        public float DTHK2;//	Analog
        public float DTHK3;//	Analog
        public float DTHK4;//	Analog
        public float DTHK5;//	Analog
        public float DTHK6;//	Analog
        public float DTHK7;//	Analog
        public float DTHK8;//	Analog
        public float DTHK9;//	Analog
        public float DWID1;//	Analog
        public float DWID2;//	Analog
        public float DWID3;//	Analog
        public float DWID4;//	Analog
        public float DWID5;//	Analog
        public float DWID6;//	Analog
        public float DWID7;//	Analog
        public float DWID8;//	Analog
        public float DWID9;//	Analog
        public float GapSet1;//	Analog
        public float GapSet2;//	Analog
        public float GapSet3;//	Analog
        public float GapSet4;//	Analog
        public float GapSet5;//	Analog
        public float GapSet6;//	Analog
        public float GapSet7;//	Analog
        public float GapSet8;//	Analog
        public float GapSet9;//	Analog
        public float Length;//	Analog
        public float OSGap;//	Analog
        public float Pass;//	Analog
        public float RollForce;//	Analog
        public float RollForceDiff;//	Analog
        public float RollForceSet1;//	Analog
        public float RollForceSet2;//	Analog
        public float RollForceSet3;//	Analog
        public float RollForceSet4;//	Analog
        public float RollForceSet5;//	Analog
        public float RollForceSet6;//	Analog
        public float RollForceSet7;//	Analog
        public float RollForceSet8;//	Analog
        public float RollForceSet9;//	Analog
        public float RunningSpeed1;//	Analog
        public float RunningSpeed2;//	Analog
        public float RunningSpeed3;//	Analog
        public float RunningSpeed4;//	Analog
        public float RunningSpeed5;//	Analog
        public float RunningSpeed6;//	Analog
        public float RunningSpeed7;//	Analog
        public float RunningSpeed8;//	Analog
        public float RunningSpeed9;//	Analog
        public float SpeedBot;//	Analog
        public float SpeedTop;//	Analog
        public float ThreadingSpeed1;//	Analog
        public float ThreadingSpeed2;//	Analog
        public float ThreadingSpeed3;//	Analog
        public float ThreadingSpeed4;//	Analog
        public float ThreadingSpeed5;//	Analog
        public float ThreadingSpeed6;//	Analog
        public float ThreadingSpeed7;//	Analog
        public float ThreadingSpeed8;//	Analog
        public float ThreadingSpeed9;//	Analog
        public float Torque1;//	Analog
        public float Torque2;//	Analog
        public float Torque3;//	Analog
        public float Torque4;//	Analog
        public float Torque5;//	Analog
        public float Torque6;//	Analog
        public float Torque7;//	Analog
        public float Torque8;//	Analog
        public float Torque9;//	Analog
        public float TorqueBot;//	Analog
        public float TorqueTop;//	Analog
        public float TotalPass;//	Analog
        public Boolean DESC1;//	Discrete
        public Boolean DESC2;//	Discrete
        public Boolean DESC3;//	Discrete
        public Boolean DESC4;//	Discrete
        public Boolean DESC5;//	Discrete
        public Boolean DESC6;//	Discrete
        public Boolean DESC7;//	Discrete
        public Boolean DESC8;//	Discrete
        public Boolean DESC9;//	Discrete
        public Boolean DUMMY1;//	Discrete
        public Boolean DUMMY2;//	Discrete
        public Boolean DUMMY3;//	Discrete
        public Boolean DUMMY4;//	Discrete
        public Boolean DUMMY5;//	Discrete
        public Boolean DUMMY6;//	Discrete
        public Boolean DUMMY7;//	Discrete
        public Boolean DUMMY8;//	Discrete
        public Boolean DUMMY9;//	Discrete
        public Boolean LAST_PASS;//	Discrete
        public Boolean ON;//	Discrete
        public string RMDZ;//	String
        public string RMZ;//	String

    }

    public class HisDB_RDT
    {
        //LYQ2250.RDT
        public DateTime DateAndTime;
        public float Duration;

        public float	LENGTH	;//	Analog
        public float	RDT1	;//	Analog
        public float	RDT2	;//	Analog
        public float	SELECT	;//	Analog
        public bool	ON	;//	Discrete

    }

    public class HisDB_RDW
    {
        //LYQ2250.RDW
        public DateTime DateAndTime;
        public float Duration;

        public float DEV_CENT;//	Analog
        public float DEV_HOT;//	Analog
        public float LENGTH;//	Analog
        public float WIDTH;//	Analog
        public float WIDTH_COLD;//	Analog
        public float WIDTH_HOT;//	Analog
        public bool  HEALTH;//	Discrete
        public bool ON;//	Discrete
        public bool VALID;//	Discrete

    }

    public class HisDB_UFC
    {
        //LYQ2250.UFC
        public DateTime DateAndTime;
        public float Duration;
        
        public float UFCTselect;//	Analog	超快冷出口温度
        public float Water_temperature;//	Analog	超快冷水温
        public float Hpress_All_Water_press_1;//	Analog	超快冷水压
        
        public float Hpress1_Dn_flux_act;//	Analog	超快冷1#下集管流量
        public float Hpress1_Up_flux_act;//	Analog	超快冷1#上集管流量
        public float Hpress2_Dn_flux_act;//	Analog	超快冷2#下集管流量
        public float Hpress2_Up_flux_act;//	Analog	超快冷2#上集管流量
        public float Hpress3_Dn_flux_act;//	Analog	超快冷3#下集管流量
        public float Hpress3_Up_flux_act;//	Analog	超快冷3#上集管流量
        public float Hpress4_Dn_flux_act;//	Analog	超快冷4#下集管流量
        public float Hpress4_Up_flux_act;//	Analog	超快冷4#上集管流量
        public float Hpress5_Dn_flux_act;//	Analog	超快冷5#下集管流量
        public float Hpress5_Up_flux_act;//	Analog	超快冷5#上集管流量

        public float Hpres6_Up_flux_act;//	Analog	超快冷6#上集管流量
        public float Hpress6_Dn_flux_act;//	Analog	超快冷6#下集管流量
        public float Hpres7_Up_flux_act;//	Analog	超快冷7#上集管流量
        public float Hpress7_Dn_flux_act;//	Analog	超快冷7#下集管流量
        public float Hpres8_Up_flux_act;//	Analog	超快冷8#上集管流量
        public float Hpres8_Dn_flux_act;//	Analog	超快冷8#下集管流量
        public float Hpres9_Dn_flux_act;//	Analog	超快冷9#下集管流量
        public float Hpres9_Up_flux_act;//	Analog	超快冷9#上集管流量
        public float Hpres10_Dn_flux_act;//	Analog	超快冷10#下集管流量
        public float Hpres10_Up_flux_act;//	Analog	超快冷10#上集管流量

        public float Hpres11_Dn_flux_act;//	Analog	超快冷11#下集管流量
        public float Hpres11_Up_flux_act;//	Analog	超快冷11#上集管流量
        public float Hpres12_Dn_flux_act;//	Analog	超快冷12#下集管流量
        public float Hpres12_Up_flux_act;//	Analog	超快冷12#上集管流量
        public float Hpres13_Dn_flux_act;//	Analog	超快冷13#下集管流量
        public float Hpres13_Up_flux_act;//	Analog	超快冷13#上集管流量
        public float Hpres14_Dn_flux_act;//	Analog	超快冷14#下集管流量
        public float Hpres14_Up_flux_act;//	Analog	超快冷14#上集管流量
        public float Hpres15_Dn_flux_act;//	Analog	超快冷15#下集管流量
        public float Hpres15_Up_flux_act;//	Analog	超快冷15#上集管流量

        public float Hpres16_Dn_flux_act;//	Analog	超快冷16#下集管流量
        public float Hpres16_Up_flux_act;//	Analog	超快冷16#上集管流量
        public float Hpres17_Dn_flux_act;//	Analog	超快冷17#下集管流量
        public float Hpres17_Up_flux_act;//	Analog	超快冷17#上集管流量
        public float Hpres18_Dn_flux_act;//	Analog	超快冷18#下集管流量
        public float Hpres18_Up_flux_act;//	Analog	超快冷18#上集管流量
        public float Hpres19_Dn_flux_act;//	Analog	超快冷19#下集管流量
        public float Hpres19_Up_flux_act;//	Analog	超快冷19#上集管流量
        public float Hpres20_Dn_flux_act;//	Analog	超快冷20#下集管流量
        public float Hpres20_Up_flux_act;//	Analog	超快冷20#上集管流量
    }

    public class HisDB_BANK
    {
        //LYQ2250.BANK1---LYQ2250.BANK9
        public DateTime DateAndTime;
        public float Duration;

        public bool TOP1;//	Discrete
        public bool TOP2;//	Discrete
        public bool TOP3;//	Discrete
        public bool TOP4;//	Discrete
        public bool TOP5;//	Discrete
        public bool TOP6;//	Discrete

        public bool BOT1;//	Discrete
        public bool BOT2;//	Discrete
        public bool BOT3;//	Discrete
        public bool BOT4;//	Discrete
        public bool BOT5;//	Discrete
        public bool BOT6;//	Discrete        

    }
}


