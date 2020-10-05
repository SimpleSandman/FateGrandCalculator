using System;

using WireMock.Logging;
using WireMock.Server;
using WireMock.Settings;

namespace FateGrandOrderPOC.Test
{
    public class WireMockFixture : IDisposable
    {
        public const string SERVER_URL = "http://localhost:8080/";

        public WireMockServer MockServer { get; private set; }

        public WireMockFixture()
        {
            // bootstrap mockserver
            MockServer = WireMockServer.Start(new WireMockServerSettings
            {
                Urls = new[] { SERVER_URL },
                StartAdminInterface = true,
                Logger = new WireMockConsoleLogger(),
                AllowPartialMapping = true
            });
        }

        public void Dispose()
        {
            MockServer.Stop();
        }
    }
}
