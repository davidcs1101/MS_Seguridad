using AutoMapper;
using SEG.Dominio.Entidades;
using SEG.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEG.Servicio.Utilidades
{
    public class AutoMapperPerfiles : Profile
    {
        public AutoMapperPerfiles() 
        {
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
        }
    }
}
