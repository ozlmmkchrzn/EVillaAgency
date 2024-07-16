using EVillaAgency.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.DataAccessLayer.Context
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server= ABYSSISS\\SQLEXPRESS; initial catalog = EVillaAgencyDb;integrated security = true; TrustServerCertificate=True");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<HouseImage> HouseImages { get; set; }
        public DbSet<HouseType> HouseTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HouseImage>()
                .HasKey(cb => new { cb.HouseId, cb.ImageId });

            modelBuilder.Entity<HouseImage>()
                .HasOne(cb => cb.House)
                .WithMany(c => c.HouseImages)
                .HasForeignKey(cb => cb.ImageId);

            modelBuilder.Entity<HouseImage>()
                .HasOne(cb => cb.Image)
                .WithMany(c => c.HouseImages)
                .HasForeignKey(cb => cb.HouseId);


        }

    }
}
