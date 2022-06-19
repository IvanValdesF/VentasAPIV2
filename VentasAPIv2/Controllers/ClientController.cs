using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VentasAPI.Models.Response;
using VentasAPI.Tools;
using VentasAPIv2.Models;
using VentasAPIv2.Models.Request;
using VentasAPIv2.Models.Response;

namespace VentasAPIv2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            string msg = "";
            Respuesta oRespuesta = new Respuesta();
            try
            {

                using (SisVentasV2Context db = new SisVentasV2Context())
                {
                    var lst = db.Clientes.ToList();



                    oRespuesta.Data = lst;


                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return Ok(oRespuesta.Data);
        }

        


        [HttpPost("register")]
        public IActionResult Add(RegisterClientRequest oRequest)
        {
            string msg = "";
            ClientResponse oRespuesta = new ClientResponse();
            try
            {

                using (SisVentasV2Context db = new SisVentasV2Context())
                {

                    var client = db.Clientes.Where(u => u.Nombre == oRequest.Nombre).FirstOrDefault();

                    if (client == null)
                    {
                        client = db.Clientes.Where(u => u.Correo == oRequest.Correo).FirstOrDefault();
                    }

                    if (client == null)
                    {
                        Cliente oCliente = new Cliente();

                        oCliente.Nombre = oRequest.Nombre;
                        oCliente.Id = oRequest.Id;
                        oCliente.Correo = oRequest.Correo;
                        oCliente.Telefono = oRequest.Telefono;
                        


                        db.Clientes.Add(oCliente);

                        db.SaveChanges();
                        oRespuesta.Nombre = oCliente.Nombre;
                        oRespuesta.Id = oCliente.Id;
                        oRespuesta.Telefono = oCliente.Telefono;
                        oRespuesta.Correo = oCliente.Correo;
                        
                        

                    }
                    else
                    {
                        msg = "El usuario o correo que intento registrar ya existe";
                    }
                }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return Ok(oRespuesta);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (SisVentasV2Context db = new SisVentasV2Context())
                {
                    Cliente oCliente = db.Clientes.Find(id);
                    db.Remove(oCliente);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Cliente eliminado Correctamente";
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
