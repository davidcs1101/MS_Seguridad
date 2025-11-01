﻿using SEG.Aplicacion.ServiciosExternos;
using SEG.Dominio.Repositorio.UnidadTrabajo;
using SEG.Dominio.Excepciones;
using SEG.Dominio.Entidades;
using SEG.Dtos;

namespace SEG.Aplicacion.Servicios.Interfaces
{
    public class ProcesadorTransacciones : IProcesadorTransacciones
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public ProcesadorTransacciones(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task EjecutarEnTransaccionAsync(Func<Task> operacion)
        {
            await using var transaccion = await _unidadDeTrabajo.IniciarTransaccionAsync();

            try{
                await operacion();
                await transaccion.CommitAsync();
            }
            catch (SolicitudHttpException){
                /*
                 *Tabmbien Hacemos RollBack si las solicitudes Http 
                 *fallan dado que puede ocurrir presencia de consumo a microservicios antes o despues de haber
                 *guardado cambios de base de datos.
                */
                await transaccion.RollbackAsync();
                throw;
            }
            catch (DatoNoEncontradoException){
                await transaccion.RollbackAsync();
                throw;
            }
            catch (DatoYaExisteException){
                await transaccion.RollbackAsync();
                throw;
            }
            catch{
                await transaccion.RollbackAsync();
                throw;
            }
        }
    }
}
