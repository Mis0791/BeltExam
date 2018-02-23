using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BeltExam.Models;

namespace BeltExam.Controllers
{
    public class HomeController : Controller
    {
        private Context _context;

        public HomeController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("register")]
        public IActionResult Register()
        {
            return View("Index");
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterViewModel model)
        {
            if(_context.User.Where(u => u.Email == model.Email).SingleOrDefault()!= null){
                ModelState.AddModelError("Email", "Email is already in use!");
            }
            if(ModelState.IsValid)
            {
                RegisterUser NewUser = new RegisterUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password
                };
                _context.User.Add(NewUser);
                _context.SaveChanges();
                return RedirectToAction("Index", "Main");  
            }
            return View("Index");
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [Route("login")]
        public IActionResult myLogin(string Email, string Password)
        {
            RegisterUser user = _context.User.SingleOrDefault(u => u.Email == Email);
            if(user == null)
            {
                ViewBag.usererror = "Email not found";
            } else if(user.Password != Password)
            {
                ViewBag.Password = "Invalid Password";

            } else if(user.Password == Password)
            {
                HttpContext.Session.SetInt32("UserId", user.UserId);
                return RedirectToAction("Index", "Main");
            }
            return View("Login");
        }
    }
}
