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
    public class ProgramaRepositorio : IProgramaRepositorio
    {
        private readonly AppDbContext _context;
        public ProgramaRepositorio(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<int> CrearAsync(SEG_Programa programa) 
        {
            _context.SEG_Programas.Add(programa);
            await _context.SaveChangesAsync();
            return programa.Id;
        }

        public async Task ModificarAsync(SEG_Programa programa) 
        {
            _context.SEG_Programas.Update(programa);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var eliminado = await _context.SEG_Programas.Where(p => p.Id == id).ExecuteDeleteAsync();
            return eliminado > 0;
        }

        public async Task<SEG_Programa?> ObtenerPorIdAsync(int id)
        {
            return await _context.SEG_Programas.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<SEG_Programa?> ObtenerPorCodigoAsync(string codigo) 
        {
            return await _context.SEG_Programas.FirstOrDefaultAsync(p => p.Codigo == codigo);
        }

        public IQueryable<ProgramaDto> Listar() 
        {
            var programas = _context.SEG_Programas
                .Include(p => p.UsuarioCreador)
                .Include(p => p.UsuarioModificador)
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
                });
            return programas;
        }
    }
}
