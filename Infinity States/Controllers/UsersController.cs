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
        public IActionResult Index(int id)
        {
            
            return View();
        }

        [Route("/users/data/{*id}")]
        [HttpGet]
        public async Task<object> Data(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                User user = await db.Users.FindAsync(id);
                User userPublicData = new User { Id = user.Id, Mail = null, Username = user.Username, Password = null, Authors = null };
                return userPublicData;
            }
        }

        [Route("/users/articles/{*id}")]
        [HttpGet]
        public async Task <List<Article>> Articles(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return await (from data in db.Articles where data.AuthorId == id select data).ToListAsync();
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
