using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GioiaApi.Models
{
    public class Friend
    {
        [Key, Column(Order = 0)]
        public string SenderId { get; set; }

        [Key,Column(Order = 1)]
        public string ReceiverId { get; set; }
        public string state { get; set; }
        public virtual ApplicationUser RequestedBy { get; set; }
        public virtual ApplicationUser RequestedTo { get; set; }


    }
}