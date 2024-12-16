using AuthService.Application.DTO;
using AuthService.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.A_i.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IUserAppServices _userAppservice;
        public AuthController(IUserAppServices userAppservice)
        {
                _userAppservice = userAppservice;
        }

        [HttpPost]
        public  IActionResult Login([FromBody] LoginDTO model)
        {
            var user= _userAppservice.LoginUser(model.Email,model.Password);
            if(user != null)
            {
                return Ok(user);
            }
            return BadRequest( new {message="Email or password is incorrect"});
        }

        [HttpPost]
        public IActionResult Register([FromBody] SignUpDTO model)
        {
            var result= _userAppservice.RegisterUser(model, model.ROles);
            if (!result)
            {
                return BadRequest(new { message = "Email of Phone Nummber Already exist" });
            }
            return Ok(new { message= " User Registered  Succesfully"});
        }

       
    }
}
