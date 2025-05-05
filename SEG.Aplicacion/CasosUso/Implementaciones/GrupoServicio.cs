using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SEG.Dominio.Entidades;
using SEG.Dtos;
using Utilidades;
using SEG.Dominio.Repositorio;
using SEG.Aplicacion.CasosUso.Interfaces;
using SEG.Aplicacion.ServiciosExternos;
using SEG.Aplicacion.Servicios.Interfaces;
using SEG.Dominio.Servicios.Interfaces;

namespace SEG.Aplicacion.CasosUso.Implementaciones
{
    public class GrupoServicio : IGrupoServicio
    {
        private readonly IGrupoRepositorio _grupoRepositorio;
        private readonly IMapper _mapper;
        private readonly IUsuarioContextoServicio _usuarioContextoServicio;
        private readonly IApiResponse _apiResponse;
        private readonly IGrupoValidador _grupoValidador;

        public GrupoServicio(IGrupoRepositorio grupoRepositorio, IMapper mapper, IUsuarioContextoServicio usuarioContextoServicio, IApiResponse apiResponseServicio, IGrupoValidador grupoValidador)
        {
            _grupoRepositorio = grupoRepositorio;
            _mapper = mapper;
            _usuarioContextoServicio = usuarioContextoServicio;
            _apiResponse = apiResponseServicio;
            _grupoValidador = grupoValidador;
        }

        public async Task<ApiResponse<int>> CrearAsync(GrupoCreacionRequest grupoCreacionRequest)
        {
            var grupoExiste = await _grupoRepositorio.ObtenerPorCodigoAsync(grupoCreacionRequest.Codigo);
            _grupoValidador.ValidarDatoYaExiste(grupoExiste, Textos.Grupos.MENSAJE_GRUPO_CODIGO_EXISTE);

            var usuarioId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

            var grupo = _mapper.Map<SEG_Grupo>(grupoCreacionRequest);
            grupo.FechaCreado = DateTime.Now;
            grupo.UsuarioCreadorId = usuarioId;

            var id = await _grupoRepositorio.CrearAsync(grupo);

            return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_CREADO, id);

        }

        public async Task<ApiResponse<string>> ModificarAsync(GrupoModificacionRequest grupoModificacionRequest)
        {
            var grupoExiste = await _grupoRepositorio.ObtenerPorIdAsync(grupoModificacionRequest.Id);
            _grupoValidador.ValidarDatoNoEncontrado(grupoExiste, Textos.Grupos.MENSAJE_GRUPO_NO_EXISTE_ID);

            var usuarioId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

            _mapper.Map(grupoModificacionRequest, grupoExiste);
            grupoExiste.FechaModificado = DateTime.Now;
            grupoExiste.UsuarioModificadorId = usuarioId;

            await _grupoRepositorio.ModificarAsync(grupoExiste);

            return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_ACTUALIZADO,"");
        }

        public async Task<ApiResponse<string>> EliminarAsync(int id)
        {
            var grupoExiste = await _grupoRepositorio.ObtenerPorIdAsync(id);
            _grupoValidador.ValidarDatoNoEncontrado(grupoExiste, Textos.Grupos.MENSAJE_GRUPO_NO_EXISTE_ID);

            var eliminado = await _grupoRepositorio.EliminarAsync(id);

            if (eliminado)
                return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_ELIMINADO, "");

            return _apiResponse.CrearRespuesta(false, Textos.Generales.MENSAJE_REGISTRO_NO_ELIMINADO, "");
        }

        public async Task<ApiResponse<GrupoDto?>> ObtenerPorIdAsync(int id)
        {
            var grupoExiste = await _grupoRepositorio.ObtenerPorIdAsync(id);
            _grupoValidador.ValidarDatoNoEncontrado(grupoExiste, Textos.Grupos.MENSAJE_GRUPO_NO_EXISTE_ID);

            var grupoDto = _mapper.Map<GrupoDto>(grupoExiste);

            return _apiResponse.CrearRespuesta<GrupoDto?>(true, "", grupoDto);
        }

        public async Task<ApiResponse<GrupoDto?>> ObtenerPorCodigoAsync(string codigo)
        {
            var grupoExiste = await _grupoRepositorio.ObtenerPorCodigoAsync(codigo);
            _grupoValidador.ValidarDatoNoEncontrado(grupoExiste, Textos.Grupos.MENSAJE_GRUPO_NO_EXISTE_CODIGO);

            var grupoDto = _mapper.Map<GrupoDto>(grupoExiste);

            return _apiResponse.CrearRespuesta<GrupoDto?>(true, "", grupoDto);
        }

        public async Task<ApiResponse<List<GrupoDto>?>> ListarAsync()
        {
            var grupos = await _grupoRepositorio.Listar().ToListAsync();

            var gruposResultado = grupos
                .Select(g => new GrupoDto
                {
                    Id = g.Id,
                    Codigo = g.Codigo,
                    Nombre = g.Nombre,
                    UsuarioCreadorId = g.UsuarioCreadorId,
                    NombreUsuarioCreador = g.UsuarioCreador.NombreUsuario,
                    FechaCreado = g.FechaCreado,
                    UsuarioModificadorId = g.UsuarioModificadorId,
                    NombreUsuarioModificador = g.UsuarioModificador != null ? g.UsuarioModificador.NombreUsuario : null,
                    FechaModificado = g.FechaModificado,
                    EstadoActivo = g.EstadoActivo
                }).ToList();

            return _apiResponse.CrearRespuesta<List<GrupoDto>?>(true, "", gruposResultado);
        }

    }
}
