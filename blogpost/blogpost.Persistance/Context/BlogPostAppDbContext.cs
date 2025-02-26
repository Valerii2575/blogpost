using blogpost.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace blogpostApi.Data
{
    public class BlogPostAppDbContext : IdentityDbContext<User>
    {
        public BlogPostAppDbContext(DbContextOptions options)
            : base(options) 
        {
        }

        public DbSet<Article> Articles { get; set; }
    }
}
