using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
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
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly AppDbContext _context;
        public UsuarioRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SEG_Usuario?> ObtenerPorIdAsync(int id)
        {
            return await _context.SEG_Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<SEG_Usuario?> ObtenerPorUsuarioAsync(string usuario)
        {
            return await _context.SEG_Usuarios.FirstOrDefaultAsync(x => x.NombreUsuario == usuario);
        }

        public async Task<SEG_Usuario?> ObtenerPorEmailAsync(string email)
        {
            return await _context.SEG_Usuarios.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<SEG_Usuario?> ObtenerPorIdentificacionAsync(int tipoIdentificacionId, string identificacion)
        {
            return await _context.SEG_Usuarios.FirstOrDefaultAsync(x => x.TipoIdentificacionId == tipoIdentificacionId && x.Identificacion == identificacion);
        }

        public async Task<int> CrearAsync(SEG_Usuario usuario)
        {
            _context.SEG_Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario.Id;
        }

        public async Task ModificarAsync(SEG_Usuario usuario) 
        {
            _context.SEG_Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public IQueryable<UsuarioDto> Listar()
        {
            var listas = _context.SEG_Usuarios
                .Select(u => new UsuarioDto
                {
                    Id = u.Id,
                    TipoIdentificacionId = u.TipoIdentificacionId,
                    Identificacion = u.Identificacion,
                    Nombre1 = u.Nombre1,
                    Nombre2 = u.Nombre2,
                    Apellido1 = u.Apellido1,
                    Apellido2 = u.Apellido2,
                    NombreUsuario = u.NombreUsuario,
                    UsuarioCreadorId = u.UsuarioCreadorId,
                    FechaCreado = u.FechaCreado,
                    UsuarioModificadorId = u.UsuarioModificadorId,
                    FechaModificado = u.FechaModificado,
                    EstadoActivo = u.EstadoActivo
                });

            return listas;
        }
    }
}
