using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QtDataTrace.Interfaces
{
    [Serializable]
    public class HistorainDataTraceHandle
    {
        string handle;

        public HistorainDataTraceHandle()
        {
        }

        public HistorainDataTraceHandle(string handle)
        {
            this.handle = handle;
        }

        public string Handle
        {
            get { return handle; }
            set { handle = value; }
        }
    }
}
