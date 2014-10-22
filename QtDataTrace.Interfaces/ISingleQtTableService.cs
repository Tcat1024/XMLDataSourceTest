using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace QtDataTrace.Interfaces
{
    public interface ISingleQtTableService
    {
        List<BOFHeatInfo> GetBOFHeatInfo(string HeatID);
        List<BOFKeyEvents> GetBOFKeyEvents(string HeatID);


        List<HrmPdi> GetHrmPdi(string matId);
        List<HrmCoilSetup> GetHrmCoilSetup(string matId);
        List<HrmCTCSetup> GetHrmCTCSetup(string matId);
        List<HisDB_HF> GetHisDB_HF(string CoilID);
        List<CoilSurfaceDefect> GetCoilSurfaceDefect(string matId);
        List<CoilDefect> GetCoilDefects(string matId);
        List<CoilDefectClass> GetCoilDefectClasses();
        HotCoilInfo GetHotCoilInfo(string matId);

        List<String> GetSteelGradeList();

        List<string> GetSlabIDListFromHeatID(string HeatID);

        List<CCHeatInfo> GetCCHeatInfo(string HeatID);
        
        List<CC_SlabInfo> GetCC_SlabInfo(string SlabNo);

        List<CC_HisData0> GetCC_Curve(string CCM, string StrandNo, string StartDateTime, string EndDateTime);

        List<CastInfo> GetCC_CastInfo(DateTime startTime, DateTime stopTime);

        List<EquipmentAreaInfo> GetDeviceAreaInfo(string device);
    }
}
