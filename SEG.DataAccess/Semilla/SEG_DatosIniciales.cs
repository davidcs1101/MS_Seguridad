using Microsoft.EntityFrameworkCore;
using SEG.Dominio.Entidades;

namespace SEG.DataAccess.Semilla
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

            builder.Entity<SEG_Accion>().HasData(
                new SEG_Accion { Id = 1, Codigo = "CONSULTAR", Nombre = "CONSULTAR", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Accion { Id = 2, Codigo = "CREAR", Nombre = "CREAR", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Accion { Id = 3, Codigo = "MODIFICAR", Nombre = "MODIFICAR", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Accion { Id = 4, Codigo = "ELIMINAR", Nombre = "ELIMINAR", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Accion { Id = 5, Codigo = "LISTAR", Nombre = "LISTAR", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now }
                );

            builder.Entity<SEG_Programa>().HasData(
                new SEG_Programa { Id = 1, Codigo = "GRUPOS", Nombre = "MAESTRO DE GRUPOS", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Programa { Id = 2, Codigo = "CONTRARREFERENCIA", Nombre = "CONTRARREFERENCIA A PACIENTES", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Programa { Id = 3, Codigo = "MEDICOSSEDES", Nombre = "MEDICOS POR SEDE", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Programa { Id = 4, Codigo = "REFERENCIA", Nombre = "REFERENCIA A PACIENTES", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Programa { Id = 5, Codigo = "EMPRESAS", Nombre = "EMPRESAS", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Programa { Id = 6, Codigo = "SEDES", Nombre = "SEDES", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Programa { Id = 7, Codigo = "LISTAS", Nombre = "MAESTRO DE LISTAS", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Programa { Id = 8, Codigo = "DATOSCONSTANTES", Nombre = "MAESTRO DE DATOS CONSTANTES", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Programa { Id = 9, Codigo = "USUARIOSSEDESGRUPOS", Nombre = "ASOCIACION DE USUARIOS SEDES GRUPOS", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Programa { Id = 10, Codigo = "PROGRAMAS", Nombre = "MAESTRO DE PROGRAMAS", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Programa { Id = 11, Codigo = "GRUPOSPROGRAMAS", Nombre = "MAESTRO DE PROGRAMAS POR GRUPO", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Programa { Id = 12, Codigo = "USUARIOS", Nombre = "MAESTRO DE USUARIOS", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now }
                );

            builder.Entity<SEG_Permiso>().HasData(
                new SEG_Permiso { Id = 1, ProgramaId = 1, AccionId = 1, Codigo = "GRUPOS.CONSULTAR", Nombre = "CONSULTAR GRUPOS", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Permiso { Id = 2, ProgramaId = 1, AccionId = 2, Codigo = "GRUPOS.CREAR", Nombre = "CREAR GRUPOS", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Permiso { Id = 3, ProgramaId = 1, AccionId = 3, Codigo = "GRUPOS.MODIFICAR", Nombre = "MODIFICAR GRUPOS", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Permiso { Id = 4, ProgramaId = 1, AccionId = 4, Codigo = "GRUPOS.ELIMINAR", Nombre = "ELIMINAR GRUPOS", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Permiso { Id = 5, ProgramaId = 1, AccionId = 5, Codigo = "GRUPOS.LISTAR", Nombre = "LISTAR GRUPOS", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now }
                );

            builder.Entity<SEG_GrupoPermiso>().HasData(
                new SEG_GrupoPermiso { Id = 1, GrupoId = 1, PermisoId = 1, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_GrupoPermiso { Id = 2, GrupoId = 1, PermisoId = 2, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_GrupoPermiso { Id = 3, GrupoId = 1, PermisoId = 3, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_GrupoPermiso { Id = 4, GrupoId = 1, PermisoId = 4, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_GrupoPermiso { Id = 5, GrupoId = 1, PermisoId = 5, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now }
                );

            builder.Entity<SEG_UsuarioSedeGrupo>().HasData(
                new SEG_UsuarioSedeGrupo { Id = 1, UsuarioId = 1, SedeId = 1, GrupoId = 1, UsuarioCreadorId = 1, FechaCreado = DateTime.Now, EstadoActivo = true }
                );
        }
    }
}
