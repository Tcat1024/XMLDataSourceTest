using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QtDataTrace.Interfaces
{
    [System.Serializable]
    public class Module
    {
        public string name;
        public string comment;

        public List<Signal> Analogs = new List<Signal>();
        public List<Signal> Digitals = new List<Signal>();

        public Module()
        {
        }

        public Module(string name)
        {
            this.name = name;
            this.comment = name;
        }

        public string Name
        {
            get { return name; }
        }

        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }
    }
}
