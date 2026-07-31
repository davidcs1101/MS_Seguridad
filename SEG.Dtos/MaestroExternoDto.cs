namespace SEG.Dtos
{
    public class MaestroExternoDto : BaseAuditoriaDto
    {
        public int Id { get; set; }
        public string ServicioOrigen { get; set; } = null!;
        public string CodigoMaestro { get; set; } = null!;
        public int OrigenId { get; set; } //Equivalente al Id de ListasDetalles
        public int? OrigenPadreId { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
    }
}
