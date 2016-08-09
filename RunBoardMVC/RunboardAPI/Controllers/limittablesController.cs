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
using RunboardAPI.Models;

namespace RunboardAPI.Controllers
{
    public class limittablesController : ApiController
    {
        private RundashboardDbContext db = new RundashboardDbContext();

        
        // GET: api/limittables
        public IQueryable<limittable> Getlimittables()
        {
            return db.limittables;
        }

        // GET: api/limittables/5
        [ResponseType(typeof(limittable))]
        public IHttpActionResult Getlimittable(string id)
        {
            limittable limittable = db.limittables.Find(id);
            if (limittable == null)
            {
                return NotFound();
            }

            return Ok(limittable);
        }

        // PUT: api/limittables/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putlimittable(string id, limittable limittable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != limittable.Ttype)
            {
                return BadRequest();
            }

            db.Entry(limittable).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!limittableExists(id))
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

        // POST: api/limittables
        [ResponseType(typeof(limittable))]
        public IHttpActionResult Postlimittable(limittable limittable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.limittables.Add(limittable);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (limittableExists(limittable.Ttype))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = limittable.Ttype }, limittable);
        }

        // DELETE: api/limittables/5
        [ResponseType(typeof(limittable))]
        public IHttpActionResult Deletelimittable(string id)
        {
            limittable limittable = db.limittables.Find(id);
            if (limittable == null)
            {
                return NotFound();
            }

            db.limittables.Remove(limittable);
            db.SaveChanges();

            return Ok(limittable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool limittableExists(string id)
        {
            return db.limittables.Count(e => e.Ttype == id) > 0;
        }
    }
}