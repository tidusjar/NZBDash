using System;
using System.Collections.Generic;
using System.IO;

using Moq;

using NUnit.Framework;

using NZBDash.Common.Helpers;
using NZBDash.Common.Interfaces;

namespace NZBDash.Common.Tests.Helpers
{
    [TestFixture]
    public class ThirdPartySerializerTest
    {
        [Test]
        public void JsonSerializer()
        {
            var jsonData = File.ReadAllText("TestData/Class.json");
            var mockWebClient = new Mock<IWebClient>();

            mockWebClient.Setup(x => x.DownloadString(It.IsAny<string>())).Returns(jsonData);


            var seralizer = new ThirdPartySerializer(mockWebClient.Object);
            var result = seralizer.SerializedJsonData<RootObject>(jsonData);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.version, Is.EqualTo(3.1));
            Assert.That(result.releaseDate, Is.EqualTo("2014-06-25T00:00:00.000Z"));
            Assert.That(result.demo, Is.True);
            Assert.That(result.person.id, Is.EqualTo(12345));
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void JsonSerializerException()
        {
            var jsonData = File.ReadAllText("TestData/Class.json");
            var mockWebClient = new Mock<IWebClient>();

            mockWebClient.Setup(x => x.DownloadString(It.IsAny<string>())).Throws<Exception>();


            var seralizer = new ThirdPartySerializer(mockWebClient.Object);
            var result = seralizer.SerializedJsonData<RootObject>(jsonData);
        }

        [Test]
        public void XmlSerializer()
        {
            var xmlData = File.ReadAllText("TestData/Class.xml");
            var mockWebClient = new Mock<IWebClient>();

            mockWebClient.Setup(x => x.DownloadString(It.IsAny<string>())).Returns(xmlData);


            var seralizer = new ThirdPartySerializer(mockWebClient.Object);
            var result = seralizer.SerializeXmlData<RootObject>(xmlData);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.version, Is.EqualTo(3.1));
            Assert.That(result.releaseDate, Is.EqualTo("2014-06-25T00:00:00.000Z"));
            Assert.That(result.demo, Is.True);
            Assert.That(result.person.id, Is.EqualTo(12345));
        }

    }

    public class Phones
    {
        public string home { get; set; }
        public string mobile { get; set; }
    }

    public class Person
    {
        public int id { get; set; }
        public string name { get; set; }
        public Phones phones { get; set; }
        public List<string> email { get; set; }
        public string dateOfBirth { get; set; }
        public bool registered { get; set; }
    }

    public class RootObject
    {
        public double version { get; set; }
        public string releaseDate { get; set; }
        public bool demo { get; set; }
        public Person person { get; set; }
    }
}
