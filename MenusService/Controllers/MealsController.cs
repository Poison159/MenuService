using MenusService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MenusService.Controllers
{
    public class MealsController : ApiController
    {
        MenuContext db = new MenuContext();
        // GET: api/Meals
        public IEnumerable<Meal> Get()
        {
            return db.Meals;
        }

        // GET: api/Meals/5
        public string Get(int id)
        {
            return db.Resturants.First(x => x.id == id).guid;
        }

        // POST: api/Meals
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Meals/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Meals/5
        public void Delete(int id)
        {
        }
    }
}
