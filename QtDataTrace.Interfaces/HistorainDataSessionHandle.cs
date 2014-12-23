using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QtDataTrace.Interfaces
{
    [Serializable]
    public class HistorainDataSessionHandle
    {
        string handle;

        public HistorainDataSessionHandle()
        {
        }

        public HistorainDataSessionHandle(string handle)
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
