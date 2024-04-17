using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Movies.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public GenresController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _context.Genres.OrderBy(g => g.Name).ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] GenreDto genreDto)
        {

            if (ModelState.IsValid)
            {
                var genre = new Genre() { Name = genreDto.Name };

                await _context.Genres.AddAsync(genre);
                _context.SaveChanges();

                return Ok(genre);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute]byte id ,[FromBody] GenreDto genreDto)
        {
            if (ModelState.IsValid)
            {
                // why can't use Find(id) ==> because id data type is not integer
                // only if integer find(id) is valid
                var genre = await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);
                if (genre == null)
                {
                    return NotFound($"No Genre Was Found With ID {id}");
                }
                genre.Name = genreDto.Name;
                _context.SaveChanges();

                return Ok(genre);
            }

            return BadRequest(ModelState);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]byte id)
        {
            var genre = await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);
            if (genre == null)
            {
                return NotFound($"No Genre Found With ID : {id}");
            }

            _context.Genres.Remove(genre);
            _context.SaveChanges();
            return Ok("This Genre has been Deleted Succussfully");
        }
    }
}
