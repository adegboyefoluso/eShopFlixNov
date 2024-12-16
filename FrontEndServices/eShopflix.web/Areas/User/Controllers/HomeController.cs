using Microsoft.AspNetCore.Mvc;

namespace eShopflix.web.Areas.User.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
