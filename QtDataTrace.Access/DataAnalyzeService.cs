using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QtDataTrace.IService;
using SPC.Base.Interface;
using SPC.Algorithm;
using EAS.Services;

namespace QtDataTrace.Access
{
    [ServiceObject("质量数据追踪服务")]
    [ServiceBind(typeof(IDataAnalyzeService))]
    public class DataAnalyzeService : ServiceObject,IDataAnalyzeService
    {
        public Guid CCTStart(string username,Guid id,int[] selected,string target,string[] f)
        {
            var data = QtDataTraceBLL.BeginAnalyzeData(username, id);
            if (data == null)
                return Guid.Empty;
            return DataAnalyzeBLL.Add(username,new CCTAnalyzeFactory(new ChoosedData(data, selected),target,f));
        }
        public Tuple<int,double[]> CCTget(string username, Guid id)
        {
            CCTAnalyzeFactory factory = DataAnalyzeBLL.GetFactory(username,id) as CCTAnalyzeFactory;
            int progress = factory.GetProgress();
            if(!factory.Working)
            {
                return new Tuple<int, double[]>(progress, factory.Result);
            }
            return new Tuple<int, double[]>(progress, null);
        }
    }
}
