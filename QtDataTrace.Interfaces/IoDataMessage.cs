using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QtDataTrace.Interfaces
{
    [System.Serializable]
    public class IoDataMessage
    {
        public Array[] data;
        public int requestId;
        public TimeRange timeRange;
    }
}
