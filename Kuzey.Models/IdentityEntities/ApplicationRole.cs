using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Kuzey.Models.IdentityEntities
{
    public class ApplicationRole : IdentityRole
    {
        [StringLength(128)]
        public string Description { get; set; }
    }
}