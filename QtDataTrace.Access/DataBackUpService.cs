using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.IO;
using QtDataTrace.IService;
using SPC.Base.Interface;
using SPC.Algorithm;
using EAS.Services;

namespace QtDataTrace.Access
{
    [ServiceObject("质量数据备份服务")]
    [ServiceBind(typeof(IDataBackUpService))]
    public class DataBackUpService : ServiceObject, IDataBackUpService
    {
        public string SaveTable(string loginid, string userid, Guid id, string tablename, int[] selected = null)
        {
            return ServiceContainer.GetService<AnalyzeIService.ILocalizationDataBackUpService>().SaveTable(loginid,userid,id,tablename,selected);
        }

        public string[] GetTableList(string loginid)
        {
            return ServiceContainer.GetService<AnalyzeIService.ILocalizationDataBackUpService>().GetTableList(loginid);
        }
        public Tuple<Guid, string> GetTable(string loginid, string userid, string tablename)
        {
            return ServiceContainer.GetService<AnalyzeIService.ILocalizationDataBackUpService>().GetTable(loginid, userid, tablename);
        }

        public string RemoveTable(string loginid, string[] tablenames)
        {
            return ServiceContainer.GetService<AnalyzeIService.ILocalizationDataBackUpService>().RemoveTable(loginid, tablenames);
        }
    }
}
