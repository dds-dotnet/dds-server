﻿using DDS.Net.Server.Core.Internal;
using DDS.Net.Server.Entities;
using DDS.Net.Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDS.Net.Server
{
    public enum ServerStatus
    {
        Stopped,
        Starting,
        Started,
        Stopping
    }

    public partial class DdsServer
    {
        private readonly ServerConfiguration _config;
        private readonly ILogger _logger;

        private ServerStatus _status = ServerStatus.Stopped;
        private BaseServer? _tcpServer;
        private BaseServer? _udpServer;

        public DdsServer(ServerConfiguration config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));

            _config = config;
            _tcpServer = null;
            _udpServer = null;

            if (_config.Logger != null)
                _logger = _config.Logger;
            else
                throw new Exception($"No instance of {nameof(ILogger)} is provided");
        }

        public void Start()
        {
            PrintLogStarting();

            if (_tcpServer == null && _config.EnableTCP)
            {
                try
                {
                    _tcpServer = new TcpServer(
                        _config.ListeningAddressIPv4,
                        _config.ListeningPortTCP,
                        _config.MaxClientsTCP,
                        _logger);

                    _tcpServer.StartServer();
                }
                catch (Exception ex)
                {
                    _tcpServer = null;
                    _logger.Error($"Cannot start TCP Server: {ex.Message}");
                }
            }
        }

        public void Stop()
        {
            PrintLogStopping();
        }
    }
}
