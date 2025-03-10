namespace MovieApplication.DTOs
{

   public record MovisDto(Guid Id, string Title, string Genre, DateTimeOffset ReleaseDate, double Rating);
}
