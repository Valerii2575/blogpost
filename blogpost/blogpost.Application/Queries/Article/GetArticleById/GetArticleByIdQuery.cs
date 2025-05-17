using blogpost.Application.DTOs;

namespace blogpostApi.Application.Queries.Article.GetArticleById
{
    public record GetArticleByIdQuery(Guid id) : IRequest<ArticleDto>;
}
