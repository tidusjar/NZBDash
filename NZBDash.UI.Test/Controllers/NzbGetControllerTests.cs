using System.Collections.Generic;
using System.Web.Mvc;

using Moq;

using NUnit.Framework;

using NZBDash.Api.Models;
using NZBDash.Common;
using NZBDash.Common.Helpers;
using NZBDash.Common.Interfaces;
using NZBDash.Common.Models.ViewModels.NzbGet;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.Core.SettingsService;
using NZBDash.DataAccessLayer;
using NZBDash.ThirdParty.Api.Interfaces;
using NZBDash.ThirdParty.Api.Service;
using NZBDash.UI.Controllers.Application;

using TestStack.FluentMVCTesting;

namespace NZBDash.UI.Test.Controllers
{
    [TestFixture]
    public class NzbGetControllerTests
    {
        private NzbGetController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new NzbGetController(new NzbGetSettingsService(new NzbGetRepository(new NLogLogger(typeof(string)),new WindowsSqliteConfiguration(new NLogLogger(typeof(string))) )), new ThirdPartyService(new ThirdPartySerializer(new CustomWebClient())), new NLogLogger(typeof(string)));
        }

        [Test]
        public void EnsureThatIndexReturnsDefaultView()
        {
            _controller = new NzbGetController(new NzbGetSettingsService(new NzbGetRepository(new NLogLogger(typeof(string)), new WindowsSqliteConfiguration(new NLogLogger(typeof(string))))), new ThirdPartyService(new ThirdPartySerializer(new CustomWebClient())), new NLogLogger(typeof(string)));

            _controller.WithCallTo(x => x.Index()).ShouldRenderDefaultView();
        }

        [Test]
        public void GetNzbGetDownloadHistory()
        {
            var expectedSettings = new NzbGetSettingsDto
            {
                Username = "test",
                Id = 2,
                Password = "1",
                ShowOnDashboard = true,
                Enabled = true,
                IpAddress = "192.168.0.1",
                Port = 25
            };

            var expectedApi = new NzbGetHistory
            {
                result = new List<NzbGetHistoryResult>
                {
                    new NzbGetHistoryResult
                    {
                       FileSizeMB = 200,
                       ID = 22,
                       Name = "test",
                       Status = "Running",
                       NZBName = "nzb",
                       Category = "cata",
                    }
                }
            };

            var mockSettings = new Mock<ISettingsService<NzbGetSettingsDto>>();
            var mockApi = new Mock<IThirdPartyService>();
            var mockLogger = new Mock<ILogger>();
            mockSettings.Setup(x => x.GetSettings()).Returns(expectedSettings).Verifiable();
            mockApi.Setup(x => x.GetNzbGetHistory(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(expectedApi).Verifiable();

            var controller = new NzbGetController(mockSettings.Object, mockApi.Object, mockLogger.Object);
            var result = (PartialViewResult)controller.GetNzbGetDownloadHistory();
            var model = (List<NzbGetHistoryViewModel>)result.Model;

            Assert.That(result.Model, Is.TypeOf<List<NzbGetHistoryViewModel>>());
            Assert.That(model, Is.Not.Null);
            Assert.That(model[0].FileSize, Is.EqualTo("200.0 MB"));
            Assert.That(model[0].Id, Is.EqualTo(22));
            Assert.That(model[0].Name, Is.EqualTo("test"));
            Assert.That(model[0].Status, Is.EqualTo("Running"));
            Assert.That(model[0].NzbName, Is.EqualTo("nzb"));
            Assert.That(model[0].Category, Is.EqualTo("cata"));

        }

        [Test]
        public void GetNzbGetStatusTest()
        {
            var expectedSettings = new NzbGetSettingsDto
            {
                IpAddress = "192.168.0.1",
                Port = 25
            };
            var expectedStatus = new NzbGetStatus { Result = new NzbGetStatusResult { ServerPaused = true } };

            var mockSettings = new Mock<ISettingsService<NzbGetSettingsDto>>();
            var mockApi = new Mock<IThirdPartyService>();
            var mockLogger = new Mock<ILogger>();

            mockSettings.Setup(x => x.GetSettings()).Returns(expectedSettings);
            mockApi.Setup(x => x.GetNzbGetStatus(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(expectedStatus);


            var controller = new NzbGetController(mockSettings.Object, mockApi.Object, mockLogger.Object);
            var result = controller.GetNzbGetStatus();
            var model = (NzbGetViewModel)result.Data;


            Assert.That(model.Status, Is.EqualTo("Paused"));
        }
    }
}
