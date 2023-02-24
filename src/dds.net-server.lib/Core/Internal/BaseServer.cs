﻿using DDS.Net.Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDS.Net.Server.Core.Internal
{
    internal abstract class BaseServer
    {
        protected readonly string _IPv4;
        protected readonly ushort _port;
        protected readonly int _maxClients;

        protected readonly ILogger _logger;

        protected BaseServer(string IPv4, ushort port, int maxClients, ILogger logger)
        {
            _IPv4 = IPv4;
            _port = port;
            _maxClients = maxClients;

            _logger = logger;
        }

        public abstract void StartServer();
        public abstract void StopServer();
    }
}
