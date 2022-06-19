using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using VentasAPI.Models.Response;
using VentasAPIv2.Models;
using VentasAPIv2.Models.Request;
using VentasAPIv2.Models.Response;

namespace VentasAPIv2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private static IWebHostEnvironment _enviroment;


        public ProductController(IWebHostEnvironment env)
        {
            _enviroment = env;
        }

        [HttpPost]
        public IActionResult Add([FromBody]ProductRequest oRequest)
        {
            ProductResponse oRespuesta = new ProductResponse();
            try
            {

                using (SisVentasV2Context db = new SisVentasV2Context())
                {

                    
                        Producto oProducto = new Producto();
                        oProducto.Idproducto = oRequest.IDProducto;
                        oProducto.NombreProducto = oRequest.NombreProducto;
                        oProducto.PrecioCompra = oRequest.PrecioCompra;
                        oProducto.PrecioVenta = oRequest.PrecioVenta;
                        oProducto.StockProducto = oRequest.stockProducto;
                        
                        string v = oRequest.NombreImagen.Substring(0, oRequest.NombreImagen.Length - 4);
                        
                        oProducto.ImagenProducto = "http://apiventas.somee.com/api/product/img/" + oProducto.Idproducto.ToString();
                        




                        if (oRequest.ImagenProducto != null)
                        {
                            string path = _enviroment.WebRootPath + "\\uploads\\";
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            byte[] imagen = new byte[Convert.FromBase64String(oRequest.ImagenProducto).Length - 1];
                            imagen = Convert.FromBase64String(oRequest.ImagenProducto);

                            var SW = new StreamWriter(path + oProducto.Idproducto.ToString() + ".jpg");
                            SW.BaseStream.Write(imagen, 0, imagen.Length - 1);
                            SW.Close();
                                
                            
                        }

                        

                        db.Productos.Add(oProducto);

                        db.SaveChanges();
                        oRespuesta.IDProducto = oProducto.Idproducto;
                        oRespuesta.NombreProducto = oProducto.NombreProducto;
                        oRespuesta.PrecioCompra = decimal.Parse(oProducto.PrecioCompra.ToString());
                        oRespuesta.PrecioVenta = decimal.Parse(oProducto.PrecioVenta.ToString());
                        oRespuesta.StockProducto = int.Parse(oProducto.StockProducto.ToString());
                        oRespuesta.ImagenProducto = "http://apiventas.somee.com/api/product/img/" + v;


                    
                }

            }
            catch (Exception ex)
            {
                oRespuesta.NombreProducto = ex.Message;
            }
            return Ok(oRespuesta);
        }

        [HttpGet("img/{id}")]

        public async Task<IActionResult> GetPrimg([FromRoute] int id)
        {
            string path = _enviroment.WebRootPath + "\\uploads\\";

            var filePath = path + id.ToString() + ".jpg";
            if (System.IO.File.Exists(filePath))
            {
                byte[] b = System.IO.File.ReadAllBytes(filePath);
                return File(b, "image/jpg");
            }
            return null;
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
                    var lst = db.Productos.ToList();



                    oRespuesta.Data = lst;


                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return Ok(oRespuesta.Data);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (SisVentasV2Context db = new SisVentasV2Context())
                {
                    Producto oUsuario = db.Productos.Find(id);
                    db.Remove(oUsuario);
                    db.SaveChanges();
                    string path = _enviroment.WebRootPath + "\\uploads\\";

                    var filePath = path + id.ToString() + ".jpg";

                    System.IO.File.Delete(filePath);
                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Producto eliminado Correctamente";
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }


        [HttpPut("updatestock/{id}")]
        public IActionResult UpdateStock([FromRoute]int id,[FromBody]int NuevoStock)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (SisVentasV2Context db = new SisVentasV2Context())
                {
                    Producto oProducto = db.Productos.Find(id);
                    oProducto.Idproducto = oProducto.Idproducto;
                    oProducto.NombreProducto = oProducto.NombreProducto;
                    oProducto.PrecioCompra = oProducto.PrecioCompra;
                    oProducto.PrecioVenta = oProducto.PrecioVenta;
                    oProducto.StockProducto = NuevoStock;



                    oProducto.ImagenProducto = oProducto.ImagenProducto;
                    db.Update(oProducto);
                    db.SaveChanges();
                    
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
