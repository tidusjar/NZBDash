using System.Web.Mvc;
using System.Web.UI.WebControls;

using Moq;

using NUnit.Framework;

using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.UI.Controllers;
using NZBDash.UI.Models.Settings;

using TestStack.FluentMVCTesting;

namespace NZBDash.UI.Test.Controllers
{
    [TestFixture]
    public class SettingsControllerTests
    {
        private SettingsController _controller;

        [Test]
        public void GetNzbGetSettingsReturnsDefaultViewWithModel()
        {
            var expectedDto = new NzbGetSettingsDto { Enabled = true, Id = 2, IpAddress = "192", Password = "pass", Port = 2, ShowOnDashboard = true, Username = "user" };
            var settingsMock = new Mock<ISettingsService<NzbGetSettingsDto>>();

            settingsMock.Setup(x => x.GetSettings()).Returns(expectedDto).Verifiable();

            _controller = new SettingsController(settingsMock.Object, null, null, null, null);
            _controller.WithCallTo(x => x.NzbGetSettings()).ShouldRenderDefaultView();

            var result = (ViewResult)_controller.NzbGetSettings();
            var model = (NzbGetSettingsViewModel)result.Model;

            Assert.That(model.Enabled, Is.EqualTo(expectedDto.Enabled));
            Assert.That(model.Id, Is.EqualTo(expectedDto.Id));
            Assert.That(model.IpAddress, Is.EqualTo(expectedDto.IpAddress));
            Assert.That(model.Password, Is.EqualTo(expectedDto.Password));
            Assert.That(model.Port, Is.EqualTo(expectedDto.Port));
            Assert.That(model.ShowOnDashboard, Is.EqualTo(expectedDto.ShowOnDashboard));
            Assert.That(model.Username, Is.EqualTo(expectedDto.Username));
        }


        [Test]
        public void SettingsReturnsDefaultIndex()
        {
            _controller = new SettingsController(null,null,null,null,null);
           _controller.WithModelErrors().WithCallTo(x => x.Index()).ShouldRenderDefaultView();
        }

        [Test]
        public void PostNzbGetSettingsReturnsErrorWithBadModel()
        {
            var expectedDto = new NzbGetSettingsDto { Enabled = true, Id = 2, IpAddress = "192", Password = "pass", Port = 2, ShowOnDashboard = true, Username = "user" };
            var settingsMock = new Mock<ISettingsService<NzbGetSettingsDto>>();

            settingsMock.Setup(x => x.GetSettings()).Returns(expectedDto).Verifiable();
            settingsMock.Setup(x => x.SaveSettings(It.IsAny<NzbGetSettingsDto>())).Returns(true).Verifiable();

            _controller = new SettingsController(settingsMock.Object, null, null, null, null);
            var model = new NzbGetSettingsViewModel();
            _controller.WithModelErrors().WithCallTo(x => x.NzbGetSettings(model)).ShouldRenderDefaultView().WithModel(model);
        }

        [Test]
        public void PostNzbGetSettingsReturnsDefaultView()
        {
            var expectedDto = new NzbGetSettingsDto { Enabled = true, Id = 2, IpAddress = "192", Password = "pass", Port = 2, ShowOnDashboard = true, Username = "user" };
            var settingsMock = new Mock<ISettingsService<NzbGetSettingsDto>>();

            settingsMock.Setup(x => x.GetSettings()).Returns(expectedDto).Verifiable();
            settingsMock.Setup(x => x.SaveSettings(It.IsAny<NzbGetSettingsDto>())).Returns(true).Verifiable();

            _controller = new SettingsController(settingsMock.Object, null, null, null, null);

            var model = new NzbGetSettingsViewModel();
            _controller.WithCallTo(x => x.NzbGetSettings(model)).ShouldRedirectTo(c => c.NzbGetSettings);
        }

        [Test]
        public void PostNzbGetSettingsCouldNotSaveToDb()
        {
            var expectedDto = new NzbGetSettingsDto { Enabled = true, Id = 2, IpAddress = "192", Password = "pass", Port = 2, ShowOnDashboard = true, Username = "user" };
            var settingsMock = new Mock<ISettingsService<NzbGetSettingsDto>>();

            settingsMock.Setup(x => x.GetSettings()).Returns(expectedDto).Verifiable();
            settingsMock.Setup(x => x.SaveSettings(It.IsAny<NzbGetSettingsDto>())).Returns(false).Verifiable();

            _controller = new SettingsController(settingsMock.Object, null, null, null, null);

            var model = new NzbGetSettingsViewModel();
            _controller.WithCallTo(x => x.NzbGetSettings(model)).ShouldRenderView("Error");
        }

