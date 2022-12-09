using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class TokenModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public string TwoFactorToken{ get; set; }
    }
}
