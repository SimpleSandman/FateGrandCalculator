using System;
using System.Linq;
using System.Net.NetworkInformation;

using WireMock.Server;
using WireMock.Settings;

namespace FateGrandOrderPOC.Test.Fixture
{
    public class WireMockFixture : IDisposable
    {
        public const string SERVER_URL = "http://localhost:8080";
        private bool _isMockServerInUse = false;

        public WireMockServer MockServer { get; private set; }

        public WireMockFixture()
        {
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
            while (_isMockServerInUse)
            {
                IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();

                _isMockServerInUse = ipGlobalProperties
                    .GetActiveTcpConnections()
                    .Any(p => p.LocalEndPoint.Port == MockServer.Ports[0]);
            }

            MockServer.Reset(); // clean up for the next test
        }
    }
}
