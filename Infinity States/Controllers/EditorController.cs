using Microsoft.AspNetCore.Mvc;
using System;
using Infinity_States.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Infinity_States.Services;

namespace Infinity_States.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class EditorController : Controller
    {
        private IWebHostEnvironment _enviroment;
        private FileHandling _fileHandling;

        public EditorController(IWebHostEnvironment env)
        {
            _enviroment = env; 
            _fileHandling = new FileHandling();
        }

        public IActionResult Index()
        {
            ViewBag.Username = HttpContext.User.Identity.Name;
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Publish(IFormFile poster, string title, string content, int category)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                string authorId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
                string author = HttpContext.User.Identity.Name;

                string pathToPoster = $"/files/images/{poster.FileName}";

                Article article = new Article 
                { 
                    Poster = pathToPoster,
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

            string fullPath = $"{_enviroment.WebRootPath}\\files\\images\\{poster.FileName}";
            await _fileHandling.UploadFile(poster, fullPath);

            return RedirectToAction("Index");
        }
    }
}
