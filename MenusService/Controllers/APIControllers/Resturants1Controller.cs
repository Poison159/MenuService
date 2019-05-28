using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MenusService.Models;

namespace MenusService.Controllers
{
    public class Resturants1Controller : ApiController
    {
        private MenusServiceContext db = new MenusServiceContext();

        // GET: api/Resturants1
        public IQueryable<Resturant> GetResturants()
        {
            return db.Resturants;
        }

        // GET: api/Resturants1/5
        [ResponseType(typeof(Resturant))]
        public Resturant GetResturant(int id)
        {
            Resturant resturant = db.Resturants.Find(id);
            resturant.meals = db.Meals.Where(x => x.resturantId == resturant.id).ToList();
            if (resturant == null)
            {
                return null;
            }

            return resturant;
        }

        // PUT: api/Resturants1/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutResturant(int id, Resturant resturant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != resturant.id)
            {
                return BadRequest();
            }

            db.Entry(resturant).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResturantExists(id))
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

        // POST: api/Resturants1
        [ResponseType(typeof(Resturant))]
        public IHttpActionResult PostResturant(Resturant resturant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Resturants.Add(resturant);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = resturant.id }, resturant);
        }

        // DELETE: api/Resturants1/5
        [ResponseType(typeof(Resturant))]
        public IHttpActionResult DeleteResturant(int id)
        {
            Resturant resturant = db.Resturants.Find(id);
            if (resturant == null)
            {
                return NotFound();
            }

            db.Resturants.Remove(resturant);
            db.SaveChanges();

            return Ok(resturant);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ResturantExists(int id)
        {
            return db.Resturants.Count(e => e.id == id) > 0;
        }
    }
}