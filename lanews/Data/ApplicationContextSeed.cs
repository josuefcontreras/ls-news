using lanews.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanews.Data
{
    public class ApplicationContextSeed
    {
        public static async Task SeedAsync(ApplicationDbContext applicationDbContext, ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;
            try
            {
                // TODO: Only run this if using a real database
                // context.Database.Migrate();
                if (!applicationDbContext.ArticleStatuses.Any<ArticleStatus>())
                {
                    await applicationDbContext.ArticleStatuses.AddRangeAsync(GetPreconfiguredArticleStatues());
                    await applicationDbContext.SaveChangesAsync();
                }
                if (!applicationDbContext.Categories.Any<Category>())
                {
                    await applicationDbContext.Categories.AddRangeAsync(GetPreconfiguredCategories());
                    await applicationDbContext.SaveChangesAsync();
                }
                if (!applicationDbContext.Tags.Any<Tag>())
                {
                    await applicationDbContext.Tags.AddRangeAsync(GetPreconfiguredTags());
                    await applicationDbContext.SaveChangesAsync();
                }
                if (!applicationDbContext.Articles.Any<Article>())
                {
                    await applicationDbContext.Articles.AddRangeAsync(GetPreconfiguredArticles(applicationDbContext));
                    await applicationDbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<ApplicationContextSeed>();
                    log.LogError(ex.Message);
                    await SeedAsync(applicationDbContext, loggerFactory, retryForAvailability);
                }
                throw;
            }
        }

        private static IEnumerable<Article> GetPreconfiguredArticles(ApplicationDbContext applicationDbContext)
        {
            return new List<Article>()
            {
                new Article(){
                    Id = new Guid(),
                    StatusId = applicationDbContext.ArticleStatuses.FirstOrDefault(state => state.StatusName == "published").Id,
                    CreationDate = DateTime.Now,
                    HeadLine = "Headline 1",
                    SubHeading = "Sub-headline 1",
                    Lead = "Leas 1",
                    Body = "Body 1",
                    CategoryId = applicationDbContext.Categories.FirstOrDefault(cat => cat.CategoryName == "politics").Id,
                    AutorId = applicationDbContext.Users.FirstOrDefault(user => user.UserName == "demoeditor@example.com").Id,
                    ReadCount = new Random().Next(150),
                    FeaturedImageUrl = "https://www.stevenstaekwondo.com/wp-content/uploads/2017/04/default-image-620x600.jpg",
                    FeaturedImageAlt = "Demo featured image",
                    FeaturedImageCaption = "Demo featured image",
                    WhereLine = "Santo Domingo"
                },
                new Article(){
                    Id = new Guid(),
                    StatusId = applicationDbContext.ArticleStatuses.FirstOrDefault(state => state.StatusName == "reviewing").Id,
                    CreationDate = DateTime.Now,
                    HeadLine = "Headline 2",
                    SubHeading = "Sub-headline 2",
                    Lead = "Leas 2",
                    Body = "Body 2",
                    CategoryId = applicationDbContext.Categories.FirstOrDefault(cat => cat.CategoryName == "politics").Id,
                    AutorId = applicationDbContext.Users.FirstOrDefault(user => user.UserName == "demoeditor@example.com").Id,
                    ReadCount = new Random().Next(150),
                    FeaturedImageUrl = "https://www.stevenstaekwondo.com/wp-content/uploads/2017/04/default-image-620x600.jpg",
                    FeaturedImageAlt = "Demo featured image",
                    FeaturedImageCaption = "Demo featured image",
                    WhereLine = "Santo Domingo"
                    },
                new Article(){
                    Id = new Guid(),
                    StatusId = applicationDbContext.ArticleStatuses.FirstOrDefault(state => state.StatusName == "draft").Id,
                    CreationDate = DateTime.Now,
                    HeadLine = "Headline 3",
                    SubHeading = "Sub-headline 3",
                    Lead = "Leas 3",
                    Body = "Body 3",
                    CategoryId = applicationDbContext.Categories.FirstOrDefault(cat => cat.CategoryName == "politics").Id,
                    AutorId = applicationDbContext.Users.FirstOrDefault(user => user.UserName == "demoeditor@example.com").Id,
                    ReadCount = new Random().Next(150),
                    FeaturedImageUrl = "https://www.stevenstaekwondo.com/wp-content/uploads/2017/04/default-image-620x600.jpg",
                    FeaturedImageAlt = "Demo featured image",
                    FeaturedImageCaption = "Demo featured image",
                    WhereLine = "Santo Domingo"
                    },
                new Article(){
                    Id = new Guid(),
                    StatusId = applicationDbContext.ArticleStatuses.FirstOrDefault(state => state.StatusName == "published").Id,
                    CreationDate = DateTime.Now,
                    HeadLine = "Headline 4",
                    SubHeading = "Sub-headline 4",
                    Lead = "Leas 4",
                    Body = "Body 4",
                    CategoryId = applicationDbContext.Categories.FirstOrDefault(cat => cat.CategoryName == "sports").Id,
                    AutorId = applicationDbContext.Users.FirstOrDefault(user => user.UserName == "demoeditor@example.com").Id,
                    ReadCount = new Random().Next(150),
                    FeaturedImageUrl = "https://www.stevenstaekwondo.com/wp-content/uploads/2017/04/default-image-620x600.jpg",
                    FeaturedImageAlt = "Demo featured image",
                    FeaturedImageCaption = "Demo featured image",
                    WhereLine = "Santo Domingo"
                },
                new Article(){
                    Id = new Guid(),
                    StatusId = applicationDbContext.ArticleStatuses.FirstOrDefault(state => state.StatusName == "published").Id,
                    CreationDate = DateTime.Now,
                    HeadLine = "Headline 5",
                    SubHeading = "Sub-headline 5",
                    Lead = "Leas 5",
                    Body = "Body 5",
                    CategoryId = applicationDbContext.Categories.FirstOrDefault(cat => cat.CategoryName == "entertainment").Id,
                    AutorId = applicationDbContext.Users.FirstOrDefault(user => user.UserName == "demoeditor@example.com").Id,
                    ReadCount = new Random().Next(150),
                    FeaturedImageUrl = "https://www.stevenstaekwondo.com/wp-content/uploads/2017/04/default-image-620x600.jpg",
                    FeaturedImageAlt = "Demo featured image",
                    FeaturedImageCaption = "Demo featured image",
                    WhereLine = "Santo Domingo"
                }
            };
        }

        private static IEnumerable<Tag> GetPreconfiguredTags()
        {
            return new List<Tag>()
            {
                new Tag(){ TagName="dominican republic"},
                new Tag(){ TagName="cuba"},
                new Tag(){ TagName="usa"},
            };
        }

        private static IEnumerable<Category> GetPreconfiguredCategories()
        {
            return new List<Category>()
            {
                new Category(){ CategoryName="Sports"},
                new Category(){ CategoryName="Politics"},
                new Category(){ CategoryName="Entertainment"},
            };
        }

        private static IEnumerable<ArticleStatus> GetPreconfiguredArticleStatues()
        {
            return new List<ArticleStatus>()
            {
                new ArticleStatus(){ StatusName="reviewing"},
                new ArticleStatus(){ StatusName="draft"},
                new ArticleStatus(){ StatusName="published"},
            };
        }
    }
}
