using SEG.Dominio.Entidades;
using SEG.Dominio.Entidades.ModelosVistas;
using SEG.Dtos;

namespace SEG.Aplicacion.ServiciosExternos.Mapeo
{
    public interface IMapperPerfiles
    {
        UsuarioCreacionRequest Map(UsuarioSedeCreacionRequest source);
        SEG_Usuario Map(UsuarioCreacionRequest source);

        SEG_UsuarioSedeGrupo Map(UsuarioSedeGrupoCreacionRequest source);
        void Map(UsuarioSedeGrupoModificacionRequest source, SEG_UsuarioSedeGrupo target);
        UsuarioSedeGrupoDto Map(SEG_UsuarioSedeGrupo source);

        SEG_Grupo Map(GrupoCreacionRequest source);
        void Map(GrupoModificacionRequest source, SEG_Grupo target);
        GrupoDto Map(SEG_Grupo source);

        SEG_Programa Map(ProgramaCreacionRequest source);
        void Map(ProgramaModificacionRequest source, SEG_Programa target);
        ProgramaDto Map(SEG_Programa source);

        SEG_Accion Map(AccionCreacionRequest source);
        void Map(AccionModificacionRequest source, SEG_Accion target);
        AccionDto Map(SEG_Accion source);

        SEG_GrupoPrograma Map(GrupoProgramaCreacionRequest source);
        GrupoProgramaDto Map(SEG_GrupoPrograma source);

        List<AutorizacionDto> Map(IEnumerable<AutorizacionMV> source);
        List<GrupoPermisoDto> Map(IEnumerable<SEG_GrupoPermiso> source);

        PermisoDto Map(SEG_Permiso source);
    }
}
