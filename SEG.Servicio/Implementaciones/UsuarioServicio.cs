using AutoMapper;
using SEG.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using SEG.Dominio.Entidades;
using SEG.Dtos;
using SEG.Repositorio.Interfaces;
using SEG.Servicio.Interfaces;
using SEG.Servicio.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades;
using static Utilidades.Textos;

namespace SEG.Servicio.Implementaciones
{
    public class UsuarioServicio : IUsuarioServicio
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IMSEnvioCorreosServicio _msEnvioCorreosServicio;
        private readonly IMapper _mapper;
        private readonly IUsuarioContextoServicio _usuarioContextoServicio;

        public UsuarioServicio(IUsuarioRepositorio usuarioRepositorio, IMSEnvioCorreosServicio msEnvioCorreosServicio, IMapper mapper, 
            IUsuarioContextoServicio usuarioContextoServicio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _mapper = mapper;
            _msEnvioCorreosServicio = msEnvioCorreosServicio;
            _usuarioContextoServicio = usuarioContextoServicio;
        }


        public async Task<ApiResponse<UsuarioOtrosDatosDto>> CrearAsync(UsuarioCreacionRequest usuarioCreacionRequest) 
        {
            var usuarioExiste = await _usuarioRepositorio.ObtenerPorUsuarioAsync(usuarioCreacionRequest.NombreUsuario);
            if (usuarioExiste != null)
                throw new DbUpdateException(Textos.Usuarios.MENSAJE_USUARIO_NOMBRE_EXISTE);

            usuarioExiste = await _usuarioRepositorio.ObtenerPorEmailAsync(usuarioCreacionRequest.Email);
            if (usuarioExiste != null)
                throw new DbUpdateException(Textos.Usuarios.MENSAJE_USUARIO_EMAIL_EXISTE);

            usuarioExiste = await _usuarioRepositorio.ObtenerPorIdentificacionAsync(usuarioCreacionRequest.TipoIdentificacionId, usuarioCreacionRequest.Identificacion);
            if (usuarioExiste != null)
                throw new DbUpdateException(Textos.Usuarios.MENSAJE_USUARIO_DOCUMENTO_EXISTE);

            var usuario = _mapper.Map<SEG_Usuario>(usuarioCreacionRequest);
            var nuevaClave = ProcesadorClaves.GenerarClaveSegura(20);
            usuario.Clave = ProcesadorClaves.EncriptarClave(nuevaClave);
            usuario.CambiarClave=true;
            usuario.FechaCreado = DateTime.Now;
            usuario.UsuarioCreadorId = 1;
            usuario.EstadoActivo = true;

            var id = await _usuarioRepositorio.CrearAsync(usuario);

            //Enviar correo al usuario indicandole su usuario y clave (Consumir servicio de envío de correos).
            var datoCorreoRequest = new DatoCorreoRequest();
            datoCorreoRequest.Destinatarios.Add(usuario.Email);
            datoCorreoRequest.Asunto = "Registro de usuario";
            datoCorreoRequest.Cuerpo = "Bienvenido " + usuario.Nombre1 + " " + usuario.Apellido1 + ", se ha registrado correctamente." + "\n\n" +
                             "Nuevo usuario registrado: " + usuario.NombreUsuario + "\n" +
                             "Clave de primer acceso: " + nuevaClave;
            datoCorreoRequest.esCuerpoHtml = false;

            var notificadoPorCorreo = await EnviarCorreoAsync(datoCorreoRequest);

            return new ApiResponse<UsuarioOtrosDatosDto> { Correcto = true, Mensaje = Textos.Generales.MENSAJE_REGISTRO_CREADO, Data = new UsuarioOtrosDatosDto { Id = id, Clave = nuevaClave, NotificadoPorCorreo = notificadoPorCorreo } };
        }

        public async Task<ApiResponse<UsuarioOtrosDatosDto>> ModificarClaveAsync(string clave)
        {
            var usuarioId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

            var usuarioExiste = await _usuarioRepositorio.ObtenerPorIdAsync(usuarioId);
            if (usuarioExiste == null)
                return new ApiResponse<UsuarioOtrosDatosDto> { Correcto = false, Mensaje = Textos.Usuarios.MENSAJE_USUARIO_NO_EXISTE_ID };

            usuarioExiste.Clave = ProcesadorClaves.EncriptarClave(clave);
            usuarioExiste.CambiarClave = false;
            usuarioExiste.FechaModificado = DateTime.Now;
            usuarioExiste.UsuarioModificadorId = usuarioId;
            await _usuarioRepositorio.ModificarAsync(usuarioExiste);

            //Enviar correo al usuario indicandole su usuario y clave (Consumir servicio de envío de correos).
            var datoCorreoRequest = new DatoCorreoRequest();
            datoCorreoRequest.Destinatarios.Add(usuarioExiste.Email);
            datoCorreoRequest.Asunto = "Cambiar clave de usuario";
            datoCorreoRequest.Cuerpo = "Hola " + usuarioExiste.Nombre1 + " " + usuarioExiste.Apellido1 +
                                       ", se ha realizado su cambio de clave correctamente." + "\n\n";
            datoCorreoRequest.esCuerpoHtml = false;

            var notificadoPorCorreo = await EnviarCorreoAsync(datoCorreoRequest);

            return new ApiResponse<UsuarioOtrosDatosDto> { Correcto = true, Mensaje = Textos.Generales.MENSAJE_REGISTRO_ACTUALIZADO, Data = new UsuarioOtrosDatosDto { NotificadoPorCorreo = notificadoPorCorreo } };
        }

