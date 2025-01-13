using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using LibraryStore.Models;

namespace LibraryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibraryDbContext _db;

        public BooksController(LibraryDbContext db)
        {
            _db = db;
        }

        //Controller to get all books

        //Controller to get a specific books

        //Controller to add a new book

        //Controller to update a book

        //Controller to delete a book

        //Controller to get all books by authorId
    }
}
