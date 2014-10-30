using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QtDataTrace.Interfaces;

namespace QtDataTrace.IService
{
    public interface IQtDataTraceServerManager
    {
        IoConfig GetIoConfig();

        IHistorainDataTrace CreateHistorainDataTrace();
        IIoDataProvider CreateIoDataProvider();

        void Disconnect();
    }
}
