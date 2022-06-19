namespace VentasAPIv2.Models.Response
{
    public class ProductResponse
    {
        public int IDProducto { get; set; }
        public string NombreProducto { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public int StockProducto { get; set; }
        public string ImagenProducto { get; set; }

       
    }
}
