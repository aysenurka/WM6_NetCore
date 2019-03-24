using System;
using System.Linq;
using System.Security.Claims;
using Kuzey.Models.Entities;
using Kuzey.Models.IdentityEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kuzey.DAL
{
    public class MyContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MyContext(DbContextOptions<MyContext> options, IHttpContextAccessor httpContextAccessor)
        : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override int SaveChanges()
        {
            var selectedEntityList = ChangeTracker.Entries()
                .Where(x => x.Entity is AuditEntity && x.State == EntityState.Added);
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            foreach (var entity in selectedEntityList)
            {
                ((AuditEntity)(entity.Entity)).CreatedUserId = userId;
            }
            selectedEntityList = ChangeTracker.Entries()
                .Where(x => x.Entity is AuditEntity && x.State == EntityState.Modified);
            foreach (var entity in selectedEntityList)
            {
                ((AuditEntity)(entity.Entity)).UpdatedUserId = userId;
                ((AuditEntity)(entity.Entity)).UpdatedDate = DateTime.Now;
            }
            return base.SaveChanges();
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
    }
}
