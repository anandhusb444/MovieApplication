using System.ComponentModel.DataAnnotations;

namespace MovieApplication.Models
{
    public class Commands
    {
        [Key]
        public Guid command_Id { get; set; }
        public Guid movie_Id { get; set; }
        public Guid user_Id { get; set; }
        public string command { get; set; }
        public DateTime created_At { get; set; }
        public Movie Movie { get; set; }
        public Users User { get; set; }

    }
}
