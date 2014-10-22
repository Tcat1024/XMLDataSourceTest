using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QtDataTrace.Interfaces
{
    [System.Serializable]
    public class Workshop
    {
        private string name;
        private string comment;
        private List<Module> modules = new List<Module>();

        public Workshop()
        {
        }

        public Workshop(string name)
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

        public List<Module> Modules
        {
            get { return modules; }
        }
    }
}
