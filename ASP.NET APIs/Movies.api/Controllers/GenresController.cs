using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.api.Services;

namespace Movies.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenresService genresService;

        public GenresController(IGenresService genresService)
        {
            this.genresService = genresService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await genresService.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] GenreDto genreDto)
        {

            if (ModelState.IsValid)
            {
                var genre = new Genre() { Name = genreDto.Name };
                
                genresService.Add(genre);
                
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
                var genre = await genresService.GetById(id);
                if (genre == null)
                {
                    return NotFound($"No Genre Was Found With ID {id}");
                }
                genresService.Update(genre);
                
                return Ok(genre);

            }

            return BadRequest(ModelState);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]byte id)
        {
            var genre = await genresService.GetById(id);
            if (genre == null)
            {
                return NotFound($"No Genre Found With ID : {id}");
            }

            genresService.Delete(genre);

            return Ok("This Genre has been Deleted Succussfully");
        }
    }
}
