using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Reflection;
using Expression;
using log4net;

namespace QtDataTrace.Access
{
    public class MaterialTrace
    {
        private string mat_no;
        private string device_no;
        private DateTime start_time;
        private DateTime stop_time;

        public string MaterialId
        {
            get { return mat_no; }
            set { mat_no = value; }
        }

        public string Device
        {
            get { return device_no; }
            set { device_no = value; }
        }

        public DateTime StartTime
        {
            get { return start_time; }
            set { start_time = value; }
        }

        public DateTime StopTime
        {
            get { return stop_time; }
            set { stop_time = value; }
        }
    }
}
