using System.Collections.Generic;
using NZBDash.Common;

namespace NZBDash.UI.Models.Dashboard
{
    public class DownloaderViewModel
    {
        public Applications Application { get; set; }
        public List<DownloadItem> DownloadItem { get; set; }

        public string DownloadSpeed { get; set; }
    }

    public class DownloadItem
    {
        public string DownloadingName { get; set; }
        public string DownloadPercentage { get; set; }
        public DownloadStatus Status { get; set; }
        public string FontAwesomeIcon { get; set; }
        public int NzbId { get; set; }
        public string ProgressBarClass { get; set; }
    }
}