namespace SEG.Dtos
{
    public class UsuarioSedeGrupoDto : BaseAuditoriaDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int SedeId { get; set; }
        public int GrupoId { get; set; }
    }
}
