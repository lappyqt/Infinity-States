using System.ComponentModel.DataAnnotations;

namespace Infinity_States.Models
{
    public class User
    {   
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(150)]
        public string Mail { get; set;}
        [Required, MaxLength(150)]
        public string Username { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
