using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using GYSWP.Configuration;
using GYSWP.Web;

namespace GYSWP.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class GYSWPDbContextFactory : IDesignTimeDbContextFactory<GYSWPDbContext>
    {
        public GYSWPDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<GYSWPDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            GYSWPDbContextConfigurer.Configure(builder, configuration.GetConnectionString(GYSWPConsts.ConnectionStringName));

            return new GYSWPDbContext(builder.Options);
        }
    }
}
