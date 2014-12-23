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
using QtDataTrace.Interfaces;

namespace QtDataTrace.UI
{
    [Module("521F1A32-A771-431A-A1FC-2BA726F6CF6B", "SPC计算", "SPC计算模块")]
    public partial class SpcModule : UserControl
    {
        public SpcModule()
        {
            InitializeComponent();
        }

        public Object DataSource
        {
            set { spcMonitorControl.DataSource = value; }
        }
        public Object DataView
        {
            get
            {
                return spcMonitorControl.DataView;
            }
        }
        public int SelectTabPageIndex
        {
            set
            {
                spcMonitorControl.SelectedTabPageIndex = value;
            }
        }
        private void SpcModule_Load(object sender, EventArgs e)
        {
        }
    }
}
