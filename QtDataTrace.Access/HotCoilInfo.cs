using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QtDataTrace.Access
{
    [System.SerializableAttribute]
    public class HotCoilInfo
    {
        public string CoilId;
        public string SlabId;
        public string HSM_No;
        public DateTime RolledTime;
        public string SteelGrade;
        public double Thickness;

        public double ThkMean;
        public double ThkMax;
        public double ThkMin;
        public double ThkPcntOn;
        public double ThkPcntOver;
        public Double ThkPcntUnder;
        public double ThkStd;

        public double WidMean;
        public double WidMax;
        public double WidMin;
        public double WidPcntOn;
        public double WidPcntOver;
        public Double WidPcntUnder;

        public double FDTMean;
        public double FDTMax;
        public double FDTMin;
        public double FDTPcntOn;
        public double FDTPcntOver;
        public Double FDTPcntUnder;

        public double CTMean;
        public double CTMax;
        public double CTMin;
        public double CTPcntOn;
        public double CTPcntOver;
        public Double CTPcntUnder;

        public double ProfMean;
        public double ProfMax;
        public double ProfMin;
        public double ProfPcntOn;
        public double ProfPcntOver;
        public Double ProfPcntUnder;

        public double WdgMean;
        public double WdgMax;
        public double WdgMin;
        public double WdgPcntOn;
        public double WdgPcntOver;
        public Double WdgPcntUnder;

        public double Width;
        public double Length;
        public double Weight;

    }
}
