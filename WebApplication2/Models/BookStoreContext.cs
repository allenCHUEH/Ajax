using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace CodeFirst.Models
{
    public class BookStoreContext:DbContext
    {
        public BookStoreContext() { }
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options) { }
        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        IConfigurationRoot Configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
        //        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("Northwind"));
        //    }
        //}
    }
}
