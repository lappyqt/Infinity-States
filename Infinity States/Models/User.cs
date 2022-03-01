using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Infinity_States.Data
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(150)]
        public string Mail { get; set;}
        [Required, MaxLength(150)]
        public string Username { get; set; }
        [Required, DataType(DataType.Password), MaxLength(350)]
        public string Password { get; set; }
        public List<string> Authors { get; set; }
    }
}
