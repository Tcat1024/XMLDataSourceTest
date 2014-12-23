using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EAS.Services;
using EAS.Modularization;

namespace QtDataTrace.UI
{
    [Module("521F1A32-A771-431A-A1FC-3BA726F6CF6B", "SPC计算", "二维SPC计算模块")]
    public partial class RelationMonitorControl : UserControl
    {
        public RelationMonitorControl()
        {
            InitializeComponent();
        }
        public Object DataSource
        {
            set { this.xyRelationControl1.DataSource = value; }
        }
        public object DataView
        {
            get
            {
                return this.xyRelationControl1.DataView;
            }
        }
    }
}
