 namespace SEG.Dominio.Entidades
{
    public class SEG_MaestroExterno : SEG_BaseAuditoria
    {
        public int Id { get; set; }
        public string ServicioOrigen { get; set; } = null!;
        public string CodigoMaestro { get; set; } = null!;
        public int OrigenId { get; set; }
        public int? OrigenPadreId { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public SEG_Usuario UsuarioCreador { get; set; } = null!;
        public SEG_Usuario? UsuarioModificador { get; set; }
    }
}
