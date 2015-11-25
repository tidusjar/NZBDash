using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Web.Http;
using System.Xml.Serialization;

using Newtonsoft.Json;

using NZBDash.Api.Models;
using NZBDash.Common.Models.Api;
using NZBDash.Core.Model;

namespace NZBDash.Api.Controllers
{
    [RoutePrefix("api/{controller}/{action}")]
    public class StatusApiController : ApiController
    {
        [HttpGet]
        [ActionName("NetworkInfo")]
        public NetworkInfo GetNetworkInfo()
        {
            return GetNetworkingDetails();
        }


        private NetworkInfo GetNetworkingDetails()
        {
            NetworkInfo info = new NetworkInfo();
            PerformanceCounterCategory performanceCounterCategory = new PerformanceCounterCategory("Network Interface");
            string cn = performanceCounterCategory.GetInstanceNames()[0];
            var networkBytesSent = new PerformanceCounter("Network Interface", "Bytes Sent/sec", cn);
            var networkBytesReceived = new PerformanceCounter("Network Interface", "Bytes Received/sec", cn);
            var networkBytesTotal = new PerformanceCounter("Network Interface", "Bytes Total/sec", cn);

            info.Sent = networkBytesSent.NextValue();
            info.Recieved = networkBytesReceived.NextValue();
            info.Total = networkBytesTotal.NextValue();

            // First counter is empty
            Thread.Sleep(1000);
            info.Sent = networkBytesSent.NextValue();
            info.Recieved = networkBytesReceived.NextValue();
            info.Total = networkBytesTotal.NextValue();
            return info;
        }
    }
}
