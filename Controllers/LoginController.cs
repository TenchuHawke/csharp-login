using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace login.Controllers
{
    public class LoginController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            ViewBag.loginState=false;
            return View("Login");
        }
    }
}
