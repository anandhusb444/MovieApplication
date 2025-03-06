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
                  Id = new Guid("087a82e4-63d9-4411-bc8a-bb245ff656af"),
                  Title = "Sonic the Hedgehog 3",
                  Genre = "Fantasy",
                  Rating = 8.0,
                  ReleseDate = DateTime.SpecifyKind(new DateTime(2025, 3, 6, 16, 31, 9), DateTimeKind.Utc) // ✅ Fix Here
              });


            base.OnModelCreating(modelBuilder);
        }
    }
}
