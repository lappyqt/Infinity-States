using System;
using Microsoft.AspNetCore.Mvc;
using Infinity_States.Models;
using System.Collections.Generic;
using System.Linq;

namespace Infinity_States.Controllers
{
    public class ArticlesController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("All");
        }

        public IActionResult Article()
        {
            return View("~/Views/Articles/Article.cshtml");
        }

        public IActionResult All()
        {
            return View("~/Views/Articles/All.cshtml");
        }

        [HttpGet]
        public List<Article> GetAll()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.Articles.ToList();
            }
        }

        [HttpGet]
        public string Read(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Article result = db.Articles.Find(id);
                return result.Poster + "|" + result.Title + "|" + result.Content;    // "|" Separates result.Title from result.Content and result.Poster
            }
        }
    }
}
