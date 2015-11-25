namespace NZBDash.Common.Models.Data.Models
{
    public class DashboardGrid : Entity
    {
        public int Col { get; set; }
        public string HtmlContent { get; set; }
        public string GridId { get; set; }
        public int Row { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }
    }
}
