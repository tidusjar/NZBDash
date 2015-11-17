using NZBDash.Api.Controllers;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.Core.Services;
using NZBDash.Core.SettingsService;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NZBDash.UI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NZBDash.UI.App_Start.NinjectWebCommon), "Stop")]

namespace NZBDash.UI.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
#if WINDOWS
            kernel.Bind<IHardwareService>().To<WindowsHardwareService>();
            kernel.Bind<IStatusApi>().To<StatusApiController>();
#endif

#if LINUX
            kernel.Bind<IHardwareService>().To<LinuxHardwareService>();
            kernel.Bind<IStatusApi>().To<StatusApiController>();
#endif
            // Applications
            kernel.Bind<ISettingsService<NzbGetSettingsDto>>().To<NzbGetSettingsService>();
            kernel.Bind<ISettingsService<SabNzbSettingsDto>>().To<SabNzbSettingsService>();
            kernel.Bind<ISettingsService<SonarrSettingsViewModelDto>>().To<SonarrSettingsService>();
            kernel.Bind<ISettingsService<CouchPotatoSettingsDto>>().To<CouchPotatoSettingsServiceService>();
            kernel.Bind<ISettingsService<PlexSettingsDto>>().To<PlexSettingsService>();
        }
    }
}
