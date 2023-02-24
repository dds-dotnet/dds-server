﻿using DDS.Net.Server.Entities;
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

        public DdsServer(ServerConfiguration config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));

            _config = config;

            if (_config.Logger != null)
                _logger = _config.Logger;
            else
                throw new Exception($"No instance of {nameof(ILogger)} is provided");
        }

        public void Start()
        {
            PrintLogStarting();
        }

        public void Stop()
        {
            PrintLogStopping();
        }
    }
}
