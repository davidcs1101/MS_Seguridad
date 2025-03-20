using SEG.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEG.Servicio.Interfaces
{
    public interface IEntidadValidador<TEntity>
    {
        void ValidarDatoYaExiste(TEntity? entidad, string mensaje);
        void ValidarDatoNoEncontrado(TEntity? entidad, string mensaje);
    }
}
