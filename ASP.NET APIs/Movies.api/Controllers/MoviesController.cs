using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.api.Services;

namespace Movies.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly List<string> _allowedExtensions = new List<string>
        {
            ".jpg" , ".png"
        };
        private readonly long _maxAllowedPosterSize = 1024 * 1024;
        private readonly IMovieService _movieService;
        private readonly IGenresService _genresService;
        private readonly IMapper _autoMapper;

        public MoviesController(IMovieService movieService , IGenresService genresService , IMapper autoMapper)
        {
            _movieService = movieService;
            _genresService = genresService;
            _autoMapper = autoMapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var movies = await _movieService.GetAll();
            // TODO: map movies to Dto

            var data = _autoMapper.Map<IEnumerable<MovieDetailsDto>>(movies);

            return Ok(data);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var movie = await _movieService.GetById(id);

            if (movie == null)
            {
                return NotFound();
            }
            var dto = _autoMapper.Map<MovieDetailsDto>(movie);

            return Ok(dto);
        }

        [HttpGet("GetByGenreId")]
        public async Task<IActionResult> GetByGenreIdAsync(byte genreId)
        {
            var movies = await _movieService.GetAll(genreId);

            if (movies == null)
            {
                return NotFound();
            }

            var data = _autoMapper.Map<IEnumerable<MovieDetailsDto>>(movies);


            return Ok(data);
        }
        [HttpPost]
        //                              Required To get Image from User
        public async Task<IActionResult> CreateAsync([FromForm] MovieDto dto)
        {
            // To Make sure that the extensions of the images is .png or .jpg
            if (!_allowedExtensions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("Only .jpg or .png images Extensions are allowed!");

            if (dto.Poster.Length > _maxAllowedPosterSize)
                return BadRequest("Maximum Allowed Length For the poster is 1 Mb!");

            var isValidGenre = await _genresService.IsValidGenre(dto.GenreId);


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

                var movie = _autoMapper.Map<Movie>(dto);
                movie.Poster = dataStream.ToArray();

                _movieService.Add(movie);
                
                return Ok(movie);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromForm] MovieDto newMovie)
        {
            var movie = await _movieService.GetById(id);
            
            if (movie == null)
            {
                return NotFound($"No Movies Found with thid ID {id}");
            }

            var isValidGenre = await _genresService.IsValidGenre(newMovie.GenreId); 
            if (!isValidGenre)
            {
                return BadRequest($"This Genre id is not valid {newMovie.GenreId}");
            }

            if (newMovie.Poster != null)
            {
              // To Make sure that the extensions of the images is .png or .jpg
            if (!_allowedExtensions.Contains(Path.GetExtension(newMovie.Poster.FileName).ToLower()))
                return BadRequest("Only .jpg or .png images Extensions are allowed!");

            if (newMovie.Poster.Length > _maxAllowedPosterSize)
                return BadRequest("Maximum Allowed Length For the poster is 1 Mb!");


                // Convert IFormFile into Bytes
                using var dataStream = new MemoryStream();
                await newMovie.Poster.CopyToAsync(dataStream);

                movie.Poster = dataStream.ToArray();
            }

            movie.Title = newMovie.Title;
            movie.Year = newMovie.Year;
            movie.StoryLine = newMovie.StoryLine;
            movie.Rate = newMovie.Rate;
            movie.GenreId = newMovie.GenreId;

            _movieService.Update(movie); 

            return Ok(movie);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var movie = await _movieService.GetById(id);
            

            if (movie == null)
            {
                return NotFound($"No Movies Found With ID {id}");
            }

            _movieService.Delete(id);

            return Ok(movie.Title);
        }

    }
}
