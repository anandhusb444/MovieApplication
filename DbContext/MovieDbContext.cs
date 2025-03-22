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
        public DbSet<Users> Users { get; set; }
        public DbSet<Commands> Commands { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Commands>()
                .HasOne(c => c.Movie)
                .WithMany(m => m.Commands)
                .HasForeignKey(c => c.movie_Id);

            modelBuilder.Entity<Commands>()
                .HasOne(c => c.User)
                .WithMany(u => u.Commands)
                .HasForeignKey(c => c.user_Id);

            modelBuilder.HasDefaultSchema("app");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
