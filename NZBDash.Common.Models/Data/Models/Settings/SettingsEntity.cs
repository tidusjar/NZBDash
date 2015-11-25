
namespace NZBDash.Common.Models.Data.Models.Settings
{
    public abstract class SettingsEntity : Entity
    {
        public bool Enabled { get; set; }
        public bool ShowOnDashboard { get; set; }
        public int Port { get; set; }
        public string IpAddress { get; set; }
    }
}
