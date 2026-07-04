using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SEG.Dominio.Entidades;

namespace SEG.DataAccess.EntidadesConfig
{
    public class SEG_GrupoPermisoConfig : IEntityTypeConfiguration<SEG_GrupoPermiso>
    {
        public void Configure(EntityTypeBuilder<SEG_GrupoPermiso> builder) 
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => new { x.GrupoId, x.PermisoId }).IsUnique();

            builder.HasOne(x => x.Grupo)
                .WithMany(x => x.GruposPermisos)
                .HasForeignKey(x => x.GrupoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Permiso)
                .WithMany(x => x.GruposPermisos)
                .HasForeignKey(x => x.PermisoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.UsuarioCreador)
                .WithMany()
                .HasForeignKey(x => x.UsuarioCreadorId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.UsuarioModificador)
                .WithMany()
                .HasForeignKey(x => x.UsuarioModificadorId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
