using System;
using System.ComponentModel.DataAnnotations;

namespace BeltExam.Models
{
    public class RegisterViewModel : BaseEntity
    {
        [Required]
        [Display(Name = "First Name")]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First Name can only contain letters and at least 2 characters")]
        public string FirstName { get; set; }
 
        [Required]
        [MinLength(2)]
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last Name can only contain letters and at least 2 characters")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Password must be at least 8 characters")]
        public string Email { get; set; }
 
        [Required]
        [Display(Name = "Password")]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirmation must match.")]
        public string ConfirmPass { get; set; }
    }
}