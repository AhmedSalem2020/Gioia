using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace GioiaApi.Models
{
    public class Message
    {

        public int Id { get; set; }
        [Required]
        public string FromId { get; set; }
        [Required]
        public string ToId { get; set; }
        [DefaultValue(false)]
        public bool IsSeen { get; set; }

        public DateTime MsgDate { get; set; }
        [DefaultValue(false)]
        public TimeSpan MsgTime { get; set; }

        public bool IsDeleted { get; set; }

        public string MessageText { get; set; }
    }
}