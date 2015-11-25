#region Copyright
//  ***********************************************************************
//  Copyright (c) 2015 Jamie Rees
//  File: DashboardControllerTests.cs
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
using System.Linq;
using System.Web.Mvc;

using Moq;

using NUnit.Framework;

using NZBDash.Common.Models.Hardware;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.DTO;
using NZBDash.Core.Model.Settings;
using NZBDash.ThirdParty.Api.Interfaces;
using NZBDash.UI.Controllers;
using NZBDash.UI.Models.Dashboard;

using Ploeh.AutoFixture;

using TestStack.FluentMVCTesting;

namespace NZBDash.UI.Test.Controllers
{
    [TestFixture]
    public class DashboardControllerTests
    {
        private DashboardController _controller;
        private Mock<IThirdPartyService> StatusApiMock { get; set; }
        private Mock<IHardwareService> HardwareServiceMock { get; set; }
        private Mock<ILinksConfiguration> LinksConfigurationServiceMock { get; set; }
        private Mock<ISettingsService<NzbGetSettingsDto>> NzbGetMock { get; set; }

        private List<LinksConfigurationDto> LinksDto { get; set; }
        private RamModel RamModel { get; set; }
        private List<DriveModel> DriveModel { get; set; }

        [SetUp]
        public void MockSetup()
        {
            var f = new Fixture();
            var mockHardware = new Mock<IHardwareService>();
            var mockApi = new Mock<IThirdPartyService>();
            var mockLinks = new Mock<ILinksConfiguration>();
            var mocknzbGet = new Mock<ISettingsService<NzbGetSettingsDto>>();
            RamModel = f.Create<RamModel>();
            DriveModel = f.CreateMany<DriveModel>().ToList();
            var linksDto = f.CreateMany<LinksConfigurationDto>();



            mockHardware.Setup(x => x.GetUpTime()).Returns(new TimeSpan(2, 12, 33, 59, 200));
            mockHardware.Setup(x => x.GetRam()).Returns(RamModel);
            mockHardware.Setup(x => x.GetDrives()).Returns(DriveModel);
            mockLinks.Setup(x => x.GetLinks()).Returns(linksDto);

            HardwareServiceMock = mockHardware;
            StatusApiMock = mockApi;
            NzbGetMock = mocknzbGet;
            LinksDto = linksDto.ToList();
            LinksConfigurationServiceMock = mockLinks;

            _controller = new DashboardController(HardwareServiceMock.Object, StatusApiMock.Object, LinksConfigurationServiceMock.Object);
        }


        [Test]
        public void EnsureThatIndexReturnsDefaultView()
        {

            _controller.WithCallTo(x => x.Index()).ShouldRenderDefaultView();
        }

        [Test]
        public void GetServerInformation()
        {
            var uptime = new TimeSpan(2, 12, 33, 59, 200);
            var result = (PartialViewResult)_controller.GetServerInformation();
            var model = (ServerInformationViewModel)result.Model;


            HardwareServiceMock.Verify(x => x.GetCpuPercentage(), Times.Once());
            HardwareServiceMock.Verify(x => x.GetAvailableRam(), Times.Once());
            HardwareServiceMock.Verify(x => x.GetUpTime(), Times.Once());

            _controller.WithCallTo(x => x.GetServerInformation()).ShouldRenderPartialView("Partial/_ServerInformation").WithModel<ServerInformationViewModel>();

            Assert.That(model.OsPlatform, Is.EqualTo(RamModel.OSPlatform));
            Assert.That(model.OsFullName, Is.EqualTo(RamModel.OSFullName));
            Assert.That(model.OsVersion, Is.EqualTo(RamModel.OSVersion));
            Assert.That(model.Uptime, Is.EqualTo(uptime));
        }

        [Test]
        public void GetRam()
        {
            var result = (PartialViewResult)_controller.GetRam();
            var model = (RamModel)result.Model;

            HardwareServiceMock.Verify(x => x.GetRam(), Times.Once());

            _controller.WithCallTo(x => x.GetRam()).ShouldRenderPartialView("Partial/_Ram").WithModel<RamModel>();

            Assert.That(model.AvailablePhysicalMemory, Is.EqualTo(RamModel.AvailablePhysicalMemory));
            Assert.That(model.AvailableVirtualMemory, Is.EqualTo(RamModel.AvailableVirtualMemory));
            Assert.That(model.VirtualPercentageFilled, Is.EqualTo(RamModel.VirtualPercentageFilled));
            Assert.That(model.PhysicalPercentageFilled, Is.EqualTo(RamModel.PhysicalPercentageFilled));
            Assert.That(model.OSFullName, Is.EqualTo(RamModel.OSFullName));
            Assert.That(model.OSPlatform, Is.EqualTo(RamModel.OSPlatform));
            Assert.That(model.OSVersion, Is.EqualTo(RamModel.OSVersion));
            Assert.That(model.TotalPhysicalMemory, Is.EqualTo(RamModel.TotalPhysicalMemory));
            Assert.That(model.TotalVirtualMemory, Is.EqualTo(RamModel.TotalVirtualMemory));
            Assert.That(model.VirtualPercentageFilled, Is.EqualTo(RamModel.VirtualPercentageFilled));
        }

        [Test]
        public void GetDriveInformation()
        {
            var result = (PartialViewResult)_controller.GetDriveInformation();
            var model = (List<DriveModel>)result.Model;

            HardwareServiceMock.Verify(x => x.GetDrives(), Times.Once());

            _controller.WithCallTo(x => x.GetDriveInformation()).ShouldRenderPartialView("Partial/_DriveInformation").WithModel<IEnumerable<DriveModel>>();

            Assert.That(model[0].PercentageFilled, Is.EqualTo(DriveModel[0].PercentageFilled));
            Assert.That(model[0].IsReady, Is.EqualTo(DriveModel[0].IsReady));
            Assert.That(model[0].Name, Is.EqualTo(DriveModel[0].Name));
            Assert.That(model[0].DriveType, Is.EqualTo(DriveModel[0].DriveType));
            Assert.That(model[0].DriveFormat, Is.EqualTo(DriveModel[0].DriveFormat));
            Assert.That(model[0].AvailableFreeSpace, Is.EqualTo(DriveModel[0].AvailableFreeSpace));
            Assert.That(model[0].VolumeLabel, Is.EqualTo(DriveModel[0].VolumeLabel));
            Assert.That(model[0].TotalSize, Is.EqualTo(DriveModel[0].TotalSize));
            Assert.That(model[0].TotalFreeSpace, Is.EqualTo(DriveModel[0].TotalFreeSpace));
            Assert.That(model[0].PercentageFilled, Is.EqualTo(DriveModel[0].PercentageFilled));
        }

        [Test]
        public void GetLinks()
        {
            var result = (PartialViewResult)_controller.GetLinks();
            var model = (List<DashboardLinksViewModel>)result.Model;

            LinksConfigurationServiceMock.Verify(x => x.GetLinks(), Times.Once);

            _controller.WithCallTo(x => x.GetLinks()).ShouldRenderPartialView("Partial/_Links").WithModel<IEnumerable<DashboardLinksViewModel>>();

            Assert.That(model[0].LinkEndpoint, Is.EqualTo(LinksDto[0].LinkEndpoint));
            Assert.That(model[0].LinkName, Is.EqualTo(LinksDto[0].LinkName));
        }
    }
}
