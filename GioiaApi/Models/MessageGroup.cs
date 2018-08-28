using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GioiaApi.Models
{
    public class MessageGroup
    {
        public int Id { get; set; }
        [Required]
        public string FromId { get; set; }
        [Required]
        public int ToIdGroup { get; set; }
        [DefaultValue(false)]
        public bool IsSeen { get; set; }


        public DateTime MsgDate { get; set; }
        [DefaultValue(false)]
        public TimeSpan MsgTime { get; set; }

        public bool IsDeleted { get; set; }
    }
}