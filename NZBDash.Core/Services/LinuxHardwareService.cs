using System;
using System.Collections.Generic;

using NZBDash.Common.Models.Api;
using NZBDash.Common.Models.Hardware;
using NZBDash.Core.Interfaces;

namespace NZBDash.Core.Services
{
    public class LinuxHardwareService : IHardwareService
    {
        public IEnumerable<DriveModel> GetDrives()
        {
			throw new NotImplementedException();
        }

        public RamModel GetRam()
        {
            throw new NotImplementedException();
        }

        public TimeSpan GetUpTime()
        {
            throw new NotImplementedException();
        }

        public float GetCpuPercentage()
        {
            throw new NotImplementedException();
        }

        public float GetAvailableRam()
        {
            throw new NotImplementedException();
        }

        public NetworkInfo GetNetworkInformation()
        {
            throw new NotImplementedException();
        }
    }
}
