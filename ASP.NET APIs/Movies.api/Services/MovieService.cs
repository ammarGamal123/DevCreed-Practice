
using Microsoft.EntityFrameworkCore;

namespace Movies.api.Services
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext _context;

        public MovieService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Movie Add(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();

            return movie;
        }

        public async Task<Movie> Delete(int id)
        {
            var movie = await GetById(id);

            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return movie;
        }

        public async Task<IEnumerable<Movie>> GetAll(byte genreId = 0)
        {

            var movies = await _context.Movies
                .Where(m => m.GenreId == genreId || genreId == 0)
                .OrderBy(m => m.Rate)
                .Include(m => m.Genre)
                .ToListAsync();

            return movies;
        }

        public async Task<Movie> GetById(int id)
        {
            return await _context.Movies.SingleOrDefaultAsync(m => m.Id == id);

          // await _context.Movies.Include(m => m.Genre).SingleOrDefaultAsync(m => m.Id == id);
          // 
        }

        public Movie Update(Movie movie)
        {
            _context.Movies.Update(movie);
            _context.SaveChanges();

            return (movie);
        }
    }
}
