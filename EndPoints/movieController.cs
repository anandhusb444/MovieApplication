using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApplication.DTOs;
using MovieApplication.Respones;
using MovieApplication.Services;

namespace MovieApplication.EndPoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class movieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public movieController(IMovieService serverice)
        {
            _movieService = serverice;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovies(CreateMovieDTO command)
        {
            try
            {
                var movie = await _movieService.CreateMoviesAsync(command);
                var response = new GenericRespones<object>(201, "Movie created successfully", movie, null);
                return Ok(response);

                //2025-03-11T17:30:00Z
                //fix the realse date time in genrel no body going to give like this .... ISSUE]
            }
            catch (Exception ex)
            {
                var errorRespones = new GenericRespones<object>(500, "Interal server error", null, ex.Message);
                return StatusCode(500, errorRespones);
            }
        }
    }
}
