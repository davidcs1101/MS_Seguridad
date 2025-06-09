namespace SEG.Dtos
{
    public class UsuarioSedeCreacionRequest
    {
        public int TipoIdentificacionId { get; set; }
        public string Identificacion { get; set; } = null!;
        public string Nombre1 { get; set; } = null!;
        public string? Nombre2 { get; set; }
        public string Apellido1 { get; set; } = null!;
        public string? Apellido2 { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string NombreUsuario { get; set; } = null!;
        public bool CambiarClave { get; set; }
        public int SedeId { get; set; }
    }
}
