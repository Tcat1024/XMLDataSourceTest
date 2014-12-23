namespace QtDataTrace.UI
{
    partial class CpkModule
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
            this.cpKtoolControl = new SPC.CPKtool.CPKtoolControl();
            this.SuspendLayout();
            // 
            // cpKtoolControl
            // 
            this.cpKtoolControl.DataMember = null;
            this.cpKtoolControl.DataSource = null;
            this.cpKtoolControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cpKtoolControl.Location = new System.Drawing.Point(0, 0);
            this.cpKtoolControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cpKtoolControl.Name = "cpKtoolControl";
            this.cpKtoolControl.Size = new System.Drawing.Size(1176, 465);
            this.cpKtoolControl.TabIndex = 0;
            // 
            // CpkModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cpKtoolControl);
            this.Name = "CpkModule";
            this.Size = new System.Drawing.Size(1176, 465);
            this.Load += new System.EventHandler(this.CpkModule_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public SPC.CPKtool.CPKtoolControl cpKtoolControl;

    }
}
