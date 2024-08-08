using EVillaAgency.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVillaAgency.DataAccessLayer.Context
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server= ABYSSISS\\SQLEXPRESS; initial catalog = EVillaAgencyDb;integrated security = true; TrustServerCertificate=True");
        }
        public DbSet<House> Houses { get; set; }
        //public DbSet<Message> Messages { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<HouseImage> HouseImages { get; set; }
        public DbSet<HouseType> HouseTypes { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<HeatingType> HeatingTypes { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }

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

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.AppUser)
                .WithMany(u => u.Favorites)
                .HasForeignKey(f => f.AppUserId)
                .OnDelete(DeleteBehavior.Restrict); // veya .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<IdentityUserLogin<int>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });
            modelBuilder.Entity<IdentityUserRole<int>>().HasKey(r => new { r.UserId, r.RoleId });
            modelBuilder.Entity<IdentityUserToken<int>>().HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
        }

    }
}
