using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;

using WireMock.Server;
using WireMock.Settings;

namespace FateGrandOrderPOC.Test.Fixture
{
    public class WireMockFixture : IDisposable
    {
        private static int _port;

        public static string ServerUrl { get; private set; }
        public WireMockServer MockServer { get; private set; }

        public WireMockFixture()
        {
            // Look for a free TCP port
            do 
            {
                _port = new Random().Next(8000, 8999);
            } while (IsPortBusy());

            ServerUrl = $"http://localhost:{_port}";

            MockServer = WireMockServer.Start(new WireMockServerSettings
            {
                Urls = new[] { ServerUrl },
                StartAdminInterface = true,
                //Logger = new WireMockConsoleLogger(),
                AllowPartialMapping = true
            });
        }

        public void Dispose()
        {
            MockServer.Stop();
        }

        /// <summary>
        /// Check if the TCP port the mock server is using is free for the next test
        /// </summary>
        public void CheckIfMockServerInUse()
        {
            // wait until the port is free
            while (IsPortBusy()) 
            {
                Thread.Sleep(1000);
            }

            if (MockServer != null)
            {
                MockServer.Reset(); // clean up for the next test
            }
        }

        private bool IsPortBusy()
        {
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();

            return ipGlobalProperties
                .GetActiveTcpConnections()
                .Any(p => p.LocalEndPoint.Port == _port);
        }
    }
}
