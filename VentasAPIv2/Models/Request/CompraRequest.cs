namespace VentasAPIv2.Models.Request
{
    public class CompraRequest
    {
        public string FechaCompra { get; set; }
        public string IDCompra { get; set; }
        public decimal TotalCompra { get; set; }

        public List<MisProductosCompra> misProductosCompra { get; set; }

        
    }

    public class MisProductosCompra
    {
        public int IDProducto { get; set; }
        public string NombreProducto { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public int StockProducto { get; set; }

        public string IDCompra { get; set; }
    }
}
