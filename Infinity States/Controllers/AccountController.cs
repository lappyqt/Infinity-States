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
using Infinity_States.Repositories;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Infinity_States.Services;

namespace Infinity_States.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private readonly IArticlesRepository _articlesRepository;
        private IWebHostEnvironment _enviroment;
        private FileHandling _fileHandling;

        public AccountController(IWebHostEnvironment env)
        {
            _articlesRepository = new ArticlesRepository();
            _enviroment = env;
            _fileHandling = new FileHandling();
        }

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
            Article article = await _articlesRepository.GetArticle(id);
            string posterPath = $"{_enviroment.ContentRootPath}\\wwwroot\\{article.Poster}";

            string author = HttpContext.User.Identity.Name;
            await _articlesRepository.Delete(id, author);
            await _fileHandling.DeleteFile(posterPath);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(string name)
        {
            string fullPath = $"{_enviroment.ContentRootPath}\\files\\images\\{name}";


            return Content(fullPath);
        }
    }
}
