using eShopflix.web.Models;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Security.Claims;
using System.Text.Json;

namespace eShopflix.web.Helper
{
    public abstract class BaseViewPage<TModel> :RazorPage<TModel>
    {
        public UserModel CurrentUSer
        {
            get
            {
                if(User.Identity.IsAuthenticated)
                {
                    var user= User.Claims.FirstOrDefault(c=>c.Type==ClaimTypes.UserData).Value;
                    return JsonSerializer.Deserialize<UserModel>(user);
                }

                return null;
            }
        }



 
    }
}
    