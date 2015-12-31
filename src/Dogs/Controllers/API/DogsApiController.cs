using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Dogs.Models;

namespace Dogs.Controllers
{
    [Produces("application/json")]
    [Route("api/DogsApi")]
    public class DogsApiController : Controller
    {
        private MyDBContext _context;

        public DogsApiController(MyDBContext context)
        {
            _context = context;
        }

        // GET: api/DogsApi
        
        [HttpGet("{id}")]
        public IEnumerable<Dog> GetDogs(string id)
        {
            return _context.Dogs.Where(x => x.UserId == id);
        }

        // GET: api/DogsApi/5
        

        // PUT: api/DogsApi/5
        [HttpPut("{id}")]
        public IActionResult PutDog(int id, [FromBody] Dog dog)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            if (id != dog.Id)
            {
                return HttpBadRequest();
            }

            _context.Entry(dog).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DogExists(id))
                {
                    return HttpNotFound();
                }
                else
                {
                    throw;
                }
            }

            return new HttpStatusCodeResult(StatusCodes.Status204NoContent);
        }

        // POST: api/DogsApi
        [HttpPost]
        public IActionResult PostDog([FromBody] Dog dog)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            _context.Dogs.Add(dog);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DogExists(dog.Id))
                {
                    return new HttpStatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return  new CreatedResult("", dog);
        }

        // DELETE: api/DogsApi/5
        [HttpDelete("{id}")]
        public IActionResult DeleteDog(int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            Dog dog = _context.Dogs.Single(m => m.Id == id);
            if (dog == null)
            {
                return HttpNotFound();
            }

            _context.Dogs.Remove(dog);
            _context.SaveChanges();

            return Ok(dog);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DogExists(int id)
        {
            return _context.Dogs.Count(e => e.Id == id) > 0;
        }
    }
}