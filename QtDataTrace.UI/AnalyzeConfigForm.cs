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
    public partial class AnalyzeConfigForm : Form
    {
        public AnalyzeConfigForm()
        {
            InitializeComponent();
        }
        public void AddConfigControl(SPC.Analysis.ConfigControls.ConfigControlBase con)
        {
            this.Controls.Add(con);
            if (this.Size.Width < con.MinimumSize.Width||this.Size.Height<con.MinimumSize.Height)
                this.Size = con.MinimumSize;
            con.Dock = DockStyle.Fill;
            con.OKEvent += (sender, e) => { this.DialogResult = System.Windows.Forms.DialogResult.OK; };
            con.CancelEvent += (sender, e) => { this.DialogResult = System.Windows.Forms.DialogResult.Cancel; };
        }
    }
}
