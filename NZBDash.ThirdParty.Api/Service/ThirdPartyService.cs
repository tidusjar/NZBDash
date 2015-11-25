using System;

using NZBDash.Api.Models;
using NZBDash.Common.Interfaces;
using NZBDash.Common.Models.Api;
using NZBDash.Core.Model;
using NZBDash.ThirdParty.Api.Interfaces;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace NZBDash.ThirdParty.Api.Service
{
    public class ThirdPartyService : IThirdPartyService
    {
        private ISerializer Serializer { get; set; }

        private const string SabNzbQueueApiAddress = "api?mode=qstatus&output=json&apikey=";
        private const string SabNzbHistoryApiAddress = "api?mode=history&start=0&limit=10&output=json&apikey=";
        private const string SonarrApiAddress = "api/system/status?apikey=";
        private const string NzbGetApiAddress = "/jsonrpc/";

        public ThirdPartyService(ISerializer serializer)
        {
            Serializer = serializer;
        }

        public void GetCouchPotatoMovies(string uri, string api)
        {
            throw new NotImplementedException("TODO");
        }

        public PlexServers GetPlexServers(string uri)
        {
            return Serializer.SerializeXmlData<PlexServers>(uri + "servers");
        }

        public SonarrSystemStatus GetSonarrSystemStatus(string uri, string api)
        {
            return Serializer.SerializedJsonData<SonarrSystemStatus>(uri + SonarrApiAddress + api);
        }

        public List<SonarrSeries> GetSonarrSeries(string uri, string api)
        {
            //var jsonData = string.Empty;

             //jsonData = Resources.Resources.Json;

            //var s = JsonConvert.DeserializeObject<List<SonarrSeries>>(jsonData);
            //return s;

            return Serializer.SerializedJsonData<List<SonarrSeries>>(uri + "api/series?apikey=" + api);

        }

        public CouchPotatoStatus GetCouchPotatoStatus(string uri, string api)
        {
            return Serializer.SerializedJsonData<CouchPotatoStatus>(uri + "api/" + api + "/app.available/");
        }

        public NzbGetHistory GetNzbGetHistory(string url, string username, string password)
        {
            return Serializer.SerializedJsonData<NzbGetHistory>(string.Format("{0}{1}:{2}{3}history", url, username, password, NzbGetApiAddress));
        }

        public NzbGetList GetNzbGetList(string url, string username, string password)
        {
            return Serializer.SerializedJsonData<NzbGetList>(string.Format("{0}{1}:{2}{3}listgroups", url, username, password, NzbGetApiAddress));
        }

        public NzbGetStatus GetNzbGetStatus(string url, string username, string password)
        {
            return Serializer.SerializedJsonData<NzbGetStatus>(string.Format("{0}{1}:{2}{3}status", url, username, password, NzbGetApiAddress));
        }

        public SabNzbHistory GetSabNzbHistory(string url, string apiKey)
        {
            return Serializer.SerializedJsonData<SabNzbHistory>(url + SabNzbHistoryApiAddress + apiKey);
        }

        public SabNzbQueue GetSanNzbQueue(string url, string apiKey)
        {
            return Serializer.SerializedJsonData<SabNzbQueue>(url + SabNzbQueueApiAddress + apiKey);
        }
    }
}