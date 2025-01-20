using LibraryStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryStore.Controllers
{
    [Route("api/Get[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        //Create an instance from the database
        private readonly LibraryDbContext _db;

        public AuthorsController(LibraryDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult List()
        {
            var data = from a in _db.Authors
                       join b in _db.Books on a.Id equals b.AuthorId
                       group b by a.Name into g
                       select new
                       {
                           AuthorName = g.Key,
                           AuthorsCount = g.Count(),
                       };

            return Ok(data);
        }


    }
}
