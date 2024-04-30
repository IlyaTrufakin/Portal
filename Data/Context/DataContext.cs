using Microsoft.EntityFrameworkCore;
using Portal.Data.Entities;

namespace Portal.Data.Context
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ContactPerson> ContactPerson { get; set; }
        public DbSet<CompanyCountry> Countries { get; set; }
        public DbSet<CompanyRegion> Regions { get; set; }  

        public DataContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactPerson>()
                .HasIndex(u => u.MainEmail)
                .IsUnique();

  /*          modelBuilder.Entity<User>()
                .HasOne(u => u.Country)
                .WithMany(c => c.Users)
                .HasForeignKey(u => u.CountryId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Region)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RegionId);

            modelBuilder.Entity<UserCountry>()
                .HasKey(u => u.CountryId);

            modelBuilder.Entity<UserRegion>()
                .HasKey(u => u.UserRegionId);*/

        }
    }
}
