using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QtDataTrace.Access
{
     [System.Serializable]
    public class SingleRowDB
    {
        public string ColName;
        public string ColValue;
    }
     [System.Serializable]
    public class BOF_WasteGas
    {
        public DateTime datetime;
        public float Duration;
        public float O2;
        public float CO;
        public float CO2;
    }
    [System.Serializable]
    public class BOF_EleAna
    {
        public string datetime;
        public float Duration;
        public string Decription;
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
