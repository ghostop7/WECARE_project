using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SMTI_SGHE.Models.Entity;

namespace SMTI_SGHE.Data
{
    public class SGHEDbContext : IdentityDbContext<IdentityUser>
    {
        public SGHEDbContext(DbContextOptions<SGHEDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Project> Projects { get; set; }

        
    }
}
