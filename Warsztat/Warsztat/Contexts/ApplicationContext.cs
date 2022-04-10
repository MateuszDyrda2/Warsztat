using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warsztat.Models
{
    class ApplicationContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-O54046C\\SQLEXPRESS;Initial Catalog=WorkshopDB;Integrated Security=True;TrustServerCertificate=True;");
        }

        public DbSet <Client> Clients { get; set; }
        
        public DbSet<Car> Cars { get; set; }

        public DbSet<Activity> Activities { get; set; }

        public DbSet<ActivityDictionary> ActivityDictionaries { get; set; }

        public DbSet<CarType> CarTypes { get; set; }

        public DbSet<Personel> Personels { get; set; }

        public DbSet<Request> Requests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .HasOne<Client>(c => c.client)
                .WithMany(c => c.Cars)
                .HasForeignKey(c => c.clientId);

            modelBuilder.Entity<Car>()
                .HasOne<CarType>(cT => cT.carType)
                .WithMany(c => c.Cars)
                .HasForeignKey(cT => cT.carTypeMark);

            modelBuilder.Entity<Request>()
                .HasOne<Car>(c => c.car)
                .WithMany(r => r.requests)
                .HasForeignKey(c => c.carId);

            modelBuilder.Entity<Request>()
                .HasOne<Personel>(p => p.personel)
                .WithMany(r => r.requests)
                .HasForeignKey(p => p.personelId);
            
            //reszta relacji
        }
    }
}
