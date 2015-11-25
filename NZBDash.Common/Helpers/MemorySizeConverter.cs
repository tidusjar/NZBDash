using System;

namespace NZBDash.Common.Helpers
{
    public class MemorySizeConverter
    {
        private static readonly string[] SizeSuffixes =
                   { "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        private static readonly string[] SizeSuffixesMb =
           { "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        public static string SizeSuffix(long value)
        {
            if (value < 0) { return "-" + SizeSuffix(-value); }
            if (value == 0) { return "0.0 bytes"; }

            var mag = (int)Math.Log(value, 1024);
            var adjustedSize = (decimal)value / (1L << (mag * 10));

            return string.Format("{0:n1} {1}", adjustedSize, SizeSuffixes[mag]);
        }
        public static string SizeSuffixMb(long value)
        {
            if (value < 0) { return "-" + SizeSuffixMb(-value); }
            if (value == 0) { return "0.0 bytes"; }
            var mag = (int)Math.Log(value, 1024);
            var adjustedSize = (decimal)value / (1L << (mag * 10));

            return string.Format("{0:n1} {1}", adjustedSize, SizeSuffixesMb[mag]);
        }
    }
}
