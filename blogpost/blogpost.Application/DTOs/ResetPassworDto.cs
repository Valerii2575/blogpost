
namespace blogpost.Application.DTOs
{
    public class ResetPassworDto
    {
        public string? Token { get; set; }
        public string? Email { get; set; }
        public string? NewPassword { get; set; }
    }
}
