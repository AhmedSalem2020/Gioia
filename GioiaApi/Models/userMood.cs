using GioiaApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gioia.Models
{
    public class userMood
    {
        [Key, Column(Order = 0)]
        [ForeignKey("mood")]
        public int moodId { get; set; }
        public virtual Mood mood { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("ApplicationUser")]
        public string userId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Key,Column(Order =2)]
        public DateTime Date { get; set; }


    }
}
