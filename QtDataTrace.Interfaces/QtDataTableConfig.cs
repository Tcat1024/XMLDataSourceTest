using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace QtDataTrace.Interfaces
{
    [Serializable]
    public class QtDataTableConfig
    {
        string tableName;
        string chineseName;
        IList<QtDataTableColumnConfig> columns = new BindingList<QtDataTableColumnConfig>();

        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        public string ChineseName
        {
            get { return chineseName; }
            set { chineseName = value; }
        }

        public IList<QtDataTableColumnConfig> Columns
        {
            get { return columns; }
            set { columns = value; }
        }
    }
}
