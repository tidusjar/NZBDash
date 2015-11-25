#region Copyright
//  ***********************************************************************
//  Copyright (c) 2015 Jamie Rees
//  File: LinksConfigurationServiceTest.cs
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
using System.Linq;

using Moq;

using NUnit.Framework;

using NZBDash.Common.Models.Data.Models;
using NZBDash.Core.Configuration;
using NZBDash.Core.Model.DTO;
using NZBDash.DataAccessLayer.Interfaces;

namespace NZBDash.Core.Test
{
    [TestFixture]
    public class LinksConfigurationServiceTest
    {
        private Mock<ISqlRepository<LinksConfiguration>> MockRepo { get; set; }
        private List<LinksConfiguration> ExpectedGetLinks { get; set; }
        private LinksConfiguration ExpectedLink { get; set; }
        private LinksConfigurationService Service { get; set; }

        [SetUp]
        public void SetUp()
        {
            var mockRepo = new Mock<ISqlRepository<LinksConfiguration>>();
            ExpectedGetLinks = new List<LinksConfiguration> { new LinksConfiguration { Id = 1, LinkEndpoint = "google.com", LinkName = "Google" } };
            ExpectedLink = new LinksConfiguration { Id = 1, LinkEndpoint = "google.com", LinkName = "Google" };

            mockRepo.Setup(x => x.GetAll()).Returns(ExpectedGetLinks).Verifiable();

            mockRepo.Setup(x => x.Get(1)).Returns(ExpectedLink).Verifiable();

            mockRepo.Setup(x => x.Update(It.IsAny<LinksConfiguration>())).Returns(true).Verifiable();

            mockRepo.Setup(x => x.Insert(It.IsAny<LinksConfiguration>())).Returns(1).Verifiable();

            mockRepo.Setup(x => x.Delete(It.IsAny<LinksConfiguration>())).Verifiable();

            MockRepo = mockRepo;
            Service = new LinksConfigurationService(MockRepo.Object);
        }

        [Test]
        public void GetLinks()
        {
            var result = Service.GetLinks().ToList();

            Assert.That(result, Is.Not.Null);
            Assert.That(result[0].Id, Is.EqualTo(ExpectedGetLinks[0].Id));
            Assert.That(result[0].LinkEndpoint, Is.EqualTo(ExpectedGetLinks[0].LinkEndpoint));
            Assert.That(result[0].LinkName, Is.EqualTo(ExpectedGetLinks[0].LinkName));
            MockRepo.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void UpdateLink()
        {
            var dto = new LinksConfigurationDto { Id = 1, LinkEndpoint = "www.microsoft.com", LinkName = "Microsoft" };
            var result = Service.UpdateLink(dto);

            Assert.That(result, Is.True);
            MockRepo.Verify(x => x.Get(1), Times.Once);
            MockRepo.Verify(x => x.Update(It.IsAny<LinksConfiguration>()), Times.Once);
        }

        [Test]
        public void AddLink()
        {
            var dto = new LinksConfigurationDto { Id = 1, LinkEndpoint = "google.com", LinkName = "Google" };
            var result = Service.AddLink(dto);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(ExpectedLink.Id));
            Assert.That(result.LinkEndpoint, Is.EqualTo(ExpectedLink.LinkEndpoint));
            Assert.That(result.LinkName, Is.EqualTo(ExpectedLink.LinkName));
            MockRepo.Verify(x => x.Insert(It.IsAny<LinksConfiguration>()), Times.Once);
        }

        [Test]
        public void RemoveLink()
        {
            Service.RemoveLink(1);

            MockRepo.Verify(x => x.Delete(It.IsAny<LinksConfiguration>()), Times.Once);
        }

        [Test]
        public void RemoveIncorrectLink()
        {
            Service.RemoveLink(2);
            MockRepo.Verify(x => x.Delete(It.IsAny<LinksConfiguration>()), Times.Never);
        }
    }
}
