namespace SEG.Dominio.Entidades
{
    public class SEG_Permiso : SEG_BaseAuditoria
    {
        public int Id { get; set; }
        public SEG_Programa Programa { get; set; } = null!;
        public int ProgramaId { get; set; }
        public SEG_Accion Accion { get; set; } = null!;
        public int AccionId { get; set; }
        public string Codigo { get; set; } = null!;
        public string? Nombre { get; set; }

        public List<SEG_GrupoPermiso> GruposPermisos { get; set; } = new();

        public SEG_Usuario UsuarioCreador { get; set; } = null!;
        public SEG_Usuario? UsuarioModificador { get; set; }
    }
}
