using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QtDataTrace.IService
{
    public interface IDataAnalyzeService
    {
        Guid CCTStart(string username,Guid id, int[] selected, string target, string[] f);
        Tuple<int, double[]> CCTget(string username, Guid id);
    }
}
