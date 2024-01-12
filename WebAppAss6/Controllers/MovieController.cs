using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppAss6.Model;

namespace WebAppAss6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {  
        private static List<Movie> _movies = new List<Movie>
        {
        new Movie { Id = 1, Title = "Inception", Genre = "Sci-Fi", Year = 2010 },
        new Movie { Id = 2, Title = "The Shawshank Redemption", Genre = "Drama", Year = 1994 }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Movie>> GetMovies()
        {
            return Ok(_movies);
        }

        [HttpGet("{id}")]
        public ActionResult<Movie> GetMovieById(int id)
        {
            var movie = _movies.Find(m => m.Id == id);
            if (movie == null)
                return NotFound();

            return Ok(movie);
        }

        [HttpPost]
        public ActionResult<Movie> CreateMovie(Movie movie)
        {
            movie.Id = _movies.Count + 1;
            _movies.Add(movie);
            return CreatedAtAction(nameof(GetMovieById), new { id = movie.Id }, movie);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMovie(int id, Movie updatedMovie)
        {
            var movie = _movies.Find(m => m.Id == id);
            if (movie == null)
                return NotFound();

            movie.Title = updatedMovie.Title;
            movie.Genre = updatedMovie.Genre;
            movie.Year = updatedMovie.Year;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            var movie = _movies.Find(m => m.Id == id);
            if (movie == null)
                return NotFound();

            _movies.Remove(movie);
            return NoContent();
        }
    }
}

