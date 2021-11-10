using Microsoft.AspNetCore.Mvc;
using System;
using Infinity_States.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infinity_States.Controllers
{
    public class EditorController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Username = Request.Cookies["InfinityStates.Session.Username"];
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Publish(string poster, string title, string content, ArticleCategories category)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var authorId = Request.Cookies["InfinityStates.Session.Id"];
                var author = Request.Cookies["InfinityStates.Session.Username"];

                Article article = new Article { 
                    Poster = poster,
                    Title = title,
                    Content = content,
                    AuthorId = Int32.Parse(authorId),
                    Author = author,
                    Category = category
                };

                db.Articles.Add(article);
                await db.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
