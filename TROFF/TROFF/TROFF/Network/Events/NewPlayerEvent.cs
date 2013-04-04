using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TROFF.Network.Events
{
    class NewPlayerEvent : Event
    {
        public string Name;

        public NewPlayerEvent() : base(1)
        {
        }
    }
}
