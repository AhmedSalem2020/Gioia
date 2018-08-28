using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GioiaApi.Models
{
    public class Group
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual List<ApplicationUser> ApplicationUsers { get; set; }
    }
}