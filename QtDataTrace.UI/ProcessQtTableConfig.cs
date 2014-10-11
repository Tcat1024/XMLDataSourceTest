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
using System.Threading;

namespace QtDataTrace.UI
{
    [Module("EDC2E0A0-7786-499C-A934-3C3CA7B30A8D", "工序质量数据表配置", "工序质量数据表配置模块")]
    public partial class ProcessQtTableConfig : UserControl
    {
        private List<DataSet> data;
        private DataView view;

        public ProcessQtTableConfig()
        {
            InitializeComponent();
            ToolStripControlHost host1 = new ToolStripControlHost(this.dropDownButton1);
            this.bindingNavigator1.Items.Insert(10, host1);
            this.dropDownButton1.Visible = true;
        }

        private void ProcessQtTableConfig_Load(object sender, EventArgs e)
        {
            //Thread getdata_thread = new Thread(InitData);
            //getdata_thread.Start();
            InitData();
        }
        private void InitData()
        {
            try
            {
                data = ServiceContainer.GetService<IBaseTableService>().GetProcessQtTableConfig();
                //Action temp = () => {
                bindingSource1.DataSource = data[0];
                bindingSource1.DataMember = "Table";
                view = data[1].Tables["USER_TAB_COLUMNS"].DefaultView;

                repositoryItemLookUpEdit1.DisplayMember = "PROC_COMMENTS";
                repositoryItemLookUpEdit1.ValueMember = "PROCESS_NO";
                repositoryItemLookUpEdit1.DataSource = data[1].Tables["PROCESS"];

                repositoryItemLookUpEdit2.DisplayMember = "TABLE_NAME";
                repositoryItemLookUpEdit2.ValueMember = "TABLE_NAME";
                repositoryItemLookUpEdit2.DataSource = data[1].Tables["USER_TABLES"];
                //};
                //this.Invoke(temp);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void toolStripSave_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("保存修改的数据", "数据保存", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                ServiceContainer.GetService<IBaseTableService>().SaveProcessQtTableConfig(data[0]);
            }
        }

        private void gridControl1_FocusedViewChanged(object sender, DevExpress.XtraGrid.ViewFocusEventArgs e)
        {
            if (e.View != null && e.View.SourceView == gridView2)
            {
                var temprow = e.View.SourceRow as DataRowView;
                if (temprow == null)
                    return;
                view.RowFilter = "TABLE_NAME = '" + temprow["TABLE_NAME"].ToString() + "'";
                this.repositoryItemComboBox1.Items.BeginUpdate();
                this.repositoryItemComboBox1.Items.Clear();
                for (int i = 0; i < view.Count; i++)
                {
                    this.repositoryItemComboBox1.Items.Add(view[i]["COLUMN_NAME"]);
                }
                this.repositoryItemComboBox1.Items.EndUpdate();
            }
        }

        //private void dropDownButton1_Click(object sender, EventArgs e)
        //{
        //    (sender as DevExpress.XtraEditors.DropDownButton).ShowDropDown();
        //}
        private DataRow currentRow;
        private void dropDownButton1_ShowDropDownControl(object sender, DevExpress.XtraEditors.ShowDropDownControlEventArgs e)
        {
            this.triStateTreeView1.BeginUpdate();
            this.triStateTreeView1.Nodes.Clear();
            var temprow = this.gridView1.GetFocusedRow() as DataRowView;
            if (temprow != null)
            {
                this.triStateTreeView1.Nodes.Add(temprow["TABLE_NAME"].ToString());
                view.RowFilter = "TABLE_NAME = '" + temprow["TABLE_NAME"].ToString() + "'";
                for (int i = 0; i < view.Count; i++)
                {
                    var columnname = view[i]["COLUMN_NAME"].ToString();
                    this.triStateTreeView1.Nodes[0].Nodes.Add(columnname, columnname);
                }
                this.triStateTreeView1.Refresh();
                this.triStateTreeView1.ExpandAll();
                this.currentRow = temprow.Row;
                var rows = currentRow.GetChildRows("Table_Column");
                for (int i = 0; i < rows.Length; i++)
                {
                    var columnname = rows[i]["COLUMN_NAME"].ToString();
                    if (this.triStateTreeView1.Nodes[0].Nodes.ContainsKey(columnname))
                        this.triStateTreeView1.Nodes[0].Nodes[columnname].Checked = true;
                }
            }
            this.triStateTreeView1.EndUpdate();
        }

        private void triStateTreeView1_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void dropDownButton1_Validated(object sender, EventArgs e)
        {
            if (currentRow != null)
            {
                foreach (TreeNode node in this.triStateTreeView1.Nodes[0].Nodes)
                {
                    if (node.Checked)
                    {
                        if ((currentRow.GetChildRows("Table_Column").FirstOrDefault((row) => { return row["COLUMN_NAME"].ToString() == node.Text; })) == null)
                        {
                            var newrow = this.data[0].Tables["COLUMN"].NewRow();
                            newrow["COLUMN_NAME"] = node.Text;
                            newrow.SetParentRow(currentRow, currentRow.Table.ChildRelations["Table_Column"]);
                            this.data[0].Tables["COLUMN"].Rows.Add(newrow);
                        }
                    }
                    else
                    {
                        DataRow temprow;
                        if ((temprow = (currentRow.GetChildRows("Table_Column").FirstOrDefault((row) => { return row["COLUMN_NAME"].ToString() == node.Text; }))) != null)
                        {
                            temprow.Delete();
                        }

                    }
                }
            }

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            var row = (this.gridControl1.FocusedView as DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRow() as DataRowView;
            row.Delete();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            var row = (this.gridControl1.FocusedView as DevExpress.XtraGrid.Views.Grid.GridView).GetFocusedRow() as DataRowView;
            if (row != null)
                row.DataView.AddNew();
        }






    }
}
