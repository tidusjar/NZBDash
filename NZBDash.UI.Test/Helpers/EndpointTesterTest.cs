using System;

using Moq;

using NUnit.Framework;

using NZBDash.Api.Models;
using NZBDash.Common;
using NZBDash.Core.Model;
using NZBDash.ThirdParty.Api.Interfaces;
using NZBDash.UI.Helpers;

using Ploeh.AutoFixture;

namespace NZBDash.UI.Test.Helpers
{
    [TestFixture]
    public class EndpointTesterTest
    {
        private Mock<IThirdPartyService> ThirdPartyServiceMock { get; set; }
        private EndpointTester Tester { get; set; }
        private NzbGetStatus NzbGetStatus { get; set; }
        private SonarrSystemStatus SonarrSystemStatus { get; set; }
        private PlexServers PlexServers { get; set; }
        private SabNzbQueue SabNzbQueue { get; set; }
        private CouchPotatoStatus CouchPotatoStatus { get; set; }

        [SetUp]
        public void SetUp()
        {
            var mockThirdPartyService = new Mock<IThirdPartyService>();
            var f = new Fixture();
            NzbGetStatus = f.Create<NzbGetStatus>();
            SonarrSystemStatus = f.Create<SonarrSystemStatus>();
            PlexServers = f.Create<PlexServers>();
            SabNzbQueue = f.Create<SabNzbQueue>();
            CouchPotatoStatus = f.Build<CouchPotatoStatus>().With(x => x.success, true).Create();

            mockThirdPartyService.Setup(x => x.GetNzbGetStatus(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(NzbGetStatus);
            mockThirdPartyService.Setup(x => x.GetSonarrSystemStatus(It.IsAny<string>(), It.IsAny<string>())).Returns(SonarrSystemStatus);
            mockThirdPartyService.Setup(x => x.GetPlexServers(It.IsAny<string>())).Returns(PlexServers);
            mockThirdPartyService.Setup(x => x.GetCouchPotatoStatus(It.IsAny<string>(), It.IsAny<string>())).Returns(CouchPotatoStatus);
            mockThirdPartyService.Setup(x => x.GetSanNzbQueue(It.IsAny<string>(), It.IsAny<string>())).Returns(SabNzbQueue);


            ThirdPartyServiceMock = mockThirdPartyService;
            Tester = new EndpointTester(ThirdPartyServiceMock.Object);
        }

        [Test]
        public void NzbGetConnection()
        {
            var result = Tester.TestApplicationConnectivity(Applications.NzbGet, "a", "a", "a", "a");
            Assert.That(result, Is.True);
        }

        [Test]
        public void GetSonarrSystemStatus()
        {
            var result = Tester.TestApplicationConnectivity(Applications.Sonarr, "a", "a", "a", "a");
            Assert.That(result, Is.True);
        }

        [Test]
        public void GetPlexServers()
        {
            var result = Tester.TestApplicationConnectivity(Applications.Plex, "a", "a", "a", "a");
            Assert.That(result, Is.True);
        }

        [Test]
        public void GetCouchPotatoStatus()
        {
            var result = Tester.TestApplicationConnectivity(Applications.CouchPotato, "a", "a", "a", "a");
            Assert.That(result, Is.True);
        }

        [Test]
        public void GetSabNzbStatus()
        {
            var result = Tester.TestApplicationConnectivity(Applications.SabNZB, "a", "a", "a", "a");
            Assert.That(result, Is.True);
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void GetSickbeardStatus()
        {
            var result = Tester.TestApplicationConnectivity(Applications.Sickbeard, "a", "a", "a", "a");
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void GetKodiStatus()
        {
            var result = Tester.TestApplicationConnectivity(Applications.Kodi, "a", "a", "a", "a");
        }

        [Test]
        [ExpectedException(typeof(NotImplementedException))]
        public void GetHeadphonesStatus()
        {
            var result = Tester.TestApplicationConnectivity(Applications.Headphones, "a", "a", "a", "a");
        }
    }
}
