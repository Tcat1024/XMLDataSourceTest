using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MPPO.Protocol.Attribute;
using System.Data;

namespace MPPOAccess
{
    [ExDataAccessClass("MainClass")]
    public class Access
    {
        [ExDataGetMethod("MainMethod")]
        public DataTable GetData()
        {
            Form1 tempform = new Form1();
            if(tempform.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return tempform.dataAnalysisStartUpAdv1.data.Tables[0];
            }
            return null;
        }
    }
}
