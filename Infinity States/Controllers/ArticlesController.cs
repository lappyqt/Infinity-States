using System;
using Microsoft.AspNetCore.Mvc;
using Infinity_States.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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

                if (HttpContext.User.Identity.Name == article.Author)
                {
                    return View();
                }
                else return RedirectToAction("All");
            }
        }

        [Route("/articles/all/{*page}")]
        public async Task<IActionResult> All(int page = 1, short filter = -1)
        {
            ViewData["filter"] = filter;

            using (ApplicationContext db = new ApplicationContext())
            {   
                short pageSize = 25;

                if (filter <= -1)
                {
                    IQueryable<Article> source = db.Articles.OrderBy(data => -data.Id);
                    int count = await source.CountAsync();
                    List<Article> items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

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
                    IQueryable<Article> source = db.Articles.OrderBy(data => -data.Id).Where(data => data.Category == filter);
                    int count = await source.CountAsync();
                    List<Article> items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
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
        public async Task<IActionResult> Followed()
        {
            using (ApplicationContext db = new ApplicationContext())
            {   
                List<Article> resultList = new List<Article>();
                int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
                User user = await db.Users.FindAsync(userId);

                foreach (string author in user.Authors)
                {
                    Article article = await db.Articles.Where(data => data.Author == author).OrderBy(data => data.Id).LastOrDefaultAsync();
                    if (article is not null) resultList.Add(article);
                } 

                IndexViewModel indexViewModel = new IndexViewModel
                {
                    Articles = resultList,
                    PageViewModel = null
                };

                return View(indexViewModel);
            }
        }

        [HttpGet]
        public async Task<List<Article>> Filter(int value)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return await db.Articles.Where(data => data.Category == value).ToListAsync();
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
