using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MovieApplication.Models;

namespace MovieApplication
{
    public class MovieDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {
              
        }
        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          

            modelBuilder.HasDefaultSchema("app");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieDbContext).Assembly);


            modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    Id = Guid.NewGuid(),
                    Title = "Inception",
                    Genre = "Sci-Fi",
                    ReleseDate = new DateTimeOffset(2010, 7, 16, 0, 0, 0, TimeSpan.Zero),
                    Rating = 8.8,
                    Created = DateTimeOffset.UtcNow,
                    LastModifed = DateTimeOffset.UtcNow
                },


            base.OnModelCreating(modelBuilder);
        }
    }
}
