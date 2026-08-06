using SEG.Dominio.Entidades;
using SEG.Dominio.Entidades.ParametrosConsultas;
namespace SEG.Infraestructura.Dominio.Repositorio.Helpers
{
    public class UsuarioQueryHelper
    {
        public static IQueryable<SEG_Usuario> AplicarFiltros(IQueryable<SEG_Usuario> query, UsuarioConsulta consulta)
        {
            if (!string.IsNullOrWhiteSpace(consulta.Nombre1))
                query = query.Where(x => x.Nombre1.StartsWith(consulta.Nombre1));

            if (!string.IsNullOrWhiteSpace(consulta.Nombre2))
                query = query.Where(x => x.Nombre2.StartsWith(consulta.Nombre2));

            if (!string.IsNullOrWhiteSpace(consulta.Apellido1))
                query = query.Where(x => x.Apellido1.StartsWith(consulta.Apellido1));

            if (!string.IsNullOrWhiteSpace(consulta.Apellido2))
                query = query.Where(x => x.Apellido2.StartsWith(consulta.Apellido2));

            if (consulta.EstadoActivo.HasValue)
                query = query.Where(x => x.EstadoActivo == consulta.EstadoActivo);

            return query;
        }
    }
}
