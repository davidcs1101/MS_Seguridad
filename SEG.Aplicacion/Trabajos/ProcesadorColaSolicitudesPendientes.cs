using SEG.Aplicacion.Servicios.Interfaces;
using SEG.Dominio.Repositorio;
using SEG.Dominio.Repositorio.UnidadTrabajo;
using SEG.Dtos;
using Utilidades;

namespace SEG.Aplicacion.Trabajos
{
    public class ProcesadorColaSolicitudesPendientes
    {
        private readonly IUnidadDeTrabajo _unidadTrabajo;
        private readonly IColaSolicitudRepositorio _colaSolicitudRepositorio;

        public ProcesadorColaSolicitudesPendientes(IUnidadDeTrabajo unidadTrabajo, IColaSolicitudRepositorio colaSolicitudRepositorio)
        {
            _unidadTrabajo = unidadTrabajo;
            _colaSolicitudRepositorio = colaSolicitudRepositorio;
        }

        public async Task EjecutarAsync()
        {
            var pendientes = _colaSolicitudRepositorio.Listar().Where(c => c.Estado == Textos.EstadosColas.PENDIENTE).Take(10).ToList();

            foreach (var solicitud in pendientes)
            {
                await using var transaccion = await _unidadTrabajo.IniciarTransaccionAsync();
                try
                {
                    solicitud.Estado = Textos.EstadosColas.PROCESANDO;
                    solicitud.FechaUltimoIntento = DateTime.Now;
                    await _unidadTrabajo.GuardarCambiosAsync();

                    switch (solicitud.Tipo)
                    {
                        case "CrearUsuarioAdministrador":
                            //await _seguridadUsuarios.Crear(Utilidades.JsonHelper.Deserializar<UsuarioSedeCreacionRequest>(solicitud.Payload));
                            break;
                    }

                    solicitud.Estado = Textos.EstadosColas.EXITOSO;
                    solicitud.ErrorMensaje = null;
                }
                catch (Exception ex)
                {
                    solicitud.Reintentos++;
                    solicitud.Estado = solicitud.Reintentos >= 3 ? Textos.EstadosColas.FALLIDO : Textos.EstadosColas.PENDIENTE;
                    solicitud.ErrorMensaje = ex.Message;
                    Utilidades.Logs.EscribirLog("e", $"Error procesando solicitud pendiente {solicitud.Id}");
                }
                await _unidadTrabajo.GuardarCambiosAsync();
                await transaccion.CommitAsync();
            }
        }
    }
}
