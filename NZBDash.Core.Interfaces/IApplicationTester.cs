namespace NZBDash.Core.Interfaces
{
    public interface IApplicationTester
    {
        string IpAddress { get; set; }
        string ApiKey { get; set; }
        string Username { get; set; }
        string Password { get; set; }

        bool TestConnection();
    }
}
