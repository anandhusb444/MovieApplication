using Microsoft.AspNetCore.Identity;

namespace MovieApplication.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
