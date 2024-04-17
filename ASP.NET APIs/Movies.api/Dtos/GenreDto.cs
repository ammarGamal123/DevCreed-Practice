namespace Movies.api.Dtos
{
    public class GenreDto
    {
        [MaxLength(80)]
        public string Name { get; set; }

    }
}
