using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace QtDataTrace.Interfaces
{
    [Serializable]
    public class QtDataSourceConfig
    {
        IList<QtDataTableConfig> tables = new BindingList<QtDataTableConfig>();

        public IList<QtDataTableConfig> Tables
        {
            get { return tables; }
            set { tables = value; }
        }
    }
}
