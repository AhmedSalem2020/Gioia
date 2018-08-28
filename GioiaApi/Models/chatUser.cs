using GioiaApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gioia.Models
{
    public class chatUser
    {
        [Key, Column(Order = 1)]
        [ForeignKey("ApplicationUser")]
        public string userId { get; set; }
        public virtual List<ApplicationUser> ApplicationUser { get; set; }


        [Key, Column(Order = 2)]
        [ForeignKey("chats")]
        public string chatId { get; set; }
        public virtual List<chats> chats { get; set; }

    }
}
