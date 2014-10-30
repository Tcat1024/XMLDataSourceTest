using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace QtDataTrace.IService
{
    public interface IBaseDataService
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
    }
}
