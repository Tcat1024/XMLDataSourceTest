using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace QtDataTrace.Interfaces
{
    [System.SerializableAttribute]
    public class HotCoilInfo
    {
        private string coilId;

        [DisplayName("钢卷号")]
        public string CoilId
        {
            get { return coilId; }
            set { coilId = value; }
        }
        private string slabId;

        [DisplayName("板坯号")]
        public string SlabId
        {
            get { return slabId; }
            set { slabId = value; }
        }
        private string hsm_No;

        [DisplayName("产线")]
        public string HSM_No
        {
            get { return hsm_No; }
            set { hsm_No = value; }
        }
        private DateTime rolledTime;

        [DisplayName("轧制时间")]
        public DateTime RolledTime
        {
            get { return rolledTime; }
            set { rolledTime = value; }
        }
        private string steelGrade;

        [DisplayName("钢种")]
        public string SteelGrade
        {
            get { return steelGrade; }
            set { steelGrade = value; }
        }
        private double thickness;

        [DisplayName("厚度")]
        public double Thickness
        {
            get { return thickness; }
            set { thickness = value; }
        }

        private double thkMean;

        [DisplayName("厚度平均值")]
        public double ThkMean
        {
            get { return thkMean; }
            set { thkMean = value; }
        }
        private double thkMax;

        [DisplayName("厚度最大值")]
        public double ThkMax
        {
            get { return thkMax; }
            set { thkMax = value; }
        }
        private double thkMin;

        [DisplayName("厚度最小值")]
        public double ThkMin
        {
            get { return thkMin; }
            set { thkMin = value; }
        }
        private double thkPcntOn;

        [DisplayName("厚度命中率")]
        public double ThkPcntOn
        {
            get { return thkPcntOn; }
            set { thkPcntOn = value; }
        }
        private double thkPcntOver;

        [DisplayName("厚度超上限率")]
        public double ThkPcntOver
        {
            get { return thkPcntOver; }
            set { thkPcntOver = value; }
        }
        private Double thkPcntUnder;

        [DisplayName("厚度超下限率")]
        public Double ThkPcntUnder
        {
            get { return thkPcntUnder; }
            set { thkPcntUnder = value; }
        }
        private double thkStd;

        [DisplayName("厚度标准差")]
        public double ThkStd
        {
            get { return thkStd; }
            set { thkStd = value; }
        }

        private double widMean;

        [DisplayName("宽度平均值")]
        public double WidMean
        {
            get { return widMean; }
            set { widMean = value; }
        }
        private double widMax;

        [DisplayName("宽度最大值")]
        public double WidMax
        {
            get { return widMax; }
            set { widMax = value; }
        }
        private double widMin;

        [DisplayName("宽度最小值")]
        public double WidMin
        {
            get { return widMin; }
            set { widMin = value; }
        }
        private double widPcntOn;

        [DisplayName("宽度命中率")]
        public double WidPcntOn
        {
            get { return widPcntOn; }
            set { widPcntOn = value; }
        }
        private double widPcntOver;

        [DisplayName("宽度超上限率")]
        public double WidPcntOver
        {
            get { return widPcntOver; }
            set { widPcntOver = value; }
        }
        private Double widPcntUnder;

        [DisplayName("宽度超下限率")]
        public Double WidPcntUnder
        {
            get { return widPcntUnder; }
            set { widPcntUnder = value; }
        }

        private double fdtMean;

        [DisplayName("FDT平均值")]
        public double FDTMean
        {
            get { return fdtMean; }
            set { fdtMean = value; }
        }
        private double fdtMax;

        [DisplayName("FDT最大值")]
        public double FDTMax
        {
            get { return fdtMax; }
            set { fdtMax = value; }
        }
        private double fdtMin;

        [DisplayName("FDT最小值")]
        public double FDTMin
        {
            get { return fdtMin; }
            set { fdtMin = value; }
        }
        private double fdtPcntOn;

        [DisplayName("FDT命中率")]
        public double FDTPcntOn
        {
            get { return fdtPcntOn; }
            set { fdtPcntOn = value; }
        }
        private double fdtPcntOver;

        [DisplayName("FDT超上限率")]
        public double FDTPcntOver
        {
            get { return fdtPcntOver; }
            set { fdtPcntOver = value; }
        }
        private Double fdtPcntUnder;

        [DisplayName("FDT超下限率")]
        public Double FDTPcntUnder
        {
            get { return fdtPcntUnder; }
            set { fdtPcntUnder = value; }
        }

        private double ctMean;

        [DisplayName("CT平均值")]
        public double CTMean
        {
            get { return ctMean; }
            set { ctMean = value; }
        }
        private double ctMax;

        [DisplayName("CT最大值")]
        public double CTMax
        {
            get { return ctMax; }
            set { ctMax = value; }
        }
        private double ctMin;

        [DisplayName("CT最小值")]
        public double CTMin
        {
            get { return ctMin; }
            set { ctMin = value; }
        }
        private double ctPcntOn;

        [DisplayName("CT命中率")]
        public double CTPcntOn
        {
            get { return ctPcntOn; }
            set { ctPcntOn = value; }
        }
        private double ctPcntOver;

        [DisplayName("CT超上限率")]
        public double CTPcntOver
        {
            get { return ctPcntOver; }
            set { ctPcntOver = value; }
        }
        private Double ctPcntUnder;

        [DisplayName("CT超下限率")]
        public Double CTPcntUnder
        {
            get { return ctPcntUnder; }
            set { ctPcntUnder = value; }
        }

        private double profMean;

        [DisplayName("板型平均值")]
        public double ProfMean
        {
            get { return profMean; }
            set { profMean = value; }
        }
        private double profMax;

        [DisplayName("板型最大值")]
        public double ProfMax
        {
            get { return profMax; }
            set { profMax = value; }
        }
        private double profMin;

        [DisplayName("板型最小值")]
        public double ProfMin
        {
            get { return profMin; }
            set { profMin = value; }
        }
        private double profPcntOn;

        [DisplayName("板型命中率")]
        public double ProfPcntOn
        {
            get { return profPcntOn; }
            set { profPcntOn = value; }
        }
        private double profPcntOver;

        [DisplayName("板型超上限率")]
        public double ProfPcntOver
        {
            get { return profPcntOver; }
            set { profPcntOver = value; }
        }
        private Double profPcntUnder;

        [DisplayName("板型超下限率")]
        public Double ProfPcntUnder
        {
            get { return profPcntUnder; }
            set { profPcntUnder = value; }
        }

        private double wdgMean;

        [DisplayName("契形平均值")]
        public double WdgMean
        {
            get { return wdgMean; }
            set { wdgMean = value; }
        }
        private double wdgMax;

        [DisplayName("契形最大值")]
        public double WdgMax
        {
            get { return wdgMax; }
            set { wdgMax = value; }
        }
        private double wdgMin;

        [DisplayName("契形最小值")]
        public double WdgMin
        {
            get { return wdgMin; }
            set { wdgMin = value; }
        }
        private double wdgPcntOn;

        [DisplayName("契形命中率")]
        public double WdgPcntOn
        {
            get { return wdgPcntOn; }
            set { wdgPcntOn = value; }
        }
        private double wdgPcntOver;

        [DisplayName("契形超上限率")]
        public double WdgPcntOver
        {
            get { return wdgPcntOver; }
            set { wdgPcntOver = value; }
        }
        private Double wdgPcntUnder;

        [DisplayName("契形超下限率")]
        public Double WdgPcntUnder
        {
            get { return wdgPcntUnder; }
            set { wdgPcntUnder = value; }
        }

        private double width;

        [DisplayName("宽度")]
        public double Width
        {
            get { return width; }
            set { width = value; }
        }
        private double length;

        [DisplayName("长度")]
        public double Length
        {
            get { return length; }
            set { length = value; }
        }
        private double weight;

        [DisplayName("重量")]
        public double Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        private double fdtTrg;

        [DisplayName("FDT目标")]
        public double FdtTrg
        {
            get { return fdtTrg; }
            set { fdtTrg = value; }
        }
        private double fdtPos;

        [DisplayName("FDT位置")]
        public double FdtPos
        {
            get { return fdtPos; }
            set { fdtPos = value; }
        }
        private double fdtNeg;

        [DisplayName("FDT下工差")]
        public double FdtNeg
        {
            get { return fdtNeg; }
            set { fdtNeg = value; }
        }

        private double ctTrg;

        [DisplayName("CT目标值")]
        public double CtTrg
        {
            get { return ctTrg; }
            set { ctTrg = value; }
        }
        private double ctPos;

        [DisplayName("CT位置")]
        public double CtPos
        {
            get { return ctPos; }
            set { ctPos = value; }
        }
        private double ctNeg;

        [DisplayName("CT工差")]
        public double CtNeg
        {
            get { return ctNeg; }
            set { ctNeg = value; }
        }

        private double flatMean;

        public double FlatMean
        {
            get { return flatMean; }
            set { flatMean = value; }
        }
        private double flatMax;

        public double FlatMax
        {
            get { return flatMax; }
            set { flatMax = value; }
        }
        private double flatMin;

        public double FlatMin
        {
            get { return flatMin; }
            set { flatMin = value; }
        }
        private double flatPcntOn;

        public double FlatPcntOn
        {
            get { return flatPcntOn; }
            set { flatPcntOn = value; }
        }
        private double flatPcntOver;

        public double FlatPcntOver
        {
            get { return flatPcntOver; }
            set { flatPcntOver = value; }
        }
        private double flatPcntUnder;

        public double FlatPcntUnder
        {
            get { return flatPcntUnder; }
            set { flatPcntUnder = value; }
        }
    }
}