        [Test]
        public void GetSonarrSettingsReturnsDefaultView()
        {
            var expectedDto = new SonarrSettingsViewModelDto { Enabled = true, Id = 2, IpAddress = "192", ApiKey = "pass", Port = 2, ShowOnDashboard = true };
            var settingsMock = new Mock<ISettingsService<SonarrSettingsViewModelDto>>();

            settingsMock.Setup(x => x.GetSettings()).Returns(expectedDto).Verifiable();

            _controller = new SettingsController(null, null, settingsMock.Object, null, null);
            _controller.WithCallTo(x => x.SonarrSettings()).ShouldRenderDefaultView();

            var result = (ViewResult)_controller.SonarrSettings();
            var model = (SonarrSettingsViewModel)result.Model;

            Assert.That(model.Enabled, Is.EqualTo(expectedDto.Enabled));
            Assert.That(model.Id, Is.EqualTo(expectedDto.Id));
            Assert.That(model.IpAddress, Is.EqualTo(expectedDto.IpAddress));
            Assert.That(model.ApiKey, Is.EqualTo(expectedDto.ApiKey));
            Assert.That(model.Port, Is.EqualTo(expectedDto.Port));
            Assert.That(model.ShowOnDashboard, Is.EqualTo(expectedDto.ShowOnDashboard));
        }

        [Test]
        public void PostSonarrSettingsReturnsErrorWithBadModel()
        {
            var expectedDto = new SonarrSettingsViewModelDto { Enabled = true, Id = 2, IpAddress = "192", ApiKey = "pass", Port = 2, ShowOnDashboard = true };
            var settingsMock = new Mock<ISettingsService<SonarrSettingsViewModelDto>>();

            settingsMock.Setup(x => x.GetSettings()).Returns(expectedDto);
            settingsMock.Setup(x => x.SaveSettings(It.IsAny<SonarrSettingsViewModelDto>())).Returns(true).Verifiable();

            _controller = new SettingsController(null, null, settingsMock.Object, null, null);

            var model = new SonarrSettingsViewModel();
            _controller.WithModelErrors().WithCallTo(x => x.SonarrSettings(model)).ShouldRenderDefaultView().WithModel(model);
        }

        [Test]
        public void PostSonarrSettingsReturnsDefaultView()
        {
            var expectedDto = new SonarrSettingsViewModelDto { Enabled = true, Id = 2, IpAddress = "192", ApiKey = "pass", Port = 2, ShowOnDashboard = true };
            var settingsMock = new Mock<ISettingsService<SonarrSettingsViewModelDto>>();

            settingsMock.Setup(x => x.GetSettings()).Returns(expectedDto);
            settingsMock.Setup(x => x.SaveSettings(It.IsAny<SonarrSettingsViewModelDto>())).Returns(true).Verifiable();

            _controller = new SettingsController(null, null, settingsMock.Object, null, null);

            var model = new SonarrSettingsViewModel();
            _controller.WithCallTo(x => x.SonarrSettings(model)).ShouldRedirectTo(c => c.SonarrSettings);
        }

        [Test]
        public void PostSonarrSettingsCouldNotSaveToDb()
        {
            var expectedDto = new SonarrSettingsViewModelDto { Enabled = true, Id = 2, IpAddress = "192", ApiKey = "pass", Port = 2, ShowOnDashboard = true };
            var settingsMock = new Mock<ISettingsService<SonarrSettingsViewModelDto>>();

            settingsMock.Setup(x => x.GetSettings()).Returns(expectedDto);
            settingsMock.Setup(x => x.SaveSettings(It.IsAny<SonarrSettingsViewModelDto>())).Returns(false).Verifiable();

            _controller = new SettingsController(null, null, settingsMock.Object, null, null);

            var model = new SonarrSettingsViewModel();
            _controller.WithCallTo(x => x.SonarrSettings(model)).ShouldRenderView("Error");
        }
    }
}
