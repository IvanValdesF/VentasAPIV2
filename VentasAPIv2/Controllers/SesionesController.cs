using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VentasAPI.Models.Response;
using VentasAPIv2.Models;

namespace VentasAPIv2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SesionesController : ControllerBase
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
                    var lst = db.Sesionesactivas.ToList();



                    oRespuesta.Data = lst;


                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return Ok(oRespuesta.Data);
        }
    }
}
