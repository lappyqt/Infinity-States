using System.ComponentModel.DataAnnotations;

namespace Infinity_States.Models
{
    public class Article
    {   
        [Key]
        public int Id { get; set; }
        [Required]
        public string Poster { get; set;}
        [Required, MaxLength(250)]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required, MaxLength(150)]
        public string Author { get; set; }
    }
}
