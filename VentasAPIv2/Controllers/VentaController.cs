using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VentasAPI.Models.Response;
using VentasAPIv2.Models;
using VentasAPIv2.Models.Request;
using VentasAPIv2.Models.Response;

namespace VentasAPIv2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        [HttpPost]
        public IActionResult AddCompra(VentaRequest request)
        {
            string msg = "";
            VentaResponse respuesta = new VentaResponse();

            try
            {
                using (SisVentasV2Context db = new SisVentasV2Context())
                {
                    var venta = new Ventum();
                    venta.Total = request.TotalVenta;
                    venta.Id = request.IDVenta;
                    venta.Fecha = request.FechaVenta;
                    venta.Idcliente = request.IDCliente;
                    venta.Montocambio = request.MontoCambio;
                    venta.Montopago = request.MontoPago;
                    
                    db.Venta.Add(venta);
                    db.SaveChanges();
                    respuesta.IDVenta = venta.Id;
                    respuesta.TotalVenta = venta.Total;
                    respuesta.FechaVenta = venta.Fecha;
                    respuesta.IDCliente = venta.Idcliente;
                    respuesta.MontoPago = venta.Montopago;
                    respuesta.MontoCambio = venta.Montocambio;

                    foreach (var producto in request.misProductosVenta)
                    {
                        var concepto = new ConceptosVentum();
                        concepto.Cantidad = producto.StockProducto;
                        concepto.Producto = producto.NombreProducto;
                        concepto.PrecioVenta = producto.PrecioVenta;
                        concepto.Total = producto.PrecioVenta * producto.StockProducto;
                        concepto.IdVenta = request.IDVenta;
                        concepto.PrecioUnitario = producto.PrecioCompra;
                        
                        

                        respuesta.misProductosVenta.Add(producto);

                        db.ConceptosVenta.Add(concepto);
                        db.SaveChanges();
                    }

                }





            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return Ok(respuesta);
        }

        [HttpGet]
        public IActionResult Get()
        {
            string msg = "";
            Respuesta oRespuesta = new Respuesta();

            try
            {

                using (SisVentasV2Context db = new SisVentasV2Context())
                {
                    var Ventas = db.Venta.ToList();
                    var Conceptos = db.ConceptosVenta.ToList();



                    foreach (var venta in Ventas)
                    {
                        VentaResponse oVenta = new VentaResponse();
                        oVenta.IDVenta = venta.Id;
                        oVenta.TotalVenta = venta.Total;
                        oVenta.FechaVenta = venta.Fecha;
                        oVenta.IDCliente = venta.Idcliente;
                        oVenta.MontoCambio = venta.Montocambio;
                        oVenta.MontoPago = venta.Montopago;

                        foreach (var concepto in Conceptos)
                        {
                            if (concepto.IdVenta == oVenta.IDVenta)
                            {
                                var oConcepto = new MisProductosVenta();
                                oConcepto.IDProducto = concepto.Id;
                                oConcepto.NombreProducto = concepto.Producto;
                                oConcepto.PrecioCompra = decimal.Parse(concepto.PrecioUnitario.ToString());
                                oConcepto.PrecioVenta = decimal.Parse(concepto.PrecioVenta.ToString());
                                oConcepto.StockProducto = int.Parse(concepto.Cantidad.ToString());
                                oConcepto.IDVenta = concepto.IdVenta;
                                oVenta.misProductosVenta.Add(oConcepto);

                            }

                        }
                        oRespuesta.listaVentas.Add(oVenta);
                    }



                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return Ok(oRespuesta.listaVentas);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (SisVentasV2Context db = new SisVentasV2Context())
                {
                    Ventum oVenta = db.Venta.Find(id);
                    db.Remove(oVenta);

                    var lst = db.ConceptosVenta.Where(d => d.IdVenta == id).ToList();

                    foreach (var concepto in lst)
                    {
                        db.Remove(concepto);
                    }







                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Compra eliminada Correctamente";
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }
    }
}
