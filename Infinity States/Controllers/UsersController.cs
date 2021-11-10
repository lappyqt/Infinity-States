using System;
using Infinity_States.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infinity_States.Controllers
{
    public class UsersController : Controller
    {
        [Route("/users/{*id}")]
        public async Task<IActionResult> Index(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                User user = await db.Users.FindAsync(id);
                List<Article> userArticles = await db.Articles.OrderBy(data => -data.Id).Where(data => data.AuthorId == id).ToListAsync();

                ViewBag.UserData = user;
                ViewBag.UserArticles = userArticles;
                return View();
            }
        }

        [Route("/users/data/{*id}")]
        [HttpGet]
        public async Task<User> Data(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                User user = await db.Users.FindAsync(id);
                return user;
            }
        }

        [Route("/users/articles/{*id}")]
        [HttpGet]
        public async Task <List<Article>> Articles(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return await db.Articles.Where(data => data.AuthorId == id).ToListAsync();
            }
        }

        [Route("/users/follow")]
        [HttpPost]
        public async Task <IActionResult> Follow(string author)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                int userId = Int32.Parse(Request.Cookies["InfinityStates.Session.Id"]);
                User user = await db.Users.FindAsync(userId);

                if (user.Authors.Contains(author) == false) user.Authors.Add(author);
                await db.SaveChangesAsync();
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
