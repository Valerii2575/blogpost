using blogpost.Application.DTOs;
using MediatR;

namespace blogpostApi.Application.Queries.Article.GetArticleById
{
    public record GetArticleByIdQuery(Guid id) : IRequest<ArticleDto>;
}
