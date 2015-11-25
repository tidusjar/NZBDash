using NZBDash.Common.Models.Settings;

namespace NZBDash.Core.Model.Settings
{
    public class CouchPotatoSettingsDto : BaseSettingsDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ApiKey { get; set; }
    }
}
