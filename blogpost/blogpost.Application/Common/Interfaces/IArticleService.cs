using blogpost.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blogpost.Application.Common.Interfaces
{
    public interface IArticleService
    {
        Task<ArticleDto> GetArticleById(Guid id);
    }
}
