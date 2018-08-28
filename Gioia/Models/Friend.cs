using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gioia.Models
{
    public class Friend
    {

        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string state { get; set; }
        public virtual ApplicationUser RequestedBy { get; set; }
        public virtual ApplicationUser RequestedTo { get; set; }
    }
}