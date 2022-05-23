using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VentasAPI.Models.Response;
using VentasAPIv2.Models;

namespace VentasAPIv2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
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
                    var lst = db.Usuarios.ToList();



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
                    Usuario oUsuario = db.Usuarios.Find(id);
                    db.Remove(oUsuario);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                    oRespuesta.Mensaje = "Usuario eliminado Correctamente";
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {

                using (SisVentasV2Context db = new SisVentasV2Context())
                {
                    Usuario oUsuario = db.Usuarios.Find(id);

                    
                    
                    oRespuesta.Exito = 1;
                    oRespuesta.Data = oUsuario;

                }

            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta.Data);
        }
    }
}
