using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QtDataTrace.Access
{
    public class LF_ElecPower
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
        

    }

    public class LF_BlowGas
    {
        public DateTime datetime;
        public float Duration;

       public float BotGasFlux;
       public float BotGasPrss;
       public float BotGasType;

       public float TopGasType;
       public float TopGasFlux;
       public float TopGasPrss;
       
            
    }

    public class LF_Elem_AddMat_Temp
    {
        public string datetime;
        public float Duration;
        public string Decription;
        public string MatCode;
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
