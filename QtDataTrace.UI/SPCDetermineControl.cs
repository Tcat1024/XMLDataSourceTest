using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using EAS.Services;
using EAS.Modularization;

namespace QtDataTrace.UI
{
    [Module("521F1A32-A771-431A-A1FC-3BA726F6CF6C", "SPC规则判定", "SPC规则判定模块")]
    public partial class SPCDetermineControl : DevExpress.XtraEditors.XtraUserControl
    {
        public SPCDetermineControl()
        {
            InitializeComponent();
        }
        public Object DataSource
        {
            set { this.determineControl1.DataSource = value; }
        }
        public Object DataView
        {
            get
            {
                return this.determineControl1.DataView;
            }
        }
    }
}
