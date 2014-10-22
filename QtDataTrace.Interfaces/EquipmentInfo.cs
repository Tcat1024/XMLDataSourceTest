using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QtDataTrace.Interfaces
{
    [System.SerializableAttribute]
    public class EquipmentInfo
    {
        private string workshop;
        private string name;
        private string matId;
        private string comment;
        private DateTime startTime;
        private DateTime stopTime;
        private List<EquipmentAreaInfo> areas = new List<EquipmentAreaInfo>();

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Workshop
        {
            get { return workshop; }
            set { workshop = value; }
        }

        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        public string MatId
        {
            get { return matId; }
            set { matId = value; }
        }

        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        public DateTime StopTime
        {
            get { return stopTime; }
            set { stopTime = value; }
        }

        public List<EquipmentAreaInfo> Areas
        {
            get { return areas; }
            set { areas = value; }
        }
    }

    [Serializable]
    public class EquipmentAreaInfo
    {
        private string name;
        private string workshop;
        private string area;
        private int displaySeq;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Workshop
        {
            get { return workshop; }
            set { workshop = value; }
        }

        public string Area
        {
            get { return area; }
            set { area = value; }
        }

        public int DisplaySeq
        {
            get { return displaySeq; }
            set { displaySeq = value; }
        }
    }
}
