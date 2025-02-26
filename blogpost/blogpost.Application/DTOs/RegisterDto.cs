
using System.ComponentModel.DataAnnotations;

namespace blogpost.Application.DTOs
{
    public class RegisterDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        [RegularExpression("^\\w+@[a-zA-z_]+?\\.[a-zA-z]{2,3}", ErrorMessage = "Invalid email address")]
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}