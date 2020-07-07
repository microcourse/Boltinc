using System;

using App.Core.Data.Map;
using Microsoft.EntityFrameworkCore;

namespace App.Core.Data
{
    public class AppDbContext : DbContext
    {
     //   public DbSet<Device> Devices { get; set; }

        public AppDbContext()
        {
           
        }

        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SearchItemMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
