namespace SEG.Dominio.Entidades
{
    public class SEG_Programa : SEG_BaseAuditoria
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public List<SEG_Permiso> Permisos { get; set; } = new();
        public SEG_Usuario UsuarioCreador { get; set; } = null!;
        public SEG_Usuario? UsuarioModificador { get; set; }
    }
}
