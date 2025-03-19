using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;
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

        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            try
            {
                var movies = await _movieService.GetAllMoviesAsync();
                if(movies == null)
                {
                    return NotFound();
                }

                var respones = new GenericRespones<object>(201, "Success", movies, null);
                return Ok(respones);
            }
            catch (Exception ex)
            {
                var errorRespones = new GenericRespones<object>(500, "Internal server error", null, ex.Message);
                return StatusCode(500, errorRespones);
            }
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetMovieById(Guid id)
        {
            try
            {
                var movie = await _movieService.GetMovieByIdAsync(id);
                if(movie == null)
                {
                    return NotFound();
                }

                var respones = new GenericRespones<object>(201, "sucess", movie, null);
                return Ok(respones);

            }
            catch (Exception ex)
            {
                var errorRespones = new GenericRespones<object>(500, "Internal server error", null, ex.Message);
                return StatusCode(500, errorRespones);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMovies(Guid id, MovisDto command)
        {
            try
            {
                var updateMovie = await _movieService.UpdateMovieAsync(id, command);

                if (updateMovie == null)
                    return NotFound();

                var respones = new GenericRespones<object>(201, "sucess", updateMovie, null);
                return Ok(respones);

            }
            catch (Exception ex)
            {
                var errorRespones = new GenericRespones<object>(500, "Internal server error", null, ex.Message);
                return StatusCode(500, errorRespones);
            }
        }

        [HttpDelete("/{id}")]
        public async Task<IActionResult> DeleteMovie(Guid id)
        {
            try
            {
                var deleteMovie = await _movieService.DeleteMovieAsync(id);
                if (!deleteMovie)
                    return NotFound();

                var respones = new GenericRespones<object>(200, "sucess", deleteMovie, null);
                return Ok(respones);

            }
            catch (Exception ex)
            {
                var errorRespones = new GenericRespones<object>(500, "Internal server error", null, ex.Message);
                return StatusCode(500,errorRespones);
            }
        }
    }
}
