
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MovieApplication.EndPoints;
using MovieApplication.Models;
using MovieApplication.Services;
using Scalar.AspNetCore;
using Serilog;
using System.Text;

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
            builder.Services.AddScoped<IjwtTokenServices, JwtTokenServices>();


            builder.Services.AddCors(option =>
            {
                option.AddPolicy("AllowReactApp", policy =>
                {
                    policy.WithOrigins("http://127.0.0.1:5500")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            //http://127.0.0.1:5500 html/css register path
            //http://localhost:5177 react resgister path



            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
            {
                option.Password.RequireDigit = true;
                option.Password.RequiredLength = 6;
                option.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<MovieDbContext>()
                .AddDefaultTokenProviders();


            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", option =>
                {
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "movieApi",
                        ValidAudience = "movieApiUser",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                        
                    };
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

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
