using System.Collections.Generic;
using System.Linq;

using NZBDash.Common;
using NZBDash.UI.Controllers;

namespace NZBDash.UI.Hubs
{
    public class LogsHub : BaseHub
    {
        public static List<string> LogLines = new List<string>();
        public LogController Controller { get; set; }

        public LogsHub()
            : base(typeof(LogController))
        {
            Controller = new LogController(new NLogLogger(typeof(LogsHub)));
        }

        public void GetLogs()
        {
            var logFiles = Controller.LogFiles();
            var currentLog = Controller.GetLog(logFiles.OrderByDescending(x => x).FirstOrDefault());
            var newLine = currentLog.Except(LogLines).ToList();
            LogLines.AddRange(newLine);
            Clients.All.addLog(LogLines);
        }

        //TODO: Remove logs if they have been removed (not sure why this would happen, but we should anyway).
        public void UpdateLogs()
        {
            var logFiles = Controller.LogFiles();
            var currentLog = Controller.GetLog(logFiles.OrderByDescending(x => x).FirstOrDefault());

            var newLine = currentLog.Except(LogLines).ToList();
            LogLines.AddRange(newLine);

            Clients.All.addLog(newLine);
        }
    }
}