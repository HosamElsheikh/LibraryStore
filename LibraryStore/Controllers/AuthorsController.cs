using LibraryStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryStore.Controllers
{
    [Route("api/[controller]")]
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
            var data = from p in _db.Authors
                       select new
                       {
                           p.Id,
                           p.Name,
                           p.Age
                       };

            return Ok(data);
        }


    }
}
