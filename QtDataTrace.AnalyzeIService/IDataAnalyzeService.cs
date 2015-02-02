using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace QtDataTrace.AnalyzeIService
{
    public interface ILocalizationDataAnalyzeService
    {
        Tuple<Guid, string> CCTStart(string username, Guid id, int[] selected, string target, string[] f);
        double[] CCTget(string username, Guid id);
        Tuple<Guid, string> KMeansStart(string username, Guid id, int[] selected, string[] properties, int maxcount, int minclustercount, int maxclustercount, double m, double s, int initialmode, int maxthread);
        DataSet KMeansGet(string username, Guid id);
        bool Stop(string username, Guid id);
        Tuple<Guid, string> ContourPlotStart(string username, Guid id, int[] selected, string x, string y, string z, int width, int height, double[] levels, bool drawline);
        System.Drawing.Image ContourPlotGet(string username, Guid id);
        Tuple<Guid, string> RpartStart(string username, Guid id, int[] selected, int width, int height, string targetcolumn, string[] sourcecolumns, string method,double cp);
        Tuple<System.Drawing.Image, string,double[,]> RpartGet(string username, Guid id);
        string GetErrorMessage(string username, Guid id);
        int GetProcess(string username, Guid id);
        bool Remove(string username, Guid id);
        Tuple<Guid, string> LmRegressStart(string username, Guid id, int[] selected, int width, int height, string targetcolumn, string[] sourcecolumns);
        Tuple<System.Drawing.Image, string, double[]> LmRegressGet(string username, Guid id);
    }
}
