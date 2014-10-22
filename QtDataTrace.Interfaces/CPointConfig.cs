using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace QtDataTrace.Interfaces
{
    [System.SerializableAttribute]
    public class CPointConfig
    {
        public IList<CDevice> Devices = new BindingList<CDevice>();
    }
}
