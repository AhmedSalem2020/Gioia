using GioiaApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gioia.Models
{
    public class Memos
    {

        [Key]
        public int memoId { get; set; }

        public string Description { get; set; }

        public string title { get; set; }

        
        [ForeignKey("ApplicationUser")]
        public string userId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }



    }
}
