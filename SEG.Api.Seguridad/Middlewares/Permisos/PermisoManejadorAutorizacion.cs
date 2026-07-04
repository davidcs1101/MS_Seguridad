using Microsoft.AspNetCore.Authorization;
using SEG.Aplicacion.CasosUso.Interfaces;
using SEG.Aplicacion.ServiciosExternos;
namespace SEG.Api.Seguridad.Middlewares.Permisos
{
    public class PermisoManejadorAutorizacion : AuthorizationHandler<PermisoRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PermisoManejadorAutorizacion(
                 IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
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

            // Resolver servicios Scoped de la petición actual
            var autorizacionServicio = _httpContextAccessor.HttpContext!
                .RequestServices
                .GetRequiredService<IAutorizacionServicio>();

            var usuarioContextoServicio = _httpContextAccessor.HttpContext!
                .RequestServices
                .GetRequiredService<IUsuarioContextoServicio>();

            var grupoId = usuarioContextoServicio.ObtenerGrupoId();

            var autorizado = await autorizacionServicio
                .TienePermisoAsync(grupoId, codigoPermiso);

            if (autorizado)
                context.Succeed(requirement);
        }
    }
}
