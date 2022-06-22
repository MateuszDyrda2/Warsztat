using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warsztat.Models
{
    class ApplicationContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            String ConnectionDB = File.ReadLines(@"..\..\..\DatabaseConnection.txt").Last();
            ConnectionDB = ConnectionDB.Replace(@"\\", @"\").Trim(new char[] { '\"'});
            optionsBuilder.UseSqlServer(ConnectionDB);
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

            
            modelBuilder.Entity<Activity>()
                .HasOne<Request>(r => r.request)
                .WithMany(a => a.activities)
                .HasForeignKey(r => r.requestId);


            modelBuilder.Entity<Activity>()
                .HasOne<Personel>(p => p.personel)
                .WithMany(a => a.Activities)
                .HasForeignKey(p => p.personelId);

            modelBuilder.Entity<Activity>()
                .HasOne<ActivityDictionary>(aD => aD.activityDictionary)
                .WithMany(a => a.Activities)
                .HasForeignKey(aD => aD.activityType);
        }
    }
}
