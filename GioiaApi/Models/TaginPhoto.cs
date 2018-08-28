using GioiaApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gioia.Models
{
    public class TaginPhoto
    {
        [Key, Column(Order = 0)]
        [ForeignKey("ApplicationUser")]
        public string userId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("photo")]
        public int photoId { get; set; }
        public virtual Photo photo { get; set; }

    }
}
