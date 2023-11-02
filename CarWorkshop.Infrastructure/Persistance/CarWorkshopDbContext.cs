﻿using CarWorkshop.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarWorkshop.Infrastructure.Persistance
{

    public class CarWorkshopDbContext : IdentityDbContext
    {
        public CarWorkshopDbContext(DbContextOptions<CarWorkshopDbContext> options) : base(options)
        {
        }
        public DbSet<Domain.Entities.CarWorkshop> CarWorkshops { get; set; }
        public DbSet<CarWorkshopService> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Domain.Entities.CarWorkshop>()
                 .OwnsOne(c => c.ContactDetails);
            modelBuilder.Entity<Domain.Entities.CarWorkshop>()
                .HasMany(c => c.Services)
                .WithOne(s => s.CarWorkshop)
                .HasForeignKey(s => s.CarWorkshopId);
        }
    }
}
