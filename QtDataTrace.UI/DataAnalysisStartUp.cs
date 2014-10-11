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
    public partial class DataAnalysisStartUp : UserControl
    {
        DataSet data;
        DataSet configFile;
        DataView configFileView;
        public DataAnalysisStartUp()
        {
            InitializeComponent();
        }

        private void DataAnalysisStartUp_Load(object sender, EventArgs e)
        {
            checkTime.Checked = true;
            dateTimeStart.Value = DateTime.Now.AddDays(-1);

            DataSet data = ServiceContainer.GetService<IBaseTableService>().GetProcessCode();
            this.lookUpEdit1.Properties.DataSource = data.Tables[0];
            lookUpEdit1.Properties.DisplayMember = "PROC_COMMENTS";
            lookUpEdit1.Properties.ValueMember = "PROCESS_NO";

            comboxGrade.DataSource = ServiceContainer.GetService<ISingleQtTableService>().GetSteelGradeList();

            configFile = ServiceContainer.GetService<IBaseTableService>().GetProcessQtTableConfigFile();
            configFileView = configFile.Tables["Table"].DefaultView;
        }

        private void toolStripCpk_Click(object sender, EventArgs e)
        {
            //if (data == null)
            //{
            //    MessageBox.Show("请选择数据源");
            //    return;
            //}

            //CpkModule module = new CpkModule();

            //module.DataSource = data.Tables[0];

            //EAS.Application.Instance.OpenModule(module);
        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            Object obj = lookUpEdit1.GetColumnValue("PROCESS_NO");
            if (obj == null)
            {
                MessageBox.Show("请选择工序");
                return;
            }
            try
            {
                configFileView.RowFilter = "PROCESS_CODE = '" + obj.ToString()+"'";
                InitTreeList();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.triStateTreeView1.EndUpdate();
            }
            //QtDataSourceConfig config = ServiceContainer.GetService<IBaseTableService>().GetQtDataSourceConfig(obj.ToString());

            //foreach (QtDataTableConfig table in config.Tables)
            //{
            //    TreeNode h0 = triStateTreeView1.Nodes.Add(table.ChineseName);
            //    h0.Tag = table;

            //    foreach (QtDataTableColumnConfig column in table.Columns)
            //    {
            //        TreeNode h1 = h0.Nodes.Add(column.ChineseName);
            //        h1.Tag = h1;
            //    }
            //}
        }
        private void InitTreeList()
        {
            this.triStateTreeView1.BeginUpdate();
            this.triStateTreeView1.Nodes.Clear();
            for(int i=0;i<configFileView.Count;i++)
            {
                string e = configFileView[i]["TABLE_NAME"].ToString();
                string c = configFileView[i]["TABLE_CHINESE"].ToString();
                if(c.Trim()=="")
                    c=e;
                this.triStateTreeView1.Nodes.Add(e,c);
                foreach(var column in configFileView[i].Row.GetChildRows("Table_Column"))
                {
                    string ee = column["COLUMN_NAME"].ToString();
                    string cc = column["COLUMN_CHINESE"].ToString();
                    if (cc.Trim() == "")
                        cc = ee;
                    this.triStateTreeView1.Nodes[i].Nodes.Add(ee, cc);
                }
            }
            this.triStateTreeView1.Refresh();
            this.triStateTreeView1.EndUpdate();
        }
        private void btnQuery_Click(object sender, EventArgs e)
        {

        }
    }
}
