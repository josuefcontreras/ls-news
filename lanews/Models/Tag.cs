using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace lanews.Models
{
    [Table("TAGS")]
    [Index(nameof(TagName), Name = "UQ__TAGS__BDE0FD1D195FBC0E", IsUnique = true)]
    public partial class Tag
    {
        public Tag()
        {
            ArticleTags = new HashSet<ArticleTag>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string TagName { get; set; }

        [InverseProperty(nameof(ArticleTag.Tag))]
        public virtual ICollection<ArticleTag> ArticleTags { get; set; }
    }
}
