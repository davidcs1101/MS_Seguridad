namespace SEG.Dominio.Entidades.ParametrosConsultas
{
    public class UsuarioConsulta : ParametroBase
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
    }
}
