using Microsoft.EntityFrameworkCore;
using SEG.Dominio.Entidades;
using SEG.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEG.Repositorio.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task<SEG_Usuario?> ObtenerPorIdAsync(int id);
        Task<SEG_Usuario?> ObtenerPorUsuarioAsync(string usuario);
        Task<SEG_Usuario?> ObtenerPorEmailAsync(string email);
        Task<SEG_Usuario?> ObtenerPorIdentificacionAsync(int tipoIdentificacionId, string identificacion);
        Task ModificarAsync(SEG_Usuario usuario);
        Task<int> CrearAsync(SEG_Usuario usuario);
        IQueryable<UsuarioDto> Listar();
    }
}
