using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

using Microsoft.AspNet.SignalR;

using NZBDash.Common;
using NZBDash.Common.Interfaces;

namespace NZBDash.UI.Hubs
{
    public class BaseHub : Hub
    {
        public ILogger Logger { get; set; }

        public BaseHub(Type classType)
        {
            Logger = new NLogLogger(classType);
        }

        public override Task OnConnected()
        {
            Logger.Trace(string.Format("Connected {0} client",Context.ConnectionId));
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            Logger.Trace(string.Format("Disconnected {0} client", Context.ConnectionId));
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            Logger.Trace(string.Format("Reconnected {0} client", Context.ConnectionId));
            return base.OnReconnected();
        }
    }
}