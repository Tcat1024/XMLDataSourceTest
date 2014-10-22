﻿namespace QtDataTrace.UI
{
    partial class DataAnalysisStartUpAdv
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataAnalysisStartUpAdv));
            this.dateTimeStart = new System.Windows.Forms.DateTimePicker();
            this.dateTimeStop = new System.Windows.Forms.DateTimePicker();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.triStateTreeView1 = new QtDataTrace.UI.TriStateTreeView();
            this.standaloneBarDockControl2 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.standaloneBarDockControl1 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            this.btnQuery = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.checkTime = new System.Windows.Forms.CheckBox();
            this.comboxGrade = new System.Windows.Forms.ComboBox();
            this.checkGrade = new System.Windows.Forms.CheckBox();
            this.textMaxWidth = new System.Windows.Forms.TextBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.checkWidth = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.textMinWidth = new System.Windows.Forms.TextBox();
            this.textMinThick = new System.Windows.Forms.TextBox();
            this.textMaxThick = new System.Windows.Forms.TextBox();
            this.checkThick = new System.Windows.Forms.CheckBox();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new SPC.Base.Control.CanChooseDataGridView();
            this.colTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOutId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSteelGrade = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colThickness = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWidth = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBatch = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOrderId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.btnCPK = new DevExpress.XtraBars.BarButtonItem();
            this.btnSampleRun = new DevExpress.XtraBars.BarButtonItem();
            this.btnSampleControl = new DevExpress.XtraBars.BarButtonItem();
            this.btnSamleAvg = new DevExpress.XtraBars.BarButtonItem();
            this.btnNormalCheck = new DevExpress.XtraBars.BarButtonItem();
            this.btnFrequency = new DevExpress.XtraBars.BarButtonItem();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.btnQuickQuery = new DevExpress.XtraBars.BarButtonItem();
            this.subTraceQuery = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateTimeStart
            // 
            this.dateTimeStart.Location = new System.Drawing.Point(69, 39);
            this.dateTimeStart.Name = "dateTimeStart";
            this.dateTimeStart.Size = new System.Drawing.Size(233, 22);
            this.dateTimeStart.TabIndex = 3;
            // 
            // dateTimeStop
            // 
            this.dateTimeStop.Location = new System.Drawing.Point(309, 39);
            this.dateTimeStop.Name = "dateTimeStop";
            this.dateTimeStop.Size = new System.Drawing.Size(233, 22);
            this.dateTimeStop.TabIndex = 4;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.triStateTreeView1);
            this.splitContainer1.Panel1.Controls.Add(this.standaloneBarDockControl2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.splitContainer1.Panel2.Controls.Add(this.gridControl1);
            this.splitContainer1.Panel2.Controls.Add(this.standaloneBarDockControl1);
            this.splitContainer1.Size = new System.Drawing.Size(1457, 459);
            this.splitContainer1.SplitterDistance = 277;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 22;
            // 
            // triStateTreeView1
            // 
            this.triStateTreeView1.CheckBoxes = true;
            this.triStateTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.triStateTreeView1.Location = new System.Drawing.Point(0, 34);
            this.triStateTreeView1.Name = "triStateTreeView1";
            this.triStateTreeView1.Size = new System.Drawing.Size(277, 425);
            this.triStateTreeView1.TabIndex = 0;
            // 
            // standaloneBarDockControl2
            // 
            this.standaloneBarDockControl2.AutoSize = true;
            this.standaloneBarDockControl2.CausesValidation = false;
            this.standaloneBarDockControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl2.Location = new System.Drawing.Point(0, 0);
            this.standaloneBarDockControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.standaloneBarDockControl2.Name = "standaloneBarDockControl2";
            this.standaloneBarDockControl2.Size = new System.Drawing.Size(277, 34);
            this.standaloneBarDockControl2.Text = "standaloneBarDockControl2";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 34);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1175, 425);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // standaloneBarDockControl1
            // 
            this.standaloneBarDockControl1.AutoSize = true;
            this.standaloneBarDockControl1.CausesValidation = false;
            this.standaloneBarDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl1.Location = new System.Drawing.Point(0, 0);
            this.standaloneBarDockControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.standaloneBarDockControl1.Name = "standaloneBarDockControl1";
            this.standaloneBarDockControl1.Size = new System.Drawing.Size(1175, 34);
            this.standaloneBarDockControl1.Text = "standaloneBarDockControl1";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(309, 12);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(54, 18);
            this.checkBox1.TabIndex = 20;
            this.checkBox1.Text = "订单";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(372, 11);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(170, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // lookUpEdit1
            // 
            this.lookUpEdit1.Location = new System.Drawing.Point(69, 10);
            this.lookUpEdit1.Name = "lookUpEdit1";
            this.lookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit1.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PROCESS_NO", "工序代码"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PROC_COMMENTS", "工序名称"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DISPLAY_NUM", "序号", 10, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.lookUpEdit1.Properties.DropDownRows = 15;
            this.lookUpEdit1.Properties.NullText = "请选择工序";
            this.lookUpEdit1.Properties.SortColumnIndex = 2;
            this.lookUpEdit1.Size = new System.Drawing.Size(233, 22);
            this.lookUpEdit1.TabIndex = 0;
            // 
            // btnQuery
            // 
            this.btnQuery.Appearance.Font = new System.Drawing.Font("Tahoma", 13.83019F);
            this.btnQuery.Appearance.Options.UseFont = true;
            this.btnQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnQuery.Image")));
            this.btnQuery.Location = new System.Drawing.Point(1164, 19);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(103, 33);
            this.btnQuery.TabIndex = 21;
            this.btnQuery.Text = "筛选";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 14);
            this.label1.TabIndex = 17;
            this.label1.Text = "工序";
            // 
            // checkTime
            // 
            this.checkTime.AutoSize = true;
            this.checkTime.Location = new System.Drawing.Point(7, 41);
            this.checkTime.Name = "checkTime";
            this.checkTime.Size = new System.Drawing.Size(54, 18);
            this.checkTime.TabIndex = 4;
            this.checkTime.Text = "时间";
            this.checkTime.UseVisualStyleBackColor = true;
            // 
            // comboxGrade
            // 
            this.comboxGrade.FormattingEnabled = true;
            this.comboxGrade.Location = new System.Drawing.Point(619, 11);
            this.comboxGrade.Name = "comboxGrade";
            this.comboxGrade.Size = new System.Drawing.Size(188, 21);
            this.comboxGrade.TabIndex = 2;
            // 
            // checkGrade
            // 
            this.checkGrade.AutoSize = true;
            this.checkGrade.Location = new System.Drawing.Point(556, 12);
            this.checkGrade.Name = "checkGrade";
            this.checkGrade.Size = new System.Drawing.Size(54, 18);
            this.checkGrade.TabIndex = 6;
            this.checkGrade.Text = "钢种";
            this.checkGrade.UseVisualStyleBackColor = true;
            // 
            // textMaxWidth
            // 
            this.textMaxWidth.Location = new System.Drawing.Point(996, 10);
            this.textMaxWidth.Name = "textMaxWidth";
            this.textMaxWidth.Size = new System.Drawing.Size(121, 22);
            this.textMaxWidth.TabIndex = 8;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(884, 40);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(233, 21);
            this.comboBox2.TabIndex = 9;
            // 
            // checkWidth
            // 
            this.checkWidth.AutoSize = true;
            this.checkWidth.Location = new System.Drawing.Point(822, 12);
            this.checkWidth.Name = "checkWidth";
            this.checkWidth.Size = new System.Drawing.Size(54, 18);
            this.checkWidth.TabIndex = 13;
            this.checkWidth.Text = "宽度";
            this.checkWidth.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(821, 41);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(54, 18);
            this.checkBox3.TabIndex = 8;
            this.checkBox3.Text = "班组";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // textMinWidth
            // 
            this.textMinWidth.Location = new System.Drawing.Point(884, 10);
            this.textMinWidth.Name = "textMinWidth";
            this.textMinWidth.Size = new System.Drawing.Size(104, 22);
            this.textMinWidth.TabIndex = 7;
            // 
            // textMinThick
            // 
            this.textMinThick.Location = new System.Drawing.Point(619, 39);
            this.textMinThick.Name = "textMinThick";
            this.textMinThick.Size = new System.Drawing.Size(81, 22);
            this.textMinThick.TabIndex = 5;
            // 
            // textMaxThick
            // 
            this.textMaxThick.Location = new System.Drawing.Point(708, 39);
            this.textMaxThick.Name = "textMaxThick";
            this.textMaxThick.Size = new System.Drawing.Size(100, 22);
            this.textMaxThick.TabIndex = 6;
            // 
            // checkThick
            // 
            this.checkThick.AutoSize = true;
            this.checkThick.Location = new System.Drawing.Point(557, 41);
            this.checkThick.Name = "checkThick";
            this.checkThick.Size = new System.Drawing.Size(54, 18);
            this.checkThick.TabIndex = 10;
            this.checkThick.Text = "厚度";
            this.checkThick.UseVisualStyleBackColor = true;
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.AppearancePage.Header.Options.UseTextOptions = true;
            this.xtraTabControl1.AppearancePage.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 98);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(1463, 491);
            this.xtraTabControl1.TabIndex = 25;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            this.xtraTabControl1.TabPageWidth = 100;
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.gridControl2);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1457, 459);
            this.xtraTabPage1.TabPageWidth = 100;
            this.xtraTabPage1.Text = "数据选择";
            // 
            // gridControl2
            // 
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.Location = new System.Drawing.Point(0, 0);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(1457, 459);
            this.gridControl2.TabIndex = 1;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.ChooseColumnName = "choose";
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTime,
            this.colInId,
            this.colOutId,
            this.colSteelGrade,
            this.colThickness,
            this.colWidth,
            this.colBatch,
            this.colOrderId});
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.OptionsSelection.MultiSelect = true;
            // 
            // colTime
            // 
            this.colTime.Caption = "生产时间";
            this.colTime.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.colTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colTime.FieldName = "Time";
            this.colTime.Name = "colTime";
            this.colTime.Visible = true;
            this.colTime.VisibleIndex = 0;
            // 
            // colInId
            // 
            this.colInId.Caption = "入口物料号";
            this.colInId.FieldName = "InId";
            this.colInId.Name = "colInId";
            this.colInId.Visible = true;
            this.colInId.VisibleIndex = 1;
            // 
            // colOutId
            // 
            this.colOutId.Caption = "出口物料号";
            this.colOutId.FieldName = "OutId";
            this.colOutId.Name = "colOutId";
            this.colOutId.Visible = true;
            this.colOutId.VisibleIndex = 2;
            // 
            // colSteelGrade
            // 
            this.colSteelGrade.Caption = "钢种";
            this.colSteelGrade.FieldName = "SteelGrade";
            this.colSteelGrade.Name = "colSteelGrade";
            this.colSteelGrade.Visible = true;
            this.colSteelGrade.VisibleIndex = 3;
            // 
            // colThickness
            // 
            this.colThickness.Caption = "厚度";
            this.colThickness.DisplayFormat.FormatString = "#0.00";
            this.colThickness.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colThickness.FieldName = "Thickness";
            this.colThickness.Name = "colThickness";
            this.colThickness.Visible = true;
            this.colThickness.VisibleIndex = 4;
            // 
            // colWidth
            // 
            this.colWidth.Caption = "宽度";
            this.colWidth.DisplayFormat.FormatString = "#0";
            this.colWidth.FieldName = "Width";
            this.colWidth.Name = "colWidth";
            this.colWidth.Visible = true;
            this.colWidth.VisibleIndex = 5;
            // 
            // colBatch
            // 
            this.colBatch.Caption = "批号";
            this.colBatch.FieldName = "Batch";
            this.colBatch.Name = "colBatch";
            this.colBatch.Visible = true;
            this.colBatch.VisibleIndex = 6;
            // 
            // colOrderId
            // 
            this.colOrderId.Caption = "订单号";
            this.colOrderId.FieldName = "OrderId";
            this.colOrderId.Name = "colOrderId";
            this.colOrderId.Visible = true;
            this.colOrderId.VisibleIndex = 7;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.splitContainer1);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.PageEnabled = false;
            this.xtraTabPage2.Size = new System.Drawing.Size(1457, 459);
            this.xtraTabPage2.Text = "数据追溯";
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockControls.Add(this.standaloneBarDockControl1);
            this.barManager1.DockControls.Add(this.standaloneBarDockControl2);
            this.barManager1.DockManager = this.dockManager1;
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnCPK,
            this.btnSampleRun,
            this.btnSampleControl,
            this.btnSamleAvg,
            this.btnNormalCheck,
            this.btnFrequency,
            this.btnQuickQuery,
            this.subTraceQuery,
            this.barButtonItem1,
            this.barButtonItem2});
            this.barManager1.MaxItemId = 15;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Standalone;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnCPK, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnSampleRun, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnSampleControl, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnSamleAvg, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnNormalCheck, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnFrequency, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.StandaloneBarDockControl = this.standaloneBarDockControl1;
            this.bar1.Text = "Tools";
            // 
            // btnCPK
            // 
            this.btnCPK.Caption = "CPK分析";
            this.btnCPK.Glyph = ((System.Drawing.Image)(resources.GetObject("btnCPK.Glyph")));
            this.btnCPK.Id = 0;
            this.btnCPK.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnCPK.LargeGlyph")));
            this.btnCPK.Name = "btnCPK";
            this.btnCPK.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCPK_ItemClick);
            // 
            // btnSampleRun
            // 
            this.btnSampleRun.Caption = "样本散点图";
            this.btnSampleRun.Glyph = ((System.Drawing.Image)(resources.GetObject("btnSampleRun.Glyph")));
            this.btnSampleRun.Id = 1;
            this.btnSampleRun.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnSampleRun.LargeGlyph")));
            this.btnSampleRun.Name = "btnSampleRun";
            this.btnSampleRun.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSampleRun_ItemClick);
            // 
            // btnSampleControl
            // 
            this.btnSampleControl.Caption = "样本控制图";
            this.btnSampleControl.Glyph = ((System.Drawing.Image)(resources.GetObject("btnSampleControl.Glyph")));
            this.btnSampleControl.Id = 2;
            this.btnSampleControl.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnSampleControl.LargeGlyph")));
            this.btnSampleControl.Name = "btnSampleControl";
            this.btnSampleControl.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSampleControl_ItemClick);
            // 
            // btnSamleAvg
            // 
            this.btnSamleAvg.Caption = "均值运行图";
            this.btnSamleAvg.Glyph = ((System.Drawing.Image)(resources.GetObject("btnSamleAvg.Glyph")));
            this.btnSamleAvg.Id = 3;
            this.btnSamleAvg.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnSamleAvg.LargeGlyph")));
            this.btnSamleAvg.Name = "btnSamleAvg";
            this.btnSamleAvg.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSamleAvg_ItemClick);
            // 
            // btnNormalCheck
            // 
            this.btnNormalCheck.Caption = "正态校验";
            this.btnNormalCheck.Glyph = ((System.Drawing.Image)(resources.GetObject("btnNormalCheck.Glyph")));
            this.btnNormalCheck.Id = 4;
            this.btnNormalCheck.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnNormalCheck.LargeGlyph")));
            this.btnNormalCheck.Name = "btnNormalCheck";
            this.btnNormalCheck.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNormalCheck_ItemClick);
            // 
            // btnFrequency
            // 
            this.btnFrequency.Caption = "频度分布";
            this.btnFrequency.Glyph = ((System.Drawing.Image)(resources.GetObject("btnFrequency.Glyph")));
            this.btnFrequency.Id = 5;
            this.btnFrequency.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnFrequency.LargeGlyph")));
            this.btnFrequency.Name = "btnFrequency";
            this.btnFrequency.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnFrequency_ItemClick);
            // 
            // bar2
            // 
            this.bar2.BarName = "Custom 4";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Standalone;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnQuickQuery, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.subTraceQuery, true)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.DrawDragBorder = false;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.StandaloneBarDockControl = this.standaloneBarDockControl2;
            this.bar2.Text = "Custom 4";
            // 
            // btnQuickQuery
            // 
            this.btnQuickQuery.Caption = "快速查询";
            this.btnQuickQuery.Glyph = ((System.Drawing.Image)(resources.GetObject("btnQuickQuery.Glyph")));
            this.btnQuickQuery.Id = 7;
            this.btnQuickQuery.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btnQuickQuery.LargeGlyph")));
            this.btnQuickQuery.Name = "btnQuickQuery";
            this.btnQuickQuery.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnQuickQuery_ItemClick);
            // 
            // subTraceQuery
            // 
            this.subTraceQuery.Caption = "追溯查询";
            this.subTraceQuery.Glyph = ((System.Drawing.Image)(resources.GetObject("subTraceQuery.Glyph")));
            this.subTraceQuery.Id = 12;
            this.subTraceQuery.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("subTraceQuery.LargeGlyph")));
            this.subTraceQuery.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2)});
            this.subTraceQuery.Name = "subTraceQuery";
            this.subTraceQuery.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "向前追溯";
            this.barButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.Glyph")));
            this.barButtonItem1.Id = 13;
            this.barButtonItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.LargeGlyph")));
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "向后追溯";
            this.barButtonItem2.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem2.Glyph")));
            this.barButtonItem2.Id = 14;
            this.barButtonItem2.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem2.LargeGlyph")));
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1463, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 589);
            this.barDockControlBottom.Size = new System.Drawing.Size(1463, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 589);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1463, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 589);
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.MenuManager = this.barManager1;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel1});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Top;
            this.dockPanel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dockPanel1.ID = new System.Guid("6daf354d-7fc9-4827-91e8-cc0c87a1f9c0");
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Options.AllowDockAsTabbedDocument = false;
            this.dockPanel1.Options.AllowDockBottom = false;
            this.dockPanel1.Options.AllowDockFill = false;
            this.dockPanel1.Options.AllowDockLeft = false;
            this.dockPanel1.Options.AllowDockRight = false;
            this.dockPanel1.Options.AllowFloating = false;
            this.dockPanel1.Options.FloatOnDblClick = false;
            this.dockPanel1.Options.ShowCloseButton = false;
            this.dockPanel1.OriginalSize = new System.Drawing.Size(200, 98);
            this.dockPanel1.Size = new System.Drawing.Size(1463, 98);
            this.dockPanel1.Text = "条件筛选";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.btnQuery);
            this.dockPanel1_Container.Controls.Add(this.textMinWidth);
            this.dockPanel1_Container.Controls.Add(this.checkBox1);
            this.dockPanel1_Container.Controls.Add(this.checkThick);
            this.dockPanel1_Container.Controls.Add(this.comboBox1);
            this.dockPanel1_Container.Controls.Add(this.textMaxThick);
            this.dockPanel1_Container.Controls.Add(this.lookUpEdit1);
            this.dockPanel1_Container.Controls.Add(this.textMinThick);
            this.dockPanel1_Container.Controls.Add(this.dateTimeStart);
            this.dockPanel1_Container.Controls.Add(this.checkBox3);
            this.dockPanel1_Container.Controls.Add(this.dateTimeStop);
            this.dockPanel1_Container.Controls.Add(this.checkWidth);
            this.dockPanel1_Container.Controls.Add(this.label1);
            this.dockPanel1_Container.Controls.Add(this.comboBox2);
            this.dockPanel1_Container.Controls.Add(this.checkTime);
            this.dockPanel1_Container.Controls.Add(this.textMaxWidth);
            this.dockPanel1_Container.Controls.Add(this.comboxGrade);
            this.dockPanel1_Container.Controls.Add(this.checkGrade);
            this.dockPanel1_Container.Location = new System.Drawing.Point(4, 24);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(1455, 70);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // DataAnalysisStartUpAdv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.dockPanel1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "DataAnalysisStartUpAdv";
            this.Size = new System.Drawing.Size(1463, 589);
            this.Load += new System.EventHandler(this.DataAnalysisStartUp_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            this.dockPanel1_Container.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimeStart;
        private System.Windows.Forms.DateTimePicker dateTimeStop;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkTime;
        private System.Windows.Forms.ComboBox comboxGrade;
        private System.Windows.Forms.CheckBox checkGrade;
        private System.Windows.Forms.TextBox textMaxWidth;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.CheckBox checkWidth;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.TextBox textMinWidth;
        private System.Windows.Forms.TextBox textMinThick;
        private System.Windows.Forms.TextBox textMaxThick;
        private System.Windows.Forms.CheckBox checkThick;
        private TriStateTreeView triStateTreeView1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl1;
        private DevExpress.XtraEditors.SimpleButton btnQuery;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem btnCPK;
        private DevExpress.XtraBars.BarButtonItem btnSampleRun;
        private DevExpress.XtraBars.BarButtonItem btnSampleControl;
        private DevExpress.XtraBars.BarButtonItem btnSamleAvg;
        private DevExpress.XtraBars.BarButtonItem btnNormalCheck;
        private DevExpress.XtraBars.BarButtonItem btnFrequency;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl2;
        private DevExpress.XtraBars.BarButtonItem btnQuickQuery;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private SPC.Base.Control.CanChooseDataGridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn colTime;
        private DevExpress.XtraGrid.Columns.GridColumn colInId;
        private DevExpress.XtraGrid.Columns.GridColumn colOutId;
        private DevExpress.XtraGrid.Columns.GridColumn colSteelGrade;
        private DevExpress.XtraGrid.Columns.GridColumn colThickness;
        private DevExpress.XtraGrid.Columns.GridColumn colWidth;
        private DevExpress.XtraGrid.Columns.GridColumn colBatch;
        private DevExpress.XtraGrid.Columns.GridColumn colOrderId;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarSubItem subTraceQuery;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
    }
}
