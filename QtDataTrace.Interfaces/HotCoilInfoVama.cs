using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace QtDataTrace.Interfaces
{
    [System.SerializableAttribute]

    public class HotCoilInfoVama
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

        private double thk;

        [DisplayName("厚度")]
        public double Thk
        {
            get { return thk; }
            set { thk = value; }
        }

        private double width;

        [DisplayName("宽度")]
        public double Width
        {
            get { return width; }
            set { width = value; }
        }

        private double len;

        [DisplayName("长度")]
        public double Len
        {
            get { return len; }
            set { len = value; }
        }

        private string thkH;

        [DisplayName("厚度判定头")]
        public string ThkH
        {
            get { return thkH; }
            set { thkH = value; }
        }

        private string thkB;

        [DisplayName("厚度判定中")]
        public string ThkB
        {
            get { return thkB; }
            set { thkB = value; }
        }

        private string thkT;

        [DisplayName("厚度判定尾")]
        public string ThkT
        {
            get { return thkT; }
            set { thkT = value; }
        }

        private string widH;

        [DisplayName("宽度判定头")]
        public string WidH
        {
            get { return widH; }
            set { widH = value; }
        }

        private string widB;

        [DisplayName("宽度判定中")]
        public string WidB
        {
            get { return widB; }
            set { widB = value; }
        }

        private string widT;

        [DisplayName("宽度判定尾")]
        public string WidT
        {
            get { return widT; }
            set { widT = value; }
        }

        private string thkOver;

        [DisplayName("厚度差")]
        public string ThkOver
        {
            get { return thkOver; }
            set { thkOver = value; }
        }

        private string dot;

        [DisplayName("dot判定")]
        public string Dot
        {
            get { return dot; }
            set { dot = value; }
        }

        private string fdtH;

        [DisplayName("FDT判定头")]
        public string FdtH
        {
            get { return fdtH; }
            set { fdtH = value; }
        }

        private string fdtB;

        [DisplayName("FDT判定中")]
        public string FdtB
        {
            get { return fdtB; }
            set { fdtB = value; }
        }

        private string fdtT;

        [DisplayName("FDT判定尾")]
        public string FdtT
        {
            get { return fdtT; }
            set { fdtT = value; }
        }

        private string ctH;

        [DisplayName("CT判定头")]
        public string CtH
        {
            get { return ctH; }
            set { ctH = value; }
        }

        private string ctB;

        [DisplayName("CT判定中")]
        public string CtB
        {
            get { return ctB; }
            set { ctB = value; }
        }

        private string ctT;

        [DisplayName("CT判定尾")]
        public string CtT
        {
            get { return ctT; }
            set { ctT = value; }
        }

        private string conv;

        [DisplayName("凸度判定")]
        public string Conv
        {
            get { return conv; }
            set { conv = value; }
        }

        private string wdg;

        [DisplayName("楔形判定")]
        public string Wdg
        {
            get { return wdg; }
            set { wdg = value; }
        }

        private string flat;

        [DisplayName("平直度判定")]
        public string Flat
        {
            get { return flat; }
            set { flat = value; }
        }


        private DateTime rTime;

        [DisplayName("判定时间")]
        public DateTime RTime
        {
            get { return rTime; }
            set { rTime = value; }
        }
       
       
    }
}
