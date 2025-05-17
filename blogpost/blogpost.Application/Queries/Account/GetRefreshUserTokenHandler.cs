using blogpost.Application.Common.Interfaces;

namespace blogpost.Application.Queries.Account
{
    public class GetRefreshUserTokenHandler : IRequestHandler<GetRefreshUserTokenQuery, GetRefreshUserTokenQueryResult>
    {
        private readonly IArticleService _articleService;

        public GetRefreshUserTokenHandler(IArticleService articleService) 
        {
            _articleService = articleService;
        }

        public async Task<GetRefreshUserTokenQueryResult> Handle(GetRefreshUserTokenQuery query, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}
