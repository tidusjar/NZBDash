using NUnit.Framework;

using NZBDash.Core.Interfaces;
using NZBDash.Core.Services;

namespace NZBDash.Core.Test
{
    [TestFixture]
    public class WindowsHardwareServiceTests
    {
        private IHardwareService Service { get; set; }

        [SetUp]
        public void SetUp()
        {
            Service = new WindowsHardwareService();
        }

        [Test]
        public void GetDrives()
        {
            var result = Service.GetDrives();

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetRam()
        {
            var result = Service.GetRam();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.PhysicalPercentageFilled, Is.Not.Null);
            Assert.That(result.VirtualPercentageFilled, Is.Not.Null);
        }

        [Test]
        public void GetUpTime()
        {
            var upTime = Service.GetUpTime();

            Assert.That(upTime,Is.Not.Null);
        }

        [Test]
        public void GetAvailableRam()
        {
            var process = Service.GetAvailableRam();

            Assert.That(process, Is.Not.Null);
            Assert.That(process, Is.GreaterThan(0));
        }

        [Test]
        public void GetCpuPercentage()
        {
            var process = Service.GetCpuPercentage();

            Assert.That(process, Is.Not.Null);
            Assert.That(process, Is.GreaterThan(0));
        }

        [Test]
        public void GetNetworkInformation()
        {
            var process = Service.GetNetworkInformation();

            Assert.That(process, Is.Not.Null);
            Assert.That(process.Recieved, Is.Not.Null);
            Assert.That(process.Sent, Is.Not.Null);
            Assert.That(process.Total, Is.Not.Null);
        }
    }
}
