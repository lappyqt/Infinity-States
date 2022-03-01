using Microsoft.AspNetCore.Mvc;
using Infinity_States.Data;
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
                string username = HttpContext.User.Identity.Name;
                ViewBag.Articles = await db.Articles.Where(data => data.Author == username).OrderBy(data => -data.Id).ToListAsync();
                return View();
            }
        }
        
        [HttpGet]
        public async Task<List<Article>> FindUserArticles()
        {   
            using (ApplicationContext db = new ApplicationContext())
            {
                string username = HttpContext.User.Identity.Name;
                var articles = from data in db.Articles where data.Author == username select data;  
                return await articles.ToListAsync();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Articles");
        }

        public async Task<IActionResult> DeleteArticle(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                string author = HttpContext.User.Identity.Name;
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
