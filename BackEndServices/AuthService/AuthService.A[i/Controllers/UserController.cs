using AuthService.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.A_i.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        IUserAppServices _userAppServices;
        public UserController(IUserAppServices userAppServices)
        {
            _userAppServices = userAppServices; 
        }
        [HttpGet]
        public IActionResult GetUser()
        {
            var users= _userAppServices.GetUsers();
            return Ok(users);
        }
    }
}
