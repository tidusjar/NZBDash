using System.Data.Entity.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace NZBDash.DataAccess.Migrations
{
    [ExcludeFromCodeCoverage]
    internal sealed class Configuration : DbMigrationsConfiguration<NZBDashContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}
