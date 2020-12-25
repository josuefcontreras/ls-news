using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using lanews.Models;

#nullable disable

namespace lanews.Data
{
    public partial class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
    {
        public ApplicationDbContext()
        {
        }

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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-LTE186U7\\SQLEXPRESS;Initial Catalog=lanews_db;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Article>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Body).IsUnicode(false);

                entity.Property(e => e.CreationDate).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.FeaturedImageAlt)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Default feature image')");

                entity.Property(e => e.FeaturedImageCaption)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Default feature image')");

                entity.Property(e => e.FeaturedImageUrl)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('~/img/default-article-image.png')");

                entity.Property(e => e.HeadLine).IsUnicode(false);

                entity.Property(e => e.Lead).IsUnicode(false);

                entity.Property(e => e.ModificationDate).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ReadCount).HasDefaultValueSql("((0))");

                entity.Property(e => e.SubHeading).IsUnicode(false);

                entity.Property(e => e.WhereLine).IsUnicode(false);

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
            modelBuilder.Entity<ArticleStatus>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.StatusName).IsUnicode(false);
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
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.CategoryName).IsUnicode(false);
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
            modelBuilder.Entity<Tag>(entity =>
            {
                entity.Property(e => e.TagName).IsUnicode(false);
            });
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
