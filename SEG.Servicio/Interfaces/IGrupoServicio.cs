﻿using SEG.Dominio.Entidades;
using SEG.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEG.Servicio.Interfaces
{
    public interface IGrupoServicio
    {
        Task<ApiResponse<int>> CrearAsync(GrupoCreacionRequest grupoCreacionRequest);
        Task<ApiResponse<string>> ModificarAsync(GrupoModificacionRequest grupoModificacionRequest);
        Task<ApiResponse<string>> EliminarAsync(int id);
        Task<ApiResponse<GrupoDto?>> ObtenerPorIdAsync(int id);
        Task<ApiResponse<GrupoDto?>> ObtenerPorCodigoAsync(string codigo);
        Task<ApiResponse<List<GrupoDto>?>> ListarAsync();
    }
}
