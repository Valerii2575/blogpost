using blogpost.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
