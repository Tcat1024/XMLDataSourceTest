using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Reflection;
using Expression;

namespace QtDataTrace.Interfaces
{
    [Serializable]
    public class MaterialTracking
    {
        private string mat_no;
        private string device_no;
        private DateTime start_time;
        private DateTime stop_time;

        public MaterialTracking()
        {
        }

        [Persistent("MAT_NO")]
        public string MaterialId
        {
            get { return mat_no; }
            set { mat_no = value; }
        }

        [Persistent("DEVICE_NO")]
        public string Device
        {
            get { return device_no; }
            set { device_no = value; }
        }

        [Persistent("START_TIME")]
        public DateTime StartTime
        {
            get { return start_time; }
            set { start_time = value; }
        }

        [Persistent("STOP_TIME")]
        public DateTime StopTime
        {
            get { return stop_time; }
            set { stop_time = value; }
        }
    }
}
