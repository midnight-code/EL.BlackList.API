using EL.BlackList.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EL.BlackList.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> opt) : base(opt) { }

        public DbSet<Drivers> Drivers { get; set; }
        public DbSet<FeedBacks> FeedBacks { get; set; }
        public DbSet<City> Citys { get; set; }
        public DbSet<TaxiPools> TaxiPools { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Drivers>()
                .HasMany(e => e.FeedBacks)
                .WithOne()
                .HasForeignKey(e => e.DriverId)
                .IsRequired(false);

            modelBuilder.Entity<FeedBacks>()
               .HasOne(e => e.TaxiPools)
               .WithOne()
               .HasForeignKey<TaxiPools>(e => e.TaxiPoolId)
               .IsRequired(false);

            modelBuilder.Entity<FeedBacks>()
               .HasOne(e => e.City)
               .WithOne()
               .HasForeignKey<City>(e => e.CityId)
               .IsRequired(false);
        }
    }
}
