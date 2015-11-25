using System.ComponentModel.DataAnnotations;

namespace NZBDash.UI.Models.Settings
{
    public class SonarrSettingsViewModel : BaseSettingsViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Settings_ApiKey", ResourceType = typeof(Resources.Resources))]
        public string ApiKey { get; set; }
    }
}