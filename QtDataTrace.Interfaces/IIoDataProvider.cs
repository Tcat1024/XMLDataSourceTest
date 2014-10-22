using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QtDataTrace.Interfaces
{
    public interface IIoDataProvider
    {
        IoDataMessage GetIoData(SignalRequest[] requests, TimeRange range, ulong resolution);
    }
}
