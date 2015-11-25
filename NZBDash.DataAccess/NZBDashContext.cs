using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics.CodeAnalysis;

using NZBDash.Common.Models.Data.Models;
using NZBDash.Common.Models.Data.Models.Settings;
using NZBDash.DataAccess.Interfaces;
namespace NZBDash.DataAccess
{
    [ExcludeFromCodeCoverage]
    public class NZBDashContext : DbContext, INzbDashContext
    {
        public NZBDashContext()
            : base("NZBDashConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NZBDashContext, Configuration>("NZBDashConnection"));
        }

        public DbSet<LinksConfiguration> LinksConfiguration { get; set; }
        public DbSet<DashboardGrid> DashboardGrid { get; set; }
        public DbSet<Applications> Applications { get; set; }

        // Settings Pages
        public DbSet<CouchPotatoSettings> CouchPotatoSettings { get; set; }
        public DbSet<NzbGetSettings> NzbGetSettings { get; set; }
        public DbSet<SabNzbSettings> SabNzbSettings { get; set; }
        public DbSet<SonarrSettings> SonarrSettings { get; set; }
        public DbSet<PlexSettings> PlexSettings { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // This is a personal preference
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
