namespace hauzio.webapi.DTOs
{
    public class RespuestaDTO<T>
    {
        public T Data { get; set; }
        public bool Estatus { get; set; }
        public string Error { get; set; }
        public string Mensaje { get; set; }
    }
}
