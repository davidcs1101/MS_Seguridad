﻿using Microsoft.EntityFrameworkCore;
using SEG.Dominio.Entidades;
using SEG.Dtos;
using SEG.Servicio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades;

namespace SEG.Servicio.Implementaciones
{
    public class ProgramaValidador : IProgramaValidador
    {
        public void ValidarDatoYaExiste(SEG_Programa? programa, string mensaje)
        {
            if (programa != null)
                throw new DbUpdateException(mensaje);
        }
        public void ValidarDatoNoEncontrado(SEG_Programa? programa, string mensaje)
        {
            if (programa == null)
                throw new KeyNotFoundException(mensaje);
        }
    }
}
