namespace NZBDash.Common.Models.Settings
{
    public class BaseSettingsDto
    {
        public int Id { get; set; }
        public bool Enabled { get; set; }
        public bool ShowOnDashboard { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
    }
}
