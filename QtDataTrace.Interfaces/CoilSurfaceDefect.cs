using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QtDataTrace.Interfaces
{
    [Serializable]
    public class CoilSurfaceDefect
    {
        [Persistent("COIL_ID")]
        public string CoilId{get;set;} 
	    [Persistent("START_TIME")]
        public DateTime StartTime{get; set;}
	    [Persistent("STOP_TIME")]
        public DateTime StopTime{get;set;}
	    [Persistent("PARAMSET")]
        public int ParamSet{get;set;}
	    [Persistent("GRADE")]
        public int Grade{get;set;}
	    [Persistent("LENGTH")]
        public double Length{get;set;}
	    [Persistent("WIDTH")]
        public double Width{get;set;} 
	    [Persistent("THICKNESS")]
        public double Thickness{get;set; }
	    [Persistent("WEIGHT")]
        public double Weight{get;set;} 
	    [Persistent("CHARGE")]
        public string Charge{get;set;}
	    [Persistent("MATERIALID")]
        public int MaterialId{get;set;}
	    [Persistent("STATUS")]
        public string Status{get;set;}
	    [Persistent("DESCRIPTION")]
        public string Description{get;set;}
	    [Persistent("LAST_DEFECT_ID")]
        public int LastDefectId{get;set;}
	    [Persistent("TARGET_QUALITY")]
        public int TargetQuality{get;set;}
	    [Persistent("PDI_RECV_TIME")]
        public DateTime PDIReceiveTime{get;set;}
	    [Persistent("SLENGTH")]
        public double SlabLength{get;set;}
	    [Persistent("DEFECT_COUNT")]
        public int DefectCount{get;set;}
	    [Persistent("SURFACE_CODE")]
        public string SurfaceCode{get;set;}
	    [Persistent("SCARFING")]
        public string Scarfing{get;set;}
    }
}
