using blogpost.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
