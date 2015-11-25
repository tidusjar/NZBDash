using System.Collections.Generic;

namespace NZBDash.Api.Models
{
    public class NzbGetParameter
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class NzbGetServerStat
    {
        public int ServerID { get; set; }
        public int SuccessArticles { get; set; }
        public int FailedArticles { get; set; }
    }

    public class NzbGetListResult
    {
        public int FirstID { get; set; }
        public int LastID { get; set; }
        public long RemainingSizeLo { get; set; }
        public long RemainingSizeHi { get; set; }
        public long RemainingSizeMB { get; set; }
        public long PausedSizeLo { get; set; }
        public long PausedSizeHi { get; set; }
        public long PausedSizeMB { get; set; }
        public int RemainingFileCount { get; set; }
        public int RemainingParCount { get; set; }
        public int MinPriority { get; set; }
        public int MaxPriority { get; set; }
        public int ActiveDownloads { get; set; }
        public string Status { get; set; }
        public int NZBID { get; set; }
        public string NZBName { get; set; }
        public string NZBNicename { get; set; }
        public string Kind { get; set; }
        public string URL { get; set; }
        public string NZBFilename { get; set; }
        public string DestDir { get; set; }
        public string FinalDir { get; set; }
        public string Category { get; set; }
        public string ParStatus { get; set; }
        public string ExParStatus { get; set; }
        public string UnpackStatus { get; set; }
        public string MoveStatus { get; set; }
        public string ScriptStatus { get; set; }
        public string DeleteStatus { get; set; }
        public string MarkStatus { get; set; }
        public string UrlStatus { get; set; }
        public long FileSizeLo { get; set; }
        public int FileSizeHi { get; set; }
        public int FileSizeMB { get; set; }
        public int FileCount { get; set; }
        public int MinPostTime { get; set; }
        public int MaxPostTime { get; set; }
        public int TotalArticles { get; set; }
        public int SuccessArticles { get; set; }
        public int FailedArticles { get; set; }
        public int Health { get; set; }
        public int CriticalHealth { get; set; }
        public string DupeKey { get; set; }
        public int DupeScore { get; set; }
        public string DupeMode { get; set; }
        public bool Deleted { get; set; }
        public int DownloadedSizeLo { get; set; }
        public int DownloadedSizeHi { get; set; }
        public int DownloadedSizeMB { get; set; }
        public int DownloadTimeSec { get; set; }
        public int PostTotalTimeSec { get; set; }
        public int ParTimeSec { get; set; }
        public int RepairTimeSec { get; set; }
        public int UnpackTimeSec { get; set; }
        public int MessageCount { get; set; }
        public int ExtraParBlocks { get; set; }
        public List<NzbGetParameter> Parameters { get; set; }
        public List<object> ScriptStatuses { get; set; }
        public List<NzbGetServerStat> ServerStats { get; set; }
        public string PostInfoText { get; set; }
        public int PostStageProgress { get; set; }
        public int PostStageTimeSec { get; set; }
        public List<object> Log { get; set; }
    }

    public class NzbGetList
    {
        public string version { get; set; }
        public List<NzbGetListResult> result { get; set; }
    }
}