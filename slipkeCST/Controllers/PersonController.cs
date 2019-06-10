using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using slipkeCST.Models;

namespace slipkeCST.Controllers
{
    [RoutePrefix("api/person")]
    public class PersonController : ApiController
    {
        private IPersonContext db = new PersonContext();

        public PersonController() { }

        public PersonController(IPersonContext context)
        {
            db = context;
        }

        // GET: api/person
        [Route("")]
        public IQueryable<Person> GetPeople()
        {
            return db.People;
        }

        // GET: api/person/5
        [Route("{id:int}", Name = "GetPerson")]
        [ResponseType(typeof(Person))]
        public async Task<IHttpActionResult> GetPerson(int id)
        {
            Person person = await db.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        // PUT: api/person/5
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPerson(int id, Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != person.Id)
            {
                return BadRequest();
            }

            //db.Entry(person).State = EntityState.Modified;
            db.MarkAsModified(person);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/person
        [Route("")]
        [ResponseType(typeof(Person))]
        public async Task<IHttpActionResult> PostPerson(Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.People.Add(person);
            await db.SaveChangesAsync();
           
            return CreatedAtRoute("GetPerson", new { id = person.Id }, person);
        }

        // DELETE: api/person/5
        [Route("{id:int}")]
        [ResponseType(typeof(Person))]
        public async Task<IHttpActionResult> DeletePerson(int id)
        {
            Person person = await db.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            db.People.Remove(person);
            await db.SaveChangesAsync();

            return Ok(person);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonExists(int id)
        {
            return db.People.Count(e => e.Id == id) > 0;
        }

        // GET: api/person/search?firstName=Michael&lastName=Slipke&address=123FakeStreet
        [Route("search")]
        public List<Person> GetPersonByFirstNameLastNameAddress(string firstName = null, string lastName = null, string address = null)
        {
            var people = from p in db.People
                         select p;

            if (!String.IsNullOrEmpty(firstName))
                people = people.Where(p => p.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase));
            if (!String.IsNullOrEmpty(lastName))
                people = people.Where(p => p.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));
            if (!String.IsNullOrEmpty(address))
                people = people.Where(p => p.Address.Equals(address, StringComparison.OrdinalIgnoreCase));

            return people.ToList();
        }
    }
}