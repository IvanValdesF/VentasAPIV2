using System;
using System.Collections.Generic;

namespace VentasAPIv2.Models
{
    public partial class Producto
    {
        public int Idproducto { get; set; }
        public string? NombreProducto { get; set; }
        public decimal? PrecioCompra { get; set; }
        public decimal? PrecioVenta { get; set; }
        public int? StockProducto { get; set; }
        public string? ImagenProducto { get; set; }
    }
}
