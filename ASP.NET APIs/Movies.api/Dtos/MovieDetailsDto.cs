namespace Movies.api.Dtos
{
    public class MovieDetailsDto
    {

        public int Id { get; set; }

        public string Title { get; set; }
        
        public int Year { get; set; }  

        public double Rate { get; set; }

        public string StoryLine { get; set; }
        
        public byte[] Poster { get; set; }

        public byte GenreId { get; set; }

        // Navigation Property
        public string GenreName { get; set; }

 
    }
}
