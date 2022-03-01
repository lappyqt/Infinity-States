using Xunit;
using Microsoft.AspNetCore.Mvc;
using Infinity_States.Controllers;
using Infinity_States.Data;
using System.Collections.Generic;

namespace Infinity_States.Tests
{
    public class AccountControllerTests
    {
        private readonly AccountController controller = new();

        [Fact]
        public void ArticlesReturnNullOnArticlesCountZero()
        {
            var result = controller.ViewBag.Articles;
            Assert.Null(result);
        }  
    }
}