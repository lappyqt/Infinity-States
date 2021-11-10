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
using Microsoft.EntityFrameworkCore;

namespace Infinity_States.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                string username = Request.Cookies["InfinityStates.Session.Username"];
                ViewBag.Articles = await db.Articles.Where(data => data.Author == username).OrderBy(data => -data.Id).ToListAsync();
                return View();
            }
        }
        
        [HttpGet]
        public async Task<List<Article>> FindUserArticles()
        {   
            using (ApplicationContext db = new ApplicationContext())
            {
                string username = Request.Cookies["InfinityStates.Session.Username"];
                var articles = from data in db.Articles where data.Author == username select data;  
                return await articles.ToListAsync();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            Response.Cookies.Delete("InfinityStates.Session.Id");
            Response.Cookies.Delete("InfinityStates.Session.Username");
            return RedirectToAction("Index", "Articles");
        }

        public async Task<IActionResult> DeleteArticle(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                string author = Request.Cookies["InfinityStates.Session.Username"];
                Article article = await db.Articles.Where(data => data.Id == id).FirstOrDefaultAsync();

                if (article.Author == author)
                {
                    db.Articles.Remove(article);
                    await db.SaveChangesAsync();
                }

                return RedirectToAction("Index");
            }
        }
    }
}
