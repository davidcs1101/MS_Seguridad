namespace SEG.Dtos
{
    public class ProgramaDto : BaseAuditoriaDto
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
    }
}
