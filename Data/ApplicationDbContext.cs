using Atlas.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Atlas.Models.ViewModels;

namespace Atlas.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Mushroom> Mushrooms { get; set; }
        public DbSet<Occurence> Occurences { get; set; }
        
    }
}
