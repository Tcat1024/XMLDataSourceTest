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
        public string promodecode {get; set;}
        public string bof_campaign { get; set; }
        public string bof_life { get; set; }
        public string tappinghole { get; set; }
        public string tap_hole_campaign { get; set; }
        public string tap_hole_life { get; set; }
        public string mainlance_id { get; set; }
        public string mainlance_life { get; set; }
        public string sublance_id { get; set; }
        public string sublance_life { get; set; }
        public string bath_level { get; set; }
        public string steelladleid { get; set; }
        public string slag_cal_weight { get; set; }
        public string slag_net_weight { get; set; }
        public string weight_cal { get; set; }
        public string weight_act { get; set; }
        public string weighting_time { get; set; }
        public string tem_act { get; set; }
        public string tem_time { get; set; }
        public string bofc_act { get; set; }
        public string o2ppm_act { get; set; }
        public string ladleid { get; set; }
        public string hmw_act { get; set; }
        public string hm_tem { get; set; }
        public string hm_c { get; set; }
        public string hm_si { get; set; }
        public string hm_mn { get; set; }
        public string hm_p { get; set; }
        public string hm_s { get; set; }
        public string bucketid { get; set; }
        public string scrw_act { get; set; }
        public string pi_act { get; set; }
        public string return_act { get; set; }
        public string metal_act { get; set; }
        public string cao_weight { get; set; }
        public string dolo_weight { get; set; }
        public string rdolo_weight { get; set; }
        public string mgo_weight { get; set; }
        public string caf2_weight { get; set; }
        public string iron_weight { get; set; }
        public string o2_act { get; set; }
        public string ar_act { get; set; }
        public string n2_act { get; set; }
        public string tsc_tem { get; set; }
        public string tsc_c { get; set; }
        public string tsc_duration { get; set; }
        public string tso_tem { get; set; }
        public string tso_c { get; set; }
        public string tso_o2ppm { get; set; }
        public string tso_duration { get; set; }
        public string o2_duration { get; set; }
        public string ar_duration { get; set; }
        public string n2_duration { get; set; }
        public string after_stiring_duration { get; set; }
        public string reblow_num { get; set; }
        public string reblow1_tem { get; set; }
        public string reblow2_tem { get; set; }
        public string deslag_num { get; set; }
        public string slag_splash_n2 { get; set; }
        private string ready_time { get; set; }

        [DisplayName("入炉时间")]
        public string Ready_time
        {
            get { return ready_time; }
            set { ready_time = value; }
        }
        public string charging_starttime { get; set; }
        public string hm_time { get; set; }
        public string scrap_time { get; set; }
        public string charging_endtime { get; set; }
        public string blow_starttime { get; set; }
        public string blow_endtime { get; set; }
        public string reblow1_starttime { get; set; }
        public string reblow1_endtime { get; set; }
        public string reblow1_duration { get; set; }
        public string reblow2_starttime { get; set; }
        public string reblow2_endtime { get; set; }
        public string reblow2_duration { get; set; }
        public string slag_nr { get; set; }
        public string tapping_starttime { get; set; }
        public string tapping_endtime { get; set; }
        public string tapping_duration { get; set; }
        public string slag_starttime { get; set; }
        public string slag_endtime { get; set; }
        public string slag_duration { get; set; }
        private string product_day { get; set; }

        public string Product_day
        {
            get { return product_day; }
            set { product_day = value; }
        }
        public string shift_id { get; set; }
        public string crew_id { get; set; }
        public string operator_c { get; set; }
        public string checkdate { get; set; }
        public string checkoperator { get; set; }
        public string checkflag { get; set; }
        public string ge_no { get; set; }
        public string tsc_starttime { get; set; }
        public string tsc_endtime { get; set; }
        public string tso_starttime { get; set; }
        public string tso_endtime { get; set; }
        public string splash_starttime { get; set; }
        public string splash_endtime { get; set; }
        public string splash_duration { get; set; }
        public string o2_press { get; set; }
        public string o2_flux { get; set; }
        public string n2_press { get; set; }
        public string n2_flux { get; set; }
        public string sheetiron_wgt { get; set; }
        public string restrin_wgt { get; set; }
        public string alloycao_wgt { get; set; }
        public string cadd_wgt { get; set; }
        public string fesi_wgt { get; set; }
        public string al_wgt { get; set; }
        public string mnsi_wgt { get; set; }
        public string femn_wgt { get; set; }
        public string fenb_wgt { get; set; }
        public string hscrw_wgt { get; set; }
        public string lscrw_wgt { get; set; }
        public string sscrw_wgt { get; set; }
        public string mfemn_wgt { get; set; }
        public string lfemn_wgt { get; set; }
        public string duststeam_vol { get; set; }
        public string dustwater_vol { get; set; }
        public string recyclesteam_vol { get; set; }
        public string outsteam_vol { get; set; }
        public string mainlance_id1 { get; set; }
        public string mainlance_life1 { get; set; }
        public string ladlear_act { get; set; }
        public string ironid { get; set; }
        public string rdolo_wgt { get; set; }
        public string change_wgt { get; set; }
        public string burnslag_wgt { get; set; }
        public string lfslag_wgt { get; set; }
        public string sicabei_wgt { get; set; }
        public string sialfe_wgt { get; set; }
        public string mn_wgt { get; set; }

        public string IRON_LADLE_ID { get; set; }
        public string IRON_ID { get; set; }
        public string HM_WEIGHT { get; set; }
        public string HM_TRPMTURE { get; set; }
        public string HM_TIME { get; set; }
        public string HM_SOUREC { get; set; }
        public string SCRAP_BUCKET_ID { get; set; }
        public string SCRAP_ID { get; set; }
        public string SCRAP_WEIGHT { get; set; }
        public string HSCRW_WEIGHT { get; set; }
        public string LSCRW_WEIGHT { get; set; }
        public string SSCRW_WEIGHT { get; set; }

    }
     
   [System.SerializableAttribute]
    public class BOF_HisDB
    {
       public DateTime datetime { get; set; }
       public float Duration { get; set; }
       public float O2 { get; set; }
       public float CO { get; set; }
       public float CO2 { get; set; }

       public float ACT_INCLINE_ANGLE { get; set; }
       public float ACT_LANCE_HEIGHT { get; set; }
       public float ACT_O2_FLUX { get; set; }

       public float ACT_N2_FLUX { get; set; }
       public float ACT_AR_FLUX { get; set; }
       public float ACT_BATH_LEVEL { get; set; }
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
        public string HEATID { get; set; }
        public string LADLEID { get; set; }
        public string IRONID { get; set; }
        public string MOLTENIRON_WEIGHT { get; set; }
        public string MOLTENIRON_TEMPERATURE { get; set; }
        public string MOLTENIRON_TEMPERATURE_TIME { get; set; }
        public string C { get; set; }
        public string SI { get; set; }
        public string MN { get; set; }
        public string P { get; set; }
        public string S { get; set; }
        public string MOLTENIRON_SOURCE { get; set; }
        public string MOLTENSTEEL_WEIGHT { get; set; }
        public string REMARK { get; set; }
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
