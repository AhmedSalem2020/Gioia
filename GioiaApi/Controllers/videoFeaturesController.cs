using GioiaApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GioiaApi.Controllers
{
    [EnableCors("*", "*", "*")]
    public class videoFeaturesController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public string Get(int id)
        {
            return db.Videos.Where(n => n.moodId == id).Select(n => n.videoLink).OrderBy(r => Guid.NewGuid()).First();

        }
    }
}
