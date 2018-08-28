using Gioia.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GioiaApi.Models
{
    public class Sound
    {
       
        [ForeignKey("mood")]
        public int moodId { get; set; }
        public virtual Mood mood { get; set; }

        [Key]
        public int MusicId { get; set; }

        

        public string MusicLink { get; set; }

        
    }
}