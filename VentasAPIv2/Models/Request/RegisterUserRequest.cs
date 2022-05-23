namespace VentasAPI.Models.Request
{
    public class RegisterUserRequest
    {
        public int IDUsuario { get; set; } 
        public string Correo { get; set; }

        public string Contraseña { get; set; }

        public string NombreUsuario { get; set; }

        public int Rol { get; set; }


    }
}
