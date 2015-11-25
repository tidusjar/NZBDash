using System.Data.Entity;

using NZBDash.Common.Models.Data.Models;
using NZBDash.Common.Models.Data.Models.Settings;

namespace NZBDash.DataAccess.Interfaces
{
    public interface INzbDashContext
    {
         DbSet<LinksConfiguration> LinksConfiguration { get; set; }
         DbSet<DashboardGrid> DashboardGrid { get; set; }
         DbSet<Applications> Applications { get; set; }
         DbSet<CouchPotatoSettings> CouchPotatoSettings { get; set; }
         DbSet<NzbGetSettings> NzbGetSettings { get; set; }
         DbSet<SabNzbSettings> SabNzbSettings { get; set; }
         DbSet<SonarrSettings> SonarrSettings { get; set; }
         DbSet<PlexSettings> PlexSettings { get; set; }
    }
}
