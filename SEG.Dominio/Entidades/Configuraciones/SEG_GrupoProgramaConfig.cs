using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEG.Dominio.Entidades.Configuraciones
{
    public class SEG_GrupoProgramaConfig : IEntityTypeConfiguration<SEG_GrupoPrograma>
    {
        public void Configure(EntityTypeBuilder<SEG_GrupoPrograma> builder) 
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => new { x.GrupoId, x.ProgramaId }).IsUnique();

            builder.HasOne(x => x.Grupo)
                .WithMany(x => x.GruposProgramas)
                .HasForeignKey(x => x.GrupoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Programa)
                .WithMany(x => x.GruposProgramas)
                .HasForeignKey(x => x.ProgramaId)
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
