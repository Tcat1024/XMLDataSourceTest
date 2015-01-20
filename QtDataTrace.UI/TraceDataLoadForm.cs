using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace QtDataTrace.UI
{
    public partial class TraceDataLoadForm : DevExpress.XtraEditors.XtraForm
    {
        public string TableName = null;
        public TraceDataLoadForm(string[] sourcetables)
        {
            InitializeComponent();
            this.listBoxControl1.Items.AddRange(sourcetables);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(this.listBoxControl1.SelectedItem==null)
            {
                MessageBox.Show("请选择要读取的数据");
                return;
            }
            this.TableName = listBoxControl1.SelectedItem.ToString();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void listBoxControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBoxControl1.SelectedItem != null)
                this.btnOK.Enabled = true;
            else
                this.btnOK.Enabled = false;
        }
    }
}