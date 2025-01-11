using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEG.Dominio.Entidades.Configuraciones
{
    public class SEG_UsuarioConfig : IEntityTypeConfiguration<SEG_Usuario>
    {
        public void Configure(EntityTypeBuilder<SEG_Usuario> builder) 
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Identificacion).HasColumnType("varchar(20)");
            builder.Property(x => x.Nombre1).HasColumnType("varchar(150)");
            builder.Property(x => x.Nombre2).HasColumnType("varchar(25)");
            builder.Property(x => x.Apellido1).HasColumnType("varchar(25)");
            builder.Property(x => x.Apellido2).HasColumnType("varchar(25)");
            builder.Property(x => x.Email).HasColumnType("varchar(150)");

            builder.Property(x => x.NombreUsuario).HasColumnType("varchar(60)");
            builder.Property(x => x.Clave).HasColumnType("varchar(50)");

            builder.HasIndex(x => new { x.TipoIdentificacionId, x.Identificacion }).IsUnique();
            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.NombreUsuario).IsUnique();

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
