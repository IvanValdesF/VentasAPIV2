namespace VentasAPIv2.Models.Request
{
    public class ProductRequest
    {
        public int IDProducto { get; set; }
        public string NombreProducto { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public int stockProducto { get; set; }
        public string ImagenProducto { get; set; }

        public string NombreImagen { get; set; }
    }
}
