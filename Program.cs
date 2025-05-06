
using Microsoft.EntityFrameworkCore;
using MovieApplication.EndPoints;
using MovieApplication.Services;
using Scalar.AspNetCore;
using Serilog;

namespace MovieApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            // Configure Serilog to write logs to a file
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/app.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog();





            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<MovieDbContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");//Seting up connection string
                options.UseNpgsql(connectionString);
            });


            builder.Services.AddScoped<IMovieService, MoviesService>();//DL Injection
            builder.Services.AddScoped<IuserServices, UserServies>();


            builder.Services.AddCors(option =>
            {
                option.AddPolicy("AllowReactApp", policy =>
                {
                    policy.WithOrigins("http://localhost:5177")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });





            var app = builder.Build();

         

            app.UseCors("AllowReactApp");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapGet("/", () => "Hello World!")
                    .Produces(200, typeof(string));

                app.MapOpenApi();

                app.MapScalarApiReference();
            }

            //app.MapMovieEndpoints()

            



            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
