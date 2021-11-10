using System.Collections.Generic;
 
namespace Infinity_States.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Article> Articles { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}