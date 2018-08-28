using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gioia.Models
{
    public class Music
    {
        [Key,Column(Order =0)]

        [ForeignKey("mood")]
        public int moodId { get; set; }
        public virtual Mood mood { get; set; }

        [Key,Column(Order =1)]
        public int MusicId { get; set; }

        public string MusicName { get; set; }

        public string MusicLink { get; set; }

    }
    public class Video
    {
        [Key, Column(Order = 0)]

        [ForeignKey("mood")]
        public int moodId { get; set; }
        public virtual Mood mood { get; set; }

        [Key, Column(Order = 1)]
        public int videoId { get; set; }

        public string videoLink { get; set; }

    }
}
