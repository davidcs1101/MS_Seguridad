using Microsoft.AspNetCore.Authorization;
using SEG.Aplicacion.CasosUso.Interfaces;
using SEG.Aplicacion.Servicios.Interfaces.Cache;
using SEG.Aplicacion.ServiciosExternos;
using System.Runtime.InteropServices;
namespace SEG.Api.Seguridad.Middlewares.Permisos
{
    public class PermisoManejadorAutorizacion : AuthorizationHandler<PermisoRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISeguridadPermisosCache _permisosCache;

        public PermisoManejadorAutorizacion(
                 IHttpContextAccessor httpContextAccessor, ISeguridadPermisosCache permisosCache)
        {
            _httpContextAccessor = httpContextAccessor;
            _permisosCache = permisosCache;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermisoRequirement requirement)
        {
            var endpoint = _httpContextAccessor.HttpContext?.GetEndpoint();

            if (endpoint == null)
                return;

            var permiso = endpoint.Metadata.GetMetadata<PermisoAttribute>();

            if (permiso == null)
                return;

            var codigoPermiso = permiso.Permiso;

            var usuarioContextoServicio = _httpContextAccessor.HttpContext!
                .RequestServices
                .GetRequiredService<IUsuarioContextoServicio>();

            var codigoGrupo = usuarioContextoServicio.ObtenerCodigoGrupo();

            var autorizado = _permisosCache.TienePermiso(codigoGrupo, codigoPermiso);

            if (autorizado)
                context.Succeed(requirement);
        }
    }
}
