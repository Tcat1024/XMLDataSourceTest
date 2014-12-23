namespace QtDataTrace.UI
{
    partial class SPCDetermineControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.determineControl1 = new SPC.Monitor.DetermineControl();
            this.SuspendLayout();
            // 
            // determineControl1
            // 
            this.determineControl1.DataMember = null;
            this.determineControl1.DataSource = null;
            this.determineControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.determineControl1.Location = new System.Drawing.Point(0, 0);
            this.determineControl1.Name = "determineControl1";
            this.determineControl1.SelectedTabPageIndex = 0;
            this.determineControl1.Size = new System.Drawing.Size(806, 464);
            this.determineControl1.TabIndex = 0;
            // 
            // SPCDetermineControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.determineControl1);
            this.Name = "SPCDetermineControl";
            this.Size = new System.Drawing.Size(806, 464);
            this.ResumeLayout(false);

        }

        #endregion

        private SPC.Monitor.DetermineControl determineControl1;
    }
}
