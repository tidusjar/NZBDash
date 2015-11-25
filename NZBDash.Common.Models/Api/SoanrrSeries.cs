using System.Collections.Generic;

using Newtonsoft.Json;

namespace NZBDash.Common.Models.Api
{
    public class Image
    {
        public string coverType { get; set; }
        public string url { get; set; }
    }

    public class Season
    {
        public int seasonNumber { get; set; }
        public bool monitored { get; set; }
    }

    public class SonarrSeries
    {
        public string title { get; set; }
        public int seasonCount { get; set; }
        public int episodeCount { get; set; }
        public int episodeFileCount { get; set; }
        public string status { get; set; }
        public string overview { get; set; }
        public string nextAiring { get; set; }
        public string network { get; set; }
        public string airTime { get; set; }
        public List<Image> images { get; set; }
        public List<Season> seasons { get; set; }
        public int year { get; set; }
        public string path { get; set; }
        public int qualityProfileId { get; set; }
        public bool seasonFolder { get; set; }
        public bool monitored { get; set; }
        public bool useSceneNumbering { get; set; }
        public int runtime { get; set; }
        public int tvdbId { get; set; }
        public int tvRageId { get; set; }
        public string firstAired { get; set; }
        public string lastInfoSync { get; set; }
        public string seriesType { get; set; }
        public string cleanTitle { get; set; }
        public string imdbId { get; set; }
        public string titleSlug { get; set; }
        public int id { get; set; }
    }
}
