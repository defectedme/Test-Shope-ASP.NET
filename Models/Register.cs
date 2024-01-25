using System.ComponentModel.DataAnnotations;

namespace Test_Shope_ASP.NET.Models
{
    public class Register
    {
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full name is required")]
        [RegularExpression(@"^[A-Za-z ]*$", ErrorMessage = "Full name letters only please")]
        [StringLength(100, ErrorMessage = "The {0} have to be minimum {2} characters.", MinimumLength = 7)]
        public string FullName { get; set; }



        [Required]
        [Display(Name = "User Name")]
        [StringLength(100, ErrorMessage = "The {0} have to be minimum {2} characters.", MinimumLength = 5)]
        public string UserName { get; set; }


        [Required]
        [EmailAddress]
        public string Email { get; set; }


   
        [Required(ErrorMessage = "Address is required")]
        [StringLength(100, ErrorMessage = "The {0} have to be minimum {2} characters.", MinimumLength = 10)]
        public string Address { get; set; }



        [Required(ErrorMessage = "City is required")]
        [StringLength(10, ErrorMessage = "The {0} have to be minimum {2} characters.", MinimumLength = 4)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "City letters only please")]
        public string City { get; set; }


        [Required(ErrorMessage = "Post code required")]
        [Display(Name = "Post code")]
        [RegularExpression("^[0-9-]*$", ErrorMessage = "Post Code must be numeric")]
        public string PostCode { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [StringLength(10, ErrorMessage = "The {0} have to be minimum {2} characters.", MinimumLength = 5)]
        [Display(Name = "Country")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Country letters only please")]

        public string Country { get; set; }

        [Required(ErrorMessage = "Phone number required")]
        [Display(Name = "Phone number")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone Number must be numeric")]
        [StringLength(10, ErrorMessage = "The {0} have to be minimum {2} characters.", MinimumLength = 9)]
        public string PhoneNumber { get; set; }




        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long and first Uppercase.", MinimumLength = 8)]
        public string Password { get; set; }


        [Required]
        [Display(Name = "Confirm password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long and first Uppercase.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

    }
}
