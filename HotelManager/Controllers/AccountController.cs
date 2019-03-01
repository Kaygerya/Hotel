using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HotelManager.Web.Configuration;
using HotelManager.Web.Models.Account;

namespace HotelManager.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            var token = Request.Cookies["AuthCookie"];
            LogoutViewModel model = new LogoutViewModel();
            model.LogoutId = token;
            return View(model);

        }

        /// <summary>
        /// Handle logout page postback
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(LogoutInputModel model)
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("LoggedOut");
        }

        public async Task<IActionResult> LoggedOut()
        {
            LoggedOutViewModel model = new LoggedOutViewModel();
            var token = Request.Cookies["AuthCookie"];
            string endSessionUrlFormat = "{0}connect/endsession?logoutId={1}&post_logout_redirect_uri={2}";
            string redirectUrl = string.Format(endSessionUrlFormat, IdentityConfigurationOptions.Authority, token, IdentityConfigurationOptions.RedirectUrl);
            model.SignOutIframeUrl = redirectUrl;
            foreach (var cookieKey in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookieKey);
            }
            return View(model);
        }
    }
}