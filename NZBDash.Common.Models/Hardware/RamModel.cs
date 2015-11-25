using System;
using System.ComponentModel.DataAnnotations;

namespace NZBDash.Common.Models.Hardware
{
    public class RamModel
    {
        [Display(Name = "Virtual Percentage Filled")]
        public int VirtualPercentageFilled
        {
            get { return 100 - (int)(AvailableVirtualMemory * 100 / TotalVirtualMemory); }
        }

        [Display(Name = "Physical Percentage Filled")]
        public int PhysicalPercentageFilled
        {
            get { return 100 - (int)(AvailablePhysicalMemory * 100 / TotalPhysicalMemory); }
        }

        [Display(Name = "Available Physical Memory")]
        public ulong AvailablePhysicalMemory { get; set; }

        [Display(Name = "Available Virtual Memory")]
        public ulong AvailableVirtualMemory { get; set; }

        [Display(Name = "OS Name")]
        public string OSFullName { get; set; }

        [Display(Name = "OS Platform")]
        public string OSPlatform { get; set; }

        [Display(Name = "OS Version")]
        public string OSVersion { get; set; }

        [Display(Name = "Total Physical Memory")]
        public ulong TotalPhysicalMemory { get; set; }

        [Display(Name = "Total Virtual Memory")]
        public ulong TotalVirtualMemory { get; set; }
    }
}