
using Microsoft.EntityFrameworkCore;

namespace Movies.api.Services
{
    public class GenreService : IGenresService
    {
        private readonly ApplicationDbContext context;

        public GenreService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<Genre> Add(Genre genre)
        {
           await context.Genres.AddAsync(genre);
            context.SaveChanges();
            
            return genre;
        }

        public Genre Delete(Genre genre)
        {
            context.Genres.Remove(genre);
            context.SaveChanges();

            return genre;
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            return await context.Genres.OrderBy(m => m.Name).ToListAsync();

        }

        public async Task<Genre> GetById(byte id)
        {
            return await context.Genres.SingleOrDefaultAsync(g => g.Id == id);

        }

        public Task<bool> IsValidGenre(byte id)
        {

            var genre = context.Genres.AnyAsync(g => g.Id == id);
            
            return genre;
        }

        public Genre Update(Genre genre)
        {
            context.Genres.Update(genre);
            context.SaveChanges();

            return genre;
        }
    }
}
