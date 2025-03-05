
using blogpost.Application.Queries.Article.GetAll;

namespace blogpost.Infrastructure.Services
{
    public class ArticleService() : IArticleService
    {
        public async Task<GetArticlesQueryResult> GetArticles(GetArticlesQuery query)
        {
            return new GetArticlesQueryResult
            {
                Articles = new List<ArticleDto>
                {
                    new ArticleDto
                    {
                        Id = Guid.NewGuid(),
                        Title = "Article 1",
                        Description = "Article description 1",
                        Content = "Some content",
                        Author = "author 1"
                    }
                }
            };
        }

        public Task<ArticleDto> GetArticleById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
