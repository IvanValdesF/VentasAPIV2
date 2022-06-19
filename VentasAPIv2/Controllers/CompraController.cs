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
    public class CompraController : ControllerBase
    {
        [HttpPost]
        public IActionResult AddCompra(CompraRequest request)
        {
            string msg = "";
            CompraResponse respuesta = new CompraResponse();

            try
            {
                using (SisVentasV2Context db = new SisVentasV2Context())
                {
                    var compra = new Compra();
                    compra.Total = request.TotalCompra;
                    compra.Id = request.IDCompra;
                    compra.Fecha = request.FechaCompra;
                    db.Compras.Add(compra);
                    db.SaveChanges();
                    respuesta.IDCompra = compra.Id;
                    respuesta.TotalCompra = compra.Total;
                    respuesta.FechaCompra = compra.Fecha;

                    foreach (var producto in request.misProductosCompra)
                    {
                        var concepto = new ConceptosCompra();
                        concepto.Cantidad = producto.StockProducto;
                        concepto.Producto = producto.NombreProducto;
                        concepto.PrecioUnitario = producto.PrecioCompra;
                        concepto.Total = producto.PrecioCompra * producto.StockProducto;
                        concepto.IdCompra = compra.Id;
                        concepto.PrecioVenta = producto.PrecioVenta;

                        respuesta.misProductosCompra.Add(producto);
                        
                        db.ConceptosCompras.Add(concepto);
                        db.SaveChanges();
                    }
                    
                }
                
                



            }
            catch(Exception ex)
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
                    var Clst = db.Compras.ToList();
                    var Conceptos = db.ConceptosCompras.ToList();



                    foreach(var compra in Clst)
                    {
                        CompraResponse oCompra = new CompraResponse();
                        oCompra.IDCompra = compra.Id;
                        oCompra.TotalCompra = compra.Total;
                        oCompra.FechaCompra = compra.Fecha;
                        
                        foreach(var concepto in Conceptos)
                        {
                            if(concepto.IdCompra == oCompra.IDCompra)
                            {
                                var oConcepto = new MisProductosCompra();
                                oConcepto.IDProducto = concepto.Id;
                                oConcepto.NombreProducto = concepto.Producto;
                                oConcepto.PrecioCompra = decimal.Parse(concepto.PrecioUnitario.ToString());
                                oConcepto.PrecioVenta = decimal.Parse(concepto.PrecioVenta.ToString());
                                oConcepto.StockProducto = int.Parse(concepto.Cantidad.ToString());
                                oConcepto.IDCompra = concepto.IdCompra;
                                oCompra.misProductosCompra.Add(oConcepto);
                                
                            }
                            
                        }
                        oRespuesta.lista.Add(oCompra);
                    }
                    


                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return Ok(oRespuesta.lista);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (SisVentasV2Context db = new SisVentasV2Context())
                {
                    Compra oCompra = db.Compras.Find(id);
                    db.Remove(oCompra);

                    var lst = db.ConceptosCompras.Where(d => d.IdCompra == id).ToList();

                    foreach(var concepto in lst)
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
