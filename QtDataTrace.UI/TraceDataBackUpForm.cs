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
    public partial class TraceDataBackUpForm : DevExpress.XtraEditors.XtraForm
    {
        public List<string> RemoveTables = new List<string>();
        public string SaveTable = null;
        public bool SaveFilter
        {
            get
            {
                return this.checkEdit1.Checked;
            }
        }
        public TraceDataBackUpForm(string[] sourcetables)
        {
            InitializeComponent();
            this.listBoxControl1.Items.AddRange(sourcetables);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = this.textEdit1.Text.Trim();
            if (name == "")
            {
                MessageBox.Show("未填写要保存的表名");
                return;
            }
            if (this.listBoxControl1.Items.Contains(name.ToUpper()) && this.listBoxControl1.Items.Count<=3)
            {
                if (MessageBox.Show("已存在表" + name + "，是否要覆盖原表？", "Warning", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                    return;
            }
            else if (this.listBoxControl1.Items.Count >= 3)
            {
                MessageBox.Show("用户数据超出限额，无法新建备份");
                return;
            }
            this.RemoveTables.Clear();
            this.SaveTable = name;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int count = this.listBoxControl1.SelectedItems.Count;
            if (count == 0)
            {
                MessageBox.Show("未选择要删除的表格");
                return;
            }
            string message = "确定删除表 ";
            string item = this.listBoxControl1.SelectedItems[0].ToString(); 
            RemoveTables.Add(item);
            message += item;
            for (int i = 1; i < count;i++ )
            {
                item = this.listBoxControl1.SelectedItems[i].ToString();
                RemoveTables.Add(item);
                message += ", "+item;
            }
            message += " 吗？";
            if (MessageBox.Show(message,"Warning", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            else
                RemoveTables.Clear();
        }

        private void listBoxControl1_MouseClick(object sender, MouseEventArgs e)
        {
            
            if(e.Button== System.Windows.Forms.MouseButtons.Left)
            {
                int index = this.listBoxControl1.IndexFromPoint(e.Location);
                if (index >= 0)
                    this.textEdit1.Text = this.listBoxControl1.Items[index].ToString();
            }
        }
    }
}