using Gioia.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Gioia.Controllers
{
    public class PostsController : Controller
    {
        // GET: Post
        public ActionResult Index()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage Msg = client.GetAsync("http://192.168.43.205:3050/api/Posts").Result;
            if (Msg.IsSuccessStatusCode)
            {
                var response = Msg.Content.ReadAsStringAsync().Result;
                var posts = JsonConvert.DeserializeObject<List<Posts>>(response);
                return View(posts);
                // posts.Select
            }

            return HttpNotFound();


        }
        // GET: Post/Create
        public ActionResult Create()

        {
            return View();
        }

        // POST: Post/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Create(string post)
        {
            if (ModelState.IsValid)
            {
                Posts NewPost = new Posts()
                {
                    post = post,

                    time = DateTime.Now,
                    userId = Session["Member"].ToString(),
                    moodId = 1
                };
                HttpClient client = new HttpClient();
                var json = JsonConvert.SerializeObject(NewPost);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var result = await client.PostAsync("http://192.168.43.205:3050/api/Posts", content);
                return RedirectToAction("Index");
            }

            return View(post);
        }
        public ActionResult Like()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage Msg = client.GetAsync("http://192.168.43.205:3050/api/PostLike").Result;
            if (Msg.IsSuccessStatusCode)
            {
                var response = Msg.Content.ReadAsStringAsync().Result;
                var posts = JsonConvert.DeserializeObject<List<PostLike>>(response).Count;
                return View(posts);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]

        public async System.Threading.Tasks.Task<ActionResult> AddLike(int id)
        {
            if (ModelState.IsValid)
            {
                PostLike NewPost = new PostLike()
                {
                    userId = Session["Member"].ToString(),
                    like = true,
                    postId = id

                };
                HttpClient client = new HttpClient();
                var json = JsonConvert.SerializeObject(NewPost);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var result = await client.PostAsync("http://192.168.43.205:3050/api/PostLike", content);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", id);

            //return View(postLike);
        }
        public ActionResult Comment()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage Msg = client.GetAsync("http://192.168.43.205:3050/api/PostComment").Result;
            if (Msg.IsSuccessStatusCode)
            {
                var response = Msg.Content.ReadAsStringAsync().Result;
                var comment = JsonConvert.DeserializeObject<List<PostComment>>(response);
                return View(comment);
            }
            return HttpNotFound();


        }
        public ActionResult AddComment()

        {
            return View();
        }
        [HttpPost]

        public async System.Threading.Tasks.Task<ActionResult> AddComment(int id, string comment)
        {
            if (ModelState.IsValid && comment != null)
            {
                PostComment NewPostComment = new PostComment()
                {
                    userId = Session["Member"].ToString(),
                    postId = id,
                    comment = comment,
                };
                HttpClient client = new HttpClient();
                var json = JsonConvert.SerializeObject(NewPostComment);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var result = await client.PostAsync("http://192.168.43.205:3050/api/PostComment", content);
                return RedirectToAction("Index");
            }
            return View(comment);
        }
        public ActionResult EditComment(PostComment comm)
        {
            return View(comm);
        }
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> EditComment(int commentId, string comment)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage Msg = client.GetAsync("http://192.168.43.205:3050/api/PostComment?Commentid=" + commentId).Result;
            var response = Msg.Content.ReadAsStringAsync().Result;
            var Comment = JsonConvert.DeserializeObject<PostComment>(response);
            //htst5dmeh f eh b3d keda 
            PostComment NewComment = new PostComment()
            {
                userId = Session["Member"].ToString(),
                postId = Comment.postId,
                comment = comment,
                id = commentId
            };
            var json = JsonConvert.SerializeObject(NewComment);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await client.PutAsync("http://192.168.43.205:3050/api/PostComment/" + commentId, content);
            if (result.IsSuccessStatusCode)
            {
                if (Comment.userId == Session["Member"].ToString())
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                }
            }
            return View(NewComment);
        }
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> DeleteComment(int id)
        {
            HttpClient client = new HttpClient();
            var result = await client.DeleteAsync("http://192.168.43.205:3050/api/PostComment/" + id);
            if (result.IsSuccessStatusCode)
            {
                //ht3mle eh b3d keda
                return RedirectToAction("Index");
            }

            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }
        public ActionResult EditPost(Posts posT)
        {
            return View(posT);
        }
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> EditPost(int postId, string post)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage Msg = client.GetAsync("http://192.168.43.10:3050/api/Posts?Postid=" + postId).Result;
            var response = Msg.Content.ReadAsStringAsync().Result;
            var poost = JsonConvert.DeserializeObject<Posts>(response);
            Posts NewPost = new Posts()
            {
                userId = Session["Member"].ToString(),
                postId = postId,
                post = post,
                moodId = poost.moodId,
                //PostComments=poost.PostComments,
                //PostLike=poost.PostLike,
                time = poost.time
            };
            var json = JsonConvert.SerializeObject(NewPost);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await client.PutAsync("http://192.168.43.205:3050/api/Posts/" + postId, content);
            if (result.IsSuccessStatusCode)
            {
                if (poost.userId == Session["Member"].ToString())
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            return View(NewPost);
        }
        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> Deletepost(int id)
        {
            HttpClient client = new HttpClient();
            var result = await client.DeleteAsync("http://192.168.43.205:3050/api/Posts/" + id);
            if (result.IsSuccessStatusCode)
            {
                //ht3mle eh b3d keda
                return RedirectToAction("Index");
            }

            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }



    }
}