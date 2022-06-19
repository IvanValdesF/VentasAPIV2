namespace VentasAPI.Models.Request
{
    public class RegisterUserRequest
    {
        public int Id { get; set; } 
        public string Correo { get; set; }

        public string Contraseña { get; set; }

        public string Nombre { get; set; }

        public int Rol { get; set; }


    }
}
