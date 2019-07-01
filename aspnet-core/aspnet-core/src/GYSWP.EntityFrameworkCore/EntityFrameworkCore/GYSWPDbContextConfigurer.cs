using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace GYSWP.EntityFrameworkCore
{
    public static class GYSWPDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<GYSWPDbContext> builder, string connectionString)
        {
            //builder.UseSqlServer(connectionString);
            builder.UseSqlServer(connectionString, r => r.UseRowNumberForPaging());
        }

        public static void Configure(DbContextOptionsBuilder<GYSWPDbContext> builder, DbConnection connection)
        {
            //builder.UseSqlServer(connection);
            builder.UseSqlServer(connection, r => r.UseRowNumberForPaging());
        }
    }
}
