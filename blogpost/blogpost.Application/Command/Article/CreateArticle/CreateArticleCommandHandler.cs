using blogpost.Application.Common.Interfaces;
using blogpost.Application.DTOs;
using MediatR;

namespace blogpost.Application.Command.Article.CreateArticle
{
    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, Guid>
    {
        private readonly IArticleService _articleService;

        public CreateArticleCommandHandler(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public async Task<Guid> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var article = new ArticleDto { Id = Guid.NewGuid(), Title = request.Title, Description = request.Description };
            //_blogPostAppDbContext.Articles.Add(article);
            //await _blogPostAppDbContext.SaveChangesAsync(cancellationToken);

            return article.Id;
        }
    }
}
