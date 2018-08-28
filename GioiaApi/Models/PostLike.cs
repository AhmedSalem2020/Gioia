using Gioia.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GioiaApi.Models
{
    public class PostLike
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Post")]
        public int postId { get; set; }
        public virtual Posts Post { get; set; }
        public bool like { get; set; }
        [Key, Column(Order = 1)]
        [ForeignKey("ApplicationUser")]
        public string userId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
    public class PostComment
    {
        [Key]
        public int id { get; set; }
        public string comment { get; set; }
        [ForeignKey("Post")]
        public int postId { get; set; }
        public virtual Posts Post { get; set; }
        [ForeignKey("ApplicationUser")]
        public string userId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }


    }
}