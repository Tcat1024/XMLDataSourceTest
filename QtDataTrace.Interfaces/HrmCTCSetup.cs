using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace QtDataTrace.Interfaces
{
    [Serializable]
    public class HrmCTCSetup
    {
        private DateTime productTime;

        [PersistentAttribute("HISTORYKEYTM"), DisplayName("生产时间")]
        public DateTime ProductTime
        {
            get { return productTime; }
            set { productTime = value; }
        }

        string slabId;

        [PersistentAttribute("SLAB_ID"), DisplayName("板坯号")]
        public string SlabId
        {
            get { return slabId; }
            set { slabId = value; }
        }

        string coilId;
        [PersistentAttribute("COIL_ID"), DisplayName("钢卷号")]
        public string CoilId
        {
            get { return coilId; }
            set { coilId = value; }
        }
        int course;

        [DisplayName("设定时序")]
        public int Course
        {
            get { return course; }
            set { course = value; }
        }
        int status;

        [DisplayName("状态")]
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        double airCoolTime;


        [PersistentAttribute("AIR_COOL_TM"), DisplayName("空冷时间")]
        public double AirCoolTime
        {
            get { return airCoolTime; }
            set { airCoolTime = value; }
        }
        double sprayBotFbk;

        [PersistentAttribute("SPRAY_BOT_FBK"), DisplayName("上部反馈控制")]
        public double SprayBotFbk
        {
            get { return sprayBotFbk; }
            set { sprayBotFbk = value; }
        }
        double sprayTopFbk;

        [PersistentAttribute("SPRAY_TOP_FBK"), DisplayName("上部反馈控制")]
        public double SprayTopFbk
        {
            get { return sprayTopFbk; }
            set { sprayTopFbk = value; }
        }
        int sprayBotFfwd;

        [PersistentAttribute("SPRAY_BOT_FFWD"), DisplayName("下部前馈控制")]
        public int SprayBotFfwd
        {
            get { return sprayBotFfwd; }
            set { sprayBotFfwd = value; }
        }
        int sprayTopFfwd;

        [PersistentAttribute("SPRAY_TOP_FFWD"), DisplayName("上部前馈控制")]
        public int SprayTopFfwd
        {
            get { return sprayTopFfwd; }
            set { sprayTopFfwd = value; }
        }
        double fdelThkTarg;

        [PersistentAttribute("FDEL_THK_TARG"), DisplayName("精轧出口厚度目标")]
        public double FdelThkTarg
        {
            get { return fdelThkTarg; }
            set { fdelThkTarg = value; }
        }
        double fdtTempTarg;

        [PersistentAttribute("FDT_TEMP_TARG"), DisplayName("精轧出口温度目标值")]
        public double FdtTempTarg
        {
            get { return fdtTempTarg; }
            set { fdtTempTarg = value; }
        }
        double fdelWidthTarg;

        [PersistentAttribute("FDEL_WIDTH_TARG"), DisplayName("精轧出口宽度目标值")]
        public double FdelWidthTarg
        {
            get { return fdelWidthTarg; }
            set { fdelWidthTarg = value; }
        }
        int grtIdx;

        [PersistentAttribute("GRT_IDX"), DisplayName("钢种索引")]
        public int GrtIdx
        {
            get { return grtIdx; }
            set { grtIdx = value; }
        }
        double lengthHead;

        [PersistentAttribute("LEN_HEAD"), DisplayName("头部长度")]
        public double LengthHead
        {
            get { return lengthHead; }
            set { lengthHead = value; }
        }
        double lengthTail;

        [PersistentAttribute("LEN_TAIL"), DisplayName("尾部长度")]
        public double LengthTail
        {
            get { return lengthTail; }
            set { lengthTail = value; }
        }
        int sprayPattern;

        [PersistentAttribute("SPRAY_PAT"), DisplayName("喷水模式")]
        public int SprayPattern
        {
            get { return sprayPattern; }
            set { sprayPattern = value; }
        }
        int isSteepCool;

        [PersistentAttribute("IS_STEP_COOL"), DisplayName("")]
        public int IsSteepCool
        {
            get { return isSteepCool; }
            set { isSteepCool = value; }
        }
        double tempOffsetHead;

        [PersistentAttribute("TEMP_OFFSET_HEAD"), DisplayName("头部温度偏差")]
        public double TempOffsetHead
        {
            get { return tempOffsetHead; }
            set { tempOffsetHead = value; }
        }
        double tempOffsetTail;

        [PersistentAttribute("TEMP_OFFSET_TAIL"), DisplayName("尾部温度偏差")]
        public double TempOffsetTail
        {
            get { return tempOffsetTail; }
            set { tempOffsetTail = value; }
        }
        int ctRangeSelect;

        [PersistentAttribute("CT_RANGE_SEL"), DisplayName("卷曲温度范围选择")]
        public int CtRangeSelect
        {
            get { return ctRangeSelect; }
            set { ctRangeSelect = value; }
        }
        double ctTempPred;

        [PersistentAttribute("CT_TEMP_PRED"), DisplayName("预测卷曲温度")]
        public double CtTempPred
        {
            get { return ctTempPred; }
            set { ctTempPred = value; }
        }
        double mtTempPred;

        [PersistentAttribute("MT_TEMP_PRED"), DisplayName("预测中间温度")]
        public double MtTempPred
        {
            get { return mtTempPred; }
            set { mtTempPred = value; }
        }
        double ctTempTarg;

        [PersistentAttribute("CT_TEMP_TARG"), DisplayName("卷曲温度目标值")]
        public double CtTempTarg
        {
            get { return ctTempTarg; }
            set { ctTempTarg = value; }
        }
        double mtTempTarg;

        [PersistentAttribute("MT_TEMP_TARG"), DisplayName("中间温度目标值")]
        public double MtTempTarg
        {
            get { return mtTempTarg; }
            set { mtTempTarg = value; }
        }
        string rotSpraySetupTop;

        [PersistentAttribute("ROT_SPRAY_STS_TOP"), DisplayName("上喷嘴开启设定")]
        public string RotSpraySetupTop
        {
            get { return rotSpraySetupTop; }
            set { rotSpraySetupTop = value; }
        }
        string rotSpraySetupBot;

        [PersistentAttribute("ROT_SPRAY_STS_BOT"), DisplayName("下喷嘴开启设定")]
        public string RotSpraySetupBot
        {
            get { return rotSpraySetupBot; }
            set { rotSpraySetupBot = value; }
        }
        string rotSpraySetupCross;

        [PersistentAttribute("ROT_SPRAY_STS_CROSS"), DisplayName("测喷开启设定")]
        public string RotSpraySetupCross
        {
            get { return rotSpraySetupCross; }
            set { rotSpraySetupCross = value; }
        }
        double dryHeadLength;

        [PersistentAttribute("DRY_HEAD_LEN"), DisplayName("头部不冷长度")]
        public double DryHeadLength
        {
            get { return dryHeadLength; }
            set { dryHeadLength = value; }
        }
    }
}
