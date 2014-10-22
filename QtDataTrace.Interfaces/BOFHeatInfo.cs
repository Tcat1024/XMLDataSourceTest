using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace QtDataTrace.Interfaces
{
    [System.SerializableAttribute]
    public class BOFHeatInfo
    {
        private string heat_id;

        [DisplayName("炉次号")]
        public string Heat_id
        {
            get { return heat_id; }
            set { heat_id = value; }
        }
        private string treatpos;

        [DisplayName("炉座号")]
        public string Treatpos
        {
            get { return treatpos; }
            set { treatpos = value; }
        }
        public  string plan_no;
        public  string pono;
        public  string steel_grade;

        [DisplayName("钢种")]
        public string Steel_grade
        {
            get { return steel_grade; }
            set { steel_grade = value; }
        }
        public string promodecode;
        public string bof_campaign;
        public string bof_life;
        public string tappinghole;
        public string tap_hole_campaign;
        public string tap_hole_life;
        public string mainlance_id;
        public string mainlance_life;
        public string sublance_id;
        public string sublance_life;
        public string bath_level;
        public string steelladleid;
        public string slag_cal_weight;
        public string slag_net_weight;
        public string weight_cal;
        public string weight_act;
        public string weighting_time;
        public string tem_act;
        public string tem_time;
        public string bofc_act;
        public string o2ppm_act;
        public string ladleid;
        public string hmw_act;
        public string hm_tem;
        public string hm_c;
        public string hm_si;
        public string hm_mn;
        public string hm_p;
        public string hm_s;
        public string bucketid;
        public string scrw_act;
        public string pi_act;
        public string return_act;
        public string metal_act;
        public string cao_weight;
        public string dolo_weight;
        public string rdolo_weight;
        public string mgo_weight;
        public string caf2_weight;
        public string iron_weight;
        public string o2_act;
        public string ar_act;
        public string n2_act;
        public string tsc_tem;
        public string tsc_c;
        public string tsc_duration;
        public string tso_tem;
        public string tso_c;
        public string tso_o2ppm;
        public string tso_duration;
        public string o2_duration;
        public string ar_duration;
        public string n2_duration;
        public string after_stiring_duration;
        public string reblow_num;
        public string reblow1_tem;
        public string reblow2_tem;
        public string deslag_num;
        public string slag_splash_n2;
        private string ready_time;

        [DisplayName("入炉时间")]
        public string Ready_time
        {
            get { return ready_time; }
            set { ready_time = value; }
        }
        public string charging_starttime;
        public string hm_time;
        public string scrap_time;
        public string charging_endtime;
        public string blow_starttime;
        public string blow_endtime;
        public string reblow1_starttime;
        public string reblow1_endtime;
        public string reblow1_duration;
        public string reblow2_starttime;
        public string reblow2_endtime;
        public string reblow2_duration;
        public string slag_nr;
        public string tapping_starttime;
        public string tapping_endtime;
        public string tapping_duration;
        public string slag_starttime;
        public string slag_endtime;
        public string slag_duration;
        private string product_day;

        public string Product_day
        {
            get { return product_day; }
            set { product_day = value; }
        }
        public string shift_id;
        public string crew_id;
        public string operator_c;
        public string checkdate;
        public string checkoperator;
        public string checkflag;
        public string ge_no;
        public string tsc_starttime;
        public string tsc_endtime;
        public string tso_starttime;
        public string tso_endtime;
        public string splash_starttime;
        public string splash_endtime;
        public string splash_duration;
        public string o2_press;
        public string o2_flux;
        public string n2_press;
        public string n2_flux;
        public string sheetiron_wgt;
        public string restrin_wgt;
        public string alloycao_wgt;
        public string cadd_wgt;
        public string fesi_wgt;
        public string al_wgt;
        public string mnsi_wgt;
        public string femn_wgt;
        public string fenb_wgt;
        public string hscrw_wgt;
        public string lscrw_wgt;
        public string sscrw_wgt;
        public string mfemn_wgt;
        public string lfemn_wgt;
        public string duststeam_vol;
        public string dustwater_vol;
        public string recyclesteam_vol;
        public string outsteam_vol;
        public string mainlance_id1;
        public string mainlance_life1;
        public string ladlear_act;
        public string ironid;
        public string rdolo_wgt;
        public string change_wgt;
        public string burnslag_wgt;
        public string lfslag_wgt;
        public string sicabei_wgt;
        public string sialfe_wgt;
        public string mn_wgt;

        public string IRON_LADLE_ID;
        public string IRON_ID;
        public string HM_WEIGHT;
        public string HM_TRPMTURE;
        public string HM_TIME;
        public string HM_SOUREC;
        public string SCRAP_BUCKET_ID; 
        public string SCRAP_ID;
        public string SCRAP_WEIGHT;
        public string HSCRW_WEIGHT;
        public string LSCRW_WEIGHT;
        public string SSCRW_WEIGHT;

    }
     
   [System.SerializableAttribute]
    public class BOF_HisDB
    {
        public DateTime datetime;
        public float Duration;
        public float O2;
        public float CO;
        public float CO2;

        public float ACT_INCLINE_ANGLE;
        public float ACT_LANCE_HEIGHT;
        public float ACT_O2_FLUX;

        public float ACT_N2_FLUX;
        public float ACT_AR_FLUX;
        public float ACT_BATH_LEVEL;
    }

    [System.SerializableAttribute]
    public class BOFKeyEvents
    {
        private string datetime;

        [DisplayName("时间")]
        public string Datetime
        {
            get { return datetime; }
            set { datetime = value; }
        }
        private float duration;

        [DisplayName("时长")]
        public float Duration
        {
            get { return duration; }
            set { duration = value; }
        }
        private string decription;

        [DisplayName("事项")]
        public string Decription
        {
            get { return decription; }
            set { decription = value; }
        }
        private string mat_Name;

        [DisplayName("料名")]
        public string Mat_Name
        {
            get { return mat_Name; }
            set { mat_Name = value; }
        }
        private string weight;

        [DisplayName("重量")]
        public string Weight
        {
            get { return weight; }
            set { weight = value; }
        }
        private string temp;

        [DisplayName("温度")]
        public string Temp
        {
            get { return temp; }
            set { temp = value; }
        }
        private string o2ppm;

        public string O2ppm
        {
            get { return o2ppm; }
            set { o2ppm = value; }
        }
        private string ele_C;

        [DisplayName("C")]
        public string Ele_C
        {
            get { return ele_C; }
            set { ele_C = value; }
        }
        private string ele_Si;

        [DisplayName("Si")]
        public string Ele_Si
        {
            get { return ele_Si; }
            set { ele_Si = value; }
        }
        private string ele_Mn;

        [DisplayName("Mn")]
        public string Ele_Mn
        {
            get { return ele_Mn; }
            set { ele_Mn = value; }
        }
        private string ele_S;

        [DisplayName("S")]
        public string Ele_S
        {
            get { return ele_S; }
            set { ele_S = value; }
        }
        private string ele_P;

        [DisplayName("P")]
        public string Ele_P
        {
            get { return ele_P; }
            set { ele_P = value; }
        }
        private string ele_Cu;

        [DisplayName("Cu")]
        public string Ele_Cu
        {
            get { return ele_Cu; }
            set { ele_Cu = value; }
        }
        private string ele_As;

        [DisplayName("As")]
        public string Ele_As
        {
            get { return ele_As; }
            set { ele_As = value; }
        }
        private string ele_Sn;

        [DisplayName("Sn")]
        public string Ele_Sn
        {
            get { return ele_Sn; }
            set { ele_Sn = value; }
        }
        private string ele_Cr;

        [DisplayName("Cr")]
        public string Ele_Cr
        {
            get { return ele_Cr; }
            set { ele_Cr = value; }
        }
        private string ele_Ni;

        [DisplayName("Ni")]
        public string Ele_Ni
        {
            get { return ele_Ni; }
            set { ele_Ni = value; }
        }
        private string ele_Mo;

        [DisplayName("Mo")]
        public string Ele_Mo
        {
            get { return ele_Mo; }
            set { ele_Mo = value; }
        }
        private string ele_Ti;

        [DisplayName("Ti")]
        public string Ele_Ti
        {
            get { return ele_Ti; }
            set { ele_Ti = value; }
        }
        private string ele_Nb;

        [DisplayName("Nb")]
        public string Ele_Nb
        {
            get { return ele_Nb; }
            set { ele_Nb = value; }
        }
        private string ele_Pb;

        [DisplayName("Pb")]
        public string Ele_Pb
        {
            get { return ele_Pb; }
            set { ele_Pb = value; }
        }
    }

    [System.SerializableAttribute]
    public class BOF_HM 
    {
        public string HEATID;
        public string LADLEID;
        public string IRONID;
        public string MOLTENIRON_WEIGHT;
        public string MOLTENIRON_TEMPERATURE;
        public string MOLTENIRON_TEMPERATURE_TIME;
        public string C;
        public string SI;
        public string MN;
        public string P;
        public string S;
        public string MOLTENIRON_SOURCE;
        public string MOLTENSTEEL_WEIGHT;
        public string REMARK;
    }

    public class HEAT_TRACK
    {
        public string HeatID;        
        public string SteelGrade;
        public string MI_Station;
        public string KR_Station;
        public string BOF_Station;
        public string LF_Station;
        public string RH_Station;
        public string CC_Station;

        public string MI_Arrive_Time;
        public string KR_Arrive_Time;
        public string BOF_Arrive_Time;
        public string LF_Arrive_Time;
        public string RH_Arrive_Time;
        public string CC_Arrive_Time;

        public string MI_Leave_Time;
        public string KR_Leave_Time;
        public string BOF_Leave_Time;
        public string LF_Leave_Time;
        public string RH_Leave_Time;
        public string CC_Leave_Time;

        public string MI_Duration;
        public string KR_Duration;
        public string BOF_Duration;
        public string LF_Duration;
        public string RH_Duration;
        public string CC_Duration;
    }

    public class Addition
    {
        public string DEVICE_NO;       
        public string STATION;
        public string HEAT_ID;
        public string ADD_TIME;
        public string ADD_BATCH;
        public string MAT_ID;  
        public string MAT_NAME;
        public string WEIGHT;
        public string HOPPER_ID;
        public string PLACE;

    }

    public class TEMPTURE 
    {
        public string DEVICE_NO;
        public string STATION;
        public string HEAT_ID;
        public string MEASURE_TYPE;
        public string MEASURE_TIME;
        public string MEASURE_NUM;
        public string TRMPTURE_VALUE;
    }
    public class ELEM_ANA
    {
        public string DEVICE_NO; 
        public string STATION;     
        public string HEAT_ID;      
        public string IRON_ID;     
        public string SAMPLETIME;  
        public string SAMPLE_NUMBER;
        public string SAMPLE_ID ;       
        public string ELE_ALS;
        public string ELE_ALT;
        public string ELE_AS;
        public string ELE_B;
        public string ELE_BI;
        public string ELE_C;
        public string ELE_CA;
        public string ELE_CE;
        public string ELE_CEQ;
        public string ELE_CO;
        public string ELE_CR;
        public string ELE_CU;
        public string ELE_H;
        public string ELE_MG;
        public string ELE_MN;
        public string ELE_MO;
        public string ELE_N;
        public string ELE_NB;
        public string ELE_NI;
        public string ELE_O;
        public string ELE_P;
        public string ELE_PB;
        public string ELE_RE;
        public string ELE_S;
        public string ELE_SB;
        public string ELE_SE;
        public string ELE_SI;
        public string ELE_SN;
        public string ELE_TA;
        public string ELE_TE;
        public string ELE_TI;
        public string ELE_V;
        public string ELE_W;
        public string ELE_ZN;
        public string ELE_ZR ;
    }

}
