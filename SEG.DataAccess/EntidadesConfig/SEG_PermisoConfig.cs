using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SEG.Dominio.Entidades;
namespace SEG.DataAccess.EntidadesConfig
{
    public class SEG_PermisoConfig : IEntityTypeConfiguration<SEG_Permiso>
    {
        public void Configure(EntityTypeBuilder<SEG_Permiso> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Codigo)
                .HasColumnType("varchar(30)")
                .HasComment("Código único del permiso.");

            builder.Property(x => x.Nombre)
                .HasColumnType("varchar(250)")
                .HasComment("Nombre descriptivo del permiso.");

            builder.Property(x => x.FechaCreado).HasColumnType("datetime");
            builder.Property(x => x.FechaModificado).HasColumnType("datetime");

            builder.HasOne(x => x.Programa)
                .WithMany(x => x.Permisos)
                .HasForeignKey(x => x.ProgramaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Accion)
                .WithMany(x => x.Permisos)
                .HasForeignKey(x => x.AccionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.GruposPermisos)
                .WithOne(x => x.Permiso)
                .HasForeignKey(x => x.PermisoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.Codigo)
                .IsUnique();

            builder.HasIndex(x => new
            {
                x.ProgramaId,
                x.AccionId
            }).IsUnique();
        }
    }
}
