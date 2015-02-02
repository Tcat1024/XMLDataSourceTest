using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace QtDataTrace.AnalyzeIService
{
    public interface ILocalizationDataTraceService
    {
        DataSet GetProcessQtTableConfigFile();
        List<DataSet> GetProcessQtTableConfig();

        void SaveProcessQtTableConfig(DataSet data);
        Tuple<Guid, string> NewDataTrace(string processNo, IList<string> iDList, IList<QtDataTrace.Interfaces.QtDataProcessConfig> processes, bool back, string username);
        DataTable GetData(string username, Guid id);
        int GetProcess(string username, Guid id);
        string GetErrorMessage(string username, Guid id);
        string CommitData(string username, Guid id, DataTable data);
        bool Stop(string username, Guid id);
    }
}
