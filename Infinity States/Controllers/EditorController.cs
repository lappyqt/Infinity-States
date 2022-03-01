using Microsoft.AspNetCore.Mvc;
using System;
using Infinity_States.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;

namespace Infinity_States.Controllers
{
    public class EditorController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Username = HttpContext.User.Identity.Name;
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Publish(string poster, string title, string content, int category)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                string authorId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                string author = HttpContext.User.Identity.Name;

                Article article = new Article 
                { 
                    Poster = poster,
                    Title = title,
                    Content = content,
                    AuthorId = Int32.Parse(authorId),
                    Author = author,
                    Category = category,
                    PublicationDateTime = DateTime.UtcNow,
                };

                db.Articles.Add(article);
                await db.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
