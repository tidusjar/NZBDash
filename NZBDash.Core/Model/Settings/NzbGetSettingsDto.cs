using NZBDash.Common.Models.Settings;

namespace NZBDash.Core.Model.Settings
{
    public class NzbGetSettingsDto : BaseSettingsDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
