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
        public async Task<IActionResult> Article(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                ViewBag.Article = (Article) await db.Articles.FindAsync(id);
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Article article = await db.Articles.FindAsync(id);
                ViewBag.Article = article;

                if (Request.Cookies["InfinityStates.Session.Username"] == article.Author)
                {
                    return View();
                }
                else return RedirectToAction("All");
            }
        }

        [Route("/articles/all/{*page}")]
        public async Task<IActionResult> All(int page = 1, int filter = -1)
        {
            ViewData["filter"] = filter;

            using (ApplicationContext db = new ApplicationContext())
            {   
                int pageSize = 50;

                if (filter <= -1)
                {
                    IQueryable<Article> source = db.Articles.OrderBy(data => -data.Id);
                    var count = await source.CountAsync();
                    var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

                    PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
                    IndexViewModel viewModel = new IndexViewModel
                    {
                        PageViewModel = pageViewModel,
                        Articles = items
                    };
                    return View(viewModel);
                } 
                else 
                {
                    IQueryable<Article> source = db.Articles.OrderBy(data => -data.Id).Where(data => data.Category == (ArticleCategories) filter);
                    var count = await source.CountAsync();
                    var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
                    PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
                    IndexViewModel viewModel = new IndexViewModel
                    {
                        PageViewModel = pageViewModel,
                        Articles = items
                    };
                    return View(viewModel);
                }

            }
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

        [HttpGet]
        public async Task<List<Article>> Filter(ArticleCategories value)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                 return await db.Articles.Where(data => data.Category == value).ToListAsync();
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
                var article = await db.Articles.FindAsync(id);

                article.Poster = poster;
                article.Title = title;
                article.Content = content;

                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Account");
            }
        }
    }
}
