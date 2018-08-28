using GioiaApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gioia.Models
{
    public class chats
    {
        
        [Key]
        [Column(Order = 2)]
        [ForeignKey("Sender")]
        public string userId { get; set; }
        public virtual ApplicationUser Sender { get; set; }
        public virtual List<ApplicationUser> Senders { get; set; }

        [Key]
        [Column(Order = 1)]
        public int chatId { get; set; }

        public string message { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime mesg_Date { get; set; }



    }
}
