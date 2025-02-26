using blogpost.Application.Common.Interfaces;
using blogpost.Application.DTOs;
using blogpostApi.Application.Queries.Article.GetArticleById;
using MediatR;

namespace blogpostApi.Application.Queries.Articles.GetArticleById
{
    public class GetArticleByIdHandler : IRequestHandler<GetArticleByIdQuery, ArticleDto>
    {
        private readonly IArticleService _articleService;

        public GetArticleByIdHandler(IArticleService articleService)
        {
            _articleService = articleService;
        }
        public async Task<ArticleDto?> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
        {
            var article = await _articleService.GetArticleById(request.id);
            return article;
        }
    }
}
