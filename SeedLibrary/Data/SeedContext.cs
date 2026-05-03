using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SeedLibrary.Models;

namespace SeedLibrary.Data
{
    public class SeedPacketContext : DbContext
    {
        public SeedPacketContext (DbContextOptions<SeedPacketContext> options)
            : base(options)
        {
        }

        public DbSet<SeedPacket> SeedPackets => Set<SeedPacket>();
        public DbSet<Variety> Varieties => Set<Variety>();
        public DbSet<CommonName> CommonNames => Set<CommonName>();
        public DbSet<Source> Sources => Set<Source>();
        public DbSet<Date> Dates => Set<Date>();
        public DbSet<Donation> Donations => Set<Donation>();
        public DbSet<Growing> Growings => Set<Growing>();
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SeedPacket>().ToTable("SeedPacket")
                .HasKey(s => s.SeedId);
            modelBuilder.Entity<Variety>().ToTable("Variety")
                .HasKey(v => v.VarietyName);
            modelBuilder.Entity<CommonName>().ToTable("CommonName")
                .HasKey(c => c.Name);
            modelBuilder.Entity<Source>().ToTable("Source")
                .HasKey(s => s.Id);
            modelBuilder.Entity<Date>().ToTable("Date")
                .HasKey(d => d.Id);
            modelBuilder.Entity<Donation>().ToTable("Donation")
                .HasKey(d => new { d.SourceId, d.SeedId, d.Year });
            modelBuilder.Entity<Growing>().ToTable("Growing")
                .HasKey(g => new { g.DatesId, g.SeedId });
            modelBuilder.Entity<SeedPacket>()
                .HasOne(s => s.Variety)
                .WithMany(v => v.SeedPackets)
                .HasForeignKey(s => s.VarietyName);
            modelBuilder.Entity<Variety>()
                .HasOne(v => v.CommonName)
                .WithMany(c => c.Varieties)
                .HasForeignKey(v => v.CommonNameName);
            
            modelBuilder.Entity<Donation>()
                .HasOne(d => d.Source)
                .WithMany(s => s.Donations)
                .HasForeignKey(d => d.SourceId);

            modelBuilder.Entity<Donation>()
                .HasOne(d => d.SeedPacket)
                .WithMany(s => s.Donations)
                .HasForeignKey(d => d.SeedId);

            modelBuilder.Entity<Growing>()
                .HasOne(g => g.Date)
                .WithMany(d => d.Growings)
                .HasForeignKey(g => g.DatesId);

            modelBuilder.Entity<Growing>()
                .HasOne(g => g.SeedPacket)
                .WithMany(s => s.Growings)
                .HasForeignKey(g => g.SeedId);
            
            
        }
    }
}
