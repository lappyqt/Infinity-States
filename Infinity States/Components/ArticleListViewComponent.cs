using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Infinity_States.Components
{
    public class ArticleListViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
