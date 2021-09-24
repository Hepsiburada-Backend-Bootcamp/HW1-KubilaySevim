using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private static List<Book> _bookList = new List<Book>()
        {
            new Book {Id = 1, Title = "Suç ve Ceza", Author = "Fyodor Dostoyevski", GenreId = 1, PageCount = 600, PublishDate = 1866},
            new Book {Id = 2, Title = "Cesur Yeni Dünya", Author = "Aldous Huxley", GenreId = 2, PageCount = 300, PublishDate = 1866},
            new Book {Id = 3, Title = "İstanbul Hatırası", Author = "Ahmet Ümit", GenreId = 3, PageCount = 300, PublishDate = 1932},
            new Book {Id = 4, Title = "Yeraltından Notlar", Author = "Fyodor Dostoyevski", GenreId = 1, PageCount = 200, PublishDate = 1864},
            new Book {Id = 5, Title = "Tutunamayanlar", Author = "Oğuz Atay", GenreId = 4, PageCount = 400, PublishDate = 1972},
        };

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            return await Task.FromResult(Ok(_bookList));
        }

        [HttpGet("sortBy/author")]
        public IActionResult GetSortByAuthorName()
        {
            var bookList = _bookList.OrderBy(x => x.Author).ToList<Book>();
            return Ok(bookList);
        }

        [HttpGet("id/fromQuery")]
        public IActionResult GetBook([FromQuery] int id)
        {
            var book = _bookList.FirstOrDefault(x => x.Id == id);

            if (book != null)
            {
                return Ok(book);
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Add([FromBody] Book book)
        {
            if (_bookList.Exists(x => x.Id == book.Id))
            {
                return BadRequest();
            }
            
            _bookList.Add(book);
            return Created($"api/books/{book.Title}", book);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Book bookDto)
        {
            var book = _bookList.FirstOrDefault(x => x.Id == id);
            if (book != null)
            {
                book.Author = bookDto.Author;
                book.Title = bookDto.Title;
                book.GenreId = bookDto.GenreId;
                book.PageCount = bookDto.PageCount;
                book.PublishDate = bookDto.PublishDate;
                return Ok(book);
            }

            return BadRequest();
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] Book bookDto)
        {
            var book = _bookList.FirstOrDefault(x => x.Id == id);
            if (book != null)
            {
                UpdateBook(bookDto, book);
                return Ok(book);
            }

            return BadRequest();
        }



        [HttpDelete("id")]
        public IActionResult Delete(int id)
        {
            var book = _bookList.FirstOrDefault(x => x.Id == id);
            
            if (book != null)
            {
                //Delete 
                _bookList.Remove(book);
                return Ok();
            }
            return NotFound();
        }
        
        
        private static void UpdateBook(Book bookDto, Book? book)
        {
            book.Title = bookDto.Title != default ? bookDto.Title : book.Title;
            book.Author = bookDto.Author != default ? bookDto.Author : book.Author;
            book.GenreId = bookDto.GenreId != default ? bookDto.GenreId : book.GenreId;
            book.PageCount = bookDto.PageCount != default ? bookDto.PageCount : book.PageCount;
            book.PublishDate = bookDto.PublishDate != default ? bookDto.PublishDate : book.PublishDate;
        }
    }
}



