namespace VentasAPIv2.Models.Request
{
    
        public class VentaRequest
        {
            public string FechaVenta { get; set; }
            public string IDVenta { get; set; }
            public string IDCliente { get; set; }
            public decimal TotalVenta { get; set; }
            public decimal MontoPago { get; set; }
            public decimal MontoCambio { get; set; }

        public List<MisProductosVenta> misProductosVenta { get; set; }


        }

        public class MisProductosVenta
        {
        public string IDVenta { get; set; }
        public int IDProducto { get; set; }
        public string NombreProducto { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public int StockProducto { get; set; }
    }

    
}
