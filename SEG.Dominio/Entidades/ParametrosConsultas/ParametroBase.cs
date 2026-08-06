namespace SEG.Dominio.Entidades.ParametrosConsultas
{
    public class ParametroBase
    {
        public int Pagina { get; set; } = 1;
        public int RegistrosPorPagina { get; set; } = 20;
        public string? CampoOrden { get; set; }
        public bool Descendente { get; set; }
    }
}
