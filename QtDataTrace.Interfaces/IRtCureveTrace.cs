using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Expression;

namespace QtDataTrace.Interfaces
{
    public interface IRtCurveTrace
    {
        void Initialize(string matId);
        void Initialize(string workshop, string matId);

        CPointConfig GetPointConfig();

        IChannelData GetChannel(string point);
    }
}
