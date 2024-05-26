using hauzio.webapi.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace hauzio.webapi.Services
{
    public class SecurityService : ISecurityService
    {
        public string CifrarTexto(string cadena)
        {
            using (SHA256 _cifrador = SHA256.Create())
            {
                byte[] _arreglo = _cifrador.ComputeHash(Encoding.UTF8.GetBytes(cadena));
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < _arreglo.Length; i++)
                {
                    sb.Append(_arreglo[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}
