namespace VentasAPI.Models.Response
{
    public class UserResponse
    {
        public string Contraseña { get; set; }
        public string Correo { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Rol { get; set; }

        public string Token { get; set; }

        public string mensaje { get; set; }

    }
}
