namespace SEG.Dtos
{
    public class PermisoDto : BaseAuditoriaDto
    {
        public int Id { get; set; }
        public int ProgramaId { get; set; }
        public int AccionId { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
    }
}
