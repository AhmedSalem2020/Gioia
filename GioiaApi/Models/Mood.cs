using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gioia.Models
{
    public class Mood
    {

        [Key]
        public int MoodId { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "length between 2 and 15 character")]
        public string moodName { get; set; }

        public string moodImage { set; get; }

        
    }
}
