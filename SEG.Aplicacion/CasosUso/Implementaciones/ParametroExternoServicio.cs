using Microsoft.EntityFrameworkCore;
using SEG.Dtos;
using Utilidades;
using SEG.Dominio.Entidades;
using SEG.Dominio.Repositorio;
using SEG.Aplicacion.CasosUso.Interfaces;
using SEG.Aplicacion.ServiciosExternos;
using SEG.Aplicacion.Servicios.Interfaces;
using SEG.Dominio.Servicios.Interfaces;
using SEG.Aplicacion.ServiciosExternos.Mapeo;

namespace SEG.Aplicacion.CasosUso.Implementaciones
{
    public class ParametroExternoServicio : IParametroExternoServicio
    {
        private readonly IProgramaRepositorio _programaRepositorio;
        private readonly IMapperPerfiles _mapper;
        private readonly IUsuarioContextoServicio _usuarioContextoServicio;
        private readonly IApiResponse _apiResponse;
        private readonly IEntidadValidador<SEG_Programa> _programaValidador;
        private readonly IAutorizacionSincronizacion _autorizacionSincronizacion;

        public ParametroExternoServicio(IProgramaRepositorio programaRepositorio, IMapperPerfiles mapper, IUsuarioContextoServicio usuarioContextoServicio, IEntidadValidador<SEG_Programa> programaValidador, IApiResponse apiResponseServicio, IAutorizacionSincronizacion autorizacionSincronizacion)
        {
            _programaRepositorio = programaRepositorio;
            _mapper = mapper;
            _usuarioContextoServicio = usuarioContextoServicio;
            _programaValidador = programaValidador;
            _apiResponse = apiResponseServicio;
            _autorizacionSincronizacion = autorizacionSincronizacion;
        }

        public async Task<ApiResponse<int>> SincronizarDatosAsync()
        {
            // Lógica para sincronizar datos
            return _apiResponse.CrearRespuesta(true, "", 0);
        }

        public async Task<ApiResponse<List<ProgramaDto>?>> ListarAsync()
        {
            var programas = await _programaRepositorio.Listar().ToListAsync();

            var programasResultado = programas
                .Select(p => new ProgramaDto
                {
                    Id = p.Id,
                    Codigo = p.Codigo,
                    Nombre = p.Nombre,
                    UsuarioCreadorId = p.UsuarioCreadorId,
                    NombreUsuarioCreador = p.UsuarioCreador.NombreUsuario,
                    FechaCreado = p.FechaCreado,
                    UsuarioModificadorId = p.UsuarioModificadorId,
                    NombreUsuarioModificador = p.UsuarioModificador != null ? p.UsuarioModificador.NombreUsuario : null,
                    FechaModificado = p.FechaModificado,
                    EstadoActivo = p.EstadoActivo
                }).ToList();

            return _apiResponse.CrearRespuesta<List<ProgramaDto>?>(true, "", programasResultado);
        }

    }
}
