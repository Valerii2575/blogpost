using blogpost.Application.DTOs;
using blogpost.Application.Queries.Article.GetAll;

namespace blogpost.Application.Common.Interfaces
{
    public interface IArticleService
    {
        Task<GetArticlesQueryResult> GetArticles(GetArticlesQuery query);
        Task<ArticleDto> GetArticleById(Guid id);
    }
}
