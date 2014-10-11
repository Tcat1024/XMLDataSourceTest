using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using EAS.Services;
using QtDataTrace.Interfaces;
using QtDataTrace.Access;

namespace QtDataTrace.Access
{
    [ServiceObject("单表数据查询服务")]
    [ServiceBind(typeof(ISingleQtTableService))]
    public class SingleQtTableService : ServiceObject, ISingleQtTableService
    {

        public List<String> GetSteelGradeList()
        {
            SingleQtTableLY210 qt = new SingleQtTableLY210();

            return qt.GetSteelGradeList();
        }

       
    }
}
