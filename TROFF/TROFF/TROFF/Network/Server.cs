using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TROFF.Network
{
    class Server
    {
        private readonly TcpListener _listener;

        public Server()
        {
            _listener = new TcpListener(IPAddress.Any, 4224);
        }

        public Client AcceptClient()
        {
            _listener.Start();
            //Client c = new Client(_listener.AcceptTcpClient());
            _listener.Stop();
        }
    }
}
