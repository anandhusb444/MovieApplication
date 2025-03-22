//using MovieApplication.DTOs;
//using MovieApplication.Respones;
//using MovieApplication.Services;

//namespace MovieApplication.EndPoints
//{
//    public static class MovieEndPoints
//    {
//        public static void MapMovieEndpoints(this IEndpointRouteBuilder routes)
//        {
//            var movieApi = routes.MapGroup("/api/movies").WithTags("Movies");


//            movieApi.MapPost("/", async (IMovieService service, CreateMovieDTO command) =>
//            {
//                try
//                {
//                    var movie = await service.CreateMoviesAsync(command);
//                    var respones = new GenericRespones<object>(201, "Movie created successfuly", movie, null);
//                    return Results.Created($"/api/movies/{movie.Id}", respones);
//                }
//                catch(Exception ex)
//                {
//                    var errorRespones = new GenericRespones<object>(500, "Internal server error", null, ex.Message);
//                    return Results.Json(errorRespones, statusCode: 500);
//                }
                
//            }).WithTags("Movies");

//            movieApi.MapGet("/", async (IMovieService service) =>
//            {
//                try
//                {
//                    var movies = await service.GetAllMoviesAsync();
//                    var respones = new GenericRespones<object>(200, "Successfuly get all the respones", movies, null);

//                    return Results.Ok(respones);
//                }
//                catch(Exception ex)
//                {
//                    var errorRespones = new GenericRespones<object>(500, "Internal server error", null, ex.Message);
//                    return Results.Json(errorRespones, statusCode: 500);
//                }
              
//            }).WithTags("Movies");

//            movieApi.MapGet("/{id}", async (IMovieService service, Guid id) =>
//            {
//                try
//                {
//                    var movie = await service.GetMovieByIdAsync(id);

//                    if (movie != null)
//                    {
//                        var respones = new GenericRespones<object>(200, "Successfult get the Movie", movie, null);
//                        return TypedResults.Ok(respones);

                        
//                    }
//                    else
//                    {
//                        var notFoundRespones = new GenericRespones<object>(400, $"Movie With ID {id} not found...", null, $"can't find the movie with Id {id}");
//                        return TypedResults.NotFound(notFoundRespones);
//                    }
//                }
//                catch(Exception ex)
//                {
//                    var errorRespones = new GenericRespones<object>(500, "Internal server error", null, ex.Message);
//                    return Results.Json(errorRespones, statusCode: 500);
//                }
               
//            });

//            movieApi.MapDelete("/{id}", async (IMovieService service, Guid id) =>
//            {
//                try
//                {
//                    await service.DeleteMovieAsync(id);
//                    return TypedResults.NoContent();
//                }
//                catch(Exception ex)
//                {
//                    var errorRespones = new GenericRespones<object>(500, "Internal server error", null, ex.Message);
//                    return Results.Json(errorRespones, statusCode: 500);
//                }
               
//                //show respones in the API 
//            });

//            movieApi.MapPut("/{id}", async (IMovieService service, Guid id, MovisDto command) =>
//            {

//                try
//                {
//                    await service.UpdateMovieAsync(id, command);
//                    return TypedResults.NoContent();
//                }
//                catch(Exception ex)
//                {
//                    var errorRespones = new GenericRespones<object>(500, "Internal server error", null, ex.Message);
//                    return Results.Json(errorRespones, statusCode: 500);
//                }
               
//                //shoe respones API
//            });

//            //-----------------------------------------IMPROVE---------------------------------------
//            // Add Generic respones
//            // Validate Case
//            //Test Case

//        }
//    }
//}
