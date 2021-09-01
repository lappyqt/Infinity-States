using System;
using Microsoft.AspNetCore.Mvc;
using Infinity_States.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Infinity_States.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string mail, string username, string password)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                User user = new User { Mail = mail, Username = username, Password = password };
                db.Users.Add(user);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
                
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            await using (ApplicationContext db = new ApplicationContext())
            {
                User user = db.Users.Where(data => data.Username == username).FirstOrDefault();

                if (user.Password == password) 
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, username)
                    };

                    var options = new CookieOptions
                    {
                        Expires = DateTime.Now.AddMinutes(60),
                        IsEssential = true,
                        HttpOnly = true
                    };

                    Response.Cookies.Append("InfinityStates.Session.Username", user.Username, options);

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var props = new AuthenticationProperties();
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();

                    return RedirectToAction("Index", "Account");
                }
                else
                {
                    return View();
                }
            }
        }
    }
}