using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace QtDataTrace.Interfaces
{
    public interface IQtDataTraceService
    {
        IList<MaterialInfo> GetMaterialList(QueryArgs arg);
        IList<MaterialInfo> GetMaterialPedigree(string matId);

        DataSet GetQtData(QueryArgs arg, QtDataTableConfig table);
    }
}
