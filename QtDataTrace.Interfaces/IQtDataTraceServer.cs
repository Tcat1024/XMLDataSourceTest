using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QtDataTrace.Interfaces
{
    public interface IQtDataTraceServer
    {
        IQtDataTraceServerManager Connect(IKeepAlive keepalive, string client, string user, string password);
    }
}
