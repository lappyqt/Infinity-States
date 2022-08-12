using System;
using Microsoft.AspNetCore.Mvc;
using Infinity_States.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Infinity_States.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Infinity_States.Services;

namespace Infinity_States.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class ArticlesController : Controller
    {
        private readonly IArticlesRepository _articlesRepository;
        private readonly IWebHostEnvironment _enviroment;
        private readonly FileHandling _fileHandling;

        public ArticlesController(IWebHostEnvironment env)
        {
            _articlesRepository = new ArticlesRepository();
            _enviroment = env;
            _fileHandling = new FileHandling();
        }

        public IActionResult Index()
        {
            return RedirectToAction("All");
        }

        [Route("/articles/article/{*id}")]
        [HttpGet]
        public async Task<IActionResult> Article(int id)
        {
            var article = await _articlesRepository.GetArticle(id);
            return View(article); 
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
        public async Task<IEnumerable<Article>> Filter(int value)
        {
            return await _articlesRepository.GetArticlesWithFilter(value);
        }

        [HttpPost] // TODO: If image is not change not update it
        public async Task<IActionResult> Update(int id, IFormFile poster, string title, string content)
        {
            var article = await _articlesRepository.GetArticle(id);

            string path = $"{_enviroment.WebRootPath}\\files\\images\\{poster.FileName}";
            string previousPosterPath = $"{_enviroment.ContentRootPath}\\wwwroot\\{article.Poster}";
            // Not WORK if ($"/files/images{poster.FileName}" != article.Poster) { await _fileHandling.UploadFileWithDeletingPrevious(poster, path, previousPosterPath); }

            MutableArticleData articleData = new MutableArticleData(poster, title, content);
            await _articlesRepository.Update(id, articleData);

            return RedirectToAction("Index", "Account");
        }
    }
}
