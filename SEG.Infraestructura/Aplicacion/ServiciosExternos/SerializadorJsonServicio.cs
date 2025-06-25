using SEG.Aplicacion.ServiciosExternos;
using Newtonsoft.Json;

namespace SEG.Infraestructura.Aplicacion.ServiciosExternos
{
    public class SerializadorJsonServicio : ISerializadorJsonServicio
    {
        public string Serializar<T>(T objeto)
        {
            return JsonConvert.SerializeObject(objeto);
        }

        public T Deserializar<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
