using System;
using Infinity_States.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Infinity_States.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class UsersController : Controller
    {
        private ApplicationContext db { get; set; }
        
        public UsersController()
        {
            db = new ApplicationContext();
        }

        [Route("/users/{*id}")]
        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            User user = await db.Users.FindAsync(id);   // Owner of page 
            List<Article> userArticles = await db.Articles.OrderBy(data => -data.Id).Where(data => data.AuthorId == id).ToListAsync();
                
            ViewBag.UserData = user;
            bool followed = false;

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                Int32.TryParse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value, out int userId);
                User currentUser = await db.Users.FirstOrDefaultAsync(x => x.Id == userId);    // The user who visits the page of another user (in this case the "user")
                followed = currentUser?.Authors.Contains(user.Username) ?? false;
            }

            ViewBag.Followed = followed;

            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = null,
                Articles = userArticles
            };

            return View(viewModel);
        }

        [Route("/users/data/{*id}")]
        [HttpGet]
        public async Task<User> Data(int id)
        {
            User user = await db.Users.FindAsync(id);
            return user;
        }

        [Route("/users/articles/{*id}")]
        [HttpGet]
        public async Task<List<Article>> Articles(int id)
        {
            return await db.Articles.Where(data => data.AuthorId == id).ToListAsync();
        }

        [Route("/users/follow")]
        [HttpPost]
        public async Task<IActionResult> Follow(string author)
        {
            int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            User user = await db.Users.FindAsync(userId);

            if (user.Authors.Contains(author) == false) user.Authors.Add(author);
            else user.Authors.Remove(author);
                
            await db.SaveChangesAsync();

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}