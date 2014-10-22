using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Expression;

namespace QtDataTrace.Interfaces
{
    public interface IHistorainDataTrace
    {
        void Prepare(string workshop, string matId);
        void Prepare(string workshop, string device, string matId);

        IList<CDevice> GetPointConfig();

        IChannelData GetChannel(string point);
    }
}
