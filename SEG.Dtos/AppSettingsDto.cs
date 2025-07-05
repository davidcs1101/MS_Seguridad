namespace SEG.Dtos.AppSettings
{
    public class JWTSettings
    {
        public string? Issuer { get; set; }
        public Audience? Audience { get; set; }
        public string? Key { get; set; }
        public string? MinutosDuracionTokenAutenticacionUsuario { get; set; }
        public string? MinutosDuracionTokenAutenticacionSede { get; set; }
    }
    public class Audience
    {
        public string? Empresas { get; set; }
        public string? DatosComunes { get; set; }
        public string? EnvioCorreos { get; set; }
    }

    public class TrabajosColasSettings
    {
        public string? ProcesarColaSolicitudesCron { get; set; }
        public string? CantidadIntentosPorRegistroEnCola { get; set; }
        public string? CantidadRegistrosProcesarIteracion { get; set; }
        public string? UsuarioIntegracion { get; set; }
        public string? ClaveIntegracion { get; set; }
    }

}
