using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace lanews.Models
{
    [Table("RELATED_ARTICLES")]
    public partial class RelatedArticle
    {
        [Key]
        public Guid OriginaleArticleId { get; set; }
        [Key]
        public Guid RelatedArticleId { get; set; }

        [ForeignKey(nameof(OriginaleArticleId))]
        [InverseProperty(nameof(Article.RelatedArticleOriginaleArticles))]
        public virtual Article OriginaleArticle { get; set; }
        [ForeignKey(nameof(RelatedArticleId))]
        [InverseProperty(nameof(Article.RelatedArticleRelatedArticleNavigations))]
        public virtual Article RelatedArticleNavigation { get; set; }
    }
}
