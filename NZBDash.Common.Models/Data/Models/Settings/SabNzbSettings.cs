using Dapper.Contrib.Extensions;

namespace NZBDash.Common.Models.Data.Models.Settings
{
    [Table("SabNzbSettings")]
    public class SabNzbSettings : SettingsEntity
    {
        public string ApiKey { get; set; }
    }
}
