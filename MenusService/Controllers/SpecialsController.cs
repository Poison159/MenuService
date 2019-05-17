using MenusService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MenusService.Controllers
{
    public class SpecialsController : ApiController
    {
        MenuContext db = new MenuContext();
        // GET: api/Specials
        public IEnumerable<Resturant> Get()
        {
            return db.Resturants;
        }

        // GET: api/Specials/5
        public List<Meal> Get(int id)
        {
            var meals = db.Meals.Where(x => x.resturantId == id).ToList();
            return meals;
        }

        // POST: api/Specials
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Specials/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Specials/5
        public void Delete(int id)
        {
        }
    }
}
