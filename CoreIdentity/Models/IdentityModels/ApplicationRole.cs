using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CoreIdentity.Models.IdentityModels
{
    public class ApplicationRole : IdentityRole
    {
        [Required, StringLength(128)]
        public string Description { get; set; }
    }
}