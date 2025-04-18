﻿namespace SEG.Dominio.Excepciones
{
    public class DatoYaExisteException : Exception
    {
        public DatoYaExisteException(string mensaje) : base(mensaje){ }
    }

    public class DatoNoEncontradoException : Exception
    {
        public DatoNoEncontradoException(string mensaje) : base(mensaje) { }
    }

    public class SolicitudHttpException : Exception
    {
        public SolicitudHttpException(string mensaje) : base(mensaje) { }
    }
}
