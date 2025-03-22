
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MovieApplication.Models
{
    public class Movie : EntityBase
    {
        [Key]
        public Guid movie_Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public Guid director_Id { get; set;}
        public DateTimeOffset ReleseDate { get; set; }
        public double Rating { get; set; } 
        public ICollection<Commands> Commands { get; set; }
        public Movie()
        {

        }

        public Movie(string title, string genre, DateTimeOffset releseDate, double rating)
        {
            this.Title = title;
            this.Genre = genre;
            this.ReleseDate = releseDate;
            this.Rating = rating;
        }

        public static Movie Create(string title, string genre, DateTimeOffset releseDate, double rating)
        {
            ValidateInputs(title, genre, releseDate, rating);

            return new Movie(title, genre, releseDate, rating);
        }

        public void Update(string title, string genre, DateTimeOffset releseDate, double rating)
        {
            ValidateInputs(title, genre, releseDate, rating);

            Title = title;
            Genre = genre;
            ReleseDate = releseDate;
            Rating = rating;

            UdateLastModified();
        }

        private static void ValidateInputs(string title, string genre, DateTimeOffset releseDate, double rating)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentException("Title cannot be null here.", nameof(title));

            if (string.IsNullOrWhiteSpace(genre))
                throw new ArgumentException("genre cannot be null here.", nameof(genre));

            if (releseDate > DateTimeOffset.UtcNow)
                throw new ArgumentException("Relese date connot be in the future", nameof(releseDate));

            if (rating < 0 || rating > 10)
                throw new ArgumentException("Rating  must be in between of 0 & 10", nameof(rating));
        }


    }
}
