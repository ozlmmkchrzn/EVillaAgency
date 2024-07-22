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
        //public DbSet<Message> Messages { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<HouseImage> HouseImages { get; set; }
        public DbSet<HouseType> HouseTypes { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<HeatingType> HeatingTypes { get; set; }    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HouseImage>()
                .HasKey(cb => cb.Id); // HouseImage tablosunun Id sütunu birincil anahtar olarak tanımlandı.

            modelBuilder.Entity<HouseImage>()
                .HasOne(cb => cb.House)
                .WithMany(c => c.HouseImages)
                .HasForeignKey(cb => cb.HouseId); // HouseId yabancı anahtar olarak tanımlandı ve House tablosuyla ilişkilendirildi.

            modelBuilder.Entity<HouseImage>()
                .HasOne(cb => cb.Image)
                .WithMany(c => c.HouseImages)
                .HasForeignKey(cb => cb.ImageId); // ImageId yabancı anahtar olarak tanımlandı ve Image tablosuyla ilişkilendirildi.




            //modelBuilder.Entity<Favorite>()
            //    .HasKey(f => new { f.UserId, f.HouseId });

            //modelBuilder.Entity<Favorite>()
            //    .HasOne(f => f.User)
            //    .WithMany(u => u.Favorites)
            //    .HasForeignKey(f => f.UserId)
            //    .OnDelete(DeleteBehavior.Restrict); // Silme davranışını ayarlayabilirsiniz

            //modelBuilder.Entity<Favorite>()
            //    .HasOne(f => f.House)
            //    .WithMany(h => h.Favorites)
            //    .HasForeignKey(f => f.HouseId)
            //    .OnDelete(DeleteBehavior.Restrict); // Silme davranışını ayarlayabilirsiniz
            // ApplicationDbContext içindeki OnModelCreating metodu

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.User)
                .WithMany(u => u.Favorites)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict); // veya .OnDelete(DeleteBehavior.NoAction);



        }

    }
}
