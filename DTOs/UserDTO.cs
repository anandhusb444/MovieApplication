using System.ComponentModel.DataAnnotations;

namespace MovieApplication.DTOs
{
   

    public record UserDTO(
        [Required] string userName,
        [Required] string userEmail,
        [StringLength(100,MinimumLength = 8)] string password
        );
}
