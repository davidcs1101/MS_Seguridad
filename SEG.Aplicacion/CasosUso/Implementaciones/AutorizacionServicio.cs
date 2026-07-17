using Microsoft.EntityFrameworkCore;
using SEG.Aplicacion.CasosUso.Interfaces;
using SEG.Aplicacion.Servicios.Interfaces;
using SEG.Aplicacion.ServiciosExternos.Mapeo;
using SEG.Dominio.Repositorio;
using SEG.Dtos;
namespace SEG.Aplicacion.CasosUso.Implementaciones
{
    public class AutorizacionServicio : IAutorizacionServicio
    {
        private readonly IGrupoPermisoRepositorio _grupoPermisoRepositorio;
        private readonly IApiResponse _apiResponse;
        private readonly IMapperPerfiles _mapper;

        public AutorizacionServicio(IGrupoPermisoRepositorio grupoPermisoRepositorio, IApiResponse apiResponse, IMapperPerfiles mapper)
        {
            _grupoPermisoRepositorio = grupoPermisoRepositorio;
            _apiResponse = apiResponse;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<AutorizacionDto>?>> ObtenerCatalogoAutorizacionAsync()
        {
            var autorizaciones = await _grupoPermisoRepositorio
                .ListarPermisosCache()
                .ToListAsync();

            var autorizacionesDto = _mapper.Map(autorizaciones);
            return _apiResponse.CrearRespuesta<List<AutorizacionDto>?>(
                true,
                "",
                autorizacionesDto);
        }

        public async Task<List<AutorizacionDto>> ListarCatalogoAutorizacionAsync()
        {
            var autorizaciones = await _grupoPermisoRepositorio
                .ListarPermisosCache()
                .ToListAsync();

            var autorizacionesDto = _mapper.Map(autorizaciones);
            return autorizacionesDto;
        }
    }
}
