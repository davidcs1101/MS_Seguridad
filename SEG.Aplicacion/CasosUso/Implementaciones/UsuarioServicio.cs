﻿using AutoMapper;
using SEG.Dtos;
using Microsoft.EntityFrameworkCore;
using SEG.Dominio.Entidades;
using SEG.Dominio.Repositorio;
using SEG.Aplicacion.CasosUso.Interfaces;
using SEG.Aplicacion.ServiciosExternos;
using SEG.Aplicacion.Servicios.Interfaces;
using Utilidades;
using SEG.Dominio.Servicios.Interfaces;
using SEG.Dominio.Repositorio.UnidadTrabajo;

namespace SEG.Aplicacion.CasosUso.Implementaciones
{
    public class UsuarioServicio : IUsuarioServicio
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IConstructorMensajesNotificacionCorreo _constructorMensajesNotificacionCorreo;
        private readonly INotificadorCorreo _notificadorCorreo;
        private readonly IMapper _mapper;
        private readonly IUsuarioContextoServicio _usuarioContextoServicio;
        private readonly IUsuarioValidador _usuarioValidador;
        private readonly IApiResponse _apiResponse;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IGrupoRepositorio _grupoRepositorio;
        private readonly IGrupoValidador _grupoValidador;
        private readonly IColaSolicitudRepositorio _colaSolicitudRepositorio;
        private readonly IUsuarioSedeGrupoRepositorio _usuarioSedeGrupoRepositorio;

        public UsuarioServicio(IUsuarioRepositorio usuarioRepositorio, IMapper mapper, IUsuarioContextoServicio usuarioContextoServicio,
            IUsuarioValidador usuarioValidador, IConstructorMensajesNotificacionCorreo constructorMensajesNotificacionCorreo, INotificadorCorreo notificadorCorreo, IApiResponse apiResponseServicio, IUnidadDeTrabajo unidadDeTrabajo, IGrupoRepositorio grupoRepositorio, IGrupoValidador grupoValidador, IColaSolicitudRepositorio colaSolicitudRepositorio, IUsuarioSedeGrupoRepositorio usuarioSedeGrupoRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _mapper = mapper;
            _usuarioContextoServicio = usuarioContextoServicio;
            _usuarioValidador = usuarioValidador;
            _constructorMensajesNotificacionCorreo = constructorMensajesNotificacionCorreo;
            _notificadorCorreo = notificadorCorreo;
            _apiResponse = apiResponseServicio;
            _unidadDeTrabajo = unidadDeTrabajo;
            _grupoRepositorio = grupoRepositorio;
            _grupoValidador = grupoValidador;
            _colaSolicitudRepositorio = colaSolicitudRepositorio;
            _usuarioSedeGrupoRepositorio = usuarioSedeGrupoRepositorio;
        }


