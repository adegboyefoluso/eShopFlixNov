﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace eShopflix.web.Helper
{
    public class CustomAuthorize : Attribute, IAuthorizationFilter
    {
        public string Roles { get; set; }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Check for Authentication
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                // check for authorization
                if (!string.IsNullOrEmpty(Roles))
                {
                    if (!context.HttpContext.User.IsInRole(Roles))
                    {
                        context.Result = new RedirectToActionResult("UnAuthorize", "Account", new { area = "" });
                    }
                }
            }
            else
            {
                context.Result = new RedirectToActionResult("Login", "Account", new { area = "" });
            }
        }
    }
}
