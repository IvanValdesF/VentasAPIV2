using VentasAPIv2.Models.Request;

namespace VentasAPIv2.Models.Response
{
    public class CompraResponse
    {
        
            public string FechaCompra { get; set; }
            public string IDCompra { get; set; }
            public decimal TotalCompra { get; set; }

            public List<MisProductosCompra> misProductosCompra { get; set; }

            public CompraResponse()
        {
            misProductosCompra = new List<MisProductosCompra>();
        }

    }

    
}
