using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MovieApplication.DTOs;
using MovieApplication.Models;

namespace MovieApplication.Services
{
    public interface IMovieService
    {
        Task<MovisDto> CreateMoviesAsync(CreateMovieDTO command);
        Task<MovisDto> GetMovieByIdAsync(Guid id);
        Task<IEnumerable<MovisDto>> GetAllMoviesAsync();
        Task<bool> UpdateMovieAsync(Guid id, MovisDto command);
        Task<bool> DeleteMovieAsync(Guid id);
    }   

    public class MoviesService : IMovieService
    {
        
        private readonly MovieDbContext _dbContext;
        private readonly ILogger<MoviesService> _logger;

        
        public MoviesService(MovieDbContext context, ILogger<MoviesService> log)
        {
            _dbContext = context;
            _logger = log;
        }

        
        public async Task<MovisDto> CreateMoviesAsync(CreateMovieDTO command)
        {
            try
            {
                _logger.LogInformation("Create Movie Started");
                var movie = Movie.Create(command.Title, command.Genre, command.ReleaseDate, command.Rating);

                await _dbContext.Movies.AddAsync(movie);
                await _dbContext.SaveChangesAsync();

                //chnage the time format , give more validation


                return new MovisDto(movie.Id, movie.Title, movie.Genre, movie.ReleseDate, movie.Rating);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<IEnumerable<MovisDto>> GetAllMoviesAsync()
        {
            try
            {
               return await _dbContext.Movies.
                    AsNoTracking()
                    .Select(movies => new MovisDto(
                        movies.Id,
                        movies.Title,
                        movies.Genre,
                        movies.ReleseDate,
                        movies.Rating
                        ))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<MovisDto> GetMovieByIdAsync(Guid id)
        {
            try
            {
                var movie =  await _dbContext.Movies
                            .AsNoTracking()
                            .FirstOrDefaultAsync(movie => movie.Id == id);

                if (movie == null)
                {
                    _logger.LogError("No movie found in that id");
                    return null;
                }

                return new MovisDto(movie.Id, movie.Title, movie.Genre, movie.ReleseDate, movie.Rating);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateMovieAsync(Guid id, MovisDto command)
        {
            try
            {
                var movieUpdate = await _dbContext.Movies.FindAsync(id);

                if (movieUpdate == null)
                {
                    _logger.LogError("Movie is not found to update..");
                    return false;
                }

                movieUpdate.Update(command.Title, command.Genre, command.ReleaseDate, command.Rating);
                await _dbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteMovieAsync(Guid id)
        {
            var movieToDelete = await _dbContext.Movies
                                    .FirstOrDefaultAsync(movie => movie.Id == id);
            if(movieToDelete != null)
            {
                _dbContext.Movies.Remove(movieToDelete);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
       
        }


    }
}
