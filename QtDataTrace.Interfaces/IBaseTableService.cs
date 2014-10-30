using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace QtDataTrace.Interfaces
{
    public interface IBaseTableService
    {
        DataSet GetWorkshopInfo();
        void    SaveWorkshopInfo(DataSet data);

        DataSet GetProcessCode();
        void    SaveProcessCode(DataSet data);

        DataSet GetEquipmentInfo();
        void    SaveEquipmentInfo(DataSet data);

        DataSet GetProcessTimeGetFunction();
        void    SaveProcessTimeGetFunction(DataSet data);

        DataSet GetDeviceRealItemConfig();
        void    SaveDeviceRealItemConfig(DataSet data);

        List<DataSet> GetProcessQtTableConfig();
        void SaveProcessQtTableConfig(DataSet data);

        DataSet GetQtTraceDataSourceConfig();
        void SaveQtTraceDataSourceConfig(DataSet data);

        DataSet GetPointConfig();
        void SavePointConfig(DataSet data);

        QtDataSourceConfig GetQtDataSourceConfig(string process);

        DataSet GetTagConfig();
        DataSet GetProcessQtTableConfigFile();

        DataSet GetSPCProsessNoConfigFile(); //获取不同工序下不同参数SPC判定的数值设定
        void ModifySPCProsessNoConfigFile(DataSet data);//修改SPC判定的不同工序参数的设定值
    }
}
