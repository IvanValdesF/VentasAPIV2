using System;
using System.Collections.Generic;

namespace VentasAPIv2.Models
{
    public partial class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Telefono { get; set; } = null!;
    }
}
