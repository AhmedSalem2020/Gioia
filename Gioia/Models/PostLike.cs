using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gioia.Models
{
    public class PostLike
    {
        public int postId { get; set; }
        public virtual Posts Post { get; set; }
        public bool like { get; set; }
        public string userId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}