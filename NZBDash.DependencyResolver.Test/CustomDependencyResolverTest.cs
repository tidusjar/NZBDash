#region Copyright
//  ***********************************************************************
//  Copyright (c) 2015 Jamie Rees
//  File: CustomDependencyResolverTest.cs
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
using System.Linq;

using Ninject;

using NUnit.Framework;

using NZBDash.Common.Interfaces;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.DependencyResolver.Modules;

namespace NZBDash.DependencyResolver.Test
{
    [TestFixture]
    public class CustomDependencyResolverTest
    {
        [Test]
        public void ResolveServiceDependency()
        {
            var module = new ServiceModule();
            var kernal = new StandardKernel(module);

            var service = kernal.Get<IHardwareService>();
            Assert.That(service,Is.Not.Null);
        }

        [Test]
        public void ResolveApplicationDependency()
        {
            var module = new ApplicationSettingsModule();
            var repoModule = new SqliteModule();
            var loggerModule = new LoggerModule();
            var kernal = new StandardKernel(module, repoModule, loggerModule);

            var service = kernal.Get<ISettingsService<PlexSettingsDto>>();
            Assert.That(service, Is.Not.Null);
        }

        [Test]
        public void ResolveSerializerDependency()
        {
            var module = new SerializerModule();
            var webClientModule = new ServiceModule();
            var kernal = new StandardKernel(module, webClientModule);

            var service = kernal.Get<ISerializer>();
            Assert.That(service, Is.Not.Null);
        }

        [Test]
        public void ResolveLoggerDependency()
        {
            var module = new LoggerModule();
            var kernal = new StandardKernel(module);
            var service = kernal.Get<TestClass>();
            Assert.That(service, Is.Not.Null);
        }

        [Test]
        public void GetCustomModules()
        {
            var customResolver = new CustomDependencyResolver();
            var modules = customResolver.GetModules();

            Assert.That(modules.Count(), Is.GreaterThan(0));
        }
    }
    public class TestClass
    {
        private readonly ILogger m_Logger;

        public TestClass(ILogger logger)
        {
            m_Logger = logger;
        }

        public ILogger Logger
        {
            get { return m_Logger; }
        }
    }
}
