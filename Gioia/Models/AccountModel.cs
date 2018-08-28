using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gioia.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please Enter Email Address")]
        [Display(Name = "Email")]
        [RegularExpression(@"[a-zA-Z0-9]+@[A-Za-z]+.[a-zA-Z]{2,4}", ErrorMessage = "Please Enter Correct Email Address")] 
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Invalid Password", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "please Enter Your First Name")]
        [RegularExpression(@"[a-zA-Z]", ErrorMessage = "Please Enter Correct First Name")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "length must between 2 to 10 ")]
        [Display(Name = "First Name")]
        public string fname { get; set; }

        [Required(ErrorMessage = "please Enter Your Last Name")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "length must between 2 to 10 ")]
        [RegularExpression(@"[a-zA-Z0-9]", ErrorMessage = "Please Enter Correct Last Name")]
        [Display(Name = "Last Name")]
        public string lname { get; set; }


        [Required(ErrorMessage = "please Enter Your UserName")]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "length must between 5 to 10 ")]
        [RegularExpression(@"[a-zA-Z0-9]", ErrorMessage = "Please Enter Correct  UserName")]
        [Display(Name = "UserName")]
        public string username { get; set; }

        //public int Age { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime bdate { get; set; }

        //public string Image { get; set; }

    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please Enter Email Address")]
        [Display(Name = "Email")]
        //[RegularExpression(@"[a-zA-Z0-9]+@[A-Za-z]+.[a-zA-Z]{2,4}", ErrorMessage = "Please Enter Correct Email Address")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
    public class tokenUser
    {
        public string access_token { set; get; }
        public string token_type { set; get; }
        public string expires_in { set; get; }
        public string userName { set; get; }
        public string userID { set; get; }

    }

    public class ChangePasswordBindingModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Id")]
        public string Id { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

}
