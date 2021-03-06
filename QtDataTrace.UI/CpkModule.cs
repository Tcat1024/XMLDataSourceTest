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

namespace QtDataTrace.UI
{
    [Module("521F1A32-A771-431A-A1FC-2BA726F6CF6A", "CPK分析", "CPK分析模块")]
    public partial class CpkModule : UserControl
    {
        public CpkModule()
        {
            InitializeComponent();
        }

        public Object DataSource
        {
            set { cpKtoolControl.DataSource = value; }
        }
        public Object DataView
        {
            get
            {
                return cpKtoolControl.DataView;
            }
        }
        private void CpkModule_Load(object sender, EventArgs e)
        {
        }
    }
}
