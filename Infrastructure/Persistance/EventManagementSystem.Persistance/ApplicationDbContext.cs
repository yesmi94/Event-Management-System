// <copyright file="ApplicationDbContext.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace EventManagementSystem.Persistance
{
    using EventManagementSystem.Domain.Entities;
    using EventManagementSystem.Persistance.Configurations;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<EventRegistration> Registrations => this.Set<EventRegistration>();

        public DbSet<Event> Events => this.Set<Event>();

        public DbSet<EventImage> EventImages => this.Set<EventImage>();

        public DbSet<User> Users => this.Set<User>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = config.GetConnectionString("DefaultConnection");

                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EventConfiguration());
            modelBuilder.ApplyConfiguration(new EventImageConfiguration());
            modelBuilder.ApplyConfiguration(new EventRegistrationConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
