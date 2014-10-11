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
        QtDataTrace.UI.DataAnalysisStartUp myDataAnalysis;
        QtDataTrace.UI.ProcessQtTableConfig myTableConfig;
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (myDataAnalysis == null)
            {
                myDataAnalysis = new QtDataTrace.UI.DataAnalysisStartUp();
                myDataAnalysis.Dock = DockStyle.Fill;
                this.xtraTabPage1.Controls.Add(myDataAnalysis);
            }
            if(myTableConfig==null)
            {
                myTableConfig = new QtDataTrace.UI.ProcessQtTableConfig();
                myTableConfig.Dock = DockStyle.Fill;
                this.xtraTabPage2.Controls.Add(myTableConfig);
            }
        }






    }
}
