using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QtDataTrace.Interfaces
{
    [System.SerializableAttribute]
    public class LFHeatInfo
    {
        public string heat_id;
        public string treatpos;
        public string strttime;
        public string endtime;
        public string station;
        public string route;

        public string strtgrade;
        public string endgrade;
        public string strtsteeltemp;
        public string endsteeltemp;
        public string strtsteelwei;
        public string endsteelwei;
        public string endslagwei;
        public string ladleno;
        public string ladlestatus;
        public string ladlewei;

        public string pon;

        public string slidgatelife;
        public string slidgatebrname;
        public string porozlife;
        public string porozbrname;
        public string emptydur;
        public string eletrdholdtm;
        public string totprieng;
        public string totseceng;
        public string gastype;
        public string totgas;
        public string shiftnr;
        public string shiftteam;
        public string monitor;
        public string mainoptr1;
        public string mainoptr2;
    }

    [System.SerializableAttribute]
    public class LF_HisDB
    {
        public DateTime datetime;
        public float Duration;

        public Int32 SetTapNo; 
        public Int32 BrkeStatus;

        public Int32 PriSideVoltageA;
        public Int32 PriSideVoltageB;
        public Int32 PriSideVoltageC;

        public Int32 PriSideCurrentA;
        public Int32 PriSideCurrentB;
        public Int32 PriSideCurrentC;

        public Int32 SecSideVlotageA;
        public Int32 SecSideVlotageB;
        public Int32 SecSideVlotageC;

        public Int32 SecSideCurrentA;
        public Int32 SecSideCurrentB;
        public Int32 SecSideCurrentC;
        
           public float BotGasFlux;
           public float BotGasPrss;
           public float BotGasType;

           public float TopGasType;
           public float TopGasFlux;
           public float TopGasPrss;
       
            
    }

    [System.SerializableAttribute]
    public class LFKeyEvents
    {
        public string datetime;
        public float Duration;
        public string Decription;
        public string MatCode;
        public string MatName;
        public string Weight;
        public string Temp;
        public string O2ppm;
        public string Ele_C;
        public string Ele_Si;
        public string Ele_Mn;
        public string Ele_S;
        public string Ele_P;
        public string Ele_Cu;
        public string Ele_As;
        public string Ele_Sn;
        public string Ele_Cr;
        public string Ele_Ni;
        public string Ele_Mo;
        public string Ele_Ti;
        public string Ele_Nb;
        public string Ele_Pb;
    }
}
