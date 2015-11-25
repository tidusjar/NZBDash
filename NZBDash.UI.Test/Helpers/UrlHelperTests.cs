using System;

using NUnit.Framework;

using NZBDash.UI.Helpers;

namespace NZBDash.UI.Test.Helpers
{
    [TestFixture]
    public class UrlHelperTests
    {
        [TestCase("www.google.com", "http://www.google.com/")]
        [TestCase("http://www.google.com/", "http://www.google.com/")]
        [TestCase("https://www.google.com", "https://www.google.com/")]
        [TestCase("www.google.com:443", "http://www.google.com:443/")]
        [TestCase("https://www.google.com:443", "https://www.google.com:443/")]
        [TestCase("http://www.google.com:443/id=2", "http://www.google.com:443/id=2")]
        [TestCase("www.google.com:4438/id=22", "http://www.google.com:4438/id=22")]
        public void TestUrlHelper(string input, string output)
        {
            var expected = new Uri(output);
            var uri = UrlHelper.ReturnUri(input);

            Assert.That(uri.AbsoluteUri, Is.EqualTo(expected.AbsoluteUri));
            Assert.That(uri.Port, Is.EqualTo(expected.Port));
            Assert.That(uri.Query, Is.EqualTo(expected.Query));
        }

        [TestCase("www.google.com", 80, "http://www.google.com:80/")]
        [TestCase("www.google.com", 443, "http://www.google.com:443/")]
        [TestCase("http://www.google.com", 443, "http://www.google.com:443/")]
        [TestCase("https://www.google.com", 443, "https://www.google.com:443/")]
        [TestCase("http://www.google.com/id=2", 443, "http://www.google.com:443/id=2")]
        [TestCase("http://www.google.com/id=2", 443, "http://www.google.com:443/id=2")]
        [TestCase("https://www.google.com/id=2", 443, "https://www.google.com:443/id=2")]
        public void TestUrlHelperWithPort(string input, int port, string output)
        {
            var expected = new Uri(output);
            var uri = UrlHelper.ReturnUri(input, port);

            Assert.That(uri.AbsoluteUri, Is.EqualTo(expected.AbsoluteUri));
            Assert.That(uri.Port, Is.EqualTo(expected.Port));
            Assert.That(uri.Query, Is.EqualTo(expected.Query));
        }
    }
}
