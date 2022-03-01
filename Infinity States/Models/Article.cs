using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Infinity_States.Data
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
        public int AuthorId { get; set; }
        [Required, MaxLength(150)]
        public string Author { get; set; }
        [Required]
        public int Category { get; set; }
        public DateTime PublicationDateTime { get; set; }

        public static Dictionary<int, string> CategoriesDictionary = new Dictionary<int, string>
        {
            { 0, "Other" },
            { 1, "Technology" },
            { 2, "Travel" },
            { 3, "Education" },
            { 4, "Reading" },
            { 5, "Movies" },
            { 6, "Gaming" },
            { 7, "Politics" },
            { 8, "Entertainment" },
            { 9, "Business" },
            { 10, "Health & Fitness" },
            { 11, "Career" },
            { 12, "Food" },
            { 13, "Self Improvement" },
        };
    }
}
