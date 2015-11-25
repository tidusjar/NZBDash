using Dapper.Contrib.Extensions;
namespace NZBDash.Common.Models.Data.Models.Settings
{
	[Table("NzbGetSettings")]
    public class NzbGetSettings : SettingsEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
     }
}
