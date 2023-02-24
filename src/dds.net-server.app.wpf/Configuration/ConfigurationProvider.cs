﻿using DDS.Net.Server.Entities;
using DDS.Net.Server.Helpers;
using DDS.Net.Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDS.Net.Server.WpfApp.Configuration
{
    internal static class ConfigurationProvider
    {
        public static Tuple<bool, ServerConfiguration?> GetServerConfiguration(string filename, ILogger logger)
        {
            bool isEnabled = false;

            INIConfigIO _confReader = new INIConfigIO(filename, logger);

            string serverEnabledConfig = _confReader.GetString("DDS Connections/Enabled").ToLower();

            if (serverEnabledConfig.Contains("true") ||
                serverEnabledConfig.Contains("yes"))
            {
                isEnabled = true;

                string tcpEnabledConfig = _confReader.GetString("DDS Connections/TCP-Enabled").ToLower();
                string udpEnabledConfig = _confReader.GetString("DDS Connections/UDP-Enabled").ToLower();

                return new(isEnabled, new ServerConfiguration(

                    listeningIPv4Address: _confReader.GetString("DDS Connections/ListeningIPv4"),

                    enableTCP: tcpEnabledConfig.Contains("true") || tcpEnabledConfig.Contains("yes"),
                    tcpPort: (ushort)_confReader.GetInteger("DDS Connections/TCP-ListeningPort"),
                    tcpMaxClients: _confReader.GetInteger("DDS Connections/TCP-MaxClients"),

                    enableUDP: udpEnabledConfig.Contains("true") || udpEnabledConfig.Contains("yes"),
                    udpPort: (ushort)_confReader.GetInteger("DDS Connections/UDP-ListeningPort"),
                    udpMaxClients: _confReader.GetInteger("DDS Connections/UDP-MaxClients"),

                    logger: logger
                    
                    ));
            }

            return new(isEnabled, null);
        }
    }
}
