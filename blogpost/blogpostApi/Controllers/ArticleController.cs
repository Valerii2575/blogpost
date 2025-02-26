using blogpost.Application.Command.Article.CreateArticle;
using blogpostApi.Application.Queries.Article.GetArticleById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace blogpostApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController(ISender sender) : ControllerBase
    {
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
