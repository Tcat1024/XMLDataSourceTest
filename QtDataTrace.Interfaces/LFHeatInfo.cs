using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QtDataTrace.Interfaces
{
    [System.SerializableAttribute]
    public class LFHeatInfo
    {
        public string heat_id { get; set;}
        public string treatpos { get; set; }
        public string strttime { get; set;}
        public string endtime { get; set;}
        public string station { get; set;}
        public string route { get; set;}

        public string strtgrade { get; set;}
        public string endgrade { get; set;}
        public string strtsteeltemp { get; set;}
        public string endsteeltemp { get; set;}
        public string strtsteelwei { get; set; }
        public string endsteelwei { get; set; }
        public string endslagwei { get; set; }
        public string ladleno { get; set; }
        public string ladlestatus { get; set; }
        public string ladlewei { get; set; }

        public string pon { get; set; }

        public string slidgatelife { get; set; }
        public string slidgatebrname { get; set; }
        public string porozlife { get; set; }
        public string porozbrname { get; set; }
        public string emptydur { get; set; }
        public string eletrdholdtm { get; set; }
        public string totprieng { get; set; }
        public string totseceng { get; set; }
        public string gastype { get; set; }
        public string totgas { get; set; }
        public string shiftnr { get; set; }
        public string shiftteam { get; set; }
        public string monitor { get; set; }
        public string mainoptr1 { get; set; }
        public string mainoptr2 { get; set; }
    }

    [System.SerializableAttribute]
    public class LF_HisDB
    {
        public DateTime datetime { get; set; }
        public float Duration { get; set; }

        public Int32 SetTapNo { get; set; }
        public Int32 BrkeStatus { get; set; }

        public Int32 PriSideVoltageA { get; set; }
        public Int32 PriSideVoltageB { get; set; }
        public Int32 PriSideVoltageC { get; set; }

        public Int32 PriSideCurrentA { get; set; }
        public Int32 PriSideCurrentB { get; set; }
        public Int32 PriSideCurrentC { get; set; }

        public Int32 SecSideVlotageA { get; set; }
        public Int32 SecSideVlotageB { get; set; }
        public Int32 SecSideVlotageC { get; set; }

        public Int32 SecSideCurrentA { get; set; }
        public Int32 SecSideCurrentB { get; set; }
        public Int32 SecSideCurrentC { get; set; }

        public float BotGasFlux { get; set; }
        public float BotGasPrss { get; set; }
        public float BotGasType { get; set; }

        public float TopGasType { get; set; }
        public float TopGasFlux { get; set; }
        public float TopGasPrss { get; set; }
    }

    [System.SerializableAttribute]
    public class LFKeyEvents
    {
        public string datetime { get; set; }
        public float Duration { get; set; }
        public string Decription { get; set; }
        public string MatCode { get; set; }
        public string MatName { get; set; }
        public string Weight { get; set; }
        public string Temp { get; set; }
        public string O2ppm { get; set; }
        public string Ele_C { get; set; }
        public string Ele_Si { get; set; }
        public string Ele_Mn { get; set; }
        public string Ele_S { get; set; }
        public string Ele_P { get; set; }
        public string Ele_Cu { get; set; }
        public string Ele_As { get; set; }
        public string Ele_Sn { get; set; }
        public string Ele_Cr { get; set; }
        public string Ele_Ni { get; set; }
        public string Ele_Mo { get; set; }
        public string Ele_Ti { get; set; }
        public string Ele_Nb { get; set; }
        public string Ele_Pb { get; set; }
    }
}
