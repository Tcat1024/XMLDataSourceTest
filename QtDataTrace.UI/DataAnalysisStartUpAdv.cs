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
    [Module("33EE9C1F-0AEF-4874-B63B-07C22960F165", "数据分析起始页", "数据分析起始页模块")]
    public partial class DataAnalysisStartUpAdv : UserControl
    {
        private DataSet data;
        private DataSet processData;
        private DataSet configFile;
        private DataView configFileView;
        private BindingSource materialBindingSource = new BindingSource();
        private QueryArgs queryArg;
        private string _processNo;
        public string ProcessNo
        {
            get
            {
                return this._processNo;
            }
            private set
            {
                this._processNo = value;
                //try
                //{
                InitTreeList();
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message);
                //}
                //finally
                //{
                //    this.triStateTreeView1.EndUpdate();
                //}
            }
        }
        private string _processName;
        public string ProcessName
        {
            get
            {
                return this._processName;
            }
            private set
            {
                this._processName = value;
            }
        }
        public DataAnalysisStartUpAdv()
        {
            InitializeComponent();
        }

        private void DataAnalysisStartUp_Load(object sender, EventArgs e)
        {
            checkTime.Checked = true;
            dateTimeStart.Value = DateTime.Now.AddDays(-1);

            processData = ServiceContainer.GetService<IBaseTableService>().GetProcessCode();
            this.lookUpEdit1.Properties.DataSource = processData.Tables[0];
            lookUpEdit1.Properties.DisplayMember = "PROC_COMMENTS";
            lookUpEdit1.Properties.ValueMember = "PROCESS_NO";

            comboxGrade.DataSource = ServiceContainer.GetService<ISingleQtTableService>().GetSteelGradeList();

            configFile = ServiceContainer.GetService<IBaseTableService>().GetProcessQtTableConfigFile();
            configFileView = configFile.Tables["Table"].DefaultView;

            this.gridControl2.DataSource = materialBindingSource;
        }

        private void toolStripCpk_Click(object sender, EventArgs e)
        {
            if (processData == null)
            {
                MessageBox.Show("请选择数据源");
                return;
            }

            CpkModule module = new CpkModule();

            module.DataSource = processData.Tables[0];

            EAS.Application.Instance.OpenModule(module);
        }
        private void InitTreeList()
        {
            this.triStateTreeView1.BeginUpdate();
            this.triStateTreeView1.Nodes.Clear();
            var rootNode = new TreeNode() { Name = this.ProcessNo, Text = this.ProcessName, ForeColor = Color.Red };
            this.triStateTreeView1.Nodes.Add(rootNode);
            var temptable = this.processData.Tables[0];
            for (int i = 0; i < temptable.Rows.Count; i++)
            {
                TreeNode processNode;
                string tempno;
                string tempname;
                if (temptable.Rows[i]["PROCESS_NO"].ToString() != this.ProcessNo)
                {
                    tempno = temptable.Rows[i]["PROCESS_NO"].ToString();
                    tempname = temptable.Rows[i]["PROC_COMMENTS"].ToString();
                    processNode = this.triStateTreeView1.Nodes.Add(tempno, tempname);
                }
                else
                {
                    tempno = this.ProcessNo;
                    tempname = this.ProcessName;
                    processNode = rootNode;
                }
                configFileView.RowFilter = "PROCESS_CODE = '" + tempno + "'";
                for (int j = 0; j < configFileView.Count; j++)
                {
                    string e = configFileView[j]["TABLE_NAME"].ToString();
                    string c = configFileView[j]["TABLE_CHINESE"].ToString();
                    if (c == null || c.Trim() == "")
                        c = e;
                    var tablenode = processNode.Nodes.Add(e, c);
                    foreach (var column in configFileView[j].Row.GetChildRows("Table_Column"))
                    {
                        string ee = column["COLUMN_NAME"].ToString();
                        string cc = column["COLUMN_CHINESE"].ToString();
                        if (cc.Trim() == "")
                            cc = ee;
                        tablenode.Nodes.Add(ee, cc);
                    }
                }
            }
            this.triStateTreeView1.EndUpdate();
            this.triStateTreeView1.Refresh();
            rootNode.Expand();
        }
        private void btnQuery_Click(object sender, EventArgs e)
        {
            QueryArgs temp;
            if ((temp = getQueryArg()) == null)
            {
                MessageBox.Show("请选择工序");
                return;
            }
            IQtDataTraceService qt = (IQtDataTraceService)ServiceContainer.GetService<IQtDataTraceService>();
            materialBindingSource.DataSource = qt.GetMaterialList(temp);
            if (this.xtraTabPage2.PageEnabled)
            {
                this.xtraTabControl1.SelectedTabPageIndex = 0;
            }
            else
            {
                this.xtraTabPage2.PageEnabled = true;
                setGraphEnable(false);
            }

            this.ProcessName = lookUpEdit1.GetColumnValue("PROC_COMMENTS").ToString();
            if (this.ProcessName == null || this.ProcessName.Trim() == "")
                this.ProcessName = temp.ProcessCode;
            this.ProcessNo = temp.ProcessCode;
            this.queryArg = temp;

            //Object obj = lookUpEdit1.GetColumnValue("PROCESS_NO");
            //if (obj == null)
            //{
            //    MessageBox.Show("请选择工序");
            //    return;
            //}

            //QueryArgs arg = new QueryArgs();

            //arg.ProcessCode = obj.ToString();
            //arg.TimeFlag = checkTime.Checked;
            //arg.SteelGradeFlag = checkGrade.Checked;
            //arg.ThickFlag = checkThick.Checked;
            //arg.WidthFlag = checkWidth.Checked;

            //arg.StartTime = dateTimeStart.Value;
            //arg.StopTime = dateTimeStop.Value;
            //arg.SteelGrade = comboxGrade.Text;

            //if (textMinThick.Text != "")
            //    arg.MinThick = Convert.ToDouble(textMinThick.Text);

            //if (textMaxThick.Text != "")
            //    arg.MaxThick = Convert.ToDouble(textMaxThick.Text);

            //if (textMinWidth.Text != "")
            //    arg.MinWidth = Convert.ToDouble(textMinWidth.Text);

            //if (textMaxWidth.Text != "")
            //    arg.MaxWidth = Convert.ToDouble(textMaxWidth.Text);

            //QtDataTableConfig table = new QtDataTableConfig();
            //bool hasdata = false;
            //for (int i = 0; i < this.triStateTreeView1.Nodes.Count; i++)
            //{
            //    var tablenode = this.triStateTreeView1.Nodes[i];
            //    if (tablenode.Checked)
            //    {
            //        table.TableName = tablenode.Name;
            //        table.ChineseName = tablenode.Text;
            //        for (int j = 0; j < tablenode.Nodes.Count; j++)
            //        {
            //            var columnnode = tablenode.Nodes[j];
            //            if (columnnode.Checked)
            //            {
            //                table.Columns.Add(new QtDataTableColumnConfig() { ChineseName = columnnode.Text, ColumnName = columnnode.Name });
            //                if (!hasdata)
            //                    hasdata = true;
            //            }
            //        }
            //        break;
            //    }
            //}
            //if (!hasdata)
            //{
            //    MessageBox.Show("没有选择数据");
            //    return;
            //}
            //data = ServiceContainer.GetService<IQtDataTraceService>().GetQtData(arg, table);
            //this.gridView1.Columns.Clear();
            //gridControl1.DataSource = data.Tables[0];
        }
        private QueryArgs getQueryArg()
        {
            Object obj = lookUpEdit1.GetColumnValue("PROCESS_NO");
            if (obj == null)
                return null;
            QueryArgs qA = new QueryArgs();

            qA.ProcessCode = obj.ToString();
            qA.TimeFlag = checkTime.Checked;
            qA.SteelGradeFlag = checkGrade.Checked;
            qA.ThickFlag = checkThick.Checked;
            qA.WidthFlag = checkWidth.Checked;

            qA.StartTime = dateTimeStart.Value;
            qA.StopTime = dateTimeStop.Value;
            qA.SteelGrade = comboxGrade.Text;

            if (textMinThick.Text != "")
                qA.MinThick = Convert.ToDouble(textMinThick.Text);

            if (textMaxThick.Text != "")
                qA.MaxThick = Convert.ToDouble(textMaxThick.Text);

            if (textMinWidth.Text != "")
                qA.MinWidth = Convert.ToDouble(textMinWidth.Text);

            if (textMaxWidth.Text != "")
                qA.MaxWidth = Convert.ToDouble(textMaxWidth.Text);
            return qA;
        }
        private void toolStripSpc_Click(object sender, EventArgs e)
        {
            if (processData == null)
            {
                MessageBox.Show("请选择数据源");
                return;
            }

            SpcModule module = new SpcModule();

            module.DataSource = processData.Tables[0];

            EAS.Application.Instance.OpenModule(module);
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            //SpcModule module = new SpcModule();
            //EAS.Application.Instance.OpenModule(module);

        }

        private void btnTraceQuery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnQuickQuery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Object obj = lookUpEdit1.GetColumnValue("PROCESS_NO");
            QtDataTableConfig table = new QtDataTableConfig();
            bool hasdata = false;
            TreeNode rootnode = null;
            for (int i = 0; i < this.triStateTreeView1.Nodes.Count; i++)
            {
                if (this.triStateTreeView1.Nodes[i].Checked)
                {
                    rootnode = this.triStateTreeView1.Nodes[i];
                    break;
                }
            }
            if (rootnode == null)
            {
                MessageBox.Show("未选择任何表");
                return;
            }
            for (int i = 0; i < rootnode.Nodes.Count; i++)
            {
                var tablenode = rootnode.Nodes[i];
                if (tablenode.Checked)
                {
                    table.TableName = tablenode.Name;
                    table.ChineseName = tablenode.Text;
                    for (int j = 0; j < tablenode.Nodes.Count; j++)
                    {
                        var columnnode = tablenode.Nodes[j];
                        if (columnnode.Checked)
                        {
                            table.Columns.Add(new QtDataTableColumnConfig() { ChineseName = columnnode.Text, ColumnName = columnnode.Name });
                            if (!hasdata)
                                hasdata = true;
                        }
                    }
                    break;
                }
            }
            if (!hasdata)
            {
                MessageBox.Show("没有选择数据");
                return;
            }
            string tempcode = queryArg.ProcessCode;
            queryArg.ProcessCode = rootnode.Name;
            data = ServiceContainer.GetService<IQtDataTraceService>().GetQtData(queryArg, table);
            queryArg.ProcessCode = tempcode;
            this.gridView1.Columns.Clear();
            gridControl1.DataSource = data.Tables[0];
            setGraphEnable(true);
        }

        private void btnCPK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (data == null)
            {
                MessageBox.Show("请选择数据源");
                return;
            }

            CpkModule module = new CpkModule();

            module.DataSource = data.Tables[0];

            EAS.Application.Instance.OpenModule(module);
        }

        private void btnSampleRun_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (data == null)
            {
                MessageBox.Show("请选择数据源");
                return;
            }

            SpcModule module = new SpcModule();

            module.DataSource = data.Tables[0];
            module.SelectTabPageIndex = 0;
            module.AccessibleDescription = "样本运行图";
            EAS.Application.Instance.OpenModule(module);
        }

        private void btnSampleControl_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (data == null)
            {
                MessageBox.Show("请选择数据源");
                return;
            }

            SpcModule module = new SpcModule();

            module.DataSource = data.Tables[0];
            module.SelectTabPageIndex = 1;
            module.AccessibleDescription = "控制图";
            EAS.Application.Instance.OpenModule(module);
        }

        private void btnSamleAvg_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (data == null)
            {
                MessageBox.Show("请选择数据源");
                return;
            }

            SpcModule module = new SpcModule();

            module.DataSource = data.Tables[0];
            module.SelectTabPageIndex = 2;
            module.AccessibleDescription = "均值运行图";
            EAS.Application.Instance.OpenModule(module);
        }

        private void btnNormalCheck_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (data == null)
            {
                MessageBox.Show("请选择数据源");
                return;
            }

            SpcModule module = new SpcModule();

            module.DataSource = data.Tables[0];
            module.SelectTabPageIndex = 3;
            module.AccessibleDescription = "正态校验";
            EAS.Application.Instance.OpenModule(module);
        }

        private void btnFrequency_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (data == null)
            {
                MessageBox.Show("请选择数据源");
                return;
            }

            SpcModule module = new SpcModule();

            module.DataSource = data.Tables[0];
            module.SelectTabPageIndex = 4;
            module.AccessibleDescription = "频度分布";
            EAS.Application.Instance.OpenModule(module);
        }
        private void setGraphEnable(bool t)
        {
            foreach (var btn in this.bar1.ItemLinks)
            {
                (btn as DevExpress.XtraBars.BarItemLink).Item.Enabled = t;
            }
        }
    }
}
