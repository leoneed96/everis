using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data
{
    public class DataContext: DbContext
    {
        public DataContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=everis;Username=postgres;Password=password");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Operator>(e =>
            {
                e.HasKey(x => x.ID);
                e.Property(x => x.Title).IsRequired();
            });
        }
        public DbSet<Operator> Operators { get; set; }
    }
}
