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
<<<<<<< HEAD
=======
        public DbSet<UserRating> UserRating { get; set; }
>>>>>>> main
    }
}