using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SeedLibrary.Models;

namespace SeedLibrary.Data
{
    public class SeedContext : IdentityDbContext
    {
        public SeedContext (DbContextOptions<SeedContext> options)
            : base(options)
        {
        }

        public DbSet<Seed> Seeds{get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Seed>().ToTable("Seed");
        }
    }
}
