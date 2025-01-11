using Microsoft.EntityFrameworkCore;
using SEG.DataAccess;
using SEG.Dominio.Entidades;
using SEG.Dtos;
using SEG.Repositorio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEG.Repositorio.Implementaciones
{
    public class GrupoRepositorio : IGrupoRepositorio
    {
        private readonly AppDbContext _context;
        public GrupoRepositorio(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<int> CrearAsync(SEG_Grupo grupo) 
        {
            _context.SEG_Grupos.Add(grupo);
            await _context.SaveChangesAsync();
            return grupo.Id;
        }

        public async Task ModificarAsync(SEG_Grupo grupo) 
        {
            _context.SEG_Grupos.Update(grupo);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var eliminado = await _context.SEG_Grupos.Where(u => u.Id == id).ExecuteDeleteAsync();
            return eliminado > 0;
        }

        public async Task<SEG_Grupo?> ObtenerPorIdAsync(int id)
        {
            return await _context.SEG_Grupos.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<SEG_Grupo?> ObtenerPorCodigoAsync(string codigo) 
        {
            return await _context.SEG_Grupos.FirstOrDefaultAsync(g => g.Codigo == codigo);
        }

        public IQueryable<GrupoDto> Listar() 
        {
            var grupos = _context.SEG_Grupos
                .Include(g => g.UsuarioCreador)
                .Include(g => g.UsuarioModificador)
                .Select(g => new GrupoDto
                         {
                             Id = g.Id,
                             Codigo = g.Codigo,
                             Nombre = g.Nombre,
                             UsuarioCreadorId = g.UsuarioCreadorId,
                             NombreUsuarioCreador = g.UsuarioCreador.NombreUsuario,
                             FechaCreado = g.FechaCreado,
                             UsuarioModificadorId = g.UsuarioModificadorId,
                             NombreUsuarioModificador = g.UsuarioModificador != null ? g.UsuarioModificador.NombreUsuario : null,
                             FechaModificado = g.FechaModificado,
                             EstadoActivo = g.EstadoActivo
                         });
            return grupos;
        }
    }
}
