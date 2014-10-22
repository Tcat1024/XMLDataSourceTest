using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QtDataTrace.Interfaces
{
    [System.Serializable]
    public class Signal
    {
        private SignalId id;
        private string name;
        private string comment;
        private double minRaw;
        private double maxRaw;
        private double minEU;
        private double maxEU;
        private string unit;

        public SignalId Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        public double MinRaw
        {
            get { return minRaw; }
            set { minRaw = value; }
        }

        public double MaxRaw
        {
            get { return maxRaw; }
            set { maxRaw = value; }
        }

        public double MinEU
        {
            get { return minEU; }
            set { minEU = value; }
        }

        public double MaxEU
        {
            get { return maxEU; }
            set { maxEU = value; }
        }

        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }
    }
}
