using MovieApplication.DTOs;
using MovieApplication.Services;

namespace MovieApplication.EndPoints
{
    public static class MovieEndPoints
    {
        public static void MapMovieEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapPost("/", async (IMovieService service, CreateMovieDTO command) =>
            {
                var movie = await service.CreateMoviesAsync(command);
                return Results.Created($"/api/movies/{movie.Id}", movie);
            }).WithTags("Movies");

            routes.MapGet("/", async (IMovieService service) =>
            {
                var movies = await service.GetAllMoviesAsync();
                return Results.Ok(movies);
            }).WithTags("Movies");

            routes.MapGet("/{id}", async (IMovieService service, Guid id) =>
            {
                var movie = await service.GetMovieByIdAsync(id);

                if (movie == null)
                {
                    return (IResult)TypedResults.NotFound(new { Message = $"Movie With ID {id} not found..." });
                }
                else
                {
                    TypedResults.Ok(movie);
                }

            });

        }
    }
}
