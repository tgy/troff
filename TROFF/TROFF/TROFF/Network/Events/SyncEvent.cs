using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using TROFF.Play;

namespace TROFF.Network.Events
{
    class SyncEvent : Event
    {
        public Point Position;
        public Direction Direction;

        public SyncEvent() : base(0)
        {
        }
    }
}
