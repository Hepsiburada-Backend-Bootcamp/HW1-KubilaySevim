using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MovieList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private static List<Movie> _movieList = new List<Movie>()
        {
            new Movie
            {
                Id = 1, Title = "The Dark Knight", Genre = "action", ReleaseDate = 2008, Director = "Christopher Nolan",
            },
            new Movie
            {
                Id = 2, Title = "Casablanca", Genre = "romance", ReleaseDate = 1942, Director = "Michael Curtiz",
            },
            new Movie
            {
                Id = 3, Title = "Alien", Genre = "horror", ReleaseDate = 1979, Director = "Ridley Scott",
            },
            new Movie
            {
                Id = 4, Title = "The GodFather, Part II", Genre = "drama", ReleaseDate = 1974, Director = "Francis Ford Coppola",
            },
            new Movie
            {
                Id = 5, Title = "Toy Story 3", Genre = "comedy", ReleaseDate = 2010, Director = "Lee Unkrich",
            },
            new Movie
            {
                Id = 6, Title = "Toy Story 2", Genre = "comedy", ReleaseDate = 1999, Director = "John Lasseter",
            },
            new Movie
            {
                Id = 7, Title = "Schindler's List", Genre = "drama", ReleaseDate = 1993, Director = "Steven Spielberg",
            },
        };

        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            return await Task.FromResult(Ok(_movieList));
        }

        [HttpGet("get/idFromQuery/")]
        public IActionResult Get([FromQuery] int id)
        {
            var movie = _movieList.FirstOrDefault(x => x.Id == id);
            if (movie != null)
            {
                return Ok(movie);
            }

            return NotFound(id);
        }

        [HttpGet("movies/")]
        public IActionResult GetSortByFieldAsc([FromQuery] string sortField, string selectAscOrDesc)
        {
            var movieList = _movieList;

            if (selectAscOrDesc.ToLower().Equals("asc"))
            {
                SortByFieldAsc(sortField, ref movieList);
            }
            else if (selectAscOrDesc.ToLower().Equals("desc"))
            {
                SortByFieldDesc(sortField, ref movieList);
            }
            else
            {
                return BadRequest("invalid argument");
            }
            return Ok(movieList);
        }


        [HttpPost]
        public IActionResult Add([FromBody] Movie movieDto)
        {
            if (movieDto == null)
            {
                return BadRequest();
            } 
            movieDto.Id = _movieList.Max(x => x.Id) + 1;
            _movieList.Add(movieDto);
            
            return Created($"api/movies/{movieDto.Title}", movieDto);
        }
        
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Movie movieDto)
        {
            var movie = _movieList.FirstOrDefault(x => x.Id == id);
            if (movie != null)
            {
                movie.Title = movieDto.Title;
                movie.Director = movieDto.Director;
                movie.ReleaseDate = movieDto.ReleaseDate;
                movie.Genre = movieDto.Genre;
                return Ok(movie);
            }
            return BadRequest();
        }
        
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] Movie movieDto)
        {
            var movie = _movieList.FirstOrDefault(x => x.Id == id);
            if (movie != null)
            {
                movie.Title = movieDto.Title != default ? movieDto.Title : movie.Title;
                movie.Director = movieDto.Director != default ? movieDto.Director : movie.Director;
                movie.ReleaseDate = movieDto.ReleaseDate != default ? movieDto.ReleaseDate : movie.ReleaseDate;
                movie.Genre = movieDto.Genre != default ? movieDto.Genre : movie.Genre;
                return Ok(movie);
            }

            return BadRequest();
        }

        [HttpDelete("id")]
        public IActionResult Delete(int id)
        {
            var movie = _movieList.FirstOrDefault(x => x.Id == id);

            if (movie != null)
            {
                _movieList.Remove(movie);
                return Ok();
            }
            return NotFound();
        }
        
        private void SortByFieldAsc(string sortField, ref List<Movie> movieList)
        {
            switch (sortField.ToLower())
            {
                case "id":
                    movieList = _movieList.OrderBy(x => x.Id).ToList<Movie>();
                    break;
                case "title":
                    movieList = _movieList.OrderBy(x => x.Title).ToList<Movie>();
                    break;
                case "director":
                    movieList = _movieList.OrderBy(x => x.Director).ToList<Movie>();
                    break;
                case "releaseDate":
                    movieList = _movieList.OrderBy(x => x.ReleaseDate).ToList<Movie>();
                    break;
                case "genre":
                    movieList = _movieList.OrderBy(x => x.Genre).ToList<Movie>();
                    break;
            }
        }

        private void SortByFieldDesc(string sortField, ref List<Movie> movieList)
        {
            switch (sortField.ToLower())
            {
                case "id":
                    movieList = _movieList.OrderByDescending(x => x.Id).ToList<Movie>();
                    break;
                case "title":
                    movieList = _movieList.OrderByDescending(x => x.Title).ToList<Movie>();
                    break;
                case "director":
                    movieList = _movieList.OrderByDescending(x => x.Director).ToList<Movie>();
                    break;
                case "releaseDate":
                    movieList = _movieList.OrderByDescending(x => x.ReleaseDate).ToList<Movie>();
                    break;
                case "genre":
                    movieList = _movieList.OrderByDescending(x => x.Genre).ToList<Movie>();
                    break;

            }
        }
    }
}