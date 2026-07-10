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
                new SEG_Accion { Id = 5, Codigo = "LISTAR", Nombre = "LISTAR", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Accion { Id = 6, Codigo = "CREARCONSEDE", Nombre = "CREAR CON SEDE", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Accion { Id = 7, Codigo = "CREARCONGRUPO", Nombre = "CREAR CON GRUPO", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now }
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
                new SEG_Programa { Id = 11, Codigo = "USUARIOS", Nombre = "MAESTRO DE USUARIOS", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now }
                );

            builder.Entity<SEG_Permiso>().HasData(
                new SEG_Permiso { Id = 1, ProgramaId = 1, AccionId = 1, Codigo = "GRUPOS.CONSULTAR", Nombre = "CONSULTAR GRUPOS", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Permiso { Id = 2, ProgramaId = 1, AccionId = 2, Codigo = "GRUPOS.CREAR", Nombre = "CREAR GRUPOS", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Permiso { Id = 3, ProgramaId = 1, AccionId = 3, Codigo = "GRUPOS.MODIFICAR", Nombre = "MODIFICAR GRUPOS", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Permiso { Id = 4, ProgramaId = 1, AccionId = 4, Codigo = "GRUPOS.ELIMINAR", Nombre = "ELIMINAR GRUPOS", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Permiso { Id = 5, ProgramaId = 1, AccionId = 5, Codigo = "GRUPOS.LISTAR", Nombre = "LISTAR GRUPOS", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },

                new SEG_Permiso { Id = 6, ProgramaId = 10, AccionId = 1, Codigo = "PROGRAMAS.CONSULTAR", Nombre = "CONSULTAR PROGRAMAS", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Permiso { Id = 7, ProgramaId = 10, AccionId = 2, Codigo = "PROGRAMAS.CREAR", Nombre = "CREAR PROGRAMAS", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Permiso { Id = 8, ProgramaId = 10, AccionId = 3, Codigo = "PROGRAMAS.MODIFICAR", Nombre = "MODIFICAR PROGRAMAS", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Permiso { Id = 9, ProgramaId = 10, AccionId = 4, Codigo = "PROGRAMAS.ELIMINAR", Nombre = "ELIMINAR PROGRAMAS", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Permiso { Id = 10, ProgramaId = 10, AccionId = 5, Codigo = "PROGRAMAS.LISTAR", Nombre = "LISTAR PROGRAMAS", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },

                new SEG_Permiso { Id = 11, ProgramaId = 11, AccionId = 6, Codigo = "USUARIOS.CREARCONSEDE", Nombre = "CREAR USUARIOS CON SEDE", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Permiso { Id = 12, ProgramaId = 11, AccionId = 7, Codigo = "USUARIOS.CREARCONGRUPO", Nombre = "CREAR USUARIOS CON GRUPO", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Permiso { Id = 13, ProgramaId = 11, AccionId = 1, Codigo = "USUARIOS.CONSULTAR", Nombre = "CONSULTAR USUARIOS", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Permiso { Id = 14, ProgramaId = 11, AccionId = 5, Codigo = "USUARIOS.LISTAR", Nombre = "LISTAR USUARIOS", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },

                new SEG_Permiso { Id = 15, ProgramaId = 9, AccionId = 2, Codigo = "USUARIOSSEDESGRUPOS.CREAR", Nombre = "CREAR USUARIOS SEDES GRUPOS", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Permiso { Id = 16, ProgramaId = 9, AccionId = 3, Codigo = "USUARIOSSEDESGRUPOS.MODIFICAR", Nombre = "MODIFICAR USUARIOS SEDES GRUPOS", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_Permiso { Id = 17, ProgramaId = 9, AccionId = 4, Codigo = "USUARIOSSEDESGRUPOS.ELIMINAR", Nombre = "ELIMINAR USUARIOS SEDES GRUPOS", EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now }
                );

            builder.Entity<SEG_GrupoPermiso>().HasData(
                new SEG_GrupoPermiso { Id = 1, GrupoId = 1, PermisoId = 1, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_GrupoPermiso { Id = 2, GrupoId = 1, PermisoId = 2, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_GrupoPermiso { Id = 3, GrupoId = 1, PermisoId = 3, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_GrupoPermiso { Id = 4, GrupoId = 1, PermisoId = 4, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_GrupoPermiso { Id = 5, GrupoId = 1, PermisoId = 5, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_GrupoPermiso { Id = 6, GrupoId = 1, PermisoId = 6, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_GrupoPermiso { Id = 7, GrupoId = 1, PermisoId = 7, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_GrupoPermiso { Id = 8, GrupoId = 1, PermisoId = 8, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_GrupoPermiso { Id = 9, GrupoId = 1, PermisoId = 9, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_GrupoPermiso { Id = 10, GrupoId = 1, PermisoId = 10, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_GrupoPermiso { Id = 11, GrupoId = 1, PermisoId = 11, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_GrupoPermiso { Id = 12, GrupoId = 1, PermisoId = 12, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_GrupoPermiso { Id = 13, GrupoId = 1, PermisoId = 13, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_GrupoPermiso { Id = 14, GrupoId = 1, PermisoId = 14, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_GrupoPermiso { Id = 15, GrupoId = 1, PermisoId = 15, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_GrupoPermiso { Id = 16, GrupoId = 1, PermisoId = 16, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now },
                new SEG_GrupoPermiso { Id = 17, GrupoId = 1, PermisoId = 17, EstadoActivo = true, UsuarioCreadorId = 1, FechaCreado = DateTime.Now }
                );

            builder.Entity<SEG_UsuarioSedeGrupo>().HasData(
                new SEG_UsuarioSedeGrupo { Id = 1, UsuarioId = 1, SedeId = 1, GrupoId = 1, UsuarioCreadorId = 1, FechaCreado = DateTime.Now, EstadoActivo = true }
                );
        }
    }
}
