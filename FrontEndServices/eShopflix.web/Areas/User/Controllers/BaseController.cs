using eShopflix.web.Helper;
using Microsoft.AspNetCore.Mvc;

namespace eShopflix.web.Areas.User.Controllers
{

    [CustomAuthorize(Roles ="User")]
    [Area("User")]
    public class BaseController : Controller
    {
       
    }
}
