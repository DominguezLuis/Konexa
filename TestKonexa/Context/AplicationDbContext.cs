using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestKonexa.Models;

namespace TestKonexa.Context
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Zona> Zona { get; set; }
        public DbSet<Sucursal> Sucursal { get; set; }
        public DbSet<ResponsableZonal> ResponsableZonal { get; set; }
        public DbSet<ZonaSucursal> ZonaSucursal { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ZonaSucursal>()
            .HasKey(c => new { c.IdSucursal, c.IdZona });

        }
    }
}
