using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ERPManager.Models.Users;
using ERPManager.Service.Abstract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ERPManager.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginModel model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            var result = _userService.LoginUser(model.Email, model.Password);

            if (result != null)
            {
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.Id)
                    };
                ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                model.Errors = result.Errors;
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            RegisterModel model = new RegisterModel();
            return View(model);
        }

        //[HttpPost]
        //public IActionResult Register(RegisterModel model)
        //{

        //    //var result =  _userService.RegisterUser(model);
        //    //if (result.IsSuccess)
        //    //{
        //    //    LoginModel loginModel = new LoginModel();
        //    //    loginModel.Username = model.Username;
        //    //    loginModel.Password = model.Password;
        //    //    return Login(loginModel);
        //    //}
        //    //else
        //    //{
        //    //    return View(model);
        //    //}

        //}

        [HttpGet]
        public  IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login", "User");
        }
    }
}