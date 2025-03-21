﻿using Microsoft.EntityFrameworkCore;
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


           


            base.OnModelCreating(modelBuilder);
        }
    }
}
