﻿using SEG.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEG.Servicio.Interfaces
{
    public interface IGrupoProgramaServicio
    {
        Task<List<SEG_Programa>> ListarProgramasPorGrupo();
    }
}
