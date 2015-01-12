using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QtDataTrace.UI
{
    public partial class AnalyzeConfigForm : DevExpress.XtraEditors.XtraForm
    {
        public AnalyzeConfigForm()
        {
            InitializeComponent();
        }
        public void AddConfigControl(SPC.Analysis.ConfigControls.ConfigControlBase con)
        {
            this.Controls.Add(con);
            con.Dock = DockStyle.Fill;
            this.MinimumSize = this.GetConstrainSize(con.MinimumSize);
            this.Size = this.MinimumSize;
            con.OKEvent += (sender, e) => { this.DialogResult = System.Windows.Forms.DialogResult.OK; };
            con.CancelEvent += (sender, e) => { this.DialogResult = System.Windows.Forms.DialogResult.Cancel; };
        }
    }
}
