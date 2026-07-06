namespace SEG.Dtos
{
    public class AutorizacionDto
    {
        public int Id { get; set; }
        public string CodigoPrograma { get; set; } = null!;
        public bool EstadoPrograma { get; set; }
        public string CodigoGrupo { get; set; } = null!;
        public bool EstadoGrupo { get; set; }
        public string CodigoPermiso { get; set; } = null!;
        public bool EstadoPermiso { get; set; }
    }
}
