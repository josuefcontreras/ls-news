using lanews.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanews.Services
{
    public interface IArticleService
    {
        IEnumerable<Article> GetArticlesByAuthorId(Guid authorId);
    }
}
