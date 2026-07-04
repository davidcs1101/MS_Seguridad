using Microsoft.AspNetCore.Http;
using SEG.Aplicacion.ServiciosExternos;
using SEG.Dominio.Excepciones;
using Utilidades;

namespace SEG.Infraestructura.Aplicacion.ServiciosExternos
{
    public class UsuarioContextoServicio : IUsuarioContextoServicio
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuarioContextoServicio(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Int32 ObtenerUsuarioIdToken()
        {
            // Obtener el 'UsuarioId' desde el token JWT en el contexto HTTP
            var usuarioIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UsuarioId")?.Value;

            if (string.IsNullOrEmpty(usuarioIdClaim))
                throw new LoguinException(Textos.Generales.MENSAJE_TOKEN_SIN_USUARIOID);

            return Convert.ToInt32(usuarioIdClaim);
        }

        public Int32 ObtenerGrupoId()
        {
            // Obtener el 'GrupoId' desde el token JWT en el contexto HTTP
            var grupoIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("GrupoId")?.Value;
            if (string.IsNullOrEmpty(grupoIdClaim))
                throw new LoguinException(Textos.Generales.MENSAJE_TOKEN_SIN_GRUPOID);

            return Convert.ToInt32(grupoIdClaim);
        }
    }
}
