using System;
using Microsoft.AspNetCore.Mvc;
using Infinity_States.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.EntityFrameworkCore;

using HashCode = Infinity_States.Models.HashCode;

namespace Infinity_States.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _enviroment;
        private readonly ApplicationContext _context;

        public HomeController(IWebHostEnvironment env)
        {
            _enviroment = env;
            _context = new ApplicationContext();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [Route("/search")]
        public List<Article> Search(string req)
        {
            List<Article> articles = _context.Articles.Where(x => x.SearchVector.Matches(req)).ToList();
            return articles;
        }

        [Route("/notfound")]
        public IActionResult NotFoundError()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file != null)
            {
                //string filePath = $"/files/image/{file.FileName}";
                string fullPath = $"{_enviroment.WebRootPath}\\files\\images\\{file.FileName}";
                
                using (FileStream fs = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(fs);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(string mail, string username, string password)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                HashCode hashCode;
                User user = new User { Mail = mail, Username = username, Password = hashCode.GenerateHashedPassword(password), Authors = new List<string>() };
                db.Users.Add(user);
                await db.SaveChangesAsync();
            }

            return RedirectToAction("SignIn", "Home");
        }
                
        [HttpPost]
        public async Task<IActionResult> SignIn(string username, string password)
        {
            await using (ApplicationContext db = new ApplicationContext())
            {
                HashCode hashCode;
                User user = db.Users.Where(data => data.Username == username).FirstOrDefault();

                if (user.Password == hashCode.GenerateHashedPassword(password)) 
                {
                    List<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, username)
                    };

                    ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                    AuthenticationProperties props = new AuthenticationProperties();
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