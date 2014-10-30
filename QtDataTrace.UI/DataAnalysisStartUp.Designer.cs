namespace QtDataTrace.UI
{
    partial class DataAnalysisStartUp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataAnalysisStartUp));
            this.dateTimeStart = new System.Windows.Forms.DateTimePicker();
            this.dateTimeStop = new System.Windows.Forms.DateTimePicker();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.triStateTreeView1 = new QtDataTrace.UI.TriStateTreeView();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripCpk = new System.Windows.Forms.ToolStripButton();
            this.toolStripSpc = new System.Windows.Forms.ToolStripButton();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnQuery = new System.Windows.Forms.Button();
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
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateTimeStart
            // 
            this.dateTimeStart.Location = new System.Drawing.Point(64, 36);
            this.dateTimeStart.Name = "dateTimeStart";
            this.dateTimeStart.Size = new System.Drawing.Size(200, 21);
            this.dateTimeStart.TabIndex = 3;
            // 
            // dateTimeStop
            // 
            this.dateTimeStop.Location = new System.Drawing.Point(270, 35);
            this.dateTimeStop.Name = "dateTimeStop";
            this.dateTimeStop.Size = new System.Drawing.Size(200, 21);
            this.dateTimeStop.TabIndex = 4;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 77);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.triStateTreeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.splitContainer1.Panel2.Controls.Add(this.gridControl1);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(1100, 391);
            this.splitContainer1.SplitterDistance = 227;
            this.splitContainer1.TabIndex = 22;
            // 
            // triStateTreeView1
            // 
            this.triStateTreeView1.CheckBoxes = true;
            this.triStateTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.triStateTreeView1.Location = new System.Drawing.Point(0, 0);
            this.triStateTreeView1.Name = "triStateTreeView1";
            this.triStateTreeView1.Size = new System.Drawing.Size(227, 391);
            this.triStateTreeView1.TabIndex = 0;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 25);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(869, 366);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripCpk,
            this.toolStripSpc});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(869, 25);
            this.toolStrip1.TabIndex = 24;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripCpk
            // 
            this.toolStripCpk.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripCpk.Image = ((System.Drawing.Image)(resources.GetObject("toolStripCpk.Image")));
            this.toolStripCpk.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripCpk.Name = "toolStripCpk";
            this.toolStripCpk.Size = new System.Drawing.Size(35, 22);
            this.toolStripCpk.Text = "Cpk";
            this.toolStripCpk.Click += new System.EventHandler(this.toolStripCpk_Click);
            // 
            // toolStripSpc
            // 
            this.toolStripSpc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripSpc.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSpc.Image")));
            this.toolStripSpc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSpc.Name = "toolStripSpc";
            this.toolStripSpc.Size = new System.Drawing.Size(33, 22);
            this.toolStripSpc.Text = "Spc";
            this.toolStripSpc.Click += new System.EventHandler(this.toolStripSpc_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(270, 13);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(48, 16);
            this.checkBox1.TabIndex = 20;
            this.checkBox1.Text = "订单";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(324, 9);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(146, 20);
            this.comboBox1.TabIndex = 1;
            // 
            // lookUpEdit1
            // 
            this.lookUpEdit1.Location = new System.Drawing.Point(64, 9);
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
            this.lookUpEdit1.Size = new System.Drawing.Size(200, 20);
            this.lookUpEdit1.TabIndex = 0;
            this.lookUpEdit1.EditValueChanged += new System.EventHandler(this.lookUpEdit1_EditValueChanged);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnQuery);
            this.panelControl1.Controls.Add(this.checkBox1);
            this.panelControl1.Controls.Add(this.comboBox1);
            this.panelControl1.Controls.Add(this.lookUpEdit1);
            this.panelControl1.Controls.Add(this.dateTimeStart);
            this.panelControl1.Controls.Add(this.dateTimeStop);
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Controls.Add(this.checkTime);
            this.panelControl1.Controls.Add(this.comboxGrade);
            this.panelControl1.Controls.Add(this.checkGrade);
            this.panelControl1.Controls.Add(this.textMaxWidth);
            this.panelControl1.Controls.Add(this.comboBox2);
            this.panelControl1.Controls.Add(this.checkWidth);
            this.panelControl1.Controls.Add(this.checkBox3);
            this.panelControl1.Controls.Add(this.textMinWidth);
            this.panelControl1.Controls.Add(this.textMinThick);
            this.panelControl1.Controls.Add(this.textMaxThick);
            this.panelControl1.Controls.Add(this.checkThick);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1100, 77);
            this.panelControl1.TabIndex = 21;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(969, 35);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(126, 26);
            this.btnQuery.TabIndex = 10;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 17;
            this.label1.Text = "工序";
            // 
            // checkTime
            // 
            this.checkTime.AutoSize = true;
            this.checkTime.Location = new System.Drawing.Point(11, 38);
            this.checkTime.Name = "checkTime";
            this.checkTime.Size = new System.Drawing.Size(48, 16);
            this.checkTime.TabIndex = 4;
            this.checkTime.Text = "时间";
            this.checkTime.UseVisualStyleBackColor = true;
            // 
            // comboxGrade
            // 
            this.comboxGrade.FormattingEnabled = true;
            this.comboxGrade.Location = new System.Drawing.Point(536, 7);
            this.comboxGrade.Name = "comboxGrade";
            this.comboxGrade.Size = new System.Drawing.Size(162, 20);
            this.comboxGrade.TabIndex = 2;
            // 
            // checkGrade
            // 
            this.checkGrade.AutoSize = true;
            this.checkGrade.Location = new System.Drawing.Point(482, 7);
            this.checkGrade.Name = "checkGrade";
            this.checkGrade.Size = new System.Drawing.Size(48, 16);
            this.checkGrade.TabIndex = 6;
            this.checkGrade.Text = "钢种";
            this.checkGrade.UseVisualStyleBackColor = true;
            // 
            // textMaxWidth
            // 
            this.textMaxWidth.Location = new System.Drawing.Point(859, 8);
            this.textMaxWidth.Name = "textMaxWidth";
            this.textMaxWidth.Size = new System.Drawing.Size(104, 21);
            this.textMaxWidth.TabIndex = 8;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(763, 38);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(200, 20);
            this.comboBox2.TabIndex = 9;
            // 
            // checkWidth
            // 
            this.checkWidth.AutoSize = true;
            this.checkWidth.Location = new System.Drawing.Point(710, 11);
            this.checkWidth.Name = "checkWidth";
            this.checkWidth.Size = new System.Drawing.Size(48, 16);
            this.checkWidth.TabIndex = 13;
            this.checkWidth.Text = "宽度";
            this.checkWidth.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(709, 38);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(48, 16);
            this.checkBox3.TabIndex = 8;
            this.checkBox3.Text = "班组";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // textMinWidth
            // 
            this.textMinWidth.Location = new System.Drawing.Point(763, 9);
            this.textMinWidth.Name = "textMinWidth";
            this.textMinWidth.Size = new System.Drawing.Size(90, 21);
            this.textMinWidth.TabIndex = 7;
            // 
            // textMinThick
            // 
            this.textMinThick.Location = new System.Drawing.Point(536, 38);
            this.textMinThick.Name = "textMinThick";
            this.textMinThick.Size = new System.Drawing.Size(70, 21);
            this.textMinThick.TabIndex = 5;
            // 
            // textMaxThick
            // 
            this.textMaxThick.Location = new System.Drawing.Point(612, 37);
            this.textMaxThick.Name = "textMaxThick";
            this.textMaxThick.Size = new System.Drawing.Size(86, 21);
            this.textMaxThick.TabIndex = 6;
            // 
            // checkThick
            // 
            this.checkThick.AutoSize = true;
            this.checkThick.Location = new System.Drawing.Point(483, 40);
            this.checkThick.Name = "checkThick";
            this.checkThick.Size = new System.Drawing.Size(48, 16);
            this.checkThick.TabIndex = 10;
            this.checkThick.Text = "厚度";
            this.checkThick.UseVisualStyleBackColor = true;
            // 
            // DataAnalysisStartUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panelControl1);
            this.Name = "DataAnalysisStartUp";
            this.Size = new System.Drawing.Size(1100, 468);
            this.Load += new System.EventHandler(this.DataAnalysisStartUp_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimeStart;
        private System.Windows.Forms.DateTimePicker dateTimeStop;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
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
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripCpk;
        private System.Windows.Forms.ToolStripButton toolStripSpc;
        private TriStateTreeView triStateTreeView1;
        private System.Windows.Forms.Button btnQuery;
    }
}
