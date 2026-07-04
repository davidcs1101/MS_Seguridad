using Microsoft.AspNetCore.Authorization;
namespace SEG.Api.Seguridad.Middlewares.Permisos
{
    public class PermisoAttribute : AuthorizeAttribute
    {
        public string Permiso { get; }

        public PermisoAttribute(string permiso)
        {
            Permiso = permiso;
            Policy = "Permiso";
        }
    }
}
