﻿namespace SEG.Infraestructura.Servicios.Interfaces
{
    public interface IRespuestaHttpValidador
    {
        void ValidarRespuesta(HttpResponseMessage respuesta, string mensaje);
    }
}
