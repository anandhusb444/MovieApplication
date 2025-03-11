using MovieApplication.DTOs;
using MovieApplication.Services;
using System.Runtime.CompilerServices;

namespace MovieApplication.EndPoints
{
    public static class MovieEndPoints
    {
        public static void MapMovieEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapPost("/api/movies", async (IMovieService service, CreateMovieDTO command) =>
            {
                var movie = await service.CreateMoviesAsync(command);
                return Results.Created($"/api/movies/{movie.Id}", movie);
            }).WithTags("Movies");

            routes.MapGet("/api/movies", async (IMovieService service) =>
            {
                var movies = await service.GetAllMoviesAsync();
                return Results.Ok(movies);
            }).WithTags("Movies");

        }
    }
}
