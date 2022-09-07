using EfCoreConcurrency.Api.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace EfCoreConcurrency.Api.DataAccess.Persistence
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(x => x.RowVersion).IsRowVersion();
            modelBuilder.Entity<Product>().Property(x => x.Price).HasPrecision(18, 2);


            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Product>  Products{ get; set; }
    }
}
