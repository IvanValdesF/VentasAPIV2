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

                    var usuario = db.Usuarios.Where(u => u.Nombre == oRequest.Nombre).FirstOrDefault();

                    if(usuario == null)
                    {
                        usuario = db.Usuarios.Where(u => u.Correo == oRequest.Correo).FirstOrDefault();
                    }

                    if (usuario == null)
                    {
                        Usuario oUsuario = new Usuario();

                        oUsuario.Nombre = oRequest.Nombre;
                        oUsuario.Contraseña = Encrypt.GetSHA256(oRequest.Contraseña);
                        oUsuario.Correo = oRequest.Correo;
                        oUsuario.Rol = oRequest.Rol;
                        oUsuario.Id = oRequest.Id;

                       
                        db.Usuarios.Add(oUsuario);
                        
                        db.SaveChanges();
                        oRespuesta.Nombre = oUsuario.Nombre;
                        oRespuesta.Id = oUsuario.Id;
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
            Sesionesactiva miSesion = new Sesionesactiva();
            Respuesta oRespuesta = new Respuesta();
            var userSresponse = _userService.Auth(model);

            if(userSresponse != null)
            {
                

                miSesion.Rol = userSresponse.Rol;
                miSesion.Contraseña = userSresponse.Contraseña;
                miSesion.Idusuario = userSresponse.Id;
                miSesion.NombreUsuario = userSresponse.Nombre;
                miSesion.Correo = userSresponse.Correo;
            }

            

            if (userSresponse == null)
            {
                oRespuesta.Mensaje = "Usuario o contrasena incorrectos";
                oRespuesta.Exito = 0;
                return BadRequest(oRespuesta);
            }
            using(SisVentasV2Context db = new SisVentasV2Context())
            {
                
                
                db.Sesionesactivas.Add(miSesion);
                db.SaveChanges();

            }
            
            oRespuesta.Exito = 1;
            oRespuesta.Data = userSresponse;
            return Ok(userSresponse);
        }

        [HttpPost("logout")]

        public IActionResult CerrarSesion([FromBody] AuthRequest model)
        {

            Respuesta oRespuesta = new Respuesta();

            var userSresponse = _userService.Auth(model);
            Sesionesactiva miSesion = new Sesionesactiva();

            


            using (SisVentasV2Context db = new SisVentasV2Context())
            {

                miSesion.Rol = userSresponse.Rol;
                miSesion.Contraseña = userSresponse.Contraseña;
                miSesion.Idusuario = userSresponse.Id;
                miSesion.NombreUsuario = userSresponse.Nombre;
                miSesion.Correo = userSresponse.Correo;
                db.Sesionesactivas.Remove(miSesion);
                db.SaveChanges();

            }
            userSresponse.mensaje = "Sesion terminada correctamenre";
            oRespuesta.Exito = 1;
            oRespuesta.Data = userSresponse;
            return Ok(userSresponse);
        }

    }
}
