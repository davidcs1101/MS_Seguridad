using System.Security.Cryptography;
using System.Text;

namespace SEG.Servicio.Utilidades
{
    public static class ProcesadorClaves
    {
        //Para generar una clave segura
        public static string GenerarClaveSegura(int longitud) 
        {
            byte[] bytesAleatorios = new byte[longitud];
            using (var cripto = RandomNumberGenerator.Create())
            {
                cripto.GetBytes(bytesAleatorios);
            }
            return Convert.ToBase64String(bytesAleatorios);
        }

        public static string EncriptarClave(string clave) 
        {
            using(var sha256 = SHA256.Create()) 
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(clave));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
