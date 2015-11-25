#region Copyright
//  ***********************************************************************
//  Copyright (c) 2015 Jamie Rees
//  File: NzbGetSettingsServiceTest.cs
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
using System.Collections.Generic;

using Moq;

using NUnit.Framework;

using NZBDash.Common.Models.Data.Models.Settings;
using NZBDash.Core.Model.Settings;
using NZBDash.Core.SettingsService;
using NZBDash.DataAccessLayer.Interfaces;

namespace NZBDash.Core.Test.Services
{
    [TestFixture]
    public class NzbGetSettingsServiceTest
    {
        private Mock<ISqlRepository<NzbGetSettings>> MockRepo { get; set; }
        private List<NzbGetSettings> ExpectedGetLinks { get; set; }
        private NzbGetSettings ExpectedLink { get; set; }
        private NzbGetSettingsService Service { get; set; }

        [SetUp]
        public void SetUp()
        {
            var mockRepo = new Mock<ISqlRepository<NzbGetSettings>>();

            ExpectedGetLinks = new List<NzbGetSettings>
            {
                new NzbGetSettings
                {
                    Id = 1, Enabled = true, IpAddress = "192", Password = "abc", Port = 25, Username = "bvc", ShowOnDashboard = true
                }
            };
            ExpectedLink = new NzbGetSettings
            {
                Id = 1,
                Enabled = true,
                IpAddress = "192",
                Password = "abc",
                Port = 25,
                Username = "bvc",
                ShowOnDashboard = true
            };

            mockRepo.Setup(x => x.GetAll()).Returns(ExpectedGetLinks).Verifiable();

            mockRepo.Setup(x => x.Get(1)).Returns(ExpectedLink).Verifiable();

            mockRepo.Setup(x => x.Update(It.IsAny<NzbGetSettings>())).Returns(true).Verifiable();

            mockRepo.Setup(x => x.Insert(It.IsAny<NzbGetSettings>())).Returns(1).Verifiable();
            
            MockRepo = mockRepo;
            Service = new NzbGetSettingsService(mockRepo.Object);
        }

        [Test]
        public void GetSettings()
        {
            var result = Service.GetSettings();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Enabled, Is.EqualTo(ExpectedGetLinks[0].Enabled));
            Assert.That(result.ShowOnDashboard, Is.EqualTo(ExpectedGetLinks[0].ShowOnDashboard));
            Assert.That(result.Port, Is.EqualTo(ExpectedGetLinks[0].Port));
            Assert.That(result.Username, Is.EqualTo(ExpectedGetLinks[0].Username));
            Assert.That(result.Password, Is.EqualTo(ExpectedGetLinks[0].Password));
            Assert.That(result.IpAddress, Is.EqualTo(ExpectedGetLinks[0].IpAddress));
            MockRepo.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void InsertSettings()
        {
            var dto = new NzbGetSettingsDto { Id = 2 };
            var result = Service.SaveSettings(dto);

            Assert.That(result, Is.True);
            MockRepo.Verify(x => x.Get(2), Times.Once);
            MockRepo.Verify(x => x.Insert(It.IsAny<NzbGetSettings>()), Times.Once);
            MockRepo.Verify(x => x.Update(It.IsAny<NzbGetSettings>()), Times.Never);
        }

        [Test]
        public void ModifySettings()
        {
            var dto = new NzbGetSettingsDto { Id = 1 };
            var result = Service.SaveSettings(dto);

            Assert.That(result, Is.True);
            MockRepo.Verify(x => x.Get(1), Times.Once);
            MockRepo.Verify(x => x.Insert(It.IsAny<NzbGetSettings>()), Times.Never);
            MockRepo.Verify(x => x.Update(It.IsAny<NzbGetSettings>()), Times.Once);
        }
    }
}
