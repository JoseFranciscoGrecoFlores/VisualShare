using Microsoft.EntityFrameworkCore;
using VisualShare.Shared;

namespace VisualShare.Server
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Video>().Property(video => video.PublishedDate).HasDefaultValueSql("DATETIME('now')");
            modelBuilder.Entity<Photo>().Property(blog => blog.PublishedDate).HasDefaultValueSql("DATETIME('now')");
            modelBuilder.Entity<Comment>().Property(comment => comment.PublishedDate).HasDefaultValueSql("DATETIME('now')");
        }
    }
}