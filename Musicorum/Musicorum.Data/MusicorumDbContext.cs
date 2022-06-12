using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Musicorum.Data.Entities;

namespace Musicorum.Data
{
    public class MusicorumDbContext : IdentityDbContext<User>
    {
        public DbSet<Photo> Photos { get; set; }

        public DbSet<Video> Videos { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Event> Events { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Like> Likes { get; set; }

        public MusicorumDbContext(DbContextOptions<MusicorumDbContext> options)
            : base(options)
        {
        }
    }
}