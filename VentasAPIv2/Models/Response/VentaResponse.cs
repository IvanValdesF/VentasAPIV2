using VentasAPIv2.Models.Request;

namespace VentasAPIv2.Models.Response
{
    public class VentaResponse
    {
        public string FechaVenta { get; set; }
        public string IDVenta { get; set; }
        public decimal TotalVenta { get; set; }
        public string IDCliente { get; set; }
        public decimal MontoCambio { get; set; }
        public decimal MontoPago { get; set; }

        public List<MisProductosVenta> misProductosVenta { get; set; }

        public VentaResponse()
        {
            misProductosVenta = new List<MisProductosVenta>();
        }
    }
}
