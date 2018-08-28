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
    public class PostCommentController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public IEnumerable<PostComment>Get(int id)
        {
            return db.postComments.Where(a => a.postId == id).ToList();
        }
        public PostComment GetById(int Commentid)
        {
            return db.postComments.SingleOrDefault(a => a.id == Commentid);
        }
        public IHttpActionResult Post(PostComment comment)
        {
            var post = db.Posts.Find(comment.postId);
            if (post == null)
            {
                return NotFound();
            }
            db.postComments.Add(comment);
            db.SaveChanges();
            return Ok();
        }

        public IHttpActionResult Put(PostComment comment)
        {
            var Fcomment = db.postComments.Find(comment.id);
            if (Fcomment == null)
            {
                return NotFound();
            }
            Fcomment.comment = comment.comment;
            db.SaveChanges();
            return Ok();
        }


        public IHttpActionResult Delete(int id)
        {
            var comment = db.postComments.Find(id);
            if (comment == null)
            {
                return NotFound();
            }
            db.postComments.Remove(comment);
            db.SaveChanges();
            return Ok();
        }
    }
}
