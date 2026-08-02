using SEG.Aplicacion.Servicios.Interfaces.Cache;
using SEG.Dominio.Entidades;
using SEG.Dominio.Repositorio;
using System.ComponentModel.Design;
using Utilidades;
using Utilidades.Dtos;

namespace SEG.Aplicacion.Servicios.Interfaces
{
    public class ProcesadorEmpresas : IProcesadorEmpresas
    {
        private readonly IMSEmpresas _msEmpresas;
        private readonly IMaestroExternoRepositorio _maestroExternoRepositorio;
        private readonly IMaestroExternoCache _maestroExternoCache;

        public ProcesadorEmpresas(IMaestroExternoRepositorio maestroExternoRepositorio, IMaestroExternoCache maestroExternoCache, IMSEmpresas msEmpresas)
        {
            _msEmpresas = msEmpresas;
            _maestroExternoRepositorio = maestroExternoRepositorio;
            _maestroExternoCache = maestroExternoCache;
        }

        //Procesos para SEDES
        public async Task ProcesarSedesAsync()
        {
            await ProcesarDatosSedesAsync();
        }

        public async Task ProcesarSedesAsync(int sedeId)
        {
            await ProcesarDatosSedesAsync(sedeId);
        }


        private async Task ProcesarDatosSedesAsync(int? sedeId = null)
        {
            // Obtener los registros desde Empresas
            var sedes = new List<Dtos.SedeDto?>();
            if (sedeId is not null)
            {
                var sede = await _msEmpresas.ObtenerSedePorId((int)sedeId);
                sedes.Add(sede);
            }
            else
                sedes = await _msEmpresas.ListarSedesAsync();

            // Convertir cada sede en un maestro externo
            var maestros = sedes.Select(x => new SEG_MaestroExterno
            {
                ServicioOrigen = CodigosMicroservicios.MS_EMPRESAS,
                CodigoMaestro = CodigosMaestrosExternos.SEDES,
                OrigenId = x.Id,
                Codigo = "",
                Nombre = x.Descripcion ?? "",
                EstadoActivo = x.EstadoActivo,
                UsuarioCreadorId = x.UsuarioCreadorId,
            }).ToList();

            // Sincronizar todas las sedes como un único maestro
            await _maestroExternoRepositorio.SincronizarMaestroAsync(
                CodigosMicroservicios.MS_EMPRESAS,
                CodigosMaestrosExternos.SEDES,
                maestros);

            // Refrescar la caché local
            await _maestroExternoCache.RefrescarAsync();
        }
    }
}
