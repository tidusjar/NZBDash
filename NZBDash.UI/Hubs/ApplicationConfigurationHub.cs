using System;
using NZBDash.Common;
using NZBDash.Common.Helpers;
using NZBDash.Common.Interfaces;
using NZBDash.ThirdParty.Api;
using NZBDash.ThirdParty.Api.Interfaces;
using NZBDash.UI.Helpers;

namespace NZBDash.UI.Hubs
{
    public class ApplicationConfigurationHub : BaseHub
    {
        private IThirdPartyService Service { get; set; }

        public ApplicationConfigurationHub(IThirdPartyService service)
            : base(typeof(ApplicationConfigurationHub))
        {
            Service = service;
        }

        public void TestNzbGetConnection(string ipAddress, int port, string username, string password)
        {
            Logger.Trace(string.Format("Started TestNzbGetConnection with {0}:{1}, {2}", ipAddress, port, username));
            const Applications selectedApp = Applications.NzbGet;

            var tester = new EndpointTester(Service);

            Logger.Trace("Converting IP Address into URI");
            var uri = UrlHelper.ReturnUri(ipAddress, port);
            if (uri == null)
            {
                Logger.Trace("Conversion failed");
                Clients.All.failed(string.Format("Incorrect IP Address"));
                return;
            }
            Logger.Trace("Conversion success");
            try
            {
                Logger.Trace("Testing application connectivity");
                var result = tester.TestApplicationConnectivity(selectedApp, string.Empty, uri.ToString(), password, username);
                if (!result)
                {
                    Logger.Trace("result False");
                    Clients.All.failed(string.Format("Could not connect to {0}", selectedApp));
                    return;
                }
                Logger.Trace("Connected successfully");
                Clients.All.message(string.Format("Connection to {0} is successful", selectedApp));
            }
            catch (Exception e)
            {
                Logger.Warn(string.Format("Test connection to NZBGet failed: {0}", e.Message));
                Clients.All.failed(string.Format("{0}", e.Message));
            }
        }

        public void TestSabNzbConnection(string ipAddress, int port, string apiKey)
        {
            const Applications selectedApp = Applications.SabNZB;

            var tester = new EndpointTester(Service);
            var uri = UrlHelper.ReturnUri(ipAddress, port);
            if (uri == null)
            {
                Clients.All.failed(string.Format("Incorrect IP Address"));
                return;
            }

            try
            {
                var result = tester.TestApplicationConnectivity(selectedApp, apiKey, uri.ToString(), string.Empty, string.Empty);
                if (!result)
                {
                    Clients.All.failed(string.Format("Could not connect to {0}", selectedApp));
                    return;
                }

                Clients.All.message(string.Format("Connection to {0} is successful", selectedApp));
            }
            catch (Exception e)
            {
                Clients.All.failed(string.Format("{0}", e.Message));
            }
        }

        public void TestPlexConnection(string ipAddress, int port, string username, string password)
        {
            const Applications selectedApp = Applications.Plex;

            var tester = new EndpointTester(Service);
            var uri = UrlHelper.ReturnUri(ipAddress, port);
            if (uri == null)
            {
                Clients.All.failed(string.Format("Incorrect IP Address"));
                return;
            }

            try
            {
                var result = tester.TestApplicationConnectivity(selectedApp, string.Empty, uri.ToString(), password, username);
                if (!result)
                {
                    Clients.All.failed(string.Format("Could not connect to {0}", selectedApp));
                    return;
                }

                Clients.All.message(string.Format("Connection to {0} is successful", selectedApp));
            }
            catch (Exception e)
            {
                Clients.All.failed(string.Format("{0}", e.Message));
            }
        }

        public void TestSonarrConnection(string ipAddress, int port, string apiKey)
        {
            const Applications selectedApp = Applications.Sonarr;

            var tester = new EndpointTester(Service);
            var uri = UrlHelper.ReturnUri(ipAddress, port);
            if (uri == null)
            {
                Clients.All.failed(string.Format("Incorrect IP Address"));
                return;
            }

            try
            {
                var result = tester.TestApplicationConnectivity(selectedApp, apiKey, uri.ToString(), string.Empty,string.Empty);
                if (!result)
                {
                    Clients.All.failed(string.Format("Could not connect to {0}", selectedApp));
                    return;
                }

                Clients.All.message(string.Format("Connection to {0} is successful", selectedApp));
            }
            catch (Exception e)
            {
                Clients.All.failed(string.Format("{0}", e.Message));
            }
        }
    }
}