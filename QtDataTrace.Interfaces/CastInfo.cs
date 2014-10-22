using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QtDataTrace.Interfaces
{
    [Serializable]
    public class CastInfo
    {
        private int castNumber;

        [Persistent("CAST_NUMBER")]
        public int CastNumber
        {
            get { return castNumber; }
            set { castNumber = value; }
        }
        private int heatCount;

        [Persistent("HEAT_COUNT")]
        public int HeatCount
        {
            get { return heatCount; }
            set { heatCount = value; }
        }
        private DateTime startTime;

        [Persistent("START_TIME")]
        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }
        private DateTime stopTime;

        [Persistent("STOP_TIME")]
        public DateTime StopTime
        {
            get { return stopTime; }
            set { stopTime = value; }
        }

        private string workshop;

        [Persistent("WORKSHOP")]
        public string Workshop
        {
            get { return workshop; }
            set { workshop = value; }
        }
        private string deviceNo;

        [Persistent("DEVICE_NO")]
        public string DeviceNo
        {
            get { return deviceNo; }
            set { deviceNo = value; }
        }
    }
}
