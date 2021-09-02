using Microsoft.AspNetCore.Mvc;
using Infinity_States.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Infinity_States.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Account()
        {
            return View("~/Views/Account/Account.cshtml");
        }

        [HttpGet]
        public List<Article> FindUserArticles()
        {   
            using (ApplicationContext db = new ApplicationContext())
            {
                var username = Request.Cookies["InfinityStates.Session.Username"];
                var articles = from data in db.Articles where data.Author.Contains(username) select data;  
                return articles.ToList();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            Response.Cookies.Delete("InfinityStates.Session.Username");
            return RedirectToAction("Index", "Articles");
        }
    }
}
