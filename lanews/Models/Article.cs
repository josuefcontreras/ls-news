using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace lanews.Models
{
    [Table("ARTICLES")]
    public partial class Article
    {
        public Article()
        {
            ArticleTags = new HashSet<ArticleTag>();
            RelatedArticleOriginaleArticles = new HashSet<RelatedArticle>();
            RelatedArticleRelatedArticleNavigations = new HashSet<RelatedArticle>();
        }

        [Key]
        public Guid Id { get; set; }
        [Display(Name = "Status id")]
        public int StatusId { get; set; }
        [Column(TypeName = "DateTime")]
        [Display(Name = "Creation date")]
        public DateTime CreationDate { get; set; }
        [Column("modificationDate", TypeName = "DateTime")]
        [Display(Name = "Last modified")]
        public DateTime ModificationDate { get; set; }
        [Required]
        [StringLength(256)]
        [Display(Name = "Headline")]
        public string HeadLine { get; set; }
        [Required]
        [StringLength(256)]
        [Display(Name = "Sub-heading")]
        public string SubHeading { get; set; }
        [Required]
        [StringLength(512)]
        [Display(Name = "Lead")]
        public string Lead { get; set; }
        [Required]
        [Display(Name = "Body")]
        public string Body { get; set; }
        [Display(Name = "Category id")]
        public int CategoryId { get; set; }
        [Display(Name = "Author id")]
        public Guid AutorId { get; set; }
        [Display(Name = "Read count")]
        public int? ReadCount { get; set; }
        [Required]
        [Column("FeaturedImageURL")]
        [StringLength(256)]
        [Display(Name = "Featured image URL")]
        public string FeaturedImageUrl { get; set; }
        [Required]
        [StringLength(256)]
        [Display(Name = "Featured image caption")]
        public string FeaturedImageCaption { get; set; }
        [Required]
        [StringLength(256)]
        public string FeaturedImageAlt { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Whereline")]
        public string WhereLine { get; set; }

        [ForeignKey(nameof(AutorId))]
        [InverseProperty(nameof(User.Articles))]
        public virtual User Autor { get; set; }
        [ForeignKey(nameof(CategoryId))]
        [InverseProperty("Articles")]
        public virtual Category Category { get; set; }
        [ForeignKey(nameof(StatusId))]
        [InverseProperty(nameof(ArticleStatus.Articles))]
        public virtual ArticleStatus Status { get; set; }
        [InverseProperty(nameof(ArticleTag.Article))]
        public virtual ICollection<ArticleTag> ArticleTags { get; set; }
        [InverseProperty(nameof(RelatedArticle.OriginaleArticle))]
        public virtual ICollection<RelatedArticle> RelatedArticleOriginaleArticles { get; set; }
        [InverseProperty(nameof(RelatedArticle.RelatedArticleNavigation))]
        public virtual ICollection<RelatedArticle> RelatedArticleRelatedArticleNavigations { get; set; }
    }
}
