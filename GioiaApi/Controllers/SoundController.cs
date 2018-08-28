using GioiaApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GioiaApi.Controllers
{
    public class SoundController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public string Get(int id)
        {
            return db.Sounds.Where(n => n.moodId == id).Select(n => n.MusicLink).OrderBy(r => Guid.NewGuid()).First();

        }
    }
}
