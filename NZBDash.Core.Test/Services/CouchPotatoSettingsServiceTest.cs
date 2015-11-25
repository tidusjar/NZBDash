#region Copyright
//  ***********************************************************************
//  Copyright (c) 2015 Jamie Rees
//  File: CouchPotatoSettingsServiceTest.cs
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
    public class CouchPotatoSettingsServiceTest
    {
        private Mock<ISqlRepository<CouchPotatoSettings>> MockRepo { get; set; }
        private List<CouchPotatoSettings> ExpectedGetLinks { get; set; }
        private CouchPotatoSettings ExpectedLink { get; set; }
        private CouchPotatoSettingsService Service { get; set; }

        [SetUp]
        public void SetUp()
        {
            var mockRepo = new Mock<ISqlRepository<CouchPotatoSettings>>();
            ExpectedGetLinks = new List<CouchPotatoSettings> { new CouchPotatoSettings { Id = 1, Enabled = true, ApiKey = "abc", IpAddress = "192", Password = "abc", Port = 25, Username = "bvc", ShowOnDashboard = true } };
            ExpectedLink = new CouchPotatoSettings
            {
                Id = 1,
                Enabled = true,
                ApiKey = "abc",
                IpAddress = "192",
                Password = "abc",
                Port = 25,
                Username = "bvc",
                ShowOnDashboard = true
            };

            mockRepo.Setup(x => x.GetAll()).Returns(ExpectedGetLinks).Verifiable();

            mockRepo.Setup(x => x.Get(1)).Returns(ExpectedLink).Verifiable();

            mockRepo.Setup(x => x.Update(It.IsAny<CouchPotatoSettings>())).Returns(true).Verifiable();

            mockRepo.Setup(x => x.Insert(It.IsAny<CouchPotatoSettings>())).Returns(1).Verifiable();


            MockRepo = mockRepo;
            Service = new CouchPotatoSettingsService(mockRepo.Object);
        }

        [Test]
        public void GetSettings()
        {
            var result = Service.GetSettings();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(ExpectedGetLinks[0].Id));
            Assert.That(result.Enabled, Is.EqualTo(ExpectedGetLinks[0].Enabled));
            Assert.That(result.ApiKey, Is.EqualTo(ExpectedGetLinks[0].ApiKey));
            Assert.That(result.IpAddress, Is.EqualTo(ExpectedGetLinks[0].IpAddress));
            Assert.That(result.Password, Is.EqualTo(ExpectedGetLinks[0].Password));
            Assert.That(result.Username, Is.EqualTo(ExpectedGetLinks[0].Username));
            Assert.That(result.Port, Is.EqualTo(ExpectedGetLinks[0].Port));
            Assert.That(result.ShowOnDashboard, Is.EqualTo(ExpectedGetLinks[0].ShowOnDashboard));
            MockRepo.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void InsertSettings()
        {
            var dto = new CouchPotatoSettingsDto { Id = 2 };
            var result = Service.SaveSettings(dto);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.True);

            MockRepo.Verify(x => x.Get(2), Times.Once);
            MockRepo.Verify(x => x.Insert(It.IsAny<CouchPotatoSettings>()), Times.Once);
            MockRepo.Verify(x => x.Update(It.IsAny<CouchPotatoSettings>()), Times.Never);
        }

        [Test]
        public void ModifySettings()
        {
            var dto = new CouchPotatoSettingsDto { Id = 1 };
            var result = Service.SaveSettings(dto);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.True);

            MockRepo.Verify(x => x.Get(1), Times.Once);
            MockRepo.Verify(x => x.Insert(It.IsAny<CouchPotatoSettings>()), Times.Never);
            MockRepo.Verify(x => x.Update(It.IsAny<CouchPotatoSettings>()), Times.Once);
        }



    }
}
