using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryPattern.AppCode;
using RepositoryPattern.Models;
using RepositoryPattern.Repository;
using Microsoft.AspNetCore.JsonPatch;

namespace RepositoryPattern.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController: ControllerBase
    {
        private IBookRepository _bookrepository;
        
        public BookController(IBookRepository bookrepository)
        {
            _bookrepository = bookrepository;
        }

        [HttpPost]
        public async Task<IActionResult> addNewBook(BookModel objBookModel)
        {
            if (objBookModel == null)
                return BadRequest();

            var result = await _bookrepository.addNewBook(objBookModel);
            if (result == -1)
                return StatusCode(500, "Something went wrong while processing the request");

            return CreatedAtAction(nameof(getSingleBook), new { id = objBookModel.Id }, objBookModel);
        }

        [HttpGet("deleteNewBook/{id}")]
        public async Task<IActionResult> deleteNewBook(int id)
        {
            var result = await _bookrepository.deleteNewBook(id);
            if (result == -1)
                return NoContent();

            return Ok(new {response = "Deleted book successfully !!"});
        }

        [HttpGet]
        public async Task<IActionResult> getAllBooks()
        {
            var result = await _bookrepository.getAllBooks();
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpGet("getSingleBook/{id}")]
        public async Task<IActionResult> getSingleBook(int id)
        { 
            var result = await _bookrepository.getSingleBook(id);
            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpPatch("updateNewBook/{id}")]
        public async Task<IActionResult> updateNewBook([FromRoute]int id, [FromBody]JsonPatchDocument bookModel)
        {
            if (bookModel == null)
                return BadRequest();

            var book = await _bookrepository.getSingleBook(id);
            if (book == null)
                return NotFound();

            bookModel.ApplyTo(book);

            var result = await _bookrepository.updateNewBook(book);
            if (result == -1)
                return StatusCode(500, "Something went wrong while processing the request");

            return Ok(new { response = "Book updated successfully!" });
        }

    }
}
