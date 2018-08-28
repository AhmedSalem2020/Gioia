using Gioia.Models;
using GioiaApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GioiaApi.Controllers
{
    public class UserMoodController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<userMood> Get(string id)
        {
            return db.UserMoods.Where(u => u.userId == id);
        }

        public IHttpActionResult Post(userMood uMood)
        {
            var user = db.Users.Find(uMood.userId);
            if (user == null)
            {
                return NotFound();
            }
            db.UserMoods.Add(uMood);
            db.SaveChanges();
            return Ok(uMood);
        }

        public IHttpActionResult Put(userMood uMood)
        {
            var user = db.Users.Find(uMood.userId);
            if (user == null)
            {
                return NotFound();
            }
            db.UserMoods.Add(uMood);
            db.SaveChanges();
            return Ok(uMood);
        }
    }
}
