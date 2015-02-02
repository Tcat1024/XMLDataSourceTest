using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using QtDataTrace.AnalyzeIService;
using SPC.Base.Interface;
using SPC.Algorithm;
using EAS.Services;

namespace QtDataTrace.AnalyzeService
{
    [ServiceObject("质量数据分析服务")]
    [ServiceBind(typeof(ILocalizationDataAnalyzeService))]
    public class LocalizationDataAnalyzeService : ServiceObject, ILocalizationDataAnalyzeService
    {
        public Tuple<Guid, string> CCTStart(string username, Guid id, int[] selected, string target, string[] f)
        {
            DataAnalyzeFactory factory = null;
            DataTable data = null;
            try
            {
                data = LocalizationDataTraceBLL.BeginAnalyzeData(username, id);
                factory = new CCTAnalyzeFactory(new ChoosedData(data, selected),target,f);
                factory.StopedWorking += (sender, e) => { LocalizationDataTraceBLL.EndAnalyzeData(username, id); };
                return new Tuple<Guid, string>(LocalizationDataAnalyzeBLL.Add(username, factory), "");
            }
            catch (Exception ex)
            {
                if (data != null)
                    LocalizationDataTraceBLL.EndAnalyzeData(username, id);
                return new Tuple<Guid, string>(Guid.Empty, ex.Message);
            }
        }
        public double[] CCTget(string username, Guid id)
        {
            CCTAnalyzeFactory factory = LocalizationDataAnalyzeBLL.GetFactory(username,id) as CCTAnalyzeFactory;
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
                data = LocalizationDataTraceBLL.BeginAnalyzeData(username, id);
                factory = new KMeansAnalyzeFactory(new ChoosedData(data, selected),properties,maxcount,minclustercount,maxclustercount,m,s,initialmode,maxthread);
                factory.StopedWorking += (sender, e) => { LocalizationDataTraceBLL.EndAnalyzeData(username, id); };
                return new Tuple<Guid, string>(LocalizationDataAnalyzeBLL.Add(username, factory), "");
            }
            catch (Exception ex)
            {
                if (data != null)
                    LocalizationDataTraceBLL.EndAnalyzeData(username, id);
                return new Tuple<Guid, string>(Guid.Empty, ex.Message);
            }
        }
        public DataSet KMeansGet(string username, Guid id)
        {
            KMeansAnalyzeFactory factory = LocalizationDataAnalyzeBLL.GetFactory(username, id) as KMeansAnalyzeFactory;
            if (factory != null && !factory.Working)
            {
                return factory.Result;
            }
            return null;
        }
        public bool Stop(string username, Guid id)
        {
            return LocalizationDataAnalyzeBLL.Stop(username, id);
        }
        public int GetProcess(string username, Guid id)
        {
            var temp = LocalizationDataAnalyzeBLL.GetFactory(username, id);
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
            var factory=LocalizationDataAnalyzeBLL.GetFactory(username,id);
            if (factory != null)
                return factory.Error;
            return null;
        }
        public bool Remove(string username,Guid id)
        {
            return LocalizationDataAnalyzeBLL.Remove(username, id);
        }
        public Tuple<Guid,string> ContourPlotStart(string username, Guid id, int[] selected, string x,string y,string z,int width,int height,double[] levels,bool drawline)
        {
            DataAnalyzeFactory factory = null;
            DataTable data = null;
            try
            {
                data = LocalizationDataTraceBLL.BeginAnalyzeData(username, id);
                factory = new ContourPlotFactory(new ChoosedData(data, selected), x, y, z, width, height, levels, drawline);
                factory.StopedWorking += (sender, e) => { LocalizationDataTraceBLL.EndAnalyzeData(username, id); };
                return new Tuple<Guid, string>(LocalizationDataAnalyzeBLL.Add(username,factory), "");
            }
            catch(Exception ex)
            {
                if(data!=null)
                    LocalizationDataTraceBLL.EndAnalyzeData(username, id);
                return new Tuple<Guid, string>(Guid.Empty, ex.Message);
            }
        }
        public System.Drawing.Image ContourPlotGet(string username, Guid id)
        {
            ContourPlotFactory factory = LocalizationDataAnalyzeBLL.GetFactory(username, id) as ContourPlotFactory;
            if (factory != null && !factory.Working)
            {
                return factory.Result;
            }
            return null;
        }
        public Tuple<Guid, string> RpartStart(string username, Guid id, int[] selected,int width, int height,string targetcolumn,string[] sourcecolumns,string method,double cp)
        {
            DataAnalyzeFactory factory = null;
            DataTable data = null;
            try
            {
                data = LocalizationDataTraceBLL.BeginAnalyzeData(username, id);
                factory = new RpartFactory(new ChoosedData(data, selected),width, height,targetcolumn,sourcecolumns,method,cp);
                factory.StopedWorking += (sender, e) => { LocalizationDataTraceBLL.EndAnalyzeData(username, id); };
                return new Tuple<Guid, string>(LocalizationDataAnalyzeBLL.Add(username, factory), "");
            }
            catch (Exception ex)
            {
                if (data != null)
                    LocalizationDataTraceBLL.EndAnalyzeData(username, id);
                return new Tuple<Guid, string>(Guid.Empty, ex.Message);
            }
        }
        public Tuple<System.Drawing.Image,string,double[,]> RpartGet(string username, Guid id)
        {
            RpartFactory factory = LocalizationDataAnalyzeBLL.GetFactory(username, id) as RpartFactory;
            if (factory != null && !factory.Working)
            {
                return factory.Result;
            }
            return null;
        }
        public Tuple<Guid, string> LmRegressStart(string username, Guid id, int[] selected, int width, int height, string targetcolumn, string[] sourcecolumns)
        {
            DataAnalyzeFactory factory = null;
            DataTable data = null;
            try
            {
                data = LocalizationDataTraceBLL.BeginAnalyzeData(username, id);
                factory = new LmRegressFactory(new ChoosedData(data, selected), width, height, targetcolumn, sourcecolumns);
                factory.StopedWorking += (sender, e) => { LocalizationDataTraceBLL.EndAnalyzeData(username, id); };
                return new Tuple<Guid, string>(LocalizationDataAnalyzeBLL.Add(username, factory), "");
            }
            catch (Exception ex)
            {
                if (data != null)
                    LocalizationDataTraceBLL.EndAnalyzeData(username, id);
                return new Tuple<Guid, string>(Guid.Empty, ex.Message);
            }
        }
        public Tuple<System.Drawing.Image, string, double[]> LmRegressGet(string username, Guid id)
        {
            LmRegressFactory factory = LocalizationDataAnalyzeBLL.GetFactory(username, id) as LmRegressFactory;
            if (factory != null && !factory.Working)
            {
                return factory.Result;
            }
            return null;
        }
    }
}
