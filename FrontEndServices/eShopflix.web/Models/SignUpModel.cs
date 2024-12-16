using System.ComponentModel.DataAnnotations;

namespace eShopflix.web.Models
{
    public class SignUpModel
    {
        public long UserId { get; set; }
        [Required(ErrorMessage = "First name is required ")]
        [StringLength(50, ErrorMessage = "Name cannot be  longer than 50 character")]
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ROles { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
    }
}
