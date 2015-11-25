using Ninject.Modules;

using NZBDash.Common.Helpers;
using NZBDash.Common.Interfaces;

namespace NZBDash.DependencyResolver.Modules
{
    public class SerializerModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISerializer>().To<ThirdPartySerializer>();
        }
    }
}
