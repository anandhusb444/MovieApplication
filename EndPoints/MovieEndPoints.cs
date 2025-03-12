using MovieApplication.DTOs;
using MovieApplication.Services;

namespace MovieApplication.EndPoints
{
    public static class MovieEndPoints
    {
        public static void MapMovieEndpoints(this IEndpointRouteBuilder routes)
        {
            var movieApi = routes.MapGroup("/api/movies").WithTags("Movies");


            movieApi.MapPost("/", async (IMovieService service, CreateMovieDTO command) =>
            {
                var movie = await service.CreateMoviesAsync(command);
                return Results.Created($"/api/movies/{movie.Id}", movie);
            }).WithTags("Movies");

            movieApi.MapGet("/", async (IMovieService service) =>
            {
                var movies = await service.GetAllMoviesAsync();
                return Results.Ok(movies);
            }).WithTags("Movies");

            movieApi.MapGet("/{id}", async (IMovieService service, Guid id) =>
            {
                var movie = await service.GetMovieByIdAsync(id);

                if (movie == null)
                {
                    return (IResult)TypedResults.NotFound(new { Message = $"Movie With ID {id} not found..." });
                }
                else
                {
                    return TypedResults.Ok(movie);
                }
            });

            movieApi.MapDelete("/{id}", async (IMovieService service, Guid id) =>
            {
                await service.DeleteMovieAsync(id);
                return TypedResults.NoContent();
                //show respones in the API 
            });

            movieApi.MapPut("/{id}", async (IMovieService service, Guid id, MovisDto command) =>
            {
                await service.UpdateMovieAsync(id, command);
                TypedResults.NoContent();
                //shoe respones API
            });

            //-----------------------------------------IMPROVE---------------------------------------
            // Add Generic respones
            // Validate Case
            //Test Case

        }
    }
}
