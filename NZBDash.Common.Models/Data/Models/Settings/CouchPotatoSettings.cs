using Dapper.Contrib.Extensions;

namespace NZBDash.Common.Models.Data.Models.Settings
{
    [Table("CouchPotatoSettings")]
    public class CouchPotatoSettings : SettingsEntity
    {
        public string ApiKey { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
