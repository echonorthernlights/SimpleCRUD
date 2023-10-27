using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SimpleCRUD.Web.Models.ViewModels
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [PersonalData]
        public string FirstName { get; set; }
        [Required]
        [PersonalData]
        public string LastName { get; set; }


    }
}
