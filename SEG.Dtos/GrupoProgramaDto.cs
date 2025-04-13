namespace SEG.Dtos
{
    public class GrupoProgramaDto: BaseAuditoriaDto
    {
        public int Id { get; set; }
        public int GrupoId { get; set; }
        public int ProgramaId { get; set; }
    }
}
