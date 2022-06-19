

using VentasAPIv2.Models.Request;
using VentasAPIv2.Models.Response;

namespace VentasAPI.Models.Response
{
    public class Respuesta
    {
        public int Exito { get; set; }

        public string Mensaje { get; set; }
        public object Data { get; set; }

        public List<CompraResponse> lista { get; set; }

        public List<VentaResponse> listaVentas { get; set; }

        public Respuesta()
        {
            this.Exito = 0;
            lista = new List<CompraResponse>();

            listaVentas = new List<VentaResponse>();
        }
    }

    
}
