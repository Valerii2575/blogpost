namespace blogpost.Domain.Entities
{
    public class Article : BaseEntity
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }

    }
}
