using System.ComponentModel.DataAnnotations;

namespace VentasAPI.Models.Request
{
    public class AuthRequest
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
