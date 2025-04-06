using System.ComponentModel.DataAnnotations;

namespace MovieApplication.Models
{
    public class Users
    {
        [Key]
        public Guid id { get; private set; }
        public string userName { get; set; }
        public string role { get; set; }
        public string email { get; set; }
        public string passwordHash { get; set; }
        public DateTimeOffset createdAt { get; set; }
        public DateTimeOffset updateAt { get; set; }
        public ICollection<Commands> Commands { get; set; }

        

    }
}
