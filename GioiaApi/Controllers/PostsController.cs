using Gioia.Models;
using GioiaApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GioiaApi.Controllers
{
    public class PostsController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public IEnumerable<Posts> Get()
        {            
            return db.Posts.OrderByDescending(p => p.time).Take(15).ToList();
        }
        public IHttpActionResult Get(string id)
        {
            var posts= db.Posts.Where(u=>u.userId==id).OrderByDescending(p => p.time).Take(15).ToList();
            if (posts == null)
                return NotFound();
            return Ok(posts);
        }
        public IHttpActionResult Get(int Postid)
        {
            var post = db.Posts.SingleOrDefault(u => u.postId == Postid);
            if (post == null)
                return NotFound();
            return Ok(post);
        }

        public IHttpActionResult Post(Posts post)
        {
            db.Posts.Add(post);
            db.SaveChanges();
            return Ok(post);
        }
        public IHttpActionResult Put(Posts post)
        {
            var Fpost = db.Posts.Find(post.postId);
            if (Fpost == null)
            {
                return NotFound();
            }
            Fpost.post = post.post;
            Fpost.moodId = post.moodId;
            db.SaveChanges();
            return Ok();
        }


        public IHttpActionResult Delete(int id)
        {
            var post = db.Posts.Find(id);
            if (post == null)
            {
                return NotFound();
            }
            var postLikes = db.PostLikes.Where(a => a.postId == id);
            if (postLikes != null)
            {
                foreach(var item in postLikes)
                {
                    db.PostLikes.Remove(item);
                }
                db.SaveChanges();
            }
            var postComment = db.postComments.Where(a => a.postId == id);
            if (postComment != null)
            {
                foreach (var item in postComment)
                {
                    db.postComments.Remove(item);
                }
                db.SaveChanges();
            }
            db.Posts.Remove(post);
            db.SaveChanges();
            return Ok();
        }
    }
}
