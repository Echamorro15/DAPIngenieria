using DAPIngenieria.Models;
using Microsoft.EntityFrameworkCore;

namespace DAPIngenieria.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Tablas del modelo
        public DbSet<Cliente> Cliente { get; set; }       // Tabla Cliente
        public DbSet<Usuario> Usuarios { get; set; }       // Tabla Usuario
        public DbSet<TipoServicios> TipoServicios { get; set; } // Tabla TipoServicios
        public DbSet<Empleado> Empleados { get; set; } //Tabla Empleado
        public DbSet<Presupuesto> Presupuestos { get; set; } //Tabla Presupuesto
        public DbSet<Servicio> Servicios { get; set; } //Tabla Servicios
     

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Presupuesto>()
                  .HasOne(p => p.Cliente)
                  .WithMany()
                  .HasForeignKey(p => p.IdCliente)
                  .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Presupuesto>()
                .HasOne(p => p.TipoServicio)
                .WithMany()
                .HasForeignKey(p => p.IdTipoServicio)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Servicio>()
                .HasOne(s => s.Cliente)
                .WithMany()
                .HasForeignKey(s => s.IdCliente);


            // Configuración de la relación entre Servicio y Cliente
            modelBuilder.Entity<Servicio>()
                .HasOne(s => s.Cliente) // Relación con Cliente
                .WithMany(c => c.Servicios) // Cliente tiene múltiples Servicios
                .HasForeignKey(s => s.IdCliente) // Clave foránea en Servicio
                .OnDelete(DeleteBehavior.Restrict); // Restringir eliminación

            // Configuración de la relación entre Servicio y OrdenTrabajo
            modelBuilder.Entity<Servicio>()
                .HasOne(s => s.OrdenTrabajo) // Relación con OrdenTrabajo
                .WithMany(o => o.Servicios) // OrdenTrabajo tiene múltiples Servicios
                .HasForeignKey(s => s.IdOrden) // Clave foránea en Servicio
                .OnDelete(DeleteBehavior.Restrict); // Restringir eliminación

            // Configuración de la relación entre Servicio y TipoServicios
            modelBuilder.Entity<Servicio>()
                .HasOne(s => s.TipoServicio) // Relación con TipoServicios
                .WithMany(t => t.Servicios) // TipoServicios tiene múltiples Servicios
                .HasForeignKey(s => s.IdTipoServicio) // Clave foránea en Servicio
                .OnDelete(DeleteBehavior.Restrict); // Restringir eliminación

            base.OnModelCreating(modelBuilder); // Llamar al método base










        }
        public DbSet<DAPIngenieria.Models.OrdenTrabajo> OrdenTrabajo { get; set; } = default!;
        public DbSet<DAPIngenieria.Models.Servicio> Servicio { get; set; } = default!;
     
        
    }
}



