using blogpost.Application.Common.Interfaces;
using MediatR;

namespace blogpost.Application.Queries.Article.GetAll
{
    public class GetArticlesHandler(IArticleService articleService) : IRequestHandler<GetArticlesQuery, GetArticlesQueryResult>
    {
        public async Task<GetArticlesQueryResult> Handle(GetArticlesQuery query, CancellationToken cancellationToken)
        {
            var articles = await articleService.GetArticles(query);
            return articles;
        }
    }
}
