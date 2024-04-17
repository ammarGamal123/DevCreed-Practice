using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Movies.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly List<string> _allowedExtensions = new List<string>
        {
            ".jpg" , ".png"
        };
        private readonly long _maxAllowedPosterSize = 1024 * 1024;

        public MoviesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        //                              Required To get Image from User
        public async Task<IActionResult> CreateAsync([FromForm]MovieDto dto)
        {
            // To Make sure that the extensions of the images is .png or .jpg
            if (!_allowedExtensions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("Only .jpg or .png images Extensions are allowed!");

            if (dto.Poster.Length > _maxAllowedPosterSize)
                return BadRequest("Maximum Allowed Length For the poster is 1 Mb!");

            var isValidGenre = await context.Genres.AnyAsync(g => g.Id == dto.GenreId);
            
            if (!isValidGenre) {
                return BadRequest($"This Genre id is not valid {dto.GenreId}");    
            }

            if (dto == null)
            {
                return BadRequest(ModelState);
            }

            if (ModelState.IsValid)
            {
                // Convert IFormFile into Bytes
                using var dataStream = new MemoryStream();
                await dto.Poster.CopyToAsync(dataStream);
               
                Movie movie = new()
                {
                    Title = dto.Title,
                    Year = dto.Year,
                    Rate = dto.Rate,
                    StoryLine = dto.StoryLine,
                    GenreId = dto.GenreId,
                    // Convert IFormFile to array of bytes
                    Poster = dataStream.ToArray(),
                };
                await context.Movies.AddAsync(movie);
                context.SaveChanges();
                return Ok(movie);
            }
            return BadRequest(ModelState);
        }

        [HttpGet]   
        public async Task<IActionResult> GetAllAsync()
        {
            var movies = await context.Movies
                .OrderByDescending(m => m.Rate)
                .Include(m => m.Genre)
                .Select(m => new MovieDetailsDto {
                    Id = m.Id,
                    Year = m.Year,
                    Rate = m.Rate,
                    StoryLine = m.StoryLine,
                    GenreId = m.GenreId,
                    Poster = m.Poster,
                    GenreName = m.Genre.Name
                })
                .ToListAsync();

            return Ok(movies);
        }

    }
}
