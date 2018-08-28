using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gioia.Models
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Please Enter Email Address")]
        [Display(Name = "Email")]
        //[RegularExpression(@"[a-zA-Z0-9]+@[A-Za-z]+.[a-zA-Z]{2,4}", ErrorMessage = "Please Enter Correct Email Address")]
        [EmailAddress]
        public string Email { get; set; }
    }
}