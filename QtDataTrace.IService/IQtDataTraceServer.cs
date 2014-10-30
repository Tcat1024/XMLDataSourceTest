using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QtDataTrace.Interfaces;

namespace QtDataTrace.IService
{
    public interface IQtDataTraceServer
    {
        IQtDataTraceServerManager Connect(IKeepAlive keepalive, string client, string user, string password);
    }
}
