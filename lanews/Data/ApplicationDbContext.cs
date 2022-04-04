using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using lanews.Models;

namespace lanews.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<ArticleStatus> ArticleStatuses { get; set; }
        public virtual DbSet<ArticleTag> ArticleTags { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<RelatedArticle> RelatedArticles { get; set; }
        //public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        //public virtual DbSet<User> Users { get; set; }
        //public virtual DbSet<UserRoles> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Article>(entity =>
            {

                entity.HasOne(d => d.Autor)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.AutorId)
                    .HasConstraintName("FK__ARTICLES__AutorI__6B24EA82");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__ARTICLES__Catego__6D0D32F4");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__ARTICLES__Status__6C190EBB");
            });

            modelBuilder.Entity<ArticleTag>(entity =>
            {
                entity.HasKey(e => new { e.ArticleId, e.TagId })
                    .HasName("PK__ARTICLE___4A35BF72EB46C607");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.ArticleTags)
                    .HasForeignKey(d => d.ArticleId)
                    .HasConstraintName("FK__ARTICLE_T__Artic__7A672E12");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.ArticleTags)
                    .HasForeignKey(d => d.TagId)
                    .HasConstraintName("FK__ARTICLE_T__TagId__7B5B524B");
            });

            modelBuilder.Entity<RelatedArticle>(entity =>
            {
                entity.HasKey(e => new { e.OriginaleArticleId, e.RelatedArticleId })
                    .HasName("PK__RELATED___C5DB75EAE9457A4F");

                entity.HasOne(d => d.OriginaleArticle)
                    .WithMany(p => p.RelatedArticleOriginaleArticles)
                    .HasForeignKey(d => d.OriginaleArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RELATED_A__Origi__73BA3083");

                entity.HasOne(d => d.RelatedArticleNavigation)
                    .WithMany(p => p.RelatedArticleRelatedArticleNavigations)
                    .HasForeignKey(d => d.RelatedArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RELATED_A__Relat__74AE54BC");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
