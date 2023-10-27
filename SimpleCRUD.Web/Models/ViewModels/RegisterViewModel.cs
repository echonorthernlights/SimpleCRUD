using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SimpleCRUD.Web.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string FirstName { get; set; }


        [Required]

        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailInUse", controller: "Auth")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords mismatch")]
        public string ConfirmPassword { get; set; }

    }
}
