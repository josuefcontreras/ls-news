using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace lanews.Models
{
    [Table("ARTICLE_STATUS")]
    public partial class ArticleStatus
    {
        public ArticleStatus()
        {
            Articles = new HashSet<Article>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(25)]
        public string StatusName { get; set; }

        [InverseProperty(nameof(Article.Status))]
        public virtual ICollection<Article> Articles { get; set; }
    }
}
