using blogpost.Application.Command.Article.CreateArticle;
using blogpost.Application.Queries.Article.GetAll;
using blogpostApi.Application.Queries.Article.GetArticleById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace blogpostApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController(ISender sender) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<GetArticlesQueryResult>> GetAll()
        {
            return await sender.Send(new GetArticlesQuery());
        }

        [HttpPost]
        public async Task<ActionResult> CreateArticle(CreateArticleCommand command)
        {
            var articleId = await sender.Send(command);

            return Ok(articleId);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticleById(Guid id)
        {
            var article = await sender.Send(new GetArticleByIdQuery(id));
            return Ok(article);
        }
    }
}
