using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QtDataTrace.Interfaces
{

    public class MIHeatInfo
    {
        public string HeatID {get; set;}
        public string IronID { get; set; }
        public string ShiftID { get; set; }
        public string CrewID { get; set; }
        public string Operator { get; set; }
        public string IronLaddleID { get; set; }
        public string WeightTime { get; set; }
        public string HM_Weight { get; set; }
        public string HM_Tempture { get; set; }
        public string ChargeTime { get; set; }

        public string HM_SendPlace { get; set; }
        public string HM_C { get; set; }
        public string HM_Si { get; set; }
        public string HM_Mn { get; set; }
        public string HM_S { get; set; }
        public string HM_P { get; set; }
        public string HM_Ti { get; set; }
    }
     

    [System.SerializableAttribute]
    public class MIKeyEvents
    {
        public string DateAndTime { get; set; }
        public string STOP_TIME { get; set; }
        public float Duration { get; set; }
        public string IN_OUT { get; set; }
        public string HEAT_ID { get; set; }
        public string IRON_ID { get; set; }
        public string IRON_LADLE_ID { get; set; }
        public string BF_ID { get; set; }
        public string BF_TAP_ID { get; set; }
        public string IRON_WEIGHT { get; set; }
        public string MIXER_WEIGHT { get; set; }
        public string BOF_ID { get; set; }
        public string SEND_PLACE { get; set; }
        public string TEMPTURE { get; set; }
        public string Shift_ID { get; set; }
        public string Crew_ID { get; set; }
        public string Operator { get; set; }
    }
}
