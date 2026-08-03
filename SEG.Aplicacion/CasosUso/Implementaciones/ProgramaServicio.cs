using Microsoft.EntityFrameworkCore;
using SEG.Aplicacion.CasosUso.Interfaces;
using SEG.Aplicacion.Servicios.Interfaces;
using SEG.Aplicacion.ServiciosExternos;
using SEG.Aplicacion.ServiciosExternos.config;
using SEG.Aplicacion.ServiciosExternos.Mapeo;
using SEG.Dominio.Entidades;
using SEG.Dominio.Repositorio;
using SEG.Dominio.Repositorio.UnidadTrabajo;
using SEG.Dominio.Servicios.Interfaces;
using SEG.Dtos;
using Utilidades;
using Utilidades.Dtos;
using Utilidades.Servicios.Responses.Interfaces;

namespace SEG.Aplicacion.CasosUso.Implementaciones
{
    public class ProgramaServicio : IProgramaServicio
    {
        private readonly IProgramaRepositorio _programaRepositorio;
        private readonly IMapperPerfiles _mapper;
        private readonly IUsuarioContextoServicio _usuarioContextoServicio;
        private readonly IApiResponse _apiResponse;
        private readonly IEntidadValidador<SEG_Programa> _programaValidador;
        private readonly ISincronizadorMicroservicios _sincronizadorMicroservicios;
        private readonly IProcesadorTransacciones _procesadorTransacciones;
        private readonly IColaSolicitudServicio _colaSolicitudServicio;
        private readonly IAppSettings _appSettings;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public ProgramaServicio(IProgramaRepositorio programaRepositorio, IMapperPerfiles mapper, IUsuarioContextoServicio usuarioContextoServicio, IEntidadValidador<SEG_Programa> programaValidador, IApiResponse apiResponseServicio, ISincronizadorMicroservicios sincronizadorMicroservicios, IProcesadorTransacciones procesadorTransacciones, IColaSolicitudServicio colaSolicitudServicio, IAppSettings appSettings, IUnidadDeTrabajo unidadDeTrabajo)
        {
            _programaRepositorio = programaRepositorio;
            _mapper = mapper;
            _usuarioContextoServicio = usuarioContextoServicio;
            _programaValidador = programaValidador;
            _apiResponse = apiResponseServicio;
            _sincronizadorMicroservicios = sincronizadorMicroservicios;
            _procesadorTransacciones = procesadorTransacciones;
            _colaSolicitudServicio = colaSolicitudServicio;
            _appSettings = appSettings;
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task<ApiResponseDto<int>> CrearAsync(ProgramaCreacionRequest programaCreacionRequest)
        {
            var programaExiste = await _programaRepositorio.ObtenerPorCodigoAsync(programaCreacionRequest.Codigo);
            _programaValidador.ValidarDatoYaExiste(programaExiste, Textos.Programas.MENSAJE_PROGRAMA_CODIGO_EXISTE);

            var usuarioId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

            var programa = _mapper.Map(programaCreacionRequest);
            programa.UsuarioCreadorId = usuarioId;

            var id = await _programaRepositorio.CrearAsync(programa);

            return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_CREADO, id);
        }

        public async Task<ApiResponseDto<string>> ModificarAsync(ProgramaModificacionRequest programaModificacionRequest)
        {
            var colas = new List<SEG_ColaSolicitud>();
            await _procesadorTransacciones.EjecutarEnTransaccionAsync(async () =>
            {
                var programaExiste = await _programaRepositorio.ObtenerPorIdAsync(programaModificacionRequest.Id);
                _programaValidador.ValidarDatoNoEncontrado(programaExiste, Textos.Programas.MENSAJE_PROGRAMA_NO_EXISTE_ID);

                var usuarioId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

                _mapper.Map(programaModificacionRequest, programaExiste);
                programaExiste.FechaModificado = DateTime.Now;
                programaExiste.UsuarioModificadorId = usuarioId;

                _programaRepositorio.MarcarModificar(programaExiste);

                var urls = _appSettings.ObtenerEventosNotificarActualizarPermisos();
                colas = await _colaSolicitudServicio.AgregarColasSolicitudes(EventosColas.PERMISOSACTUALIZADOS, "", urls);

                await _unidadDeTrabajo.GuardarCambiosAsync();
            });

            // Llamada para actualizar la sincronización de permisos después de modificar una acción
            await _sincronizadorMicroservicios.SincronizarPermisosAsync(colas.Select(c => c.Id).ToList());

            return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_ACTUALIZADO, "");
        }

        public async Task<ApiResponseDto<string>> EliminarAsync(int id)
        {
            var programaExiste = await _programaRepositorio.ObtenerPorIdAsync(id);
            _programaValidador.ValidarDatoNoEncontrado(programaExiste, Textos.Programas.MENSAJE_PROGRAMA_NO_EXISTE_ID);

            var eliminado = await _programaRepositorio.EliminarAsync(id);

            if (eliminado)
                return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_ELIMINADO, "");

            return _apiResponse.CrearRespuesta(false, Textos.Generales.MENSAJE_REGISTRO_NO_ELIMINADO, "");
        }

        public async Task<ApiResponseDto<ProgramaDto?>> ObtenerPorIdAsync(int id)
        {
            var programaExiste = await _programaRepositorio.ObtenerPorIdAsync(id);
            _programaValidador.ValidarDatoNoEncontrado(programaExiste, Textos.Programas.MENSAJE_PROGRAMA_NO_EXISTE_ID);

            var programaDto = _mapper.Map(programaExiste!);

            return _apiResponse.CrearRespuesta<ProgramaDto?>(true, "", programaDto);
        }

        public async Task<ApiResponseDto<ProgramaDto?>> ObtenerPorCodigoAsync(string codigo)
        {
            var programaExiste = await _programaRepositorio.ObtenerPorCodigoAsync(codigo);
            _programaValidador.ValidarDatoNoEncontrado(programaExiste, Textos.Programas.MENSAJE_PROGRAMA_NO_EXISTE_CODIGO);

            var programaDto = _mapper.Map(programaExiste!);

            return _apiResponse.CrearRespuesta<ProgramaDto?>(true, "", programaDto);
        }

        public async Task<ApiResponseDto<List<ProgramaDto>?>> ListarAsync()
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
