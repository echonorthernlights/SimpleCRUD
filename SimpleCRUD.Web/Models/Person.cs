using System.ComponentModel.DataAnnotations;

namespace SimpleCRUD.Web.Models
{
    public class Person
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(50)]
        public int Age { get; set; }
        public List<Addresses> Addresses { get; set; }
        public List<EmailAddresses> EmailAddresses { get; set; }

    }
}
