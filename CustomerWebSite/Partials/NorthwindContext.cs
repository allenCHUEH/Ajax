using Microsoft.EntityFrameworkCore;

namespace CustomerWebSite.Models
{
    public partial class NorthwindContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            if(!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot Configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("Northwind"));
            }
        }
    }
}
