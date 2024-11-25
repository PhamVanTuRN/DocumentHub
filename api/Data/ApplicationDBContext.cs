using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        {
        }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CareAbout> CareAbout { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CareAbout>(x => x.HasKey(p => new { p.AppUserId, p.DocumentId }));

            builder.Entity<CareAbout>()
                .HasOne(u => u.AppUser)
                .WithMany(u => u.CareAbout)
                .HasForeignKey(p => p.AppUserId);

            builder.Entity<CareAbout>()
                .HasOne(u => u.Document)
                .WithMany(u => u.CareAbout)
                .HasForeignKey(p => p.DocumentId);


            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}