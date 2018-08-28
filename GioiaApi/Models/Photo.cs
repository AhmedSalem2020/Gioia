using GioiaApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gioia.Models
{
    public class Photo
    {
        [Key]
        public int photoId { get; set; }

        //[ForeignKey("post")]
        //public int postId { get; set; }
        //public virtual Posts post { get; set; }


        public string photo { get; set; }
        
        [ForeignKey("ApplicationUser")]
        public string userId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
