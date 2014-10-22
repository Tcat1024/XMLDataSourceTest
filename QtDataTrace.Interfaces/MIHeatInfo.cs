using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QtDataTrace.Interfaces
{

    public class MIHeatInfo
    {
        public string HeatID;
        public string IronID;
        public string ShiftID;
        public string CrewID;
        public string Operator;
        public string IronLaddleID;
        public string WeightTime;
        public string HM_Weight;
        public string HM_Tempture;
        public string ChargeTime;

        public string HM_SendPlace;
        public string HM_C;
        public string HM_Si;
        public string HM_Mn;
        public string HM_S;
        public string HM_P;
        public string HM_Ti;
    }
     

    [System.SerializableAttribute]
    public class MIKeyEvents
    {
        public string DateAndTime;
        public string STOP_TIME;
        public float Duration;
        public string  IN_OUT;
        public string  HEAT_ID;
        public string IRON_ID;
        public string IRON_LADLE_ID;
        public string BF_ID;
        public string BF_TAP_ID;          
        public string IRON_WEIGHT;
        public string MIXER_WEIGHT;         
        public string BOF_ID;
        public string SEND_PLACE;
        public string TEMPTURE;       
        public string Shift_ID;
        public string Crew_ID;
        public string Operator;   
    }
}
