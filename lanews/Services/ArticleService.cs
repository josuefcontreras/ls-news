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
        private readonly ApplicationDbContext _context;

        public ArticleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateArticle(Article article)
        {
            article.Id = Guid.NewGuid();
            _context.Add(article);
            return await _context.SaveChangesAsync();
        }
        public  IEnumerable<Article> GetArticlesByAuthorId(string authorId)
        {
           return  _context.Articles.Include(a => a.Autor).Include(a => a.Category).Include(a => a.Status).Where(article => article.AutorId.ToString() == authorId);
        }

        public async Task<IEnumerable<Article>> GetArticlesByCategoryName(string categoryName)
        {
            return await _context.Articles.Where(a => a.Category.CategoryName == categoryName && a.Status.StatusName == "published").ToListAsync();
        }

        public IEnumerable<Article> GetArticlesByStatus(string status)
        {
            int statusId =  _context.ArticleStatuses.First(s => s.StatusName == status).Id;
            return _context.Articles.Include(a => a.Category).Include(a => a.Status).Where(a => a.StatusId == statusId);
        }
    }
}
