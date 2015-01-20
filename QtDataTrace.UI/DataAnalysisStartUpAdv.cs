﻿using System;
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
using QtDataTrace.IService;
using System.Threading;
using System.Management;
using System.Threading.Tasks;

namespace QtDataTrace.UI
{
    [Module("33EE9C1F-0AEF-4874-B63B-07C22960F165", "数据分析起始页", "数据分析起始页模块")]
    public partial class DataAnalysisStartUpAdv : UserControl
    {
        private DataTable data;
        private DataSet processData;
        private DataSet configFile;
        private DataView configFileView;
        private BindingSource materialBindingSource = new BindingSource();
        private QueryArgs queryArg;
        private string _processNo;
        private Guid currentDataId = Guid.Empty;
        private Guid currentTraceFactoryId = Guid.Empty;
        private Guid currentAnalyzeFactoryId = Guid.Empty;
        private bool currentTraceDir;
        private string loginId = "";
        private string macId = "";
        private string userId
        {
            get
            {
                return loginId + macId;
            }
        }
        private Thread timerTask;
        enum WorkingMode
        {
            None,
            Trace,
            Analyze
        }
        private WorkingMode currentWorkingMode = WorkingMode.None;
        public string ProcessNo
        {
            get
            {
                return this._processNo;
            }
            private set
            {
                if (this._processNo != value)
                {
                    this._processNo = value;
                    InitTreeList();
                }
            }
        }
        public string ProcessSEQ { get; set; }
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
            if (EAS.Application.Instance != null)
                this.loginId = (EAS.Application.Instance.Session.Client as EAS.Explorer.IAccount).LoginID;
            else
                this.loginId = "Debug";
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (mo["IPEnabled"].ToString() == "True")
                {
                    this.macId += mo["MacAddress"].ToString();
                    break;
                }
            }
        }
        private void InitTreeList()
        {
            this.triStateTreeView1.BeginUpdate();
            this.triStateTreeView1.Nodes.Clear();
            var root = this.configFile.Tables["Table"].Select("PROCESS_CODE ='" + this.ProcessNo + "'");
            TreeNode rootNode = null;
            if (root.Count() > 0)
            {
                rootNode = this.triStateTreeView1.Nodes.Add(root.FirstOrDefault()["PROCESS_SEQ"].ToString(), root.FirstOrDefault()["PROCESS_CHINESE"].ToString());
                rootNode.ForeColor = Color.Red;
                this.ProcessSEQ = rootNode.Name;
                this.btnBackQrace.Enabled = true;
                this.btnPreQtrace.Enabled = true;
            }
            else
            {
                this.ProcessSEQ = "";
                this.btnBackQrace.Enabled = false;
                this.btnPreQtrace.Enabled = false;
            }
            foreach (var temprow in (from r in this.configFile.Tables["Table"].AsEnumerable() orderby r["PROCESS_SEQ"] group r by r["PROCESS_CHINESE"] into g select g.FirstOrDefault()))
            {
                TreeNode processnode;
                if (rootNode != null && temprow["PROCESS_CHINESE"].ToString() == rootNode.Text)
                    processnode = rootNode;
                else
                    processnode = this.triStateTreeView1.Nodes.Add(temprow["PROCESS_SEQ"].ToString(), temprow["PROCESS_CHINESE"].ToString());
                var tables = from r in this.configFile.Tables["Table"].AsEnumerable() where r["PROCESS_CHINESE"].ToString() == processnode.Text group r by r["TABLE_CHINESE"] into g select g.FirstOrDefault()["TABLE_CHINESE"];
                for (int i = 0; i < tables.Count(); i++)
                {
                    var tablenode = processnode.Nodes.Add(tables.ElementAt(i).ToString());
                    var columns = from r in this.configFile.Tables["Column"].AsEnumerable() where r.GetParentRow("Table_Column")["TABLE_CHINESE"].ToString() == tablenode.Text group r by r["COLUMN_CHINESE"].ToString() == "" ? r["COLUMN_NAME"] : r["COLUMN_CHINESE"] into g select g.FirstOrDefault();
                    foreach (var column in columns)
                    {
                        var s = column["COLUMN_CHINESE"].ToString();
                        tablenode.Nodes.Add(s == "" ? column["COLUMN_NAME"].ToString() : s);
                    }
                }
            }
            this.triStateTreeView1.EndUpdate();
            this.triStateTreeView1.Refresh();
            if (rootNode != null)
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
            this.ProcessNo = temp.ProcessCode;
            this.queryArg = temp;
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
        private void btnCPK_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (data == null)
            {
                MessageBox.Show("请选择数据源");
                return;
            }

            CpkModule module = new CpkModule();
            module.DataSource = data;
            (module.DataView as SPC.Base.Control.CanChooseDataGridView).Synchronize(this.gridView1, DevExpress.XtraGrid.Views.Base.SynchronizationMode.Full);
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

            module.DataSource = data;
            (module.DataView as SPC.Base.Control.CanChooseDataGridView).Synchronize(this.gridView1, DevExpress.XtraGrid.Views.Base.SynchronizationMode.Full);
            module.SelectTabPageIndex = 0;
            module.AccessibleDescription = "样本散点图";
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

            module.DataSource = data;
            (module.DataView as SPC.Base.Control.CanChooseDataGridView).Synchronize(this.gridView1, DevExpress.XtraGrid.Views.Base.SynchronizationMode.Full);
            module.SelectTabPageIndex = 1;
            module.AccessibleDescription = "样本控制图";
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

            module.DataSource = data;
            (module.DataView as SPC.Base.Control.CanChooseDataGridView).Synchronize(this.gridView1, DevExpress.XtraGrid.Views.Base.SynchronizationMode.Full);
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

            module.DataSource = data;
            (module.DataView as SPC.Base.Control.CanChooseDataGridView).Synchronize(this.gridView1, DevExpress.XtraGrid.Views.Base.SynchronizationMode.Full);
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

            module.DataSource = data;
            (module.DataView as SPC.Base.Control.CanChooseDataGridView).Synchronize(this.gridView1, DevExpress.XtraGrid.Views.Base.SynchronizationMode.Full);
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
        private void setTraceEnable(bool t)
        {
            foreach (var btn in this.bar2.ItemLinks)
            {
                (btn as DevExpress.XtraBars.BarItemLink).Item.Enabled = t;
            }
            this.gridControl1.Enabled = t;
        }
        private void btnQtrace_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.ProcessSEQ == null || this.ProcessSEQ.Trim() == "")
            {
                MessageBox.Show("所选工序不符合追溯条件");
                return;
            }
            List<QtDataProcessConfig> processes = new List<QtDataProcessConfig>();
            bool hasdata = false;
            int targetseq = int.Parse(this.ProcessSEQ);
            Func<int, bool> check;
            if(e.Item == this.btnPreQtrace)
            {
                check = (a) => { return a <= targetseq; };
                currentTraceDir = false;
            }
            else
            {
                check = (a) => { return a >= targetseq; };
                currentTraceDir = true;
            }
            foreach (TreeNode processnode in this.triStateTreeView1.Nodes)
            {
                if (check(int.Parse(processnode.Name)) && processnode.Checked != false)
                {
                    var process = new QtDataProcessConfig() { ChineseName = processnode.Text };
                    processes.Add(process);
                    for (int i = 0; i < processnode.Nodes.Count; i++)
                    {
                        var tablenode = processnode.Nodes[i];
                        if (tablenode.Checked)
                        {
                            var table = new QtDataTableConfig() { ChineseName = tablenode.Text };
                            process.Tables.Add(table);
                            for (int j = 0; j < tablenode.Nodes.Count; j++)
                            {
                                var columnnode = tablenode.Nodes[j];
                                if (columnnode.Checked)
                                {
                                    table.Columns.Add(new QtDataTableColumnConfig() { ChineseName = columnnode.Text });
                                    if (!hasdata)
                                        hasdata = true;
                                }
                            }
                        }
                    }
                }
            }
            var idlist = this.getIdList();
            if (!hasdata || idlist.Count <= 0)
            {
                MessageBox.Show("没有选择数据");
                return;
            }
            var re = ServiceContainer.GetService<IQtDataTraceService>().NewDataTrace(this.ProcessNo, idlist, processes, currentTraceDir, userId);
            currentTraceFactoryId = re.Item1;
            if (currentTraceFactoryId == Guid.Empty)
            {
                MessageBox.Show(re.Item2);
                return;
            }
            NewTrace();        
        }
        private void NewTrace()
        {
            BeginWait(WorkingMode.Trace);
            timerTask = new Thread(() =>
            {
                DateTime start = DateTime.Now;
                int process;
                try
                {
                    while (true)
                    {
                        process = ServiceContainer.GetService<IQtDataTraceService>().GetProcess(userId, currentTraceFactoryId);
                        if (process == -2)
                        {
                            throw new Exception("无法获取指定追溯过程，可能未成功建立或已被删除");
                        }
                        if (process == -1)
                        {
                            throw new Exception("发生错误:" + ServiceContainer.GetService<IQtDataTraceService>().GetErrorMessage(userId, currentTraceFactoryId));
                        }
                        if (process == 1000)
                        {
                            var result = ServiceContainer.GetService<IQtDataTraceService>().GetData(userId, this.currentTraceFactoryId);
                            this.data = result;
                            this.Invoke(new Action(() => { traceCallBack(); }));
                            break;
                        }
                        this.Invoke(new Action(() => { this.waitPanel1.Position = process; }));
                        Thread.Sleep(1000);
                    }
                }
                catch (Exception ex)
                {
                    if (ex.GetType() != typeof(ThreadAbortException))
                        MessageBox.Show(ex.Message);
                }
                finally
                {
                    DateTime end = DateTime.Now;
                    Console.WriteLine("Cost:" + (end - start));
                    this.Invoke(new Action(() => { EndWait(); }));
                }
            }) { IsBackground = true };
            timerTask.Start();
        }
        private void traceCallBack()
        {
            this.gridView1.Columns.Clear();
            gridControl1.DataSource = data;
            currentDataId = currentTraceFactoryId;
            AddTraceHis(currentTraceFactoryId, string.Format("{0}_{1}_{2}", DateTime.Now.ToString("hh:mm:ss"), ProcessNo, currentTraceDir ? "后追" : "前追"));
        }
        private void AddTraceHis(Guid id,string text)
        {
            stHisNone.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            var btn = new DevExpress.XtraBars.BarButtonItem(this.barManager1, text);
            btn.Tag = id;
            btn.ItemClick += btnHis_ItemClick;
            btn.Name = this.menuTraceHis.ItemLinks.IndexOf(this.menuTraceHis.AddItem(btn)).ToString();
            
        }

        void btnHis_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                var con = e.Item;
                var result = ServiceContainer.GetService<IQtDataTraceService>().GetData(userId, (Guid)con.Tag);
                if (result == null)
                {
                    RemoveTraceHis(int.Parse(con.Name));
                    this.barManager1.Items.Remove(con);
                    MessageBox.Show("数据已丢失");
                }
                else
                {
                    this.data = result;
                    this.gridView1.Columns.Clear();
                    gridControl1.DataSource = data;
                    this.currentDataId = (Guid)con.Tag;
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void RemoveTraceHis(int Index)
        {
            this.menuTraceHis.LinksPersistInfo.RemoveAt(Index);
            if (this.menuTraceHis.LinksPersistInfo.Count == 1 && this.menuTraceHis.LinksPersistInfo[0].Item.Visibility == DevExpress.XtraBars.BarItemVisibility.Never)
                this.menuTraceHis.LinksPersistInfo[0].Item.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
        }
        private class TraceThreadDataType
        {
            public List<QtDataProcessConfig> processes;
            public List<string> idlist;
            public bool back;
            public TraceThreadDataType(List<QtDataProcessConfig> p, List<string> i, bool n)
            {
                this.processes = p;
                this.idlist = i;
                this.back = n;
            }
        }
        private List<string> getIdList()
        {
            List<string> result = new List<string>();
            var da = (this.materialBindingSource.DataSource as IList<MaterialInfo>);
            for (int i = 0; i < da.Count; i++)
            {
                if (da[i].choose)
                    result.Add(da[i].OutId);
            }
            return result;
        }
        private void gridControl1_ClientSizeChanged(object sender, EventArgs e)
        {
            this.waitPanel1.Location = new Point(gridControl1.Location.X + gridControl1.Width / 2 - this.waitPanel1.Width / 2, gridControl1.Location.Y + gridControl1.Height / 2 - this.waitPanel1.Height / 2);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            timerTask.Abort();
            switch (currentWorkingMode)
            {
                case WorkingMode.Trace:
                    ServiceContainer.GetService<IQtDataTraceService>().Stop(userId, currentTraceFactoryId);
                    currentTraceFactoryId = Guid.Empty;
                    break;
                case WorkingMode.Analyze:
                    ServiceContainer.GetService<IDataAnalyzeService>().Stop(userId, currentAnalyzeFactoryId);
                    currentAnalyzeFactoryId = Guid.Empty;
                    break;
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (data == null)
            {
                MessageBox.Show("请选择数据源");
                return;
            }

            RelationMonitorControl module = new RelationMonitorControl();
            module.DataSource = data;
            (module.DataView as SPC.Base.Control.CanChooseDataGridView).Synchronize(this.gridView1, DevExpress.XtraGrid.Views.Base.SynchronizationMode.Full);
            module.AccessibleDescription = "相关性散点图";
            EAS.Application.Instance.OpenModule(module);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (data == null)
            {
                MessageBox.Show("请选择数据源");
                return;
            }

            SpcModule module = new SpcModule();

            module.DataSource = data;
            (module.DataView as SPC.Base.Control.CanChooseDataGridView).Synchronize(this.gridView1, DevExpress.XtraGrid.Views.Base.SynchronizationMode.Full);
            module.SelectTabPageIndex = 5;
            module.AccessibleDescription = "箱型图";
            EAS.Application.Instance.OpenModule(module);
        }

        private void btnSPCdm_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (data == null)
            {
                MessageBox.Show("请选择数据源");
                return;
            }

            SPCDetermineControl module = new SPCDetermineControl();

            module.DataSource = data;
            (module.DataView as SPC.Base.Control.CanChooseDataGridView).Synchronize(this.gridView1, DevExpress.XtraGrid.Views.Base.SynchronizationMode.Full);
            module.AccessibleDescription = "SPC判定";
            EAS.Application.Instance.OpenModule(module);
        }
        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TraceDataBackUpForm form = new TraceDataBackUpForm(ServiceContainer.GetService<IDataBackUpService>().GetTableList(this.loginId));
            if(form.ShowDialog()== DialogResult.OK)
            {
                if (form.RemoveTables.Count != 0)
                    MessageBox.Show(ServiceContainer.GetService<IDataBackUpService>().RemoveTable(this.loginId, form.RemoveTables.ToArray()));
                else if (form.SaveFilter)
                    MessageBox.Show(ServiceContainer.GetService<IDataBackUpService>().SaveTable(this.loginId, this.userId, this.currentDataId, form.SaveTable.ToUpper(), this.gridView1.GetChoosedRowIndexs()));
                else
                    MessageBox.Show(ServiceContainer.GetService<IDataBackUpService>().SaveTable(this.loginId, this.userId, this.currentDataId, form.SaveTable.ToUpper()));
            }
        }
        private void btnLoad_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TraceDataLoadForm form = new TraceDataLoadForm(ServiceContainer.GetService<IDataBackUpService>().GetTableList(this.loginId));
            if(form.ShowDialog()==DialogResult.OK)
            {
                if(form.TableName!=null)
                {
                    var result = ServiceContainer.GetService<IDataBackUpService>().GetTable(this.loginId, this.userId, form.TableName);
                    if (result.Item1 == Guid.Empty)
                    {
                        MessageBox.Show(result.Item2);
                        return;
                    }
                    currentTraceFactoryId = result.Item1;
                    var resultdata = ServiceContainer.GetService<IQtDataTraceService>().GetData(userId, currentTraceFactoryId);
                    if (resultdata == null)
                    {
                        MessageBox.Show("数据已丢失");
                        return;
                    }
                    this.data = resultdata;
                    this.gridView1.Columns.Clear();
                    gridControl1.DataSource = data;
                    currentDataId = currentTraceFactoryId;
                    AddTraceHis(currentTraceFactoryId, string.Format("{0}_{1}_{2}", DateTime.Now.ToString("hh:mm:ss"),form.TableName,"归档数据"));
                }
            }
        }
        private void DebugOpenModule(Control target)
        {
            if (EAS.Application.Instance != null)
                EAS.Application.Instance.OpenModule(target);
            else
            {
                Form debugform = new Form();
                debugform.Controls.Add(target);
                debugform.Size = target.Size;
                target.Dock = DockStyle.Fill;
                debugform.Show();
            }
        }
        private void NewAnalyze(int time, Action callBack)
        {
            BeginWait(WorkingMode.Analyze);
            timerTask = new Thread(() =>
            {
                DateTime start = DateTime.Now;
                try
                {
                    int process;
                    while (true)
                    {
                        process = ServiceContainer.GetService<IDataAnalyzeService>().GetProcess(userId, currentAnalyzeFactoryId);
                        if (process == -2)
                        {
                            throw new Exception("无法获取指定分析过程，可能未成功建立或已被删除");
                        }
                        if (process == -1)
                        {
                            throw new Exception("发生错误:" + ServiceContainer.GetService<IDataAnalyzeService>().GetErrorMessage(userId, currentAnalyzeFactoryId));
                        }
                        if (process == 1000)
                        {
                            callBack();
                            break;
                        }
                        this.Invoke(new Action(() => { this.waitPanel1.Position = process; }));
                        Thread.Sleep(time);
                    }
                }
                catch (Exception ex)
                {
                    if (ex.GetType() != typeof(ThreadAbortException))
                        MessageBox.Show(ex.Message);
                }
                finally
                {
                    DateTime end = DateTime.Now;
                    Console.WriteLine("Cost:" + (end - start));
                    this.Invoke(new Action(() => { EndWait(); }));
                }
            }) { IsBackground = true };
            timerTask.Start();
        }
        private void BeginWait(WorkingMode mode)
        {
            if (timerTask != null && timerTask.ThreadState == ThreadState.Running)
                timerTask.Abort();
            setGraphEnable(false);
            setTraceEnable(false);
            currentWorkingMode = mode;
            this.waitPanel1.Visible = true;
            this.waitPanel1.Position = 0;
        }
        private void EndWait()
        {
            if (timerTask != null && timerTask.ThreadState == ThreadState.Running)
                timerTask.Abort();
            if (this.gridView1.RowCount > 0)
                setGraphEnable(true);
            else
                menuFile.Enabled = true;
            setTraceEnable(true);
            this.waitPanel1.Visible = false;
            currentWorkingMode = WorkingMode.None;
        }
        private void btnRelationAnalyze_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SPC.Analysis.ConfigControls.CCTConfigControl configcontrol = new SPC.Analysis.ConfigControls.CCTConfigControl();
            AnalyzeConfigForm configForm = new AnalyzeConfigForm();
            configForm.AddConfigControl(configcontrol);
            configcontrol.Init(this.gridView1.GetVisibleColumnNames(false,typeof(string),typeof(DateTime),typeof(bool)));
            if (configForm.ShowDialog() == DialogResult.OK)
            {
                var re = ServiceContainer.GetService<IDataAnalyzeService>().CCTStart(userId, currentDataId, this.gridView1.GetChoosedRowIndexs(), configcontrol.TargetColumn, configcontrol.Columns);
                currentAnalyzeFactoryId = re.Item1;
                if (currentTraceFactoryId == Guid.Empty)
                {
                    MessageBox.Show(re.Item2);
                    return;
                }
                NewAnalyze(1000, new Action(() =>
                {
                    var result = ServiceContainer.GetService<IDataAnalyzeService>().CCTget(userId, currentAnalyzeFactoryId);
                    this.Invoke(new Action(() =>
                    {
                        SPC.Analysis.ResultControls.CCTResultControl resultcontrol = new SPC.Analysis.ResultControls.CCTResultControl();
                        AnalyzeResultControl resultForm = new AnalyzeResultControl();
                        resultForm.AddResultControl(resultcontrol);
                        resultcontrol.Init(configcontrol.Columns, result);
                        resultForm.AccessibleDescription = "相关系数" + DateTime.Now.ToString("hh:mm:ss");
                        DebugOpenModule(resultForm);
                    }));
                }));
            }
        }

        private void btnQuickCluster_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SPC.Analysis.ConfigControls.KMeansConfigControl configcontrol = new SPC.Analysis.ConfigControls.KMeansConfigControl();
            AnalyzeConfigForm configForm = new AnalyzeConfigForm();
            configForm.AddConfigControl(configcontrol);
            var choosed = this.gridView1.GetChoosedRowIndexs();
            configcontrol.Init(this.gridView1.GetVisibleColumnNames(false, typeof(string), typeof(DateTime), typeof(bool)),choosed.Length);
            if (configForm.ShowDialog() == DialogResult.OK)
            {
                var re = ServiceContainer.GetService<IDataAnalyzeService>().KMeansStart(userId, currentDataId, choosed, configcontrol.SelectedColumns, configcontrol.MaxCount, configcontrol.StartClustNum, configcontrol.EndClustNum, configcontrol.Avg, configcontrol.Stdev, configcontrol.InitialMode, configcontrol.MaxThread);
                currentAnalyzeFactoryId = re.Item1;
                if (currentTraceFactoryId == Guid.Empty)
                {
                    MessageBox.Show(re.Item2);
                    return;
                }
                NewAnalyze(1000, new Action(() =>
                {
                    var result = ServiceContainer.GetService<IDataAnalyzeService>().KMeansGet(userId, currentAnalyzeFactoryId);
                    this.Invoke(new Action(() =>
                    {
                        SPC.Analysis.ResultControls.KMeansResultControl resultcontrol = new SPC.Analysis.ResultControls.KMeansResultControl();
                        AnalyzeResultControl resultForm = new AnalyzeResultControl();
                        resultForm.AddResultControl(resultcontrol);
                        resultcontrol.Init(result, new SPC.Base.Interface.ViewChoosedData(this.gridView1, choosed));
                        resultForm.AccessibleDescription = "快速聚类" + DateTime.Now.ToString("hh:mm:ss");
                        DebugOpenModule(resultForm);
                    }));
                }));
            }
        }

        private void btnCPlot_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SPC.Analysis.ConfigControls.ContourPlotConfigControl configcontrol = new SPC.Analysis.ConfigControls.ContourPlotConfigControl();
            AnalyzeConfigForm configForm = new AnalyzeConfigForm();
            configForm.AddConfigControl(configcontrol);
            var choosed = this.gridView1.GetChoosedRowIndexs();
            configcontrol.Init(this.gridView1.GetVisibleColumnNames(false, typeof(string), typeof(DateTime), typeof(bool)));
            if (configForm.ShowDialog() == DialogResult.OK)
            {
                var re = ServiceContainer.GetService<IDataAnalyzeService>().ContourPlotStart(userId, currentDataId, choosed, configcontrol.X, configcontrol.Y, configcontrol.Z, configcontrol.PicWidth, configcontrol.PicHeight, configcontrol.Levels, configcontrol.IsDrawLine);
                currentAnalyzeFactoryId = re.Item1;
                if (currentTraceFactoryId == Guid.Empty)
                {
                    MessageBox.Show(re.Item2);
                    return;
                }
                NewAnalyze(1000, new Action(() =>
                {
                    var result = ServiceContainer.GetService<IDataAnalyzeService>().ContourPlotGet(userId, currentAnalyzeFactoryId);
                    this.Invoke(new Action(() =>
                    {
                        SPC.Analysis.ResultControls.ContourPlotResultControl resultcontrol = new SPC.Analysis.ResultControls.ContourPlotResultControl();
                        AnalyzeResultControl resultForm = new AnalyzeResultControl();
                        resultForm.AddResultControl(resultcontrol);
                        resultcontrol.Init(result, configcontrol.X, configcontrol.Y, configcontrol.Z);
                        resultForm.AccessibleDescription = "等高线图" + DateTime.Now.ToString("hh:mm:ss");
                        DebugOpenModule(resultForm);
                    }));
                }));
            }
        }


    }
}
