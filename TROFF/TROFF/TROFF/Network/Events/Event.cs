using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TROFF.Network.Events
{
    abstract class Event
    {
        public byte PacketOpCode { get; private set; }

        protected Event(byte packetOpCode)
        {
            PacketOpCode = packetOpCode;
        }
    }
}
