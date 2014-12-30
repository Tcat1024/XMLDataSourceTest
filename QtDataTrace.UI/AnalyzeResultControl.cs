using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using EAS.Modularization;

namespace QtDataTrace.UI
{
    [Module("90EB9FDC-29B3-4538-AB07-6D98D6EF32B4", "分析模块结果控件", "追溯模块中数据分析通用结果控件")]
    public partial class AnalyzeResultControl : DevExpress.XtraEditors.XtraUserControl
    {
        public AnalyzeResultControl()
        {
            InitializeComponent();
        }
        public void AddResultControl(Control con)
        {
            this.Controls.Add(con);
            con.Dock = DockStyle.Fill;
        }
    }
}
