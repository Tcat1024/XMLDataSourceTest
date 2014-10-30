using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace QtDataTrace.Interfaces
{
    [Serializable]
    public class CoilDefect
    {
        [Persistent("COIL_ID")]
        public string CoilId { get; set; }
	    [Persistent("DEFECT_ID")]
        public string DefectId { get; set; }
	    [Persistent("CLASS")]
        public int Class { get; set; }
	    [Persistent("GRADE")]
        public int Grade { get; set; }
	    [Persistent("PERIODID")]
        public int PeriodId { get; set; }
	    [Persistent("PERIOD_LENGTH")]
        public double PeriodLength { get; set; }
	    [Persistent("POSITION_CD")]
        public double PositionCD { get; set; }
	    [Persistent("POSITION_RCD")]
        public double PositionRCD { get; set; }
	    [Persistent("POSITION_MD")]
        public double PositionMD { get; set; }
	    [Persistent("SIDE")]
        public int Side { get; set; }
	    [Persistent("SIZE_CD")]
        public double SizeCD { get; set; }
	    [Persistent("SIZE_MD")]
        public double SizeMD { get; set; }
	    [Persistent("CAMERA_NO")]
        public int CameraNo { get; set; }
	    [Persistent("DEFECT_NO")]
        public int DefectNo { get; set; }
	    [Persistent("MERGEDTO")]
        public int MergedTo { get; set; }
	    [Persistent("CONFIDENCE")]
        public int Confidence { get; set; }
	    [Persistent("ROIX0")]
        public int RoiX0 { get; set; }
	    [Persistent("ROIX1")]
        public int RoiX1 { get; set; }
	    [Persistent("ROIY0")]
        public int RoiY0 { get; set; }
	    [Persistent("ROIY1")]
        public int RoiY1 { get; set; }
	    [Persistent("ORIGINAL_CLASS")]
        public int OriginalClass { get; set; }
	    [Persistent("PP_ID")]
        public int PPId { get; set; }
	    [Persistent("POST_CL")]
        public int PostCL { get; set; }
	    [Persistent("MERGERPP")]
        public int MergerPP { get; set; }
	    [Persistent("ONLINE_CPP")]
        public int OlineCPP { get; set; }
	    [Persistent("OFFLINE_CPP")]
        public int OfflineCPP { get; set; }
	    [Persistent("CL_PROD_CLASS")]
        public int CLProdClass { get; set; }
	    [Persistent("CL_TEST_CLASS")]
        public int CLTestClass { get; set; }
	    [Persistent("ABS_POS_CD")]
        public double AbsPosCD { get; set; }
    }

    [Serializable]
    public class CoilDefectClass
    {
        [Persistent("CLASSID")]
        public int ClassId { get; set; }

        [Persistent("NAME")]
        public string Name { get; set; }

    }
}
