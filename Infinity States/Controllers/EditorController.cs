﻿using Microsoft.AspNetCore.Mvc;
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
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Publish(string poster, string title, string content)
        {
            var author = Request.Cookies["InfinityStates.Session.Username"];
            using (ApplicationContext db = new ApplicationContext())
            {
                Article article = new Article { Poster = poster, Title = title, Content = content, Author = author };
                db.Articles.Add(article);
                await db.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
