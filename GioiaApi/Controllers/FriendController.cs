using GioiaApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GioiaApi.Controllers
{
    public class FriendController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public IEnumerable<Friend> Get(string id,string State)
        {
            return db.Friends.Where(a => (a.ReceiverId == id || a.SenderId == id) && a.state == State).ToList();
        }
        public string GetState(string senderID,string recieverId)
        {
            var relation= db.Friends.SingleOrDefault(a =>(a.ReceiverId == senderID && a.SenderId == recieverId)|| (a.ReceiverId == recieverId && a.SenderId == senderID));
            if (relation == null)
            {
                return "None";
            }
            else if(relation.SenderId==senderID &&relation.state=="Pending")
            {
                return "Cancel Request";
            }
            return relation.state;
        }

            public IHttpActionResult Post(Friend friend)
        {
            if (db.Users.Find(friend.ReceiverId) == null)
            {
                return NotFound();
            }
            
            db.Friends.Add(friend);
            db.SaveChanges();
            return Ok(friend);
        }
        public IHttpActionResult Put(Friend friend)
        {
            Friend edited = db.Friends.SingleOrDefault(a => a.ReceiverId == friend.ReceiverId && a.SenderId == friend.SenderId);
            if (edited == null)
            {
                edited = db.Friends.SingleOrDefault(a => a.ReceiverId == friend.SenderId && a.SenderId == friend.ReceiverId);
                if (edited == null)
                {
                    return NotFound();
                }
            }
            edited.state = friend.state;
            db.Entry(edited).State=System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Ok(friend);

        }
        public IHttpActionResult Delete(string senderID, string recieverId)
        {
            var relation = db.Friends.SingleOrDefault(a => (a.ReceiverId == senderID && a.SenderId == recieverId) || (a.ReceiverId == recieverId && a.SenderId == senderID));

            if (relation == null)
                return NotFound();
            db.Friends.Remove(relation);
            db.SaveChanges();
            return Ok();

        }
    }
}
