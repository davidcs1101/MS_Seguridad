using AutoMapper;
using SEG.Dominio.Entidades;
using SEG.Dtos;

namespace SEG.Infraestructura.Mapeos
{
    public class AutoMapperPerfiles : Profile
    {
        public AutoMapperPerfiles()
        {
            CreateMap<UsuarioSedeCreacionRequest, UsuarioCreacionRequest>();
            CreateMap<UsuarioCreacionRequest, SEG_Usuario>();
            
            CreateMap<UsuarioSedeGrupoCreacionRequest, SEG_UsuarioSedeGrupo>();
            CreateMap<UsuarioSedeGrupoModificacionRequest, SEG_UsuarioSedeGrupo>();
            CreateMap<SEG_UsuarioSedeGrupo, UsuarioSedeGrupoDto>();

            CreateMap<GrupoCreacionRequest, SEG_Grupo>();
            CreateMap<GrupoModificacionRequest, SEG_Grupo>();
            CreateMap<SEG_Grupo, GrupoDto>();

            CreateMap<ProgramaCreacionRequest, SEG_Programa>();
            CreateMap<ProgramaModificacionRequest, SEG_Programa>();
            CreateMap<SEG_Programa, ProgramaDto>();

            CreateMap<GrupoProgramaCreacionRequest, SEG_GrupoPrograma>();
            CreateMap<SEG_GrupoPrograma, GrupoProgramaDto>();
        }
    }
}
