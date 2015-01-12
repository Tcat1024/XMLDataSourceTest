using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
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
        public Guid KMeansStart(string username, Guid id, int[] selected,string[] properties,int maxcount,int minclustercount,int maxclustercount,double m,double s,int initialmode,int maxthread)
        {
            var data = QtDataTraceBLL.BeginAnalyzeData(username, id);
            if (data == null)
                return Guid.Empty;
            return DataAnalyzeBLL.Add(username, new KMeansAnalyzeFactory(new ChoosedData(data, selected),properties,maxcount,minclustercount,maxclustercount,m,s,initialmode,maxthread));
        }
        public Tuple<int, DataSet> KMeansGet(string username, Guid id)
        {
            KMeansAnalyzeFactory factory = DataAnalyzeBLL.GetFactory(username, id) as KMeansAnalyzeFactory;
            int progress = factory.GetProgress();
            if (!factory.Working)
            {
                return new Tuple<int, DataSet>(progress, factory.Result);
            }
            return new Tuple<int, DataSet>(progress, null);
        }
        public bool Stop(string username, Guid id)
        {
            return DataAnalyzeBLL.GetFactory(username, id).Stop();
        }
        public Guid ContourPlotStart(string username, Guid id, int[] selected, string x,string y,string z)
        {
            var data = QtDataTraceBLL.BeginAnalyzeData(username, id);
            if (data == null)
                return Guid.Empty;
            return DataAnalyzeBLL.Add(username, new ContourPlotFactory(new ChoosedData(data, selected),x,y,z));
        }
        public Tuple<int, System.Drawing.Image> ContourPlotGet(string username, Guid id)
        {
            ContourPlotFactory factory = DataAnalyzeBLL.GetFactory(username, id) as ContourPlotFactory;
            int progress = factory.GetProgress();
            if (!factory.Working)
            {
                return new Tuple<int, System.Drawing.Image>(progress, factory.Result);
            }
            return new Tuple<int, System.Drawing.Image>(progress, null);
        }
    }
}
