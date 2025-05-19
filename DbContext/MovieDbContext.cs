using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MovieApplication.Models;

namespace MovieApplication  
{
    public class MovieDbContext : IdentityDbContext<ApplicationUser>
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {
              
        }
        public DbSet<Movie> Movies { get; set; }
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

            /*modelBuilder.Entity<IdentityRole>()
                .HasData(
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "User",
                    NormalizedName = "USER"
                }
                );*/

            modelBuilder.HasDefaultSchema("app");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
