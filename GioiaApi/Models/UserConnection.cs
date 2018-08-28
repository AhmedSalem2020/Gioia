using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GioiaApi.Models
{
    public class UserConnection
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        [Column(Order = 1)]
        public string Id { get; set; }

        [Key]
        [Column(Order = 2)]
        public string ConnectionId { get; set; }


        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}