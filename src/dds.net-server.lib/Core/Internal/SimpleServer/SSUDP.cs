﻿using DDS.Net.Server.Core.Internal.Interfaces;
using DDS.Net.Server.Core.Internal.SimpleServer.Types;
using DDS.Net.Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDS.Net.Server.Core.Internal.SimpleServer
{
    internal class SSUDP : SSBase
    {
        private volatile bool isClientListenerThreadRunning = false;
        private Thread? clientListenerThread = null;

        public SSUDP(
            ISyncDataInputQueueEnd<SSPacket> dataInputQueue,
            ISyncDataOutputQueueEnd<SSPacket> dataOutputQueue,
            
            string IPv4, ushort port, int maxClients, ILogger logger)

            : base(dataInputQueue, dataOutputQueue,
                   IPv4, port, maxClients, SSType.UDP, logger)
        {
        }

        public override void StartServer()
        {
            lock (this)
            {
                if (clientListenerThread == null &&
                    localSocket != null)
                {
                    isClientListenerThreadRunning = true;
                    clientListenerThread = new Thread(ClientListenerThread);
                    clientListenerThread.IsBackground = true;
                    clientListenerThread.Start();
                }
            }
        }

        public override void StopServer()
        {
            lock (this)
            {
                if (clientListenerThread != null)
                {
                    isClientListenerThreadRunning = false;
                    clientListenerThread.Join();
                }
            }
        }

        private void ClientListenerThread()
        {
            bool bindingOk = false;

            if (localSocket != null)
            {
                try
                {
                    localSocket.Bind(localEndPoint);

                    logger.Info($"UDP socket bound @{localEndPoint}");

                    bindingOk = true;
                }
                catch (Exception ex)
                {
                    logger.Error($"UDP socket binding error @{localEndPoint}: {ex.Message}");
                }
            }

            if (bindingOk && localSocket != null)
            {
                SetServerStatus(SSStatus.Running);

                if (isClientListenerThreadRunning)
                {
                    logger.Info($"SSUDP server running @{localEndPoint}");
                }

                while (isClientListenerThreadRunning)
                {

                }

                logger.Info($"SSUDP server @{localEndPoint} exited");
            }

            isClientListenerThreadRunning = false;
            clientListenerThread = null;

            SetServerStatus(SSStatus.Stopped);
        }
    }
}
