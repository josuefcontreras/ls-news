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

namespace lanews.Controllers
{
    [Authorize(Roles="Administrator, Editor")]
    public class ArticlesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArticlesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: All Articles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Articles.Include(a => a.Category).Include(a => a.Status);
            return View(await applicationDbContext.ToListAsync());
        }

        //GET: Articles by user
        public async Task<IActionResult> MyArticles(Guid? id)
        {
            var applicationDbContext = _context.Articles.Include(a => a.Autor).Include(a => a.Category).Include(a => a.Status);
            return View(await applicationDbContext.ToListAsync());
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
            ViewData["AutorId"] = new SelectList(_context.Users, "Id", "FisrtName");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName");
            ViewData["StatusId"] = new SelectList(_context.ArticleStatuses, "Id", "Id");
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
                article.Id = Guid.NewGuid();
                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AutorId"] = new SelectList(_context.Users, "Id", "FisrtName", article.AutorId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", article.CategoryId);
            ViewData["StatusId"] = new SelectList(_context.ArticleStatuses, "Id", "Id", article.StatusId);
            return View(article);
        }

        // GET: Articles/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
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
                try
                {
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

            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var article = await _context.Articles.FindAsync(id);
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(Guid id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }
    }
}
