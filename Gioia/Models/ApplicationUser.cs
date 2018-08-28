using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gioia.Models
{
    public class ApplicationUser
    {
        public string Id { get; set; }

        public string OldPassword { get; set; }

        public Byte[] Image { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "please Enter Your First Name")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "length must between 2 to 10 ")]
        [Display(Name = "First Name")]
        public string Fname { get; set; }

        [Required(ErrorMessage = "please Enter Your Last Name")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "length must between 2 to 10 ")]
        [Display(Name = "Last Name")]
        public string Lname { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

      

        [Required(ErrorMessage = "Please Enter Email Address")]
        [Display(Name = "Email")]
        [RegularExpression(@"[a-zA-Z0-9]+@[A-Za-z]+.[a-zA-Z]{2,4}", ErrorMessage = "Please Enter Correct Email Address")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public  string PhoneNumber { get; set; }

        [Required(ErrorMessage = "please Enter Your UserName")]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "length must between 5 to 10 ")]
        [Display(Name = "UserName")]
        public  string UserName { get; set; }


    }
}
