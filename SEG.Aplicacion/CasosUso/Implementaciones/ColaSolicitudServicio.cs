using SEG.Aplicacion.CasosUso.Interfaces;
using SEG.Aplicacion.Servicios.Interfaces;
using SEG.Aplicacion.ServiciosExternos;
using SEG.Aplicacion.ServiciosExternos.config;
using SEG.Dominio.Enumeraciones;
using SEG.Dominio.Repositorio;
using SEG.Dominio.Repositorio.UnidadTrabajo;
using SEG.Dominio.Servicios.Interfaces;
using SEG.Dtos;
using Utilidades;

namespace SEG.Aplicacion.CasosUso.Implementaciones
{
    public class ColaSolicitudServicio : IColaSolicitudServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IColaSolicitudRepositorio _colaSolicitudRepositorio;
        private readonly INotificadorCorreo _notificadorCorreo;
        private readonly ISerializadorJsonServicio _serializadorJsonServicio;
        private readonly IColaSolicitudValidador _colaSolicitudValidador;
        private readonly IConfiguracionesTrabajosColas _configuracionesTrabajosColas;

        public ColaSolicitudServicio(IUnidadDeTrabajo unidadTrabajo, IColaSolicitudRepositorio colaSolicitudRepositorio, INotificadorCorreo notificadorCorreo, ISerializadorJsonServicio serializadorJsonServicio, IColaSolicitudValidador colaSolicitudValidador, IConfiguracionesTrabajosColas configuracionesTrabajosColas)
        {
            _unidadDeTrabajo = unidadTrabajo;
            _colaSolicitudRepositorio = colaSolicitudRepositorio;
            _notificadorCorreo = notificadorCorreo;
            _serializadorJsonServicio = serializadorJsonServicio;
            _colaSolicitudValidador = colaSolicitudValidador;
            _configuracionesTrabajosColas = configuracionesTrabajosColas;
        }

        public async Task ProcesarColaSolicitudesAsync()
        {
            var pendientes = _colaSolicitudRepositorio.Listar().Where(c => c.Estado == EstadoCola.Pendiente).OrderBy(c => c.Id)
                .Take(_configuracionesTrabajosColas.ObtenerCantidadRegistrosProcesarIteracion()).ToList();

            foreach (var solicitud in pendientes)
            {
                await this.ProcesarPorColaSolicitudIdAsync(solicitud.Id);
            }
        }

        public async Task ProcesarPorColaSolicitudIdAsync(int id, bool validarEstadoPendiente = false)
        {
            await using var transaccion = await _unidadDeTrabajo.IniciarTransaccionAsync();

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

                switch (solicitudExiste.Tipo)
                {
                    case Textos.EventosColas.ENVIARCORREO:
                        await _notificadorCorreo.EnviarAsync(_serializadorJsonServicio.Deserializar<DatoCorreoRequest>(solicitudExiste.Payload));
                        break;
                }

                solicitudExiste.Estado = EstadoCola.Exitoso;
                solicitudExiste.ErrorMensaje = null;
            }
            catch (Exception ex)
            {
                solicitudExiste.Intentos++;
                solicitudExiste.Estado = solicitudExiste.Intentos >= _configuracionesTrabajosColas.ObtenerCantidadIntentosPorRegistroEnCola() ? EstadoCola.Fallido : EstadoCola.Pendiente;
                solicitudExiste.ErrorMensaje = ex.Message;
                Logs.EscribirLog("e", $"{Textos.ColasSolicitudes.MENSAJE_COLASOLICITUD_ERROR_PROCESO} : {solicitudExiste.Id}", ex);
            }
            _colaSolicitudRepositorio.MarcarModificar(solicitudExiste);
            await _unidadDeTrabajo.GuardarCambiosAsync();
            await transaccion.CommitAsync();
        }
    }
}
