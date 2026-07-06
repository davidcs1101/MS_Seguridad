namespace SEG.Dtos
{
    public class GrupoPermisoDto : BaseAuditoriaDto
    {
        public int Id { get; set; }
        public GrupoDto Grupo { get; set; } = new GrupoDto();
        public PermisoDto Permiso { get; set; } = new PermisoDto();
    }
}
