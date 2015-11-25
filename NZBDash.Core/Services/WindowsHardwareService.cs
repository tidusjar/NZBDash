#region Copyright
//  ***********************************************************************
//  Copyright (c) 2015 Jamie Rees
//  File: WindowsHardwareService.cs
//  Created By: Jamie Rees
//
//  Permission is hereby granted, free of charge, to any person obtaining
//  a copy of this software and associated documentation files (the
//  "Software"), to deal in the Software without restriction, including
//  without limitation the rights to use, copy, modify, merge, publish,
//  distribute, sublicense, and/or sell copies of the Software, and to
//  permit persons to whom the Software is furnished to do so, subject to
//  the following conditions:
//
//  The above copyright notice and this permission notice shall be
//  included in all copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//  EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//  MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//  NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
//  LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
//  OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
//  WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//  ***********************************************************************
#endregion
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

using Microsoft.VisualBasic.Devices;

using NZBDash.Common.Models.Api;
using NZBDash.Common.Models.Hardware;
using NZBDash.Core.Interfaces;

using Omu.ValueInjecter;

namespace NZBDash.Core.Services
{
    public class WindowsHardwareService : IHardwareService
    {
        /// <summary>
        /// Gets the drives.
        /// </summary>
        public IEnumerable<DriveModel> GetDrives()
        {
            var drives = GetDriveInfo();
            var model = new List<DriveModel>();
            foreach (var drive in drives)
            {
                if (!drive.IsReady)
                {
                    continue;
                }

                var driveModel = new DriveModel();
                var mapped = driveModel.InjectFrom(drive);
                model.Add((DriveModel)mapped);
            }

            return model;
        }

        /// <summary>
        /// Gets the ram.
        /// </summary>
        public RamModel GetRam()
        {
            var ramInfo = GetRamInfo();
            var ramModel = new RamModel();
            return (RamModel)ramModel.InjectFrom(ramInfo);
        }

        /// <summary>
        /// Gets up time of the current PC.
        /// </summary>
        public TimeSpan GetUpTime()
        {
            using (var uptime = new PerformanceCounter("System", "System Up Time"))
            {
                // Call this an extra time before reading its value
                uptime.NextValue();
                return TimeSpan.FromSeconds(uptime.NextValue());
            }
        }

        /// <summary>
        /// Gets the cpu percentage of the current PC.
        /// </summary>
        public float GetCpuPercentage()
        {
            using (var process = new PerformanceCounter("Processor", "% Processor Time", "_Total"))
            {
                // Call this an extra time before reading its value
                process.NextValue();

                // We require the PC tp update it self... We need to wait.
                Thread.Sleep(500);
                return process.NextValue();
            }
        }

        /// <summary>
        /// Gets the available ram.
        /// </summary>
        public float GetAvailableRam()
        {
            using (var memory = new PerformanceCounter("Memory", "Available MBytes"))
            {
                // Call this an extra time before reading its value
                memory.NextValue();

                return memory.NextValue();
            }
        }

        /// <summary>
        /// Gets the network information.
        /// </summary>
        public NetworkInfo GetNetworkInformation()
        {
            return GetNetworkingDetails();
        }

        private ComputerInfo GetRamInfo()
        {
            return new ComputerInfo();
        }

        private DriveInfo[] GetDriveInfo()
        {
            return DriveInfo.GetDrives();
        }

        private NetworkInfo GetNetworkingDetails()
        {
            var info = new NetworkInfo();
            var performanceCounterCategory = new PerformanceCounterCategory("Network Interface");
            var cn = performanceCounterCategory.GetInstanceNames();
            var firstNetworkCard = cn[0]; //TODO: Change this, the user needs to select the NIC to monitor in a settings page.
            var networkBytesSent = new PerformanceCounter("Network Interface", "Bytes Sent/sec", firstNetworkCard);
            var networkBytesReceived = new PerformanceCounter("Network Interface", "Bytes Received/sec", firstNetworkCard);
            var networkBytesTotal = new PerformanceCounter("Network Interface", "Bytes Total/sec", firstNetworkCard);

            info.Sent = networkBytesSent.NextValue();
            info.Recieved = networkBytesReceived.NextValue();
            info.Total = networkBytesTotal.NextValue();

            // First counter is empty
            Thread.Sleep(1000);
            info.Sent = networkBytesSent.NextValue();
            info.Recieved = networkBytesReceived.NextValue();
            info.Total = networkBytesTotal.NextValue();
            return info;
        }
    }
}
