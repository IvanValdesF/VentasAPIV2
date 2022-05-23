using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VentasAPI.Models;
using VentasAPI.Models.Request;
using VentasAPI.Models.Response;
using VentasAPI.Services;
using VentasAPI.Tools;
using VentasAPIv2.Models;

namespace VentasAPIv2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {


        private VentasAPI.Services.IUserService _userService;
        public UserAuthController(IUserService userService)
        {
            _userService = userService;
        }



        [HttpPost("register")]
        public IActionResult Add(RegisterUserRequest oRequest)
        {
            UserResponse oRespuesta = new UserResponse();
            try
            {

                using (SisVentasV2Context db = new SisVentasV2Context())
                {

                    var usuario = db.Usuarios.Where(u => u.NombreUsuario == oRequest.NombreUsuario).FirstOrDefault();

                    if(usuario == null)
                    {
                        usuario = db.Usuarios.Where(u => u.Correo == oRequest.Correo).FirstOrDefault();
                    }

                    if (usuario == null)
                    {
                        Usuario oUsuario = new Usuario();
                        oUsuario.NombreUsuario = oRequest.NombreUsuario;
                        oUsuario.Contraseña = Encrypt.GetSHA256(oRequest.Contraseña);
                        oUsuario.Correo = oRequest.Correo;
                        oUsuario.Rol = oRequest.Rol;
                        oUsuario.Idusuario = oRequest.IDUsuario;

                       
                        db.Usuarios.Add(oUsuario);
                        
                        db.SaveChanges();
                        oRespuesta.NombreUsuario = oUsuario.NombreUsuario;
                        oRespuesta.IDUsuario = oUsuario.Idusuario;
                        oRespuesta.Contraseña = oUsuario.Contraseña;
                        oRespuesta.Correo = oUsuario.Correo;
                        oRespuesta.Rol = oUsuario.Rol;
                        oRespuesta.mensaje = "Usuario registrado exitosamente";

                    }
                    else
                    {
                        oRespuesta.mensaje = "El usuario o correo que intento registrar ya existe";
                    }
                }

            }
            catch (Exception ex)
            {
                oRespuesta.mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }

        [HttpPost("login")]

        public IActionResult Autenticar([FromBody] AuthRequest model)
        {

            Respuesta oRespuesta = new Respuesta();
            var userSresponse = _userService.Auth(model);

            if (userSresponse == null)
            {
                oRespuesta.Mensaje = "Usuario o contrasena incorrectos";
                oRespuesta.Exito = 0;
                return BadRequest(oRespuesta);
            }

            oRespuesta.Exito = 1;
            oRespuesta.Data = userSresponse;
            return Ok(userSresponse);
        }

    }
}
