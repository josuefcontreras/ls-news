using lanews.Data;
using lanews.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanews.Services
{
    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ArticleService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IEnumerable<Article> GetArticlesByAuthorId(Guid authorId)
        {
           return _applicationDbContext.Articles.Include(a => a.Autor).Include(a => a.Category).Include(a => a.Status).Where(article => article.AutorId == authorId);
        }
    }
}
