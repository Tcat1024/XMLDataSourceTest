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
    [ServiceObject("质量数据分析服务")]
    [ServiceBind(typeof(IDataAnalyzeService))]
    public class DataAnalyzeService : ServiceObject,IDataAnalyzeService
    {
        public Tuple<Guid, string> CCTStart(string username, Guid id, int[] selected, string target, string[] f)
        {
            DataAnalyzeFactory factory = null;
            DataTable data = null;
            try
            {
                data = QtDataTraceBLL.BeginAnalyzeData(username, id);
                factory = new CCTAnalyzeFactory(new ChoosedData(data, selected),target,f);
                factory.StopedWorking += (sender, e) => { QtDataTraceBLL.EndAnalyzeData(username, id); };
                return new Tuple<Guid, string>(DataAnalyzeBLL.Add(username, factory), "");
            }
            catch (Exception ex)
            {
                if (data != null)
                    QtDataTraceBLL.EndAnalyzeData(username, id);
                return new Tuple<Guid, string>(Guid.Empty, ex.Message);
            }
        }
        public double[] CCTget(string username, Guid id)
        {
            CCTAnalyzeFactory factory = DataAnalyzeBLL.GetFactory(username,id) as CCTAnalyzeFactory;
            if(factory!=null&&!factory.Working)
            {
                return factory.Result;
            }
            return null;
        }
        public Tuple<Guid, string> KMeansStart(string username, Guid id, int[] selected,string[] properties,int maxcount,int minclustercount,int maxclustercount,double m,double s,int initialmode,int maxthread)
        {
            DataAnalyzeFactory factory = null;
            DataTable data = null;
            try
            {
                data = QtDataTraceBLL.BeginAnalyzeData(username, id);
                factory = new KMeansAnalyzeFactory(new ChoosedData(data, selected),properties,maxcount,minclustercount,maxclustercount,m,s,initialmode,maxthread);
                factory.StopedWorking += (sender, e) => { QtDataTraceBLL.EndAnalyzeData(username, id); };
                return new Tuple<Guid, string>(DataAnalyzeBLL.Add(username, factory), "");
            }
            catch (Exception ex)
            {
                if (data != null)
                    QtDataTraceBLL.EndAnalyzeData(username, id);
                return new Tuple<Guid, string>(Guid.Empty, ex.Message);
            }
        }
        public DataSet KMeansGet(string username, Guid id)
        {
            KMeansAnalyzeFactory factory = DataAnalyzeBLL.GetFactory(username, id) as KMeansAnalyzeFactory;
            if (factory != null && !factory.Working)
            {
                return factory.Result;
            }
            return null;
        }
        public bool Stop(string username, Guid id)
        {
            return DataAnalyzeBLL.Stop(username, id);
        }
        public int GetProcess(string username, Guid id)
        {
            var temp = DataAnalyzeBLL.GetFactory(username, id);
            if (temp == null)
                return -2;
            if (temp.Working)
                return temp.GetProgress();
            if (temp.Error != "")
                return -1;
            return 1000;
        }
        public string GetErrorMessage(string username,Guid id)
        {
            var factory=DataAnalyzeBLL.GetFactory(username,id);
            if (factory != null)
                return factory.Error;
            return null;
        }
        public bool Remove(string username,Guid id)
        {
            return DataAnalyzeBLL.Remove(username, id);
        }
        public Tuple<Guid,string> ContourPlotStart(string username, Guid id, int[] selected, string x,string y,string z,int width,int height,double[] levels,bool drawline)
        {
            DataAnalyzeFactory factory = null;
            DataTable data = null;
            try
            {
                data = QtDataTraceBLL.BeginAnalyzeData(username, id);
                factory = new ContourPlotFactory(new ChoosedData(data, selected), x, y, z, width, height, levels, drawline);
                factory.StopedWorking += (sender, e) => { QtDataTraceBLL.EndAnalyzeData(username, id); };
                return new Tuple<Guid, string>(DataAnalyzeBLL.Add(username,factory), "");
            }
            catch(Exception ex)
            {
                if(data!=null)
                    QtDataTraceBLL.EndAnalyzeData(username, id);
                return new Tuple<Guid, string>(Guid.Empty, ex.Message);
            }
        }
        public System.Drawing.Image ContourPlotGet(string username, Guid id)
        {
            ContourPlotFactory factory = DataAnalyzeBLL.GetFactory(username, id) as ContourPlotFactory;
            if (factory != null && !factory.Working)
            {
                return factory.Result;
            }
            return null;
        }
    }
}
