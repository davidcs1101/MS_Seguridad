using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SEG.Dominio.Entidades;

namespace SEG.DataAccess.EntidadesConfig
{
    public class SEG_ParametroExternoConfig : IEntityTypeConfiguration<SEG_ParametroExterno>
    {
        public void Configure(EntityTypeBuilder<SEG_ParametroExterno> builder) 
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Origen).HasColumnType("varchar(30)");
            builder.Property(x => x.CodigoCatalogo).HasColumnType("varchar(30)");
            builder.Property(x => x.Codigo).HasColumnType("varchar(30)");
            builder.Property(x => x.Nombre).HasColumnType("varchar(250)");
            builder.Property(x => x.FechaCreado).HasColumnType("datetime");
            builder.Property(x => x.FechaModificado).HasColumnType("datetime");

            builder.HasIndex(x => new { x.Origen, x.CodigoCatalogo, x.OrigenId }).IsUnique();

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
