namespace SEG.Dtos
{
    public class UsuarioOtrosDatosDto
    {
        public int Id { get; set; }
        public string Clave { get; set; } = null!;
        public bool NotificadoPorCorreo { get; set; } = false;
    }
}
