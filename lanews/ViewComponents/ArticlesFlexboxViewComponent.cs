using lanews.Data;
using lanews.Models;
using lanews.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanews.ViewComponents
{
    public class ArticlesFlexboxViewComponent : ViewComponent
    {
        private readonly IArticleService _articleService;

        public ArticlesFlexboxViewComponent(IArticleService articleService)
        {
            _articleService = articleService;
        }
        public async Task<IViewComponentResult> InvokeAsync(string categoryName, IEnumerable<Article> articles = null)
        {
            if(articles == null)
            {
                articles = await _articleService.GetArticlesByCategoryName(categoryName);
            }
      
            ViewBag.CategoryName = categoryName;

            return View(articles);
        }
    }
}
