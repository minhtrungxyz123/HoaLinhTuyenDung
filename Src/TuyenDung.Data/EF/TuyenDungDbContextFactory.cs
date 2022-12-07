using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TuyenDung.Data.EF
{
    public class TuyenDungDbContextFactory : IDesignTimeDbContextFactory<TuyenDungDbContext>
    {
        public TuyenDungDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("TuyenDungDatabase");

            var optionsBuilder = new DbContextOptionsBuilder<TuyenDungDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new TuyenDungDbContext(optionsBuilder.Options);
        }
    }
}