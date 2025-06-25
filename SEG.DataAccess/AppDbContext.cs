using Microsoft.EntityFrameworkCore;
using SEG.Dominio.Entidades;
using SEG.DataAccess.EntidadesConfig;
using SEG.DataAccess.Semilla;

namespace SEG.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            SEG_DatosIniciales.Seed(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder){}

        public DbSet<SEG_Usuario> SEG_Usuarios { get; set; }
        public DbSet<SEG_Grupo> SEG_Grupos { get; set; }
        public DbSet<SEG_Programa> SEG_Programas { get; set; }
        public DbSet<SEG_GrupoPrograma> SEG_GruposProgramas { get; set; }
        public DbSet<SEG_UsuarioSedeGrupo> SEG_UsuariosSedesGrupos { get; set; }

        public DbSet<SEG_ColaSolicitud> SEG_ColaSolicitudes { get; set; }
    }
}
