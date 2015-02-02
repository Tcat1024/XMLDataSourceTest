using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using QtDataTrace.IService;
using EAS.Services;
using QtDataTrace.AnalyzeIService;

namespace QtDataTrace.Access
{
    [ServiceObject("质量数据分析服务")]
    [ServiceBind(typeof(IDataAnalyzeService))]
    public class DataAnalyzeService : ServiceObject, IDataAnalyzeService
    {
        public Tuple<Guid, string> CCTStart(string username, Guid id, int[] selected, string target, string[] f)
        {
            return ServiceContainer.GetService<ILocalizationDataAnalyzeService>("LocalServiceBridger").CCTStart(username, id, selected, target, f);
        }
        public double[] CCTget(string username, Guid id)
        {
            return ServiceContainer.GetService<ILocalizationDataAnalyzeService>("LocalServiceBridger").CCTget(username, id);
        
        }
        public Tuple<Guid, string> KMeansStart(string username, Guid id, int[] selected,string[] properties,int maxcount,int minclustercount,int maxclustercount,double m,double s,int initialmode,int maxthread)
        {
            return ServiceContainer.GetService<ILocalizationDataAnalyzeService>("LocalServiceBridger").KMeansStart(username, id, selected, properties, maxcount, minclustercount, maxclustercount, m, s, initialmode, maxthread);
        
        }
        public DataSet KMeansGet(string username, Guid id)
        {
            return ServiceContainer.GetService<ILocalizationDataAnalyzeService>("LocalServiceBridger").KMeansGet(username, id);
        
        }
        public bool Stop(string username, Guid id)
        {
            return ServiceContainer.GetService<ILocalizationDataAnalyzeService>("LocalServiceBridger").Stop(username, id);
        }
        public int GetProcess(string username, Guid id)
        {
            return ServiceContainer.GetService<ILocalizationDataAnalyzeService>("LocalServiceBridger").GetProcess(username, id);
        }
        public string GetErrorMessage(string username,Guid id)
        {
            return ServiceContainer.GetService<ILocalizationDataAnalyzeService>("LocalServiceBridger").GetErrorMessage(username, id);
        }
        public bool Remove(string username,Guid id)
        {
            return ServiceContainer.GetService<ILocalizationDataAnalyzeService>("LocalServiceBridger").Remove(username, id);
        }
        public Tuple<Guid,string> ContourPlotStart(string username, Guid id, int[] selected, string x,string y,string z,int width,int height,double[] levels,bool drawline)
        {
            return ServiceContainer.GetService<ILocalizationDataAnalyzeService>("LocalServiceBridger").ContourPlotStart(username, id, selected, x, y, z, width, height, levels, drawline);
        }
        public System.Drawing.Image ContourPlotGet(string username, Guid id)
        {
            return ServiceContainer.GetService<ILocalizationDataAnalyzeService>("LocalServiceBridger").ContourPlotGet(username, id);
        }
        public Tuple<Guid, string> RpartStart(string username, Guid id, int[] selected, int width, int height, string targetcolumn, string[] sourcecolumns, string method,double cp)
        {
            return ServiceContainer.GetService<ILocalizationDataAnalyzeService>("LocalServiceBridger").RpartStart(username, id, selected, width, height, targetcolumn, sourcecolumns, method,cp);
        }
        public Tuple<System.Drawing.Image, string,double[,]> RpartGet(string username, Guid id)
        {
            return ServiceContainer.GetService<ILocalizationDataAnalyzeService>("LocalServiceBridger").RpartGet(username, id);
        }
        public Tuple<Guid, string> LmRegressStart(string username, Guid id, int[] selected, int width, int height, string targetcolumn, string[] sourcecolumns)
        {
            return ServiceContainer.GetService<ILocalizationDataAnalyzeService>("LocalServiceBridger").LmRegressStart(username, id, selected, width, height, targetcolumn, sourcecolumns);
        
        }
        public Tuple<System.Drawing.Image, string, double[]> LmRegressGet(string username, Guid id)
        {
            return ServiceContainer.GetService<ILocalizationDataAnalyzeService>("LocalServiceBridger").LmRegressGet(username, id);
        
        }
    }
}
