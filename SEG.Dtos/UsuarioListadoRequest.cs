using Utilidades.Dtos;
namespace SEG.Dtos
{
    public class UsuarioListadoRequest
    {
        public int TipoIdentificacionId { get; set; }
        public string? Identificacion { get; set; }
        public string? Nombre1 { get; set; }
        public string? Nombre2 { get; set; }
        public string? Apellido1 { get; set; }
        public string? Apellido2 { get; set; }
        public string? Email { get; set; }
        public string? NombreUsuario { get; set; }
        public bool? CambiarClave { get; set; }
        public bool? EstadoActivo { get; set; }
        public PaginacionRequest Paginacion { get; set; } = new PaginacionRequest();
    }
}
