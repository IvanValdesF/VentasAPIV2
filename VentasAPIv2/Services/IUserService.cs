using VentasAPI.Models.Response;
using VentasAPI.Models.Request;

namespace VentasAPI.Services
{
    public interface IUserService
    {
        UserResponse Auth(AuthRequest model);
    }
}
