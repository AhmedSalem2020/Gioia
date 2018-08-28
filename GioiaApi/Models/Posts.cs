using GioiaApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gioia.Models
{
    public class Posts
    {
        [Key]
        public int postId { get; set; }

        public string post { get; set; }

        public DateTime time { get; set; }

        [ForeignKey("mood")]
        public int moodId { get; set; }
        public virtual Mood mood { get; set; }

        [ForeignKey("ApplicationUser")]
        public string userId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }


        [ForeignKey("photo")]
        public int? photoId { get; set; }
        public virtual Photo photo { get; set; }
        public virtual ICollection<PostLike> PostLikes { get; set; }
        public virtual ICollection<PostComment> PostComments { get; set; }


    }

}
