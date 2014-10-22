using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QtDataTrace.Interfaces
{
    public interface IIoDataEventHandler
    {
        void OnNewIoData(string serverName, IoDataMessage newData);
    }

    public delegate void NewIoDataEventHandler(string servername, IoDataMessage newData);
}
