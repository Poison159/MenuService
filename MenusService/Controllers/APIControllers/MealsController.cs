﻿using System;
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
using System.Data.SqlClient;
using System.Configuration;

namespace MenusService.Controllers.APIControllers
{
    public class MealsController : ApiController
    {
        private MenusServiceContext db = new MenusServiceContext();
       

        // GET: api/Meals
        public IQueryable<Meal> GetMeals()
        {
            return db.Meals;
        }

        // GET: api/Meals/5
        [ResponseType(typeof(Meal))]
        public object GetMeal(int id)
        {
            var meals = db.Meals.Where(x => x.resturantId == id).ToList();
            return db.Resturants.First(x => x.id == id);
        }

        // PUT: api/Meals/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMeal(int id, Meal meal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != meal.id)
            {
                return BadRequest();
            }

            db.Entry(meal).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MealExists(id))
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

        // POST: api/Meals
        [ResponseType(typeof(Meal))]
        public IHttpActionResult PostMeal(Meal meal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Meals.Add(meal);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = meal.id }, meal);
        }

        // DELETE: api/Meals/5
        [ResponseType(typeof(Meal))]
        public IHttpActionResult DeleteMeal(int id)
        {
            Meal meal = db.Meals.Find(id);
            if (meal == null)
            {
                return NotFound();
            }

            db.Meals.Remove(meal);
            db.SaveChanges();

            return Ok(meal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MealExists(int id)
        {
            return db.Meals.Count(e => e.id == id) > 0;
        }
    }
}