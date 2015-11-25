using Ninject.Modules;

using NZBDash.Common.Models.Data.Models.Settings;
using NZBDash.DataAccessLayer;
using NZBDash.DataAccessLayer.Interfaces;
using NZBDash.DataAccessLayer.Repository;

namespace NZBDash.DependencyResolver.Modules
{
	public class SqliteModule : NinjectModule
	{
		public override void Load()
		{
			Bind<ISqlRepository<SonarrSettings>>().To<SonarrRepository>();
			Bind<ISqlRepository<PlexSettings>>().To<PlexRepository>();
            Bind<ISqlRepository<NzbGetSettings>>().To<NzbGetRepository>();
            Bind<ISqlRepository<CouchPotatoSettings>>().To<CouchPotatoRepository>();
            Bind<ISqlRepository<SabNzbSettings>>().To<SabNzbRepository>();
            

			#if WINDOWS
			Bind<ISqliteConfiguration>().To<WindowsSqliteConfiguration>();
			#endif
			#if LINUX || DEBUG
			Bind<ISqliteConfiguration>().To<MonoSqliteConfiguration>();
			#endif
		}
	}
}

