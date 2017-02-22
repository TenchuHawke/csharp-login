using System;
using System.ComponentModel.DataAnnotations;

namespace Login.Models
{
    public class User : BaseEntity
    {

        public int user_id { get; set; }

        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string first_name { get; set; }

        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string last_name { get; set; }

        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string username { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [MinLength(8)]
        public string password { get; set; }

        [Compare("password", ErrorMessage = "Password confirmation must match Password")]
        public string confirmPassword { get; set; }

        public DateTime created_at { get; set; }

        public DateTime modified_at { get; set; }

        public DateTime last_on { get; set; }

        public DateTime birthday { get; set;}
    }
}