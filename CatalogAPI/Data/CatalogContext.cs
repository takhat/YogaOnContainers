using CatalogAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogAPI.Data
{
    public class CatalogContext : DbContext
    {
        //Dependency injection from Startup.cs to define location of db
        public CatalogContext(DbContextOptions options) : base(options)
        {
            
        }

        //Define Tables - "What"/Which classes need to be converted into tables
        public DbSet<CatalogType> CatalogTypes { get; set; }
        public DbSet<CatalogItem> CatalogItems { get; set; }

        //Define Rules and Methods - "How" to build tables 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CatalogType>(e => 
            {
                e.ToTable("CatalogTypes");
                e.Property(t => t.Id)
                .IsRequired()
                .UseHiLo("catalog_type_hilo");
                e.Property(t => t.Type)
                .IsRequired()
                .HasMaxLength(100);
            });

            modelBuilder.Entity<CatalogItem>(e =>
            {
                e.ToTable("Catalog");
                e.Property(c => c.Id)
                   .IsRequired()
                   .UseHiLo("catalog_hilo");
                e.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
                e.Property(c => c.Timings)
                .IsRequired()
                .HasMaxLength(100);
                e.Property(c => c.Price)
                .IsRequired();
                e.HasOne(c => c.CatalogType)
                .WithMany()
                .HasForeignKey(c => c.CatalogTypeId);
            });
        }

        //Define "Where" to create the database in: Startup.cs
        
    }   
}
