using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QtDataTrace.Interfaces;
using Expression;

namespace QtDataTrace.IService
{
    public interface IHistorainDataTrace
    {
        void Prepare(string workshop, string matId);
        void Prepare(string workshop, string device, string matId);

        IList<CDevice> GetPointConfig();

        IChannelData GetChannel(string point);
    }
}
