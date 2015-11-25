using System;

using NZBDash.Common;
using NZBDash.Common.Interfaces;
using NZBDash.ThirdParty.Api.Interfaces;

namespace NZBDash.UI.Helpers
{
    public class EndpointTester
    {

        public EndpointTester(IThirdPartyService service)
        {
            Api = service;
            _logger = new NLogLogger(typeof(EndpointTester));
        }

        private ILogger _logger { get; set; }

        public IThirdPartyService Api { get; set; }

        public bool TestApplicationConnectivity(Applications application, string apiKey, string ipAddress, string password, string userName)
        {
            _logger.Trace(string.Format("Testing application {0}", application));
            switch (application)
            {
                case Applications.SabNZB:
                    return SabNzbConnection(ipAddress, apiKey);
                case Applications.Sickbeard:
                    throw new NotImplementedException();
                case Applications.CouchPotato:
                    return CouchPotatoConnection(ipAddress, apiKey);
                case Applications.Kodi:
                    throw new NotImplementedException();
                case Applications.Sonarr:
                    return SonarrConnection(ipAddress, apiKey);
                case Applications.Plex:
                    return PlexConnection(ipAddress);
                case Applications.NzbGet:
                    return NzbGetConnection(ipAddress, userName, password);
                case Applications.Headphones:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException("application");
            }
        }


        public bool NzbGetConnection(string ip, string username, string password)
        {
            _logger.Trace("Calling GetNZBGetStatus");
            try
            {
                var result = Api.GetNzbGetStatus(ip, username, password);
                _logger.Trace(string.Format("Calling GetNZBGetStatus result = {0}", result.version != null));
                return result.version != null;
            }
            catch (Exception e)
            {
                _logger.Fatal(e);
                return false;
            }

        }

        public bool SabNzbConnection(string ip, string apiKey)
        {
            try
            {
                var result = Api.GetSanNzbQueue(ip, apiKey);

                return result.state != null;
            }
            catch (Exception e)
            {
                _logger.Fatal(e);
                return false;
            }
        }

        public bool CouchPotatoConnection(string ip, string api)
        {
            try
            {
                var result = Api.GetCouchPotatoStatus(ip, api);
                return result.success;
            }
            catch (Exception e)
            {
                _logger.Fatal(e);
                return false;
            }
        }

        public bool SonarrConnection(string ip, string api)
        {
            try
            {
                var result = Api.GetSonarrSystemStatus(ip, api);
                return result.version != null;
            }
            catch (Exception e)
            {
                _logger.Fatal(e);
                return false;
            }
        }

        public bool PlexConnection(string ip)
        {
            try
            {
                var result = Api.GetPlexServers(ip);
                return result.Server.Name != null;
            }
            catch (Exception e)
            {
                _logger.Fatal(e);
                return false;
            }
        }

    }
}