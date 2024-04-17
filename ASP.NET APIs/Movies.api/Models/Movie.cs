using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.api.Models
{
    public class Movie
    {

        public int Id { get; set; }

        [MaxLength(250)]
        public string Title { get; set; }
        
        public int Year { get; set; }  

        public double Rate { get; set; }

        public string StoryLine { get; set; }
        
        public byte[] Poster { get; set; }

        [ForeignKey("Genre")]
        public byte GenreId { get; set; }

        // Navigation Property
        public Genre Genre { get; set; }

    }
}
