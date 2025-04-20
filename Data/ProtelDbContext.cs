using Microsoft.EntityFrameworkCore;

namespace ProtelAppT.Data
{
    public class ProtelDbContext : DbContext
    {
        public ProtelDbContext(DbContextOptions<ProtelDbContext> options) : base(options)
        {
        }

        // Definir los dbsets para cada una de las tablas de la base de datos
        // los dbset son propiedades que representan las tablas de la base de datos

        public DbSet<Cliente> CLIENTE { get; set; }
        public DbSet<Factibilidad> FACTIBILIDAD{ get; set; }
        public DbSet<Usuario> USUARIO { get; set; }
        public DbSet<Rol> ROL { get; set; }
        public DbSet<EstadoCliente> ESTADOCLIENTE { get; set; }
        public DbSet<EstadoFactibilidad> ESTADOFACTIBILIDAD { get; set; }
        public DbSet<Autorizacion> AUTORIZACION { get; set; }
        public DbSet<AsignacionRolAutorizacion> ASIGNACIONROLAUTORIZACION { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().HasOne(c => c.EstadoCliente).WithMany(e => e.Clientes).HasForeignKey(c => c.IdEstadoCliente);
        }
    }
}
