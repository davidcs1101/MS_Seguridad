using Riok.Mapperly.Abstractions;
using SEG.Aplicacion.ServiciosExternos.Mapeo;
using SEG.Dominio.Entidades;
using SEG.Dominio.Entidades.ModelosVistas;
using SEG.Dtos;

namespace SEG.Infraestructura.Mapeos
{
    [Mapper]
    public partial class MapperPerfiles : IMapperPerfiles
    {
        public partial UsuarioCreacionRequest Map(UsuarioSedeCreacionRequest source);
        public partial SEG_Usuario Map(UsuarioCreacionRequest source);

        public partial SEG_UsuarioSedeGrupo Map(UsuarioSedeGrupoCreacionRequest source);
        public partial void Map(UsuarioSedeGrupoModificacionRequest source, SEG_UsuarioSedeGrupo target);
        public partial UsuarioSedeGrupoDto Map(SEG_UsuarioSedeGrupo source);

        public partial SEG_Grupo Map(GrupoCreacionRequest source);
        public partial void Map(GrupoModificacionRequest source, SEG_Grupo target);
        public partial GrupoDto Map(SEG_Grupo source);

        public partial SEG_Programa Map(ProgramaCreacionRequest source);
        public partial void Map(ProgramaModificacionRequest source, SEG_Programa target);
        public partial ProgramaDto Map(SEG_Programa source);

        public partial SEG_Accion Map(AccionCreacionRequest source);
        public partial void Map(AccionModificacionRequest source, SEG_Accion target);
        public partial AccionDto Map(SEG_Accion source);

        public partial SEG_GrupoPermiso Map(GrupoPermisoCreacionRequest source);
        public partial GrupoPermisoDto Map(SEG_GrupoPermiso source);

        public partial List<AutorizacionDto> Map(IEnumerable<AutorizacionMV> source);
        public partial List<GrupoPermisoDto> Map(IEnumerable<SEG_GrupoPermiso> source);

        public partial PermisoDto Map(SEG_Permiso source);
    }
}