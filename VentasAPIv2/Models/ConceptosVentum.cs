using System;
using System.Collections.Generic;

namespace VentasAPIv2.Models
{
    public partial class ConceptosVentum
    {
        public int Id { get; set; }
        public string IdVenta { get; set; } = null!;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal Total { get; set; }
        public string Producto { get; set; } = null!;

        public virtual Ventum IdVentaNavigation { get; set; } = null!;
    }
}
