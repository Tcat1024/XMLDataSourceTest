using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QtDataTrace.Interfaces
{
    [System.Serializable]
    public class SignalRequest
    {
        // Fields
        public SignalId signalId;
        public bool valid;

        public SignalRequest()
        {
        }

        // Methods
        public SignalRequest(SignalId id)
        {
            this.signalId = id;
        }
    }
}
