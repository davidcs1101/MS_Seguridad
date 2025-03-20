using SEG.Dominio.Entidades;
using SEG.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEG.Servicio.Interfaces
{
    public interface IUsuarioSedeGrupoServicio
    {
        Task<ApiResponse<int>> CrearAsync(UsuarioSedeGrupoCreacionRequest usuarioSedeGrupoCreacion);
        Task<ApiResponse<string>> ModificarAsync(UsuarioSedeGrupoModificacionRequest usuarioSedeGrupoModificacionRequest);
        Task<ApiResponse<string>> EliminarAsync(int id);
        Task<ApiResponse<UsuarioSedeGrupoDto?>> ObtenerUsuarioSedeAsync(int usuarioId, int sedeId);
        Task<ApiResponse<List<UsuarioSedeGrupoDto>?>> ListarPorUsuarioIdLogueadoAsync();
    }
}
