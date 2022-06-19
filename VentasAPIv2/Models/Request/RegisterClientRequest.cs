namespace VentasAPIv2.Models.Request
{
    public class RegisterClientRequest
    {
        public string Correo { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
    }
}
