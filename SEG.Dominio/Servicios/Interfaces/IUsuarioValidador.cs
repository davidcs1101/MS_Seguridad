using SEG.Dominio.Entidades;

namespace SEG.Dominio.Servicios.Interfaces
{
    public interface IUsuarioValidador : IEntidadValidador<SEG_Usuario>
    {
        void ValidarEmailTieneOtroUsuario(SEG_Usuario? usuario, int idUsuario, string mensaje);
        void ValidarLoguin(SEG_Usuario? usuario, string clave, string mensaje);
        void ValidarTieneGrupoDirecto(SEG_Usuario? usuario, string mensaje);
    }
}
