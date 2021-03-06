﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using QtDataTrace.Interfaces;

namespace QtDataTrace.IService
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

        DataSet GetProcessQtTableConfigFile();

        String GetSpcConfig();
        void SaveSpcConfig(String doc2);
        DataSet GetTablenameAndColume();
        DataSet QuerySpcLog(string sql);

        String Get_HRMQDS_Config();
        void Save_HRMQDS_Config(String doc2);
    }
}
