
namespace blogpost.Application.DTOs
{
    public class ArticleDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public string? Content { get; set; }
    }
}
