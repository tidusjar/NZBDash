using System;
using System.Collections.Generic;

using NZBDash.Common.Models.Api;
using NZBDash.Common.Models.Hardware;

namespace NZBDash.Core.Interfaces
{
    /// <summary>
    /// The Interface for interacting with any hardware components e.g. RAM, CPU, GPU
    /// </summary>
    public interface IHardwareService
    {
        /// <summary>
        /// Gets the drives for the current PC.
        /// </summary>
        IEnumerable<DriveModel> GetDrives();

        /// <summary>
        /// Gets the ram for the current PC.
        /// </summary>
        RamModel GetRam();

        /// <summary>
        /// Gets up time for the current PC.
        /// </summary>
        TimeSpan GetUpTime();

        /// <summary>
        /// Gets the cpu percentage for the current PC.
        /// </summary>
        float GetCpuPercentage();

        /// <summary>
        /// Gets the available ram for the current PC.
        /// </summary>
        float GetAvailableRam();

        /// <summary>
        /// Gets the network information.
        /// </summary>
        NetworkInfo GetNetworkInformation();
    }
}
