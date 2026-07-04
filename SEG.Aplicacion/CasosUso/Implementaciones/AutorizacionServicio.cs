using SEG.Aplicacion.CasosUso.Interfaces;
using SEG.Dominio.Entidades;
using SEG.Dominio.Repositorio;
using SEG.Dominio.Servicios.Implementaciones;
using SEG.Dominio.Servicios.Interfaces;
using Utilidades;
namespace SEG.Aplicacion.CasosUso.Implementaciones
{
    public class AutorizacionServicio : IAutorizacionServicio
    {
        private readonly IGrupoRepositorio _grupoRepositorio;
        private readonly IPermisoRepositorio _permisoRepositorio;
        private readonly IEntidadValidador<SEG_Permiso> _permisoValidador;
        private readonly IGrupoPermisoRepositorio _grupoPermisoRepositorio;
        private readonly IEntidadValidador<SEG_GrupoPermiso> _grupoPermisoValidador;

        public AutorizacionServicio(IGrupoRepositorio grupoRepositorio, IPermisoRepositorio permisoRepositorio, IGrupoPermisoRepositorio grupoPermisoRepositorio, IEntidadValidador<SEG_Permiso> permisoValidador, IEntidadValidador<SEG_GrupoPermiso> grupoPermisoValidador)
        {
            _grupoRepositorio = grupoRepositorio;
            _permisoRepositorio = permisoRepositorio;
            _grupoPermisoRepositorio = grupoPermisoRepositorio;
            _permisoValidador = permisoValidador;
            _grupoPermisoValidador = grupoPermisoValidador;
        }

        public async Task<bool> TienePermisoAsync(int grupoId, string codigoPermiso)
        {
            var permisoExiste = await _permisoRepositorio.ObtenerPorCodigoAsync(codigoPermiso);
            _permisoValidador.ValidarDatoNoEncontrado(permisoExiste, Textos.Permisos.MENSAJE_PERMISO_NO_EXISTE_CODIGO(codigoPermiso));
            _permisoValidador.ValidarDatoActivo(permisoExiste!.EstadoActivo, Textos.Permisos.MENSAJE_PERMISO_INACTIVO(codigoPermiso));

            var grupoPermisoExiste = await _grupoPermisoRepositorio.ObtenerGrupoPermisoAsync(grupoId, permisoExiste!.Id);
            _grupoPermisoValidador.ValidarDatoNoEncontrado(grupoPermisoExiste, Textos.Grupos.MENSAJE_GRUPO_NO_EXISTE_ID);
            //_grupoPermisoValidador.ValidarDatoActivo(grupoPermisoExiste!.EstadoActivo, Textos.Grupos.MENSAJE_GRUPO_INACTIVO);

            //return _apiResponse.CrearRespuesta<GrupoDto?>(true, "", grupoDto);

            return true;
        }
    }
}
