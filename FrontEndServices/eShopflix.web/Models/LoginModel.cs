using System.ComponentModel.DataAnnotations;

namespace eShopflix.web.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage =" Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = " Enter Valid pAssword")]
        public string Password { get; set; }
    }
}
