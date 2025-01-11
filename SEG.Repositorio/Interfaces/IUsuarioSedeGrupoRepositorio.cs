using SEG.Dominio.Entidades;
using SEG.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEG.Repositorio.Interfaces
{
    public interface IUsuarioSedeGrupoRepositorio
    {
        Task<int> CrearAsync(SEG_UsuarioSedeGrupo usuariosSedesGrupos);
        Task ModificarAsync(SEG_UsuarioSedeGrupo usuariosSedesGrupos);
        Task<bool> EliminarAsync(int id);
        Task<SEG_UsuarioSedeGrupo?> ObtenerUsuarioSedeAsync(int usuarioId, int sedeId);
        Task<SEG_UsuarioSedeGrupo?> ObtenerPorIdAsync(int id);
        IQueryable<UsuarioSedeGrupoDto> ListarPorUsuarioId(int usuarioId);
    }
}
