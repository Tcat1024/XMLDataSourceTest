using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace QtDataTrace.IService
{
    public interface IDataAnalyzeService
    {
        Tuple<Guid, string> CCTStart(string username, Guid id, int[] selected, string target, string[] f);
        Tuple<int, double[]> CCTget(string username, Guid id);
        Tuple<Guid, string> KMeansStart(string username, Guid id, int[] selected, string[] properties, int maxcount, int minclustercount, int maxclustercount, double m, double s, int initialmode, int maxthread);
        Tuple<int, DataSet> KMeansGet(string username, Guid id);
        bool Stop(string username, Guid id);
        Tuple<Guid, string> ContourPlotStart(string username, Guid id, int[] selected, string x, string y, string z,int width,int height);
        Tuple<int, System.Drawing.Image> ContourPlotGet(string username, Guid id);
    }
}
