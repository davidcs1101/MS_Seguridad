using SEG.Dominio.Entidades;

namespace SEG.Dominio.Servicios.Interfaces
{
    public interface IUsuarioValidador : IEntidadValidador<SEG_Usuario>
    {
        void ValidarEmailTieneOtroUsuario(SEG_Usuario? usuarioEmail, int idUsuario, string mensaje);
    }
}
