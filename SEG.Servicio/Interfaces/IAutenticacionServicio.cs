using SEG.Dtos;
using SEG.Repositorio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEG.Servicio.Interfaces
{
    public interface IAutenticacionServicio
    {
        Task<ApiResponse<string>> AutenticarUsuarioAsync(AutenticacionRequest autenticacionDto);
        Task<ApiResponse<string>> AutenticarSedeAsync(int sedeId);
    }
}
