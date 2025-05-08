using LibraryManagementSystem.Application.DTOS.Books;
using LibraryManagementSystem.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks() => Ok(await _bookService.GetAllAsync());
        [HttpGet("GetBookById/{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            return book is null ? NotFound() : Ok(book);
        }
        [HttpPost("AddBook")]
        public async Task<IActionResult> Create(CreateBookDto input)
        {
            var createdBook=await _bookService.AddAsync(input);
            return CreatedAtAction(nameof(GetBookById),new { id=createdBook.Id },createdBook);
        }
        [HttpPut("UpdateBook")]
        public async Task<IActionResult> Update(UpdateBookDto input)
        {
         
            await _bookService.UpdateAsync(input);
            return NoContent();
        }

        [HttpDelete("DeleteBook/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.DeleteAsync(id);
            return NoContent();
        }
    }
}
