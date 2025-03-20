using SEG.Dominio.Entidades;
using SEG.Dtos;
using SEG.Servicio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Utilidades.Textos;

namespace SEG.Servicio.Implementaciones
{
    public class ConstructorMensajesNotificacionCorreo : IConstructorMensajesNotificacionCorreo
    {
        private readonly IConstructorTextosNotificacion _constructorTextosNotificacion;

        public ConstructorMensajesNotificacionCorreo(IConstructorTextosNotificacion constructorTextosNotificacion)
        {
            _constructorTextosNotificacion = constructorTextosNotificacion;
        }

        public DatoCorreoRequest ConstruirMensajeCreacionUsuario(SEG_Usuario usuario, string nuevaClave)
        {
            return ConfigurarDatos(
                destinatarios: new List<string> { usuario.Email },
                asunto: "Registro de usuario",
                cuerpoMensaje: _constructorTextosNotificacion.ConstruirTextoCreacionUsuario(usuario, nuevaClave),
                esHtml: false);
        }

        public DatoCorreoRequest ConstruirMensajeModificacionClaveUsuario(SEG_Usuario usuario)
        {
            return ConfigurarDatos(
                destinatarios: new List<string> { usuario.Email },
                asunto: "Cambiar clave de usuario",
                cuerpoMensaje: _constructorTextosNotificacion.ConstruirTextoModificacionClaveUsuario(usuario),
                esHtml: false);
        }

        public DatoCorreoRequest ConstruirMensajeRestablecimientoClaveUsuario(SEG_Usuario usuario, string nuevaClave) {
            return ConfigurarDatos(
                destinatarios: new List<string> { usuario.Email },
                asunto: "Restablecer clave de usuario",
                cuerpoMensaje: _constructorTextosNotificacion.ConstruirTextoRestablecimientoClaveUsuario(usuario, nuevaClave),
                esHtml: false);
        }

        private DatoCorreoRequest ConfigurarDatos(List<string> destinatarios, string asunto, string cuerpoMensaje,bool esHtml) 
        {
            var datoCorreoRequest = new DatoCorreoRequest();
            datoCorreoRequest.Destinatarios = destinatarios;
            datoCorreoRequest.Asunto = asunto;
            datoCorreoRequest.Cuerpo = cuerpoMensaje;
            datoCorreoRequest.EsCuerpoHtml = esHtml;

            return datoCorreoRequest;
        }
    }
}
