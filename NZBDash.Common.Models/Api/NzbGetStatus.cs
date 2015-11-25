using System.Collections.Generic;

using Newtonsoft.Json;

namespace NZBDash.Api.Models
{

    public class NewsServer
    {
        public int ID { get; set; }
        public bool Active { get; set; }
    }

    public class NzbGetStatusResult
    {
        public long RemainingSizeLo { get; set; }
        public long RemainingSizeHi { get; set; }
        public long RemainingSizeMB { get; set; }
        public long ForcedSizeLo { get; set; }
        public long ForcedSizeHi { get; set; }
        public long ForcedSizeMB { get; set; }
        public long DownloadedSizeLo { get; set; }
        public long DownloadedSizeHi { get; set; }
        public long DownloadedSizeMB { get; set; }
        public long ArticleCacheLo { get; set; }
        public long ArticleCacheHi { get; set; }
        public long ArticleCacheMB { get; set; }
        public long DownloadRate { get; set; }
        public int AverageDownloadRate { get; set; }
        public int DownloadLimit { get; set; }
        public int ThreadCount { get; set; }
        public int ParJobCount { get; set; }
        public int PostJobCount { get; set; }
        public int UrlCount { get; set; }
        public int UpTimeSec { get; set; }
        public int DownloadTimeSec { get; set; }
        public bool ServerPaused { get; set; }
        public bool DownloadPaused { get; set; }
        public bool Download2Paused { get; set; }
        public bool ServerStandBy { get; set; }
        public bool PostPaused { get; set; }
        public bool ScanPaused { get; set; }
        public long FreeDiskSpaceLo { get; set; }
        public long FreeDiskSpaceHi { get; set; }
        public long FreeDiskSpaceMB { get; set; }
        public int ServerTime { get; set; }
        public int ResumeTime { get; set; }
        public bool FeedActive { get; set; }
        public int QueueScriptCount { get; set; }
        public List<NewsServer> NewsServers { get; set; }
    }

    public class NzbGetStatus
    {
        public string version { get; set; }
        [JsonProperty(PropertyName = "result")]
        public NzbGetStatusResult Result { get; set; }
    }
}