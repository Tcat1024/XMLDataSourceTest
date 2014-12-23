namespace QtDataTrace.UI
{
    partial class RelationMonitorControl
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
            this.xyRelationControl1 = new SPC.Monitor.XYRelationControl();
            this.SuspendLayout();
            // 
            // xyRelationControl1
            // 
            this.xyRelationControl1.DataMember = null;
            this.xyRelationControl1.DataSource = null;
            this.xyRelationControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xyRelationControl1.Location = new System.Drawing.Point(0, 0);
            this.xyRelationControl1.Name = "xyRelationControl1";
            this.xyRelationControl1.SelectedTabPageIndex = 0;
            this.xyRelationControl1.Size = new System.Drawing.Size(917, 518);
            this.xyRelationControl1.TabIndex = 0;
            // 
            // RelationMonitorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xyRelationControl1);
            this.Name = "RelationMonitorControl";
            this.Size = new System.Drawing.Size(917, 518);
            this.ResumeLayout(false);

        }

        #endregion

        private SPC.Monitor.XYRelationControl xyRelationControl1;
    }
}
