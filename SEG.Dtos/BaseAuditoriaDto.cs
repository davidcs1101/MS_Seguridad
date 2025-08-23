namespace SEG.Dtos
{
    public class BaseAuditoriaDto
    {
        public int UsuarioCreadorId { get; set; }
        public string? NombreUsuarioCreador { get; set; }
        public DateTime FechaCreado { get; set; }
        public int? UsuarioModificadorId { get; set; }
        public string? NombreUsuarioModificador { get; set; }
        public DateTime? FechaModificado { get; set; }
        public bool EstadoActivo { get; set; }
    }
}
