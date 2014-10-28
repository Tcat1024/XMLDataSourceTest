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
        IList<QtDataTableColumnConfig> columns = new List<QtDataTableColumnConfig>();

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
    [Serializable]
    public class QtDataProcessConfig
    {
        string chineseName;
        IList<QtDataTableConfig> _Tables = new List<QtDataTableConfig>();

        public string ChineseName
        {
            get { return chineseName; }
            set { chineseName = value; }
        }

        public IList<QtDataTableConfig> Tables
        {
            get { return _Tables; }
            set { _Tables = value; }
        }
    }
}
