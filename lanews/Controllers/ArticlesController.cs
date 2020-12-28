using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lanews.Data;
using lanews.Models;
using Microsoft.AspNetCore.Authorization;
using lanews.Services;
using Microsoft.AspNetCore.Identity;

namespace lanews.Controllers
{
    [Authorize(Roles = "Administrator, Editor")]
    public class ArticlesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IArticleService _articleService;
        private readonly UserManager<User> _userManager;

        public ArticlesController(ApplicationDbContext context, IArticleService articleService, UserManager<User> userManager)
        {
            _context = context;
            _articleService = articleService;
            _userManager = userManager;
        }
          
        // GET: All Articles
        public IActionResult Index()
        {
            IEnumerable<Article> publishedArticles = _articleService.GetArticlesByStatus("published");
            return View(publishedArticles);
        }

        //GET: Articles by user
        [Authorize(Roles = "Editor")]
        public IActionResult MyArticles()
        {
            var articles = _articleService.GetArticlesByAuthorId(_userManager.GetUserId(User));
            return View(articles);
        }

        // GET: Articles/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .Include(a => a.Autor)
                .Include(a => a.Category)
                .Include(a => a.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // GET: Articles/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName");
            ViewData["StatusId"] = new SelectList(_context.ArticleStatuses, "Id", "StatusName");
            return View();
        }

        // POST: Articles/Create    
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StatusId,CreationDate,ModificationDate,HeadLine,SubHeading,Lead,Body,CategoryId,AutorId,ReadCount,FeaturedImageUrl,FeaturedImageCaption,FeaturedImageAlt,WhereLine")] Article article)
        {
            if (ModelState.IsValid)
            {
                await _articleService.CreateArticle(article);
               return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", article.Category.CategoryName);
            ViewData["StatusId"] = new SelectList(_context.ArticleStatuses, "Id", "Id", article.Status.StatusName);
            return View(article);
        }

        // GET: Articles/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.Include(a => a.Autor).FirstOrDefaultAsync(a => a.Id == id);
            if (article == null)
            {
                return NotFound();
            }
            if(User.Identity.Name != article.Autor.UserName)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewData["AutorId"] = new SelectList(_context.Users, "Id", "FisrtName", article.AutorId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", article.CategoryId);
            ViewData["StatusId"] = new SelectList(_context.ArticleStatuses, "Id", "Id", article.StatusId);
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,StatusId,CreationDate,ModificationDate,HeadLine,SubHeading,Lead,Body,CategoryId,AutorId,ReadCount,FeaturedImageUrl,FeaturedImageCaption,FeaturedImageAlt,WhereLine")] Article article)
        {
            if (id != article.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (User.Identity.Name != article.Autor.UserName)
                {
                    return RedirectToAction(nameof(Index));
                }
                try
                {
                    article.ModificationDate = DateTime.Now;
                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AutorId"] = new SelectList(_context.Users, "Id", "FisrtName", article.AutorId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", article.CategoryId);
            ViewData["StatusId"] = new SelectList(_context.ArticleStatuses, "Id", "Id", article.StatusId);
            return View(article);
        }

        // GET: Articles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .Include(a => a.Autor)
                .Include(a => a.Category)
                .Include(a => a.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }
            if (User.Identity.Name != article.Autor.UserName)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            
            var article = await _context.Articles.FindAsync(id);
            if (User.Identity.Name != article.Autor.UserName)
            {
                return RedirectToAction(nameof(Index));
            }
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Articles/Caregories/sport
        [AllowAnonymous]
        public async Task<IActionResult> Categories(string id)
        {
            bool categoryExists = _context.Categories.Where(c => c.CategoryName == id).Count() == 0  ? false: true;
           
            if (categoryExists)
            {
                var articles = await _context.Articles
               .Include(a => a.Category)
               .Where(a => a.Category
               .CategoryName == id)
               .ToListAsync();

                ViewBag.category = id;

                return View(articles);
            }
            else
            {
                return NotFound();
            }
        }
        private bool ArticleExists(Guid id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }
        
    }
}
