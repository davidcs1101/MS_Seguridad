﻿namespace SEG.Aplicacion.ServiciosExternos
{
    public interface IRespuestaHttpValidador
    {
        Task ValidarRespuesta(HttpResponseMessage respuesta, string mensaje);
    }
}
