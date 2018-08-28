using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gioia.Models
{
    public class Posts
    {
        public int postId { get; set; }
        public string post { get; set; }
        public DateTime time { get; set; }
        public int moodId { get; set; }
        public virtual Mood mood { get; set; }
        public string userId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        //public int? photoId { get; set; }
        //public virtual Photo photo { get; set; }
        public virtual ICollection<PostLike> PostLike { get; set; }
        public virtual ICollection<PostComment> PostComments { get; set; }


    }
}