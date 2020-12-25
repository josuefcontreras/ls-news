using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace lanews.Models
{
    [Table("USER_ROLES")]
    public partial class UserRoles : IdentityUserRole<int>
    {
    }
}
