using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEG.DataAccess;
using SEG.Dominio.Entidades;
using SEG.Dominio.Repositorio;

namespace SEG.Infraestructura.Dominio.Repositorio
{
    public class MaestroExternoRepositorio : IMaestroExternoRepositorio
    {
        private readonly AppDbContext _context;
        public MaestroExternoRepositorio(AppDbContext context) 
        {
            _context = context;
        }

        public IQueryable<SEG_MaestroExterno> Listar()
        {
            return _context.SEG_MaestrosExternos
                .Include(p => p.UsuarioCreador)
                .Include(p => p.UsuarioModificador);
        }

        public async Task SincronizarCatalogoAsync(string microservicioOrigen, string codigoCatalogo,List<SEG_MaestroExterno> registros)
        {
            var existentes = await _context.SEG_MaestrosExternos
                .Where(x => x.ServicioOrigen == microservicioOrigen && x.CodigoMaestro == codigoCatalogo).ToListAsync();

            // Crear y actualizar
            foreach (var registro in registros)
            {
                var existente = existentes.FirstOrDefault(x => x.OrigenId == registro.OrigenId);

                if (existente == null)
                {
                    _context.SEG_MaestrosExternos.Add(registro);
                    continue;
                }

                existente.Codigo = registro.Codigo;
                existente.Nombre = registro.Nombre;
                existente.EstadoActivo = registro.EstadoActivo;
                existente.FechaModificado = registro.FechaModificado;
                existente.UsuarioModificadorId = registro.UsuarioModificadorId;
            }

            // Desactivar los que ya no llegaron
            foreach (var existente in existentes)
            {
                if (!registros.Any(x => x.OrigenId == existente.OrigenId))
                {
                    existente.EstadoActivo = false;
                    existente.FechaModificado = DateTime.Now;
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
