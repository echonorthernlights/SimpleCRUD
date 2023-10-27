using System.ComponentModel.DataAnnotations;

namespace SimpleCRUD.Web.Models
{
    public class EmailAddresses
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string EmailAddress { get; set; }
    }
}
