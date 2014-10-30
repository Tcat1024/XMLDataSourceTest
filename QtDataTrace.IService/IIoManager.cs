using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QtDataTrace.Interfaces;

namespace QtDataTrace.IService
{
    public interface IIoManager
    {
        IoConfig IoConfiguration { get; }
    }
}
