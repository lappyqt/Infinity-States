using System;
using Microsoft.AspNetCore.Mvc;
using Infinity_States.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infinity_States.Controllers
{
    public class ArticlesController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("All");
        }

        [Route("/articles/article/{*id}")]
        [HttpGet]
        public IActionResult Article(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var article = db.Articles.Where(data => data.Id == id).FirstOrDefault();;
                if (Request.Cookies["InfinityStates.Session.Username"] == article.Author)
                {
                    return View();
                }
                else return RedirectToAction("All");
            }
        }

        public IActionResult All()
        {
            return View();
        }

        [HttpGet]
        public async Task<List<Article>> GetAll()
        {
            using (ApplicationContext db = new ApplicationContext())
            {   
                return await db.Articles.ToListAsync();
            }
        }

        [HttpGet]
        public async Task<List<Article>> ByFollowedAuthors()
        {
            using (ApplicationContext db = new ApplicationContext())
            {   
                List<Article> resultList = new List<Article>();
                var userId = Int32.Parse(Request.Cookies["InfinityStates.Session.Id"]);
                User user = await db.Users.FindAsync(userId);

                foreach (string author in user.Authors)
                {
                    Article article = await db.Articles.Where(data => data.Author == author).OrderBy(data => data.Id).LastOrDefaultAsync();
                    resultList.Add(article);
                } 

                return resultList;
            }
        }

        [Route("/articles/read/{*id}")]
        [HttpGet]
        public async Task<object> Read(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Article article = await db.Articles.FindAsync(id);
                return article;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, string poster, string title, string content)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var article = await db.Articles.Where(data => data.Id == id).FirstOrDefaultAsync();

                article.Poster = poster;
                article.Title = title;
                article.Content = content;

                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Account");
            }
        }
    }
}
