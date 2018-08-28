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
using GioiaApi.Models;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.IO;



namespace GioiaApi.Controllers
{
    public class ApplicationUsersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ApplicationUsers
        public IQueryable<ApplicationUser> GetApplicationUsers()
        {
            return db.Users;
            //return db.Users.Select(a=>new {a.Fname,a.Lname, a.BirthDate ,a.PasswordHash ,a.PhoneNumber,a.UserName,a.Email});
        }

        // GET: api/ApplicationUsers/5
        [ResponseType(typeof(ApplicationUser))]
        public IHttpActionResult GetApplicationUser(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return Ok(applicationUser);
        }

        // GET: api/ApplicationUsers/5
        //[ResponseType(typeof(ApplicationUser))]
        public IHttpActionResult GetUserMood(string userId)
        {
            ApplicationUser applicationUser = db.Users.Find(userId);

            //var query = db.UserMoods.Where(a => a.userId == userId).Select(a => a.Date.Year);

            var happy = db.UserMoods.Where(a => a.userId == userId && a.moodId == 1).Count();
            var sad = db.UserMoods.Where(a => a.userId == userId && a.moodId == 2).Count();
            var netural = db.UserMoods.Where(a => a.userId == userId && a.moodId == 3).Count();
            var angery = db.UserMoods.Where(a => a.userId == userId && a.moodId == 4).Count();

            var sum = happy + sad + netural + angery;

            double happypersent = (double)happy / (double)sum * 100;
            double sadpersent = (double)sad / (double)sum * 100;
            double neturalpersent = (double)netural / (double)sum * 100;
            double angerypersent = (double)angery / (double)sum * 100;

            if (applicationUser == null)
            {
                return NotFound();
            }

            return Ok(new
            {
              applicationUser =applicationUser,
              happypersent =happypersent,
              sadpersent =sadpersent,
              neturalpersent =neturalpersent,
              angerypersent =angerypersent
            });
        }

        public IHttpActionResult GetApplicationUserByName(string name)
        {
            List<ApplicationUser> applicationUser = db.Users.Where(a=>a.Fname.Contains(name)||a.Lname.Contains(name)).ToList();
            if (applicationUser == null)
            {
                return NotFound();
            }

            return Ok(applicationUser);
        }

        // PUT: api/ApplicationUsers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutApplicationUser(string id, ApplicationUser applicationUser)
        {
            //string usId = User.Identity.GetUserId();
            var user = db.Users.SingleOrDefault(a => a.Email == applicationUser.Email && a.Id != applicationUser.Id);
            if (user != null)
            {
                return BadRequest("this Email already exist");
            }
            user = db.Users.SingleOrDefault(a => a.UserName == applicationUser.UserName && a.Id != applicationUser.Id);
            if (user != null)
            {
                return BadRequest("this username already exist");
            }
            ApplicationUser OldUser = db.Users.FirstOrDefault(a => a.Id == id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != applicationUser.Id)
            {
                return BadRequest();
            }

            OldUser.Fname = applicationUser.Fname;
            OldUser.Lname = applicationUser.Lname;
            OldUser.Email = applicationUser.Email;
            OldUser.UserName = applicationUser.UserName;
            OldUser.PhoneNumber = applicationUser.PhoneNumber;
            OldUser.BirthDate = applicationUser.BirthDate;
            if (applicationUser.Image != null)
                OldUser.Image = applicationUser.Image;
            

            //db.Entry(applicationUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(OldUser);
        }

        // POST: api/ApplicationUsers
        [ResponseType(typeof(ApplicationUser))]
        public IHttpActionResult PostApplicationUser(ApplicationUser applicationUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(applicationUser);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ApplicationUserExists(applicationUser.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = applicationUser.Id }, applicationUser);
        }

        // DELETE: api/ApplicationUsers/5
        [ResponseType(typeof(ApplicationUser))]
        public IHttpActionResult DeleteApplicationUser(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            db.Users.Remove(applicationUser);
            db.SaveChanges();

            return Ok(applicationUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ApplicationUserExists(string id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}