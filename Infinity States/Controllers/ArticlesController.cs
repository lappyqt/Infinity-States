﻿using System;
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

        public IActionResult Article()
        {
            return View("~/Views/Articles/Article.cshtml");
        }

        public IActionResult Edit(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var article = db.Articles.Where(data => data.Id == id).FirstOrDefault();;
                if (Request.Cookies["InfinityStates.Session.Username"] == article.Author)
                {
                    return View("~/Views/Articles/Edit.cshtml");
                }
                else return RedirectToAction("All");
            }
        }

        public IActionResult All()
        {
            return View("~/Views/Articles/All.cshtml");
        }

        [HttpGet]
        public async Task <List<Article>> GetAll()
        {
            using (ApplicationContext db = new ApplicationContext())
            {   
                return await db.Articles.ToListAsync();
            }
        }
        public async Task<string> Read(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Article result = await db.Articles.FindAsync(id);
                return result.Poster + "|" + result.Title + "|" + result.Content;    // "|" Separates result.Title from result.Content and result.Poster
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(string poster, string title, string content)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var article = await db.Articles.Where(data => data.Title == title).FirstOrDefaultAsync();

                article.Content = content;
                article.Poster = poster;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Account");
            }
        }
    }
}
