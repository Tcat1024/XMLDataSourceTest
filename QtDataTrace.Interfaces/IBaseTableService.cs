using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace QtDataTrace.Interfaces
{
    public interface IBaseTableService
    {

        DataSet GetProcessCode();


        List<DataSet> GetProcessQtTableConfig();
        void SaveProcessQtTableConfig(DataSet data);


        QtDataSourceConfig GetQtDataSourceConfig(string process);


        DataSet GetProcessQtTableConfigFile();
    }
}