        public async Task<ApiResponse<UsuarioOtrosDatosDto>> RegistrarAsync(UsuarioCreacionRequest usuarioCreacionRequest) 
        {
            var nuevaClave = ProcesadorClaves.GenerarClaveSegura(20);
            var usuario = await this.AsignarDatosAsync(usuarioCreacionRequest, 1, nuevaClave);
            var id = await _usuarioRepositorio.CrearAsync(usuario);
            var datosCorreo = _constructorMensajesNotificacionCorreo.ConstruirMensajeCreacionUsuario(usuario, nuevaClave);
            var notificado = await _notificadorCorreo.EnviarAsync(datosCorreo);

            return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_CREADO, new UsuarioOtrosDatosDto { Id = id, NotificadoPorCorreo = notificado });
        }

        public async Task<ApiResponse<UsuarioOtrosDatosDto>> RegistrarConSedeAsync(UsuarioSedeCreacionRequest usuarioSedeCreacionRequest)
        {
            await using var transaccion = await _unidadDeTrabajo.IniciarTransaccionAsync();

            try
            {
                var nuevaClave = ProcesadorClaves.GenerarClaveSegura(20);
                var usuarioCreacionRequest = _mapper.Map<UsuarioCreacionRequest>(usuarioSedeCreacionRequest);
                var usuarioCreadorId = _usuarioContextoServicio.ObtenerUsuarioIdToken();
                var usuario = await this.AsignarDatosAsync(usuarioCreacionRequest, usuarioCreadorId, nuevaClave);

                _usuarioRepositorio.MarcarCrear(usuario);
                await _unidadDeTrabajo.GuardarCambiosAsync();

                var grupo = await _grupoRepositorio.ObtenerPorCodigoAsync("ADMINISTRADOREMPRESA");
                _grupoValidador.ValidarDatoNoEncontrado(grupo, Textos.Grupos.MENSAJE_GRUPO_NO_EXISTE_CODIGO);

                var usuarioSedeGrupo = new SEG_UsuarioSedeGrupo()
                {
                    UsuarioId = usuario.Id,
                    SedeId = usuarioSedeCreacionRequest.SedeId,
                    GrupoId = grupo.Id,
                    FechaCreado = DateTime.Now,
                    UsuarioCreadorId = usuarioCreadorId,
                    EstadoActivo = true
                };
                _usuarioSedeGrupoRepositorio.MarcarCrear(usuarioSedeGrupo);

                var datosCorreo = _constructorMensajesNotificacionCorreo.ConstruirMensajeCreacionUsuario(usuario, nuevaClave);
                var solicitud = new SEG_ColaSolicitud
                {
                    Tipo = "EnviarCorreoCreacionUsuario",
                    Payload = Utilidades.JsonHelper.Serializar(datosCorreo),
                    Estado = Textos.EstadosColas.PENDIENTE,
                    Reintentos = 0,
                    FechaCreado = DateTime.Now
                };
                _colaSolicitudRepositorio.MarcarCrear(solicitud);

                await _unidadDeTrabajo.GuardarCambiosAsync();
                await transaccion.CommitAsync();

                return _apiResponse.CrearRespuesta<UsuarioOtrosDatosDto>(true, "Usuario creado", null);
            }
            catch (Exception e)
            {
                await transaccion.RollbackAsync();
                throw new Exception(e.Message);
            }
        }

        public async Task<ApiResponse<UsuarioOtrosDatosDto>> ModificarClaveAsync(string clave)
        {
            var usuarioId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

            var usuarioExiste = await _usuarioRepositorio.ObtenerPorIdAsync(usuarioId);
            _usuarioValidador.ValidarDatoNoEncontrado(usuarioExiste, Textos.Usuarios.MENSAJE_USUARIO_NO_EXISTE_ID);

            usuarioExiste.Clave = ProcesadorClaves.EncriptarClave(clave);
            usuarioExiste.CambiarClave = false;
            usuarioExiste.FechaModificado = DateTime.Now;
            usuarioExiste.UsuarioModificadorId = usuarioId;
            await _usuarioRepositorio.ModificarAsync(usuarioExiste);

            var datosCorreo = _constructorMensajesNotificacionCorreo.ConstruirMensajeModificacionClaveUsuario(usuarioExiste);
            var notificado = await _notificadorCorreo.EnviarAsync(datosCorreo);

            return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_ACTUALIZADO, new UsuarioOtrosDatosDto { NotificadoPorCorreo = notificado });
        }

        public async Task<ApiResponse<UsuarioOtrosDatosDto>> RestablecerClavePorUsuarioAsync(string nombreUsuario)
        {
            var usuarioExiste = await _usuarioRepositorio.ObtenerPorUsuarioAsync(nombreUsuario);
            _usuarioValidador.ValidarDatoNoEncontrado(usuarioExiste, Textos.Usuarios.MENSAJE_USUARIO_NO_EXISTE_NOMBRE);

            var nuevaClave = ProcesadorClaves.GenerarClaveSegura(20);
            usuarioExiste.Clave = ProcesadorClaves.EncriptarClave(nuevaClave);
            usuarioExiste.CambiarClave = true;
            usuarioExiste.FechaModificado = DateTime.Now;
            usuarioExiste.UsuarioModificadorId = 1;
            await _usuarioRepositorio.ModificarAsync(usuarioExiste);

            var datosCorreo = _constructorMensajesNotificacionCorreo.ConstruirMensajeRestablecimientoClaveUsuario(usuarioExiste,nuevaClave);
            var notificado = await _notificadorCorreo.EnviarAsync(datosCorreo);

            return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_ACTUALIZADO, new UsuarioOtrosDatosDto { NotificadoPorCorreo = notificado, Clave = nuevaClave });
        }

        public async Task<ApiResponse<string>> ModificarEmailAsync(string email)
        {
            var usuarioId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

            var usuarioExiste = await _usuarioRepositorio.ObtenerPorIdAsync(usuarioId);
            _usuarioValidador.ValidarDatoNoEncontrado(usuarioExiste, Textos.Usuarios.MENSAJE_USUARIO_NO_EXISTE_ID);

            var usuarioEmail = await _usuarioRepositorio.ObtenerPorEmailAsync(email);
            _usuarioValidador.ValidarEmailTieneOtroUsuario(usuarioEmail, usuarioExiste.Id, Textos.Usuarios.MENSAJE_USUARIO_EMAIL_EXISTE);

            usuarioExiste.Email = email;
            usuarioExiste.FechaModificado = DateTime.Now;
            usuarioExiste.UsuarioModificadorId = usuarioId;
            await _usuarioRepositorio.ModificarAsync(usuarioExiste);

            return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_ACTUALIZADO,"");
        }

        public async Task<ApiResponse<string>> ObtenerNombreUsuarioPorIdAsync(int id) 
        {
            var usuarioExiste = await _usuarioRepositorio.ObtenerPorIdAsync(id);
            _usuarioValidador.ValidarDatoNoEncontrado(usuarioExiste, Textos.Usuarios.MENSAJE_USUARIO_NO_EXISTE_ID);

            return _apiResponse.CrearRespuesta(true, "", usuarioExiste.NombreUsuario);
        }

        public async Task<ApiResponse<List<UsuarioDto>?>> ListarAsync(IdsListadoDto? idsListado = null)
        {
            var usuariosResultado = _usuarioRepositorio.Listar()
                .Select(u => new UsuarioDto
                {
                    Id = u.Id,
                    TipoIdentificacionId = u.TipoIdentificacionId,
                    Identificacion = u.Identificacion,
                    Nombre1 = u.Nombre1,
                    Nombre2 = u.Nombre2,
                    Apellido1 = u.Apellido1,
                    Apellido2 = u.Apellido2,
                    NombreUsuario = u.NombreUsuario,
                    UsuarioCreadorId = u.UsuarioCreadorId,
                    FechaCreado = u.FechaCreado,
                    UsuarioModificadorId = u.UsuarioModificadorId,
                    FechaModificado = u.FechaModificado,
                    EstadoActivo = u.EstadoActivo
                });

            if (idsListado != null && idsListado.Ids.Any())
                usuariosResultado = usuariosResultado.Where(u => idsListado.Ids.Contains(u.Id));

            var usuariosObtenidos = await usuariosResultado.ToListAsync();
            return _apiResponse.CrearRespuesta<List<UsuarioDto>?>(true,"", usuariosObtenidos);
        }



        private async Task<SEG_Usuario> AsignarDatosAsync(UsuarioCreacionRequest usuarioCreacionRequest, int usuarioCreadorId, string nuevaClave) 
        {
            var usuarioExiste = await _usuarioRepositorio.ObtenerPorUsuarioAsync(usuarioCreacionRequest.NombreUsuario);
            _usuarioValidador.ValidarDatoYaExiste(usuarioExiste, Textos.Usuarios.MENSAJE_USUARIO_NOMBRE_EXISTE);

            usuarioExiste = await _usuarioRepositorio.ObtenerPorEmailAsync(usuarioCreacionRequest.Email);
            _usuarioValidador.ValidarDatoYaExiste(usuarioExiste, Textos.Usuarios.MENSAJE_USUARIO_EMAIL_EXISTE);

            usuarioExiste = await _usuarioRepositorio.ObtenerPorIdentificacionAsync(usuarioCreacionRequest.TipoIdentificacionId, usuarioCreacionRequest.Identificacion);
            _usuarioValidador.ValidarDatoYaExiste(usuarioExiste, Textos.Usuarios.MENSAJE_USUARIO_DOCUMENTO_EXISTE);

            var usuario = _mapper.Map<SEG_Usuario>(usuarioCreacionRequest);
            usuario.Clave = ProcesadorClaves.EncriptarClave(nuevaClave);
            usuario.CambiarClave = true;
            usuario.FechaCreado = DateTime.Now;
            usuario.UsuarioCreadorId = usuarioCreadorId;
            usuario.EstadoActivo = true;

            return usuario;
        }
    }
}
