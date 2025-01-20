using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using LibraryStore.Models;
using LibraryStore.Services.Interfaces;
using LibraryStore.DTOs;

namespace LibraryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _bookService;

        public BooksController(IBooksService booksService)
        {
            _bookService = booksService;
        }
        //In the controller, we try to keep things as minimal as possible. For this reason, we will use the services. We will create an interface to avoid redundancy of classes.

        //Controller to get all books
        [HttpGet]
        public async Task<ActionResult> IndexAsync(int pageNum = 1)
        {
            var data = await _bookService.GetAllAsync(pageNum);
            return Ok(data);
        }

        //Controller to get a specific books
        [HttpGet("{Id}")]
        public async Task<ActionResult> GetBookById(int Id)
        {
            var data = await _bookService.GetBookById(Id);
            return Ok(data);
        }

        //Controller to add a new book
        [HttpPost]
        public async Task<ActionResult> CreateBook(BooksFormDTOs NewBook)
        {
            var data = await _bookService.CreateAsync(NewBook);
            return Ok(data);
        }

        //Controller to update a book
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook(int id, BooksFormDTOs dto)
        {
            var data = await _bookService.UpdateAsync(id, dto);
            return Ok(data);
        }

        //Controller to delete a book
        [HttpDelete]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var data = await _bookService.DeleteAsync(id);
            return Ok(data);
        }

        //Controller to get all books by authorId
        [HttpGet("AuthorId")]
        public async Task<ActionResult> GetBooksByAuthor(int pageNumber, int AuthorId)
        {
            var data = await _bookService.GetBooksByAuthorIdAsync(pageNumber, AuthorId);

            return Ok(data);
        }
    }
}
