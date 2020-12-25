using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace lanews.Models
{
    [Table("ARTICLE_TAG")]
    public partial class ArticleTag
    {
        [Key]
        public Guid ArticleId { get; set; }
        [Key]
        public int TagId { get; set; }

        [ForeignKey(nameof(ArticleId))]
        [InverseProperty("ArticleTags")]
        public virtual Article Article { get; set; }
        [ForeignKey(nameof(TagId))]
        [InverseProperty("ArticleTags")]
        public virtual Tag Tag { get; set; }
    }
}
