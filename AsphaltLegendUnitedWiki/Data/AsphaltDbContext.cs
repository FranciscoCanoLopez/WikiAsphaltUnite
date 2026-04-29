using Microsoft.EntityFrameworkCore;
using AsphaltLegendUnitedWiki.Models;

namespace AsphaltLegendUnitedWiki.Data
{
    public class AsphaltDbContext : DbContext
    {
        public AsphaltDbContext(DbContextOptions<AsphaltDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Auto> Autos { get; set; }
        public DbSet<Ubicacion> Ubicaciones { get; set; }
        public DbSet<Pista> Pistas { get; set; }
        public DbSet<MetaGuide> MetaGuides { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración para que los Enums se guarden como texto en SQL Server
            modelBuilder.Entity<Usuario>()
                .Property(u => u.rol)
                .HasConversion<string>()
                .HasMaxLength(20); // Es buena práctica definir un largo máximo

            modelBuilder.Entity<Auto>()
                .Property(a => a.Clase)
                .HasConversion<string>()
                .HasMaxLength(5);

            modelBuilder.Entity<Auto>()
                .Property(a => a.TipoManejo)
                .HasConversion<string>()
                .HasMaxLength(30);

            // Precisión para SQL Server en decimales
            modelBuilder.Entity<Pista>()
                .Property(p => p.Longitud)
                .HasColumnType("decimal(5,2)");

            // Relaciones y eliminaciones en cascada
            modelBuilder.Entity<MetaGuide>()
                .HasOne(m => m.Auto)
                .WithMany(a => a.MetaGuides)
                .HasForeignKey(m => m.AutoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}