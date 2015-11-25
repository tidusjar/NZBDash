using Dapper.Contrib.Extensions;

namespace NZBDash.Common.Models.Data.Models.Settings
{
	[Table("SonarrSettings")]
    public class SonarrSettings : SettingsEntity
    {
        public string ApiKey { get; set; }
    }
}
