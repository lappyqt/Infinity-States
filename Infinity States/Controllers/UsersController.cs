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
                User userPublicData = new User { Id = user.Id, Username = user.Username };
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
    }
}
