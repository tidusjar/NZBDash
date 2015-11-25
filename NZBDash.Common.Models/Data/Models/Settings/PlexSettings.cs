using Dapper.Contrib.Extensions;

namespace NZBDash.Common.Models.Data.Models.Settings
{
    [Table("PlexSettings")]
    public class PlexSettings : SettingsEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
