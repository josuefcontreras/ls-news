using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace lanews.Models
{
    [Table("CATEGORIES")]
    [Index(nameof(CategoryName), Name = "UQ__CATEGORI__8517B2E00E914E67", IsUnique = true)]
    public partial class Category
    {
        public Category()
        {
            Articles = new HashSet<Article>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string CategoryName { get; set; }

        [InverseProperty(nameof(Article.Category))]
        public virtual ICollection<Article> Articles { get; set; }
    }
}
