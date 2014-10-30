using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QtDataTrace.Interfaces
{
    public interface IHistorainDataService
    {
        IoConfig GetIoConfig();

        IoDataMessage GetIoData(SignalRequest[] requests, TimeRange range, ulong resolution);
    }
}
