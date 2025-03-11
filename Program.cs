
using Microsoft.EntityFrameworkCore;
using MovieApplication.EndPoints;
using MovieApplication.Services;
using Scalar.AspNetCore;

namespace MovieApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<MovieDbContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseNpgsql(connectionString);
            });


            builder.Services.AddTransient<IMovieService, MoviesService>();

            var app = builder.Build();

            


            

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapGet("/", () => "Hello World!")
                    .Produces(200, typeof(string));

                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.MapMovieEndpoints();



            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
