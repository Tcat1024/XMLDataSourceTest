using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QtDataTrace.Interfaces;

namespace QtDataTrace.IService
{
    public interface IIoDataProvider
    {
        IoDataMessage GetIoData(SignalRequest[] requests, TimeRange range, ulong resolution);
    }
}
