using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gioia.Models
{
    public class PostComment
    {
        public int id { get; set; }
        public string comment { get; set; }
        public int postId { get; set; }
        public virtual Posts Post { get; set; }
        public string userId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}