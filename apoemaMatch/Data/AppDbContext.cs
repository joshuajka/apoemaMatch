using apoemaMatch.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apoemaMatch.Data
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DemandaSolucionador>().HasKey(am => new
            {
                am.DemandaId,
                am.SolucionadorId
            });

            modelBuilder.Entity<DemandaSolucionador>().HasOne(m => m.Demanda).WithMany(am => am.DemandaSolucionador)
                .HasForeignKey(m => m.DemandaId);
            modelBuilder.Entity<DemandaSolucionador>().HasOne(m => m.Solucionador).WithMany(am => am.DemandaSolucionador)
                .HasForeignKey(m => m.SolucionadorId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Demanda> Demandas { get; set; }
        public DbSet<Solucionador> Solucionadores { get; set; }
        public DbSet<DemandaSolucionador> DemandasSolucionadores { get; set; }


    }
}
