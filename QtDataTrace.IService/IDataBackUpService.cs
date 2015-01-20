using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QtDataTrace.IService
{
    public interface IDataBackUpService
    {
        string SaveTable(string loginid,string userid, Guid id, string tablename, int[] selected = null);
        string[] GetTableList(string loginid);
        Tuple<Guid, string> GetTable(string loginid,string userid, string tablename);
        string RemoveTable(string loginid, string[] tablenames);
    }
}
