using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QtDataTrace.Interfaces
{
    [Serializable]
    public class QtDataTableColumnConfig
    {
        string chineseName;
        string columnName;
        public string ChineseName
        {
            get { return chineseName; }
            set { chineseName = value; }
        }
        public string ColumnName
        {
            get { return columnName; }
            set { columnName = value; }
        }
    }

}
