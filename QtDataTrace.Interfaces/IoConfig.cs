using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QtDataTrace.Interfaces
{
    [System.Serializable]
    public class IoConfig
    {
        private List<Workshop> workshops = new List<Workshop>();

        public IoConfig()
        {
        }

        public List<Workshop> Workshops
        {
            get { return workshops; }
        }
    }
}
