using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace QtDataTrace.Interfaces
{
    [System.SerializableAttribute]

    public class HotCoilInfoVamaCal
    {
        private string Grade;

        [DisplayName("钢种")]
        public string sGrade
        {
            get { return Grade; }
            set { Grade = value; }
        }        

        private string total_sum;

        [DisplayName("总数")]
        public string Total_sum
        {
            get { return total_sum; }
            set { total_sum = value; }
        }

        private string failed_sum;

        [DisplayName("不合格数")]
        public string Failed_sum
        {
            get { return failed_sum; }
            set { failed_sum = value; }
        }

        private string failed_pcnt;

        [DisplayName("不合格率")]
        public string Failed_pcnt
        {
            get { return failed_pcnt; }
            set { failed_pcnt = value; }
        }
    }
}
