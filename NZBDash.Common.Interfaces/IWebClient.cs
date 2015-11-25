using System;

namespace NZBDash.Common.Interfaces
{
    public interface IWebClient : IDisposable
    {
        string DownloadString(string address);
    }
}
