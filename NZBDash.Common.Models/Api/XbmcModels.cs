using System.Collections.Generic;

namespace NZBDash.Core.Model
{

    public class SystemLinks
    {
        public string SabNzbApi { get; set; }
        public string SabNZB { get; set; }
        public string SickBeard { get; set; }
        public string Torrent { get; set; }
        public List<XbmcConfigs> Xbmc { get; set; }

    }

    public class XbmcConfigs
    {
        public string IpAddress { get; set; }
        public string Name { get; set; }
    }
    public class StageLog
    {
        public string name { get; set; }
        public List<string> actions { get; set; }
    }

}
