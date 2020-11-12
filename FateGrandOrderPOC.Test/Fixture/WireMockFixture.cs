using System;
using System.Linq;
using System.Net.NetworkInformation;

using WireMock.Server;
using WireMock.Settings;

namespace FateGrandOrderPOC.Test.Fixture
{
    public class WireMockFixture : IDisposable
    {
        private static readonly int _port = 8080;
        public static readonly string SERVER_URL = $"http://localhost:{_port}";

        public WireMockServer MockServer { get; private set; }

        public WireMockFixture()
        {
            CheckIfMockServerInUse();

            // bootstrap mockserver
            MockServer = WireMockServer.Start(new WireMockServerSettings
            {
                Urls = new[] { SERVER_URL },
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
            while (IsPortBusy()) { }

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
