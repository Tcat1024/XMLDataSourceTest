using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XMLDataSourceTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        QtDataTrace.UI.DataAnalysisStartUpAdv myDataAnalysis;
        QtDataTrace.UI.ProcessQtTableConfig myTableConfig;
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (myDataAnalysis == null)
            {
                myDataAnalysis = new QtDataTrace.UI.DataAnalysisStartUpAdv();
                myDataAnalysis.Dock = DockStyle.Fill;
                this.panelControl1.Controls.Add(myDataAnalysis);
            }

        }






    }
}
