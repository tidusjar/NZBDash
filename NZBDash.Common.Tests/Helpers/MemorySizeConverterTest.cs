using System;

using NUnit.Framework;

using NZBDash.Common.Helpers;

namespace NZBDash.Common.Tests.Helpers
{
    [TestFixture]
    public class MemorySizeConverterTest
    {
        [TestCase(1, "1.0 KB")]
        [TestCase(1024, "1.0 MB")]
        [TestCase(2048, "2.0 MB")]
        [TestCase(4879456, "4.7 GB")]
        [TestCase(10485760, "10.0 GB")]
        [TestCase(104857600000, "97.7 TB")]
        public void SizeSuffix(Int64 input, string expected)
        {
            var result = MemorySizeConverter.SizeSuffix(input);
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase(1024, "1.0 GB")]
        [TestCase(3817, "3.7 GB")]
        [TestCase(2048, "2.0 GB")]
        [TestCase(4879456, "4.7 TB")]
        [TestCase(10485760, "10.0 TB")]
        [TestCase(104857600000, "97.7 PB")]
        public void SizeSuffixMb(Int64 input, string expected)
        {
            var result = MemorySizeConverter.SizeSuffixMb(input);
            Assert.That(result, Is.EqualTo(expected));
        }

    }
}
