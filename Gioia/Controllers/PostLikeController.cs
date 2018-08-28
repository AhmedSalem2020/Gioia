using Gioia.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Gioia.Controllers
{
    public class PostLikeController : Controller
    {
        // GET: PostLike
        //public ActionResult Index()
        //{
        //    HttpClient client = new HttpClient();
        //    HttpResponseMessage Msg = client.GetAsync("http://192.168.43.10:3050/api/PostLike").Result;
        //    if (Msg.IsSuccessStatusCode)
        //    {
        //        var response = Msg.Content.ReadAsStringAsync().Result;
        //        var posts = JsonConvert.DeserializeObject<List<PostLike>>(response);
        //        return View(posts);
        //    }
        //    return RedirectToAction("");
        //}
        //public ActionResult Like()
        //{


        //}
    }
}