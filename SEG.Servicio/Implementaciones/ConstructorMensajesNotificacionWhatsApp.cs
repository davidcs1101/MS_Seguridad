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
    public class ConstructorMensajesNotificacionWhatsApp : IConstructorMensajesNotificacionWhatsApp
    {
        private readonly IConstructorTextosNotificacion _constructorTextosNotificacion;

        public ConstructorMensajesNotificacionWhatsApp(IConstructorTextosNotificacion constructorTextosNotificacion)
        {
            _constructorTextosNotificacion = constructorTextosNotificacion;
        }

        public DatoWhatsAppRequest ConstruirMensajeCreacionUsuario(SEG_Usuario usuario, string nuevaClave)
        {
            return ConfigurarDatos(
                destinatarios: new List<string> { "3153575850" },
                cuerpoMensaje: _constructorTextosNotificacion.ConstruirTextoCreacionUsuario(usuario, nuevaClave));
        }

        public DatoWhatsAppRequest ConstruirMensajeModificacionClaveUsuario(SEG_Usuario usuario)
        {
            return ConfigurarDatos(
                destinatarios: new List<string> { "3153575850" },
                cuerpoMensaje: _constructorTextosNotificacion.ConstruirTextoModificacionClaveUsuario(usuario));
        }

        public DatoWhatsAppRequest ConstruirMensajeRestablecimientoClaveUsuario(SEG_Usuario usuario, string nuevaClave) {
            return ConfigurarDatos(
                destinatarios: new List<string> { "3153575850" },
                cuerpoMensaje: _constructorTextosNotificacion.ConstruirTextoRestablecimientoClaveUsuario(usuario, nuevaClave));
        }

        private DatoWhatsAppRequest ConfigurarDatos(List<string> destinatarios, string cuerpoMensaje) 
        {
            var datoWhatsRequest = new DatoWhatsAppRequest();
            datoWhatsRequest.Destinatarios = destinatarios;
            datoWhatsRequest.Mensaje = cuerpoMensaje;

            return datoWhatsRequest;
        }
    }
}
