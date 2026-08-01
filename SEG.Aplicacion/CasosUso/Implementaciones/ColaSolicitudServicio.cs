using SEG.Aplicacion.CasosUso.Interfaces;
using SEG.Aplicacion.Servicios.Interfaces;
using SEG.Aplicacion.ServiciosExternos;
using SEG.Aplicacion.ServiciosExternos.config;
using SEG.Dominio.Entidades;
using SEG.Dominio.Enumeraciones;
using SEG.Dominio.Repositorio;
using SEG.Dominio.Repositorio.UnidadTrabajo;
using SEG.Dominio.Servicios.Interfaces;
using SEG.Dtos;
using Utilidades;
using Utilidades.Dtos;
using Utilidades.Servicios.Responses.Interfaces;

namespace SEG.Aplicacion.CasosUso.Implementaciones
{
    public class ColaSolicitudServicio : IColaSolicitudServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IColaSolicitudRepositorio _colaSolicitudRepositorio;
        private readonly IEntidadValidador<SEG_ColaSolicitud> _colaSolicitudValidador;
        private readonly IAppSettings _appSettings;
        private readonly IProcesadorTransacciones _procesadorTransacciones;
        private readonly IApiResponse _apiResponse;
        private readonly IProcesadorEventos _procesadorEventos;

        public ColaSolicitudServicio(IUnidadDeTrabajo unidadDeTrabajo, IColaSolicitudRepositorio colaSolicitudRepositorio, IEntidadValidador<SEG_ColaSolicitud> colaSolicitudValidador, IAppSettings appSettings, IProcesadorTransacciones procesadorTransacciones, IApiResponse apiResponse, IProcesadorEventos procesadorEventos)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
            _colaSolicitudRepositorio = colaSolicitudRepositorio;
            _colaSolicitudValidador = colaSolicitudValidador;
            _appSettings = appSettings;
            _procesadorTransacciones = procesadorTransacciones;
            _apiResponse = apiResponse;
            _procesadorEventos = procesadorEventos;
        }

        public async Task ProcesarColaSolicitudesAsync()
        {
            var cantidadRegistrosProcesar = _appSettings.ObtenerTrabajosColasSettings().CantidadRegistrosProcesarIteracion;
            var pendientes = _colaSolicitudRepositorio.Listar().
                Where(c => c.Estado == EstadoCola.Pendiente).OrderBy(c => c.Id)
                .Take(cantidadRegistrosProcesar).ToList();

            foreach (var solicitud in pendientes)
            {
                await this.ProcesarPorColaSolicitudIdAsync(solicitud.Id);
            }
        }

        public async Task ProcesarPorColaSolicitudIdAsync(int id, bool validarEstadoPendiente = false)
        {
            await _procesadorTransacciones.EjecutarEnTransaccionAsync(async () =>
            {
                var cantidadIntentos = _appSettings.ObtenerTrabajosColasSettings().CantidadIntentosPorRegistroEnCola;
                var solicitudExiste = await _colaSolicitudRepositorio.ObtenerPorIdAsync(id);
                _colaSolicitudValidador.ValidarDatoNoEncontrado(solicitudExiste, Textos.ColasSolicitudes.MENSAJE_COLASOLICITUD_NO_EXISTE_ID);

                if (validarEstadoPendiente)
                {
                    if (solicitudExiste.Estado != EstadoCola.Pendiente)
                    {
                        Logs.EscribirLog("w", $"{Textos.ColasSolicitudes.MENSAJE_COLASOLICITUD_YA_PROCESADA}: {solicitudExiste.Id}");
                        return;
                    }
                }

                try
                {
                    solicitudExiste.Estado = EstadoCola.Procesando;
                    solicitudExiste.FechaUltimoIntento = DateTime.Now;
                    _colaSolicitudRepositorio.MarcarModificar(solicitudExiste);
                    await _unidadDeTrabajo.GuardarCambiosAsync();

                    await _procesadorEventos.ProcesarAsync(solicitudExiste.Tipo, solicitudExiste.Payload, solicitudExiste.UrlDestino);

                    solicitudExiste.Estado = EstadoCola.Exitoso;
                    solicitudExiste.ErrorMensaje = null;
                }
                catch (Exception ex)
                {
                    solicitudExiste.Intentos++;
                    solicitudExiste.Estado = solicitudExiste.Intentos >= cantidadIntentos ? EstadoCola.Fallido : EstadoCola.Pendiente;
                    solicitudExiste.ErrorMensaje = ex.Message;
                    Logs.EscribirLog("e", $"{Textos.ColasSolicitudes.MENSAJE_COLASOLICITUD_ERROR_PROCESO} : {solicitudExiste.Id}", ex);
                }
                _colaSolicitudRepositorio.MarcarModificar(solicitudExiste);
                await _unidadDeTrabajo.GuardarCambiosAsync();
            });
        }

        public async Task<ApiResponseDto<int>> CrearAsync(ColaSolicitudCreacionRequest colaSolicitudCreacionRequest)
        {
            var solicitud = new SEG_ColaSolicitud
            {
                Tipo = colaSolicitudCreacionRequest.Tipo,
                Payload = colaSolicitudCreacionRequest.Payload,
                Estado = EstadoCola.Pendiente,
            };

            var id = await _colaSolicitudRepositorio.CrearAsync(solicitud);

            return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_CREADO, id);
        }
    }
}
