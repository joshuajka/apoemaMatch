using apoemaMatch.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace apoemaMatch.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EncomendaSolucionador>().HasKey(es => new
            {
                es.EncomendaId,
                es.SolucionadorId
            });

            modelBuilder.Entity<EncomendaSolucionador>().HasOne(e => e.Encomenda).WithMany(es => es.EncomendaSolucionador)
                .HasForeignKey(e => e.EncomendaId);
            modelBuilder.Entity<EncomendaSolucionador>().HasOne(s => s.Solucionador).WithMany(es => es.EncomendaSolucionador)
                .HasForeignKey(s => s.SolucionadorId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Demandante> Demandantes { get; set; }
        public DbSet<Solucionador> Solucionadores { get; set; }
        public DbSet<EncomendaSolucionador> EncomendasSolucionadores { get; set; }

        public DbSet<Encomenda> Encomendas { get; set; }

    }
}
