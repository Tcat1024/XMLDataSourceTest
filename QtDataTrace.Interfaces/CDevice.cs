using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace QtDataTrace.Interfaces
{
    [System.SerializableAttribute]
    public class CDevice
    {
        public string Name;
        public string Comment;

        public IList<CAcqLocation> Locations = new BindingList<CAcqLocation>();
    }
}
