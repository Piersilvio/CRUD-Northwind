
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace DBLayer.Models
{
    public partial class NorthwindDefContext : DbContext
    {
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Orderr> Orderr { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Shipper> Shipper { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }

        private IConfiguration Configuration { get; }
        private IConfigurationBuilder ConfigurationBuilder { get; }
        private readonly string? connString;


        public NorthwindDefContext()
        {
            var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, false);

            Configuration = configurationBuilder.Build();
            connString = Configuration.GetConnectionString("MyConn");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(connString)
                .EnableSensitiveDataLogging()
                /*.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)**/;
            ;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}