using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using Microsoft.AspNet.Identity;

using Moq;

using NUnit.Framework;

using NZBDash.UI.App_Start;

namespace NZBDash.UI.Test
{
    [TestFixture]
    public class AppStartConfigurationTests
    {
        [Test]
        public void CreateBundles()
        {
            Assert.DoesNotThrow(() => BundleConfig.RegisterBundles(new BundleCollection()));
        }

        [Test]
        public void CreateFilterConfig()
        {
            Assert.DoesNotThrow(() => FilterConfig.RegisterGlobalFilters(new GlobalFilterCollection()));
        }

        [Test]
        public void RegisterRoutes()
        {
            Assert.DoesNotThrow(() => RouteConfig.RegisterRoutes(new RouteCollection()));
        }
    }
}
