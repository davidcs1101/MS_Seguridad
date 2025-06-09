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
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.ApplyConfiguration(new SEG_UsuarioConfig());
            modelBuilder.ApplyConfiguration(new SEG_GrupoConfig());
            modelBuilder.ApplyConfiguration(new SEG_ProgramaConfig());
            modelBuilder.ApplyConfiguration(new SEG_GrupoProgramaConfig());
            modelBuilder.ApplyConfiguration(new SEG_UsuarioSedeGrupoConfig());

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
