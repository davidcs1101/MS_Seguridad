namespace SEG.Dtos
{
    public class SedeDto : BaseAuditoriaDto
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public short Consecutivo { get; set; }
        public string? Descripcion { get; set; }
        public int? MunicipioId { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Extension { get; set; }
        public string? Celular { get; set; }
        public string? Email { get; set; }
    }
}
