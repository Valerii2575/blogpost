using MediatR;

namespace blogpost.Application.Command.Article.CreateArticle
{
    public record CreateArticleCommand(string Title, string Description) : IRequest<Guid>;


}
