using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KakarazaZoohop.Model
{
    public class ZooShopContext : DbContext, IZooShopContext
    {
        private readonly IConnectionOptions _options;

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Owner> Owners { get; set; }

        public DbSet<VisitRecord> VisitRecords { get; set; }

        public ZooShopContext()
        {
        }

        public ZooShopContext(IConnectionOptions options)
        {
            _options = options;
            ChangeTracker.AutoDetectChangesEnabled = true;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_options is not null)
            {
                optionsBuilder.UseSqlServer(_options.ConnectionString);
            }
            else
            {
                ConnectionOptions designTimeOptions = new();

                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .Build();

                designTimeOptions.ConnectionString = configuration
                    .GetSection("ConnectionOptions:ConnectionString")
                    .Value;

                optionsBuilder.UseSqlServer(designTimeOptions.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
