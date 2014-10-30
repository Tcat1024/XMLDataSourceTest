using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QtDataTrace.Interfaces
{
    class LY2250_RM
    {
    }
    public class RM_BaseInfo
    {
        public string C_PIECENAME;//
        public string C_HISTORYKEYTM;//
        public string GT_HISTORYKEYTM;//
        public string C_SLABID;//
        public string C_COILID;//
        public string I_RSUCOURSE;//
        public string I_ESUCOURSE;//
        public string I_AWCCOURSE;//
        public string F_HSBSLABTHK;// --hsb 除磷箱
        public string F_HSBSLABWID;//
        public string F_HSBSLABLEN;//
        public string B_USEHSBHEADER1;//--hsb 除磷箱中1号喷头是否使用
        public string B_USEHSBHEADER2;//
        public string F_HSBTRANSFERSPD;//
        public string F_R1SLABTHK;// --板坯厚度
        public string F_R1SLABWID;//
        public string F_R1SLABLEN;//
        public string F_RMDELTHK;//  --DEL出口
        public string F_RMDELTHKCOLD;//
        public string F_RMDELWID;//
        public string F_RMDELWIDCOLD;// --出口宽度冷尺
        public string F_RDWWIDTARG;// --目标
        public string F_RMDELLEN;//
        public string F_RMDELLENCOLD;//
        public string F_RMDELTEMP;//
        public string F_TRANSFERBARRDTCALC;//--轧件出口温度计算值
        public string F_R1FINDELTHK;// --最终厚度
        public string F_R1FINDELWID;//
        public string I_RMTOTALPASS;//--初轧区总道次数
        public string I_R1TOTALPASS;//--只有1个初轧机，因此等于RM
        public string I_MAPINDEX;//
        public string I_DESCPTNNO;//
        public string B_CBUSEMODE;//--热卷箱使用模式
        public string B_PLATEROLLINGMODE;//--轧板模式
        public string F_TRANSFERBARWID;//
        public string I_FAMILY;// --钢种族
        public string F_FFAWCWIDTARG;// --前馈AWC宽度目标
        public string F_FFAWCINFLCOEF;//--前馈AWC可调系数
        public string F_E1NECKCPSGAPOPENVAL;//--立辊E1缩颈辊缝开度
        public string F_E1NECKCPSSTARTLEN;//--立辊E1缩颈起始位置
        public string F_E1NECKCPSLEN;//--立辊E1缩颈长度
        public string I_TPRFFAWCMODE;//--锥度
        public string B_ISFE1SOFTDMY;//
        public string F_FE1REDUCT;// --FE1精轧前立辊压下量
        public string F_FE1THRDSPD;//--穿带速度
        public string F_FE1RUNSPD;//--运行速度
        public string F_FE1FRC;//--摩擦系数
        public string F_FE1TORQ;//--转矩
        public string F_FE1PWR;//--功率
        public string F_FE1CURRRATIO;//--电流比率
        public string F_FE1GAP;//--辊缝
        public string F_FEF1TENS;//--张力
        public string F_FE1NECKCPSGAPOPENVAL;//--FE1精轧前立辊缩颈辊缝开度
        public string F_FE1NECKCPSLEN;//
        public string F_FE1NECKCPSSTARTLEN;//
        public string F_FE1NECKCPSWID;//---FE1精轧前立辊缩颈补偿宽度
        public string F_FE1LLTCDFRCDDEFORM;//--
        public string F_FE1LLTCDFRCDFTENS;//--
        public string F_FE1LLTCDTORQDDEFORM;//
        public string F_FE1LLTCDTORQDFTENS;//
        public string F_RMENTTBLSCANHEADTRIM;// --入口辊道扫描头部
        public string F_RMENTTBLSCANTAILTRIM;// --尾部
        public string F_RMDELTBLSCANHEADTRIM;// --出口
        public string F_RMDELTBLSCANTAILTRIM;//
        public string F_FMDELWID;//--精轧出口宽度
        public string F_FMDELTBLSCANHEADTRIM;//--精轧出口头
        public string F_FMDELTBLSCANTAILTRIM;//
    }

    public class RM_HisDb
    {
        DateTime DateTime;
    }
}
