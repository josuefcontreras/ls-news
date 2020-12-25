using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace lanews.Models
{
    [Table("USERS")]
    public partial class User : IdentityUser<Guid>
    {
        public User()
        {
            Articles = new HashSet<Article>();
        }
        [Required]
        [StringLength(200)]
        [Display(Name = "First name")]
        public string FisrtName { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        public bool IsEmailConfirmed { get; set; } = false;
        [Required]
        public bool? Active { get; set; } = true;
        [Column(TypeName = "date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Registration date")]
        public DateTime RegistrationDate { get; set; }

        [InverseProperty(nameof(Article.Autor))]
        public virtual ICollection<Article> Articles { get; set; }
    }
}
