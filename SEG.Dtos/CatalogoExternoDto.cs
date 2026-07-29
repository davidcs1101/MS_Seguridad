namespace SEG.Dtos
{
    public class CatalogoExternoDto : BaseAuditoriaDto
    {
        public int Id { get; set; }
        public string ServicioOrigen { get; set; } = null!;
        public string CodigoCatalogo { get; set; } = null!;
        public int OrigenId { get; set; } //Equivalente al Id de ListasDetalles
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
    }
}
