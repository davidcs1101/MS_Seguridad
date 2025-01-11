using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEG.Dominio.Entidades.Semilla
{
    public class SEG_DatosIniciales
    {
        public SEG_DatosIniciales() { }
        public static void Seed(ModelBuilder builder) 
        {

            builder.Entity<SEG_Usuario>().HasData(
                new SEG_Usuario { Id = 1, TipoIdentificacionId = 1, Identificacion = "ADMINISTRADOR", Nombre1 = "ADMINISTRADOR", Nombre2 = "", Apellido1 = "SISTEMA", Apellido2 = "", Email = "CORREO@GMAIL.COM", NombreUsuario = "ADMINISTRADOR", Clave = "1feTCdMwhKKkOSWaM5+yXEI0ZrBPlq9pbnB4k4+JRUU=", CambiarClave = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now, EstadoActivo = true }
                );

            builder.Entity<SEG_Grupo>().HasData(
                new SEG_Grupo { Id = 1, Codigo = "ADMINISTRADORSISTEMA", Nombre = "ADMINISTRADOR SISTEMA", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Grupo { Id = 2, Codigo = "ADMINISTRADOREMPRESA", Nombre = "ADMINISTRADOR DE EMPRESA", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Grupo { Id = 3, Codigo = "ADMINISTRADORSEDE", Nombre = "ADMINISTRADOR DE SEDE", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Grupo { Id = 4, Codigo = "CONTRARREFERENCIA", Nombre = "USUARIOS DE CONTRARREFERENCIA", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Grupo { Id = 5, Codigo = "REFERENCIA", Nombre = "USUARIOS DE REFERENCIA", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Grupo { Id = 6, Codigo = "REFERENCIAYCONTRARREFERENCIA", Nombre = "USUARIOS DE REFERENCIA Y CONTRARREFERENCIA", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now }
                );

            builder.Entity<SEG_Programa>().HasData(
                new SEG_Programa { Id = 1, Codigo = "USUARIOSSEDESGRUPOS", Nombre = "ASOCIACION DE USUARIOS SEDES GRUPOS", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Programa { Id = 2, Codigo = "CONTRARREFERENCIA", Nombre = "CONTRARREFERENCIA A PACIENTES", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Programa { Id = 3, Codigo = "MEDICOSSEDES", Nombre = "MEDICOS POR SEDE", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Programa { Id = 4, Codigo = "REFERENCIA", Nombre = "REFERENCIA A PACIENTES", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Programa { Id = 5, Codigo = "EMPRESAS", Nombre = "EMPRESAS", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Programa { Id = 6, Codigo = "SEDES", Nombre = "SEDES", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Programa { Id = 7, Codigo = "LISTAS", Nombre = "MAESTRO DE LISTAS", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Programa { Id = 8, Codigo = "DATOSCONSTANTES", Nombre = "MAESTRO DE DATOS CONSTANTES", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Programa { Id = 9, Codigo = "GRUPOS", Nombre = "MAESTRO DE GRUPOS", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Programa { Id = 10, Codigo = "PROGRAMAS", Nombre = "MAESTRO DE PROGRAMAS", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Programa { Id = 11, Codigo = "GRUPOSPROGRAMAS", Nombre = "MAESTRO DE PROGRAMAS POR GRUPO", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Programa { Id = 12, Codigo = "USUARIOS", Nombre = "MAESTRO DE USUARIOS", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now }
                );

            builder.Entity<SEG_GrupoPrograma>().HasData(
                new SEG_GrupoPrograma { Id = 1, GrupoId = 1, ProgramaId = 1, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_GrupoPrograma { Id = 2, GrupoId = 1, ProgramaId = 2, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_GrupoPrograma { Id = 3, GrupoId = 1, ProgramaId = 3, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_GrupoPrograma { Id = 4, GrupoId = 1, ProgramaId = 4, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_GrupoPrograma { Id = 5, GrupoId = 1, ProgramaId = 5, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_GrupoPrograma { Id = 6, GrupoId = 1, ProgramaId = 6, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_GrupoPrograma { Id = 7, GrupoId = 1, ProgramaId = 7, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_GrupoPrograma { Id = 8, GrupoId = 1, ProgramaId = 8, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_GrupoPrograma { Id = 9, GrupoId = 1, ProgramaId = 9, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_GrupoPrograma { Id = 10, GrupoId = 1, ProgramaId = 10, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_GrupoPrograma { Id = 11, GrupoId = 1, ProgramaId = 11, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_GrupoPrograma { Id = 12, GrupoId = 1, ProgramaId = 12, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now }
                );

            builder.Entity<SEG_UsuarioSedeGrupo>().HasData(
                new SEG_UsuarioSedeGrupo { Id = 1, UsuarioId = 1, SedeId = 1, GrupoId = 1, UsuarioCreadorId = 1, FechaCreado = DateTime.Now, EstadoActivo = true }
                );
        }
    }
}
