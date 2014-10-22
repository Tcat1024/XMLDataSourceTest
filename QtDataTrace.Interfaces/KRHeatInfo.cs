using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QtDataTrace.Interfaces
{
    [System.SerializableAttribute]
    public class KRHeatInfo
    {
        public string HEAT_ID;
        public string IRON_ID;
        public string DES_STATION_NO;
        public string PLAN_NO;
        public string PONO;
        public string STEEL_GRADE;
        public string AIM_S;
        public string DES_STEP_NUM;
        public string IRON_LADLE_ID;
        public string INI_TEMP;
        public string INI_WGT;
        public string INI_C;
        public string INI_SI;
        public string INI_MN;
        public string INI_P;
        public string INI_S;
        public string INI_TI;
        public string FIN_TEMP;
        public string FIN_WGT;
        public string MATERIALID_ACT;
        public string ADDWGT_ACT;
        public string STIRRER_DURATION;
        public string STIRRER_SPEED_MAX;
        public string STIRRER_SPEED_MIN;
        public string STIRRER_SPEED_AVG;
        public string STIRRER_HEIGHT_MAX;
        public string STIRRER_HEIGHT_MIN;
        public string STIRRER_HEIGHT_AVG;
        public string STIRRER_ID;
        public string STIRRER_TIMES;
        public string LADLE_ARRIVE;
        public string LADLE_LEAVE;
        public string DES_START;
        public string DES_END;
        public string RESIDUE_FIRST_S;
        public string RESIDUE_FIRST_E;
        public string RESIDUE_FIRST_DURATION;
        public string RESIDUE_FIRST_SLAG_WGT;
        public string RESIDUE_LAST_S;
        public string RESIDUE_LAST_E;
        public string RESIDUE_LAST_DURATION;
        public string RESIDUE_LAST_SLAG_WGT;
        public string CALEFACIENT_USED;
        public string DES_DURATION;
        public string PRODUCE_DATE;
        public string CREW_ID;
        public string SHIFT_ID;
        public string VALID_FLAG;
        public string PERIOD_ID;
        public string LADLE_WEIGHT;
        public string TEMP_TIME_F;
        public string TEMP_TIME_E;

    }

    [System.SerializableAttribute]
    public class KRKeyEvents
    {
       public string DateAndTime;
       public float Duration;
       public string Descripion;
       public string Tempture;
       public string Weight;
       public string Ele_C;
       public string Ele_Si;
       public string Ele_Mn;
       public string Ele_S;
       public string Ele_P;
       public string Ele_Cu;
       public string Ele_As;
       public string Ele_Sn;
       public string Ele_Cu5As8Sn;
       public string Ele_Cr;
       public string Ele_Ni;
       public string Ele_Mo;
       public string Ele_Ti;
       public string Ele_Nb;
       public string Ele_Pb;
   }

    [System.SerializableAttribute]
   public class KRMixerHeightSpeed
   {
       public DateTime DateAndTime;
       public float sngDuration;
       public string DateTimeDuration;
       public float Height;
       public float Speed;
   }
}
