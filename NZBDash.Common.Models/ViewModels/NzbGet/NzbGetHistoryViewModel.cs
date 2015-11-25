using System.ComponentModel.DataAnnotations;

namespace NZBDash.Common.Models.ViewModels.NzbGet
{
    public class NzbGetHistoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        [Display(Name = "Nzb Name")]
        public string NzbName { get; set; }
        public string Category { get; set; }
        [Display(Name = "File Size")]
        public string FileSize { get; set; }
        public int Health { get; set; }
        [Display(Name = "Time")]
        public int HistoryTime { get; set; }
    }
}