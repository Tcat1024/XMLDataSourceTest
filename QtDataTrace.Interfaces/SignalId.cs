using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QtDataTrace.Interfaces
{
    [System.Serializable]
    public class SignalId
    {
        string workshop;
        string device;
        string name;
        bool digital;

        public SignalId()
        {
        }

        public SignalId(string name, bool digital)
        {
            this.digital = digital;
            this.name = name;
        }

        public SignalId(string device, string name, bool digital)
        {
            this.digital = digital;
            this.device = device;
            this.name = name;
        }

        public SignalId(string workshop, string device, string name, bool digital)
        {
            this.digital = digital;
            this.workshop = workshop;
            this.device = device;
            this.name = name;
        }

        public string Name
        {
            get { return name; }
        }

        public string IdString
        {
            get { return string.Format("{0}.{1}.{2}", workshop, device, name); }
        }

        public override string ToString() 
        {
            return string.Format("[0]", IdString);
        }

        public string Workshop
        {
            get { return workshop; }
        }

        public string Module
        {
            get { return device; }
        }

        public bool Digital
        {
            get { return digital; }
        }

        public static SignalId Parser(string name, bool digital = false)
        {
            string[] values = name.Split('.');

            if (values.Length >= 3)
            {
                return new SignalId(values[0], values[1], values[2], digital);
            }
            else if (values.Length >= 2)
            {
                return new SignalId(values[0], values[1], digital);
            }
            else
            {
                return new SignalId(values[0], digital);
            }
        }
    }
}
