using System.Collections.Generic;
using System.Linq;
using Login.Factories;
using Login.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Login.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserFactory _userFactory;

        public LoginController(UserFactory userFactory)
        {
            _userFactory = userFactory;
        }

        [HttpGet]
        [Route("LogReg")]
        public IActionResult LogRegPage()
        {
            ViewBag.Errors = new List<string>();
            return View("login");
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(User model)
        {
            if(ModelState.IsValid)
            {
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                model.password = Hasher.HashPassword(model, model.password);
                _userFactory.Add(model);
                User CurrentUser = _userFactory.GetLatestUser();
                HttpContext.Session.SetInt32("CurrUser_id", CurrentUser.user_id);
                return RedirectToAction("Show");
            }
            ViewBag.Errors = new List<string>();
            foreach (var error in ModelState.Values)
            {
                if(error.Errors.Count>0)
                {
                    string errorcode = (error.Errors[0].ErrorMessage); 
                    ViewBag.Errors.Add(errorcode); 
                }
            }
            return View("Login");
        }

        [HttpGet]
        [Route("login")]
        public IActionResult LoginPage()
        {
            ViewBag.Errors = new List<string>();
            return View("Login");
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/Home/");
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(string username, string password)
        {
            if(username != null)
            {
                User CheckUser = _userFactory.GetuserByUsername(username);
                if(CheckUser != null)
                {
                    var Hasher = new PasswordHasher<User>();
                    if(0 != Hasher.VerifyHashedPassword(CheckUser, CheckUser.password, password))
                    {
                        HttpContext.Session.SetInt32("CurrUser_id", CheckUser.user_id);
                        return RedirectToAction("Show");
                    }
                }
            }
            ViewBag.Errors = new List<string>{
                "Invalid Username or Password"
            };
            return View("Login");
        }

        [HttpGet]
        [Route("show")]
        public IActionResult Show()
        {
            User CurrentUser = _userFactory.GetUserById((int)HttpContext.Session.GetInt32("CurrUser_id"));
            HttpContext.Session.SetString("loginState", "true");
            ViewBag.loginState="true";
            ViewBag.User = CurrentUser;
            return View("Output");
        }
    }
}

