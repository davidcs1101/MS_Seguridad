namespace SEG.Dominio.Entidades
{
    public class SEG_ParametroExterno : SEG_BaseAuditoria
    {
        public int Id { get; set; }
        public string Origen { get; set; } = null!;
        public string CodigoCatalogo { get; set; } = null!;
        public int OrigenId { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public SEG_Usuario UsuarioCreador { get; set; } = null!;
        public SEG_Usuario? UsuarioModificador { get; set; }
    }
}
