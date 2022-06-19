using System;
using System.Collections.Generic;

namespace VentasAPIv2.Models
{
    public partial class Sesionesactiva
    {
        public int Idusuario { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public int Rol { get; set; }
        public string Contraseña { get; set; } = null!;
        public string Correo { get; set; } = null!;
    }
}
