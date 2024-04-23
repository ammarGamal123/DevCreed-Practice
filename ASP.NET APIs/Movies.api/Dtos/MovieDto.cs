namespace Movies.api.Dtos
{
    public class MovieDto
    {
        [MaxLength(250)]
        public string Title { get; set; }
        
        public int Year { get; set; }  

        public double Rate { get; set; }

        public string StoryLine { get; set; }
        
        public IFormFile? Poster { get; set; }

        public byte GenreId { get; set; }

    }
}
