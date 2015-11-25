using System.Data.Entity.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace NZBDash.DataAccess
{
    [ExcludeFromCodeCoverage]
    internal sealed class Configuration : DbMigrationsConfiguration<NZBDashContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "NZBDash.DataAccess.NZBDashContext";
            AutomaticMigrationDataLossAllowed = true;
        }
    }
}
