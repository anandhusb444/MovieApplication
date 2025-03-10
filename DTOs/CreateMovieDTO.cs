namespace MovieApplication.DTOs
{
    public record CreateMovieDTO(string Title, string Genre, DateTimeOffset ReleaseDate, double Rating);
}
