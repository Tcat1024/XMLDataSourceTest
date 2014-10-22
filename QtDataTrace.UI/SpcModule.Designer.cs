namespace QtDataTrace.UI
{
    partial class SpcModule
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
            this.spcMonitorControl = new SPC.Monitor.MonitorControl();
            this.SuspendLayout();
            // 
            // spcMonitorControl
            // 
            this.spcMonitorControl.DataMember = null;
            this.spcMonitorControl.DataSource = null;
            this.spcMonitorControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcMonitorControl.Location = new System.Drawing.Point(0, 0);
            this.spcMonitorControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.spcMonitorControl.Name = "spcMonitorControl";
            this.spcMonitorControl.SelectedTabPageIndex = 0;
            this.spcMonitorControl.Size = new System.Drawing.Size(1176, 465);
            this.spcMonitorControl.TabIndex = 0;
            // 
            // SpcModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.spcMonitorControl);
            this.Name = "SpcModule";
            this.Size = new System.Drawing.Size(1176, 465);
            this.Load += new System.EventHandler(this.SpcModule_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public SPC.Monitor.MonitorControl spcMonitorControl;

    }
}

