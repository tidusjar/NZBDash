using System.Diagnostics.CodeAnalysis;
using System.Net;

using NZBDash.Common.Interfaces;

namespace NZBDash.Common.Helpers
{
    [ExcludeFromCodeCoverage]
    public class CustomWebClient : IWebClient
    {
        private WebClient Client { get; set; }
        public CustomWebClient()
        {
            Client = new WebClient();
        }

        public string DownloadString(string address)
        {
            return Client.DownloadString(address);
        }

        public void Dispose()
        {
            Client.Dispose();
        }
    }
}
