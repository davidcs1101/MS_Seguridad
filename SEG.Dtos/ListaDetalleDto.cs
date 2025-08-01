namespace SEG.Dtos
{
    public class ListaDetalleDto : BaseAuditoriaDto
    {
        public int Id { get; set; }
        public int ListaId { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public string CodigoLista { get; set; } = null!;
        public string CodigoDatoConstante { get; set; } = null!;
    }
}
