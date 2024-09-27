using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRegistrationApp.Models;

namespace UserRegistrationApp.Data
{
    public class RegistrationDbContext : DbContext
    {
        public RegistrationDbContext(DbContextOptions<RegistrationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring the User entity
            modelBuilder.Entity<User>()
                .ToTable("tblUserRegistration")
                .HasKey(u => u.Id);

            modelBuilder.Entity<User>()
                .Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .Property(u => u.Phone)
                .IsRequired()
                .HasMaxLength(15);

            modelBuilder.Entity<User>()
                .Property(u => u.Address)
                .HasMaxLength(250);

            // Configuring the State entity
            modelBuilder.Entity<State>()
                .ToTable("tblState")
                .HasKey(s => s.Id);

            modelBuilder.Entity<State>()
                .Property(s => s.StateName)
                .IsRequired()
                .HasMaxLength(50);

            // Configuring the City entity
            modelBuilder.Entity<City>()
                .ToTable("tblCity")
                .HasKey(c => c.Id);

            modelBuilder.Entity<City>()
                .Property(c => c.CityName)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<City>()
                .HasOne<State>()
                .WithMany()
                .HasForeignKey(c => c.StateId);
        }
    }
}