        public async Task<ApiResponse<UsuarioOtrosDatosDto>> RestablecerClavePorUsuarioAsync(string nombreUsuario)
        {
            var usuarioExiste = await _usuarioRepositorio.ObtenerPorUsuarioAsync(nombreUsuario);
            if (usuarioExiste == null)
            {
                return new ApiResponse<UsuarioOtrosDatosDto> { Correcto = false, Mensaje = Textos.Usuarios.MENSAJE_USUARIO_NO_EXISTE_NOMBRE };
            }

            var nuevaClave = ProcesadorClaves.GenerarClaveSegura(20);
            usuarioExiste.Clave = ProcesadorClaves.EncriptarClave(nuevaClave);
            usuarioExiste.CambiarClave = true;
            usuarioExiste.FechaModificado = DateTime.Now;
            usuarioExiste.UsuarioModificadorId = 1;
            await _usuarioRepositorio.ModificarAsync(usuarioExiste);

            //Enviar correo al usuario indicandole su usuario y clave (Consumir servicio de envío de correos).
            var datoCorreoRequest = new DatoCorreoRequest();
            datoCorreoRequest.Destinatarios.Add(usuarioExiste.Email);
            datoCorreoRequest.Asunto = "Restablecer clave de usuario";
            datoCorreoRequest.Cuerpo = "Hola " + usuarioExiste.Nombre1 + " " + usuarioExiste.Apellido1 +
                                       ", se ha restablecido su clave correctamente." + "\n\n" +
                                       "Clave de primer acceso: " + nuevaClave;
            datoCorreoRequest.esCuerpoHtml = false;

            var notificadoPorCorreo = await EnviarCorreoAsync(datoCorreoRequest);

            return new ApiResponse<UsuarioOtrosDatosDto> { Correcto = true, Mensaje = Textos.Generales.MENSAJE_REGISTRO_ACTUALIZADO, Data = new UsuarioOtrosDatosDto { NotificadoPorCorreo = notificadoPorCorreo, Clave = nuevaClave } };

        }

        public async Task<ApiResponse<string>> ModificarEmailAsync(string email)
        {
            var usuarioId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

            var usuarioExiste = await _usuarioRepositorio.ObtenerPorIdAsync(usuarioId);
            if (usuarioExiste == null)
                throw new KeyNotFoundException(Textos.Usuarios.MENSAJE_USUARIO_NO_EXISTE_ID);

            var usuarioEmail = await _usuarioRepositorio.ObtenerPorEmailAsync(email);
            if (usuarioEmail != null)
                if (usuarioExiste.Id != usuarioEmail.Id)
                    throw new DbUpdateException(Textos.Usuarios.MENSAJE_USUARIO_EMAIL_EXISTE);


            usuarioExiste.Email = email;
            usuarioExiste.FechaModificado = DateTime.Now;
            usuarioExiste.UsuarioModificadorId = usuarioId;
            await _usuarioRepositorio.ModificarAsync(usuarioExiste);

            return new ApiResponse<string> { Correcto = true, Mensaje = Textos.Generales.MENSAJE_REGISTRO_ACTUALIZADO };
        }

        public async Task<ApiResponse<string>> ObtenerNombreUsuarioPorIdAsync(int id) 
        {
            var usuarioExiste = await _usuarioRepositorio.ObtenerPorIdAsync(id);
            if (usuarioExiste == null)
                throw new KeyNotFoundException(Textos.Usuarios.MENSAJE_USUARIO_NO_EXISTE_ID);

            return new ApiResponse<string> { Correcto = true, Mensaje = "", Data = usuarioExiste.NombreUsuario };
        }

        public async Task<ApiResponse<List<UsuarioDto>?>> ListarAsync(IdsListadoDto? idsListado = null)
        {
            var usuariosResultado = _usuarioRepositorio.Listar();
            if (idsListado != null && idsListado.Ids.Any())
                usuariosResultado = usuariosResultado.Where(u => idsListado.Ids.Contains(u.Id));

            var usuariosObtenidos = await usuariosResultado.ToListAsync();
            return new ApiResponse<List<UsuarioDto>?> { Correcto = true, Mensaje = "", Data = usuariosObtenidos };
        }


        #region REG_Metodos privados del servicio
        private async Task<bool> EnviarCorreoAsync(DatoCorreoRequest datoCorreoRequest)
        {
            try
            {
                var respuesta = await _msEnvioCorreosServicio.EnviarCorreoAsync(datoCorreoRequest);
                return true;
            }
            catch (Exception e)
            {
                Logs.EscribirLog("e", e.Message);
                return false;
            }
        }
        #endregion
    }
}
