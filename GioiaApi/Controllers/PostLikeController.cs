using GioiaApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GioiaApi.Controllers
{
    public class PostLikeController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public IEnumerable<PostLike> Get(int id)
        {
            return db.PostLikes.Where(a => a.postId == id).ToList();

        }
        public IHttpActionResult Post(PostLike post)
        {
            
            var p = db.Posts.Find(post.postId);
            if (p == null)
            {
                return NotFound();
            }
            var postlike = db.PostLikes.SingleOrDefault(a => a.postId == post.postId && a.userId == post.userId);
            if (postlike == null)
            {
                db.PostLikes.Add(post);
                db.SaveChanges();
            }
            else
            {
                if (postlike.like == true)
                {
                    postlike.like = false;
                }
                else
                {
                    postlike.like = true;

                }
                db.SaveChanges();

            }
            return Ok();
        }
    }
}
