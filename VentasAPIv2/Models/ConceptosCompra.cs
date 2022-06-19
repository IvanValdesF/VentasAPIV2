using System;
using System.Collections.Generic;

namespace VentasAPIv2.Models
{
    public partial class ConceptosCompra
    {
        public int Id { get; set; }
        public string IdCompra { get; set; } = null!;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Total { get; set; }
        public string Producto { get; set; } = null!;
        public decimal PrecioVenta { get; set; }

        public virtual Compra IdCompraNavigation { get; set; } = null!;
    }
}
