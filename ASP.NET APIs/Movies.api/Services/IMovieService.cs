using System.Collections.Generic;

namespace Movies.api.Services
{
    public interface IMovieService
    {
        Task<Movie> GetById(int id);

        Task<IEnumerable<Movie>> GetAll(byte genreId = 0);
        
        Movie Add(Movie movie);

        Movie Update (Movie movie);

        Task<Movie> Delete(int id);

    }
}
