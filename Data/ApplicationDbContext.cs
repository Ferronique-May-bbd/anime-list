using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace anime_list.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AnimeList> AnimeList { get; set; }
        public DbSet<UserRating> UserRating { get; set; }
        public DbSet<User> User { get; set; }
    }
}