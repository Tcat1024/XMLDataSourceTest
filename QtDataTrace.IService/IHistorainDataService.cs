using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Expression;
using QtDataTrace.Interfaces;

namespace QtDataTrace.IService
{
    public interface IHistorainDataService
    {
        IoConfig GetIoConfig();
        IoDataMessage GetIoData(SignalRequest[] requests, TimeRange range, ulong resolution);

        HistorainDataSessionHandle OpenSession(string loginId);
        void CloseSession(HistorainDataSessionHandle handle);

        HistorainDataTraceHandle CreateHistorainDataTrace(HistorainDataSessionHandle session, string workshop, string matId);
        HistorainDataTraceHandle CreateHistorainDataTrace(HistorainDataSessionHandle session, string workshop, string device, string matId);

        IList<CDevice> GetPointConfig(HistorainDataTraceHandle handle);

        IChannelData GetChannel(HistorainDataTraceHandle handle, string id);
    }
}
