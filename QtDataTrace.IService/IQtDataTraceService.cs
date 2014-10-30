using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using QtDataTrace.Interfaces;

namespace QtDataTrace.IService
{
    public interface IQtDataTraceService
    {
        IList<MaterialInfo> GetMaterialList(QueryArgs arg);
        IList<MaterialInfo> GetMaterialPedigree(string matId);

        DataSet GetQtData(QueryArgs arg, QtDataTableConfig table);

        Guid NewDataTrace(string processNo, IList<string> iDList, IList<QtDataProcessConfig> processes, bool back, string username);

        Tuple<int, DataTable> TryGetTraceData(string username, Guid id);

        bool Stop(string username, Guid id);

    }
}
