using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace GYSWP.EntityFrameworkCore
{
    public static class GYSWPDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<GYSWPDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<GYSWPDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
