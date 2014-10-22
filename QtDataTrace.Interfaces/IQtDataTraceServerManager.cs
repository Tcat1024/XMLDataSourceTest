using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QtDataTrace.Interfaces
{
    public interface IQtDataTraceServerManager
    {
        IoConfig GetIoConfig();

        IHistorainDataTrace CreateHistorainDataTrace();
        IIoDataProvider CreateIoDataProvider();

        void Disconnect();
    }
}
