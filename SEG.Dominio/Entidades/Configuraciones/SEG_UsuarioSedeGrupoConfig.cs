using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEG.Dominio.Entidades.Configuraciones
{
    public class SEG_UsuarioSedeGrupoConfig : IEntityTypeConfiguration<SEG_UsuarioSedeGrupo>
    {
        public void Configure(EntityTypeBuilder<SEG_UsuarioSedeGrupo> builder) 
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => new { x.UsuarioId, x.SedeId }).IsUnique();

            builder.HasOne(x => x.Grupo)
                .WithMany(x => x.UsuariosSedes)
                .HasForeignKey(x => x.GrupoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Usuario)
                .WithMany(x => x.UsuariosSedes)
                .HasForeignKey(x => x.UsuarioId)
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
