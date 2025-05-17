using blogpost.Application.DTOs;

namespace blogpost.Application.Queries.Article.GetAll
{
    public class GetArticlesQuery : IRequest<GetArticlesQueryResult>
    {

    }

    public class GetArticlesQueryResult
    {
        public List<ArticleDto>? Articles { get; set; }
    }
}
