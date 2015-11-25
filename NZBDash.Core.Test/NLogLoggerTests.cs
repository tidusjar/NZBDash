using System;
using System.IO;

using NUnit.Framework;

using NZBDash.Common;
using NZBDash.Common.Interfaces;

namespace NZBDash.Core.Test
{
    [TestFixture]
    public class NLogLoggerTests
    {
        private ILogger Logger = new NLogLogger(typeof(NLogLoggerTests));

        [Test]
        public void AssertLoggerIsFunctioningCorrectly()
        {
            Logger.Info("info");
            Logger.Warn("Warn");
            Logger.Debug("Debug");
            Logger.Error("Error");
            Logger.Error(new Exception());
            Logger.Error("Error", new Exception());
            Logger.Fatal("Fatal");
            Logger.Fatal(new Exception());
        }
    }
}
