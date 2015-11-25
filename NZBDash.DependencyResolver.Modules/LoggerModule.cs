using System;

using Ninject.Modules;
using NZBDash.Common;
using NZBDash.Common.Interfaces;

namespace NZBDash.DependencyResolver.Modules
{
    public class LoggerModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILogger>().To<NLogLogger>().WithConstructorArgument(typeof(Type), x => x.Request.ParentContext.Plan.Type);
        }
    }
}
