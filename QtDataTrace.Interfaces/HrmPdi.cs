using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace QtDataTrace.Interfaces
{
    [Serializable]
    public class HrmPdi
    {
        string coilId;

        [Persistent("C_COILID"), DisplayName("钢卷号")]
        public string CoilId
        {
            get { return coilId; }
            set { coilId = value; }
        }
        string slabId;

        [Persistent("C_SLABID"), DisplayName("板坯号")]
        public string SlabId
        {
            get { return slabId; }
            set { slabId = value; }
        }
        string steelGrade;

        [Persistent("C_GRDNAME"), DisplayName("钢种")]
        public string SteelGrade
        {
            get { return steelGrade; }
            set { steelGrade = value; }
        }
        double slabThkCold;

        [Persistent("F_SLABTHKCOLD"), DisplayName("板坯厚度(冷)")]
        public double SlabThkCold
        {
            get { return slabThkCold; }
            set { slabThkCold = value; }
        }

        double slabWidthCold;

        [Persistent("F_SLABWIDCOLD"), DisplayName("板坯宽度(冷)")]
        public double SlabWidthCold
        {
            get { return slabWidthCold; }
            set { slabWidthCold = value; }
        }

        double slabLengthCold;

        [Persistent("F_SLABLENCOLD"), DisplayName("板坯长度(冷)")]
        public double SlabLengthCold
        {
            get { return slabLengthCold; }
            set { slabLengthCold = value; }
        }


        double rm_entTempTarg;

        [Persistent("F_RMENTTEMPTARG"), DisplayName("粗轧入口温度目标")]
        public double Rm_entTempTarg
        {
            get { return rm_entTempTarg; }
            set { rm_entTempTarg = value; }
        }

        double rm_entTempTolNeg;

        [Persistent("F_RMENTTEMPTARGTOLNEG"), DisplayName("粗轧入口温度下工差")]
        public double Rm_entTempTolNeg
        {
            get { return rm_entTempTolNeg; }
            set { rm_entTempTolNeg = value; }
        }

        double rm_entTempTolPos;

        [Persistent("F_RMENTTEMPTARGTOLPOS"), DisplayName("粗轧入口温度下工差")]
        public double Rm_entTempTolPos
        {
            get { return rm_entTempTolPos; }
            set { rm_entTempTolPos = value; }
        }

        double rm_delTempTarg;

        [Persistent("F_RMDELTEMPTARG"), DisplayName("粗轧出口温度目标")]
        public double Rm_delTempTarg
        {
            get { return rm_delTempTarg; }
            set { rm_delTempTarg = value; }
        }
        
    }
}
