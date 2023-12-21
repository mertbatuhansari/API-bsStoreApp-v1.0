using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using Repositories.EFCore;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IRepositoryManager _manager;

        public BooksController(IRepositoryManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult GetAllBooks() 
        {
            try
            {
                var books = _manager.book.GetAllBooks(false);
                return Ok(books);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpGet("{id:int}")]
        public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
        {
            try
            {
                var book = _manager.book.GetOneBooksByID(id,false);
                if (book is null) { return NotFound(); }
                return Ok(book);
            }
            catch (Exception ex)
            {               
                throw new Exception(ex.Message);
            }
        }

        [HttpPost] 
        public IActionResult CreateOneBook([FromBody] Book book)
        {
            try
            {
                if (book is null)
                { return BadRequest(); }
                _manager.book.CreateOneBook(book);
                return StatusCode(201, book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] Book book)
        {
            try
            {
                var entity = _manager.book.GetOneBooksByID(id, true);
               
                if (entity is null)
                { return NotFound(); }

                if (id != book.Id)
                {
                    return BadRequest();
                }

                entity.Title= book.Title;
                entity.Price= book.Price;
                _manager.Save();
                return Ok(book);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
        {
            try
            {
                var entity = _manager.book.GetOneBooksByID(id, false);
                if (entity is null)
                {
                    return NotFound(new
                    {
                        statusCode = 404,
                        message = $"Book with id:{id} could not found"
                    });
                }

                _manager.book.DeleteOneBook(entity);
                _manager.Save();
                return NoContent();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id,[FromBody] JsonPatchDocument<Book> book)
        {
            try
            {
                if (book is null) return BadRequest();

                    var entity = _manager.book.GetOneBooksByID(id, true);

                if (entity is null)  return NotFound();

                book.ApplyTo(entity);
                _manager.book.Update(entity);
                return NoContent();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
