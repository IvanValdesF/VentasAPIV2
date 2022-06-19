using VentasAPI.Models.Request;
using VentasAPI.Models.Response;
using VentasAPI.Models;
using VentasAPI.Tools;
using VentasAPI.Models.Common;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using VentasAPIv2.Models;

namespace VentasAPI.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public UserResponse Auth(AuthRequest model)
        {
            UserResponse response = new UserResponse();
            using(var db = new SisVentasV2Context())
            {
                string spassword = Encrypt.GetSHA256(model.Password);

                var usuario = db.Usuarios.Where(d => d.Correo == model.Email &&
                                                d.Contraseña == spassword).FirstOrDefault();
                if (usuario == null)
                {
                    return null;
                }
                else
                {
                    response.Correo = usuario.Correo;
                    response.Contraseña = usuario.Contraseña;
                    response.Rol = usuario.Rol;
                    response.Id = usuario.Id;
                    response.Nombre = usuario.Nombre;
                    response.Token = getToken(usuario);
                    response.mensaje = "Sesion iniciada correctamente";
                }
                
            }
            return response;

        }

        private string getToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var llave = Encoding.ASCII.GetBytes(_appSettings.Secreto);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                        new Claim(ClaimTypes.Email, usuario.Correo)
                    }
                    ),
                    Expires = DateTime.UtcNow.AddDays(60),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave),SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
