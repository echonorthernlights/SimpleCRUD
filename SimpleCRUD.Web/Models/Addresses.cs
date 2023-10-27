using System.ComponentModel.DataAnnotations;

namespace SimpleCRUD.Web.Models
{
    public class Addresses
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string StreetName { get; set; }
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
        [Required]
        [MaxLength(50)]
        public string State { get; set; }
        [Required]
        [MaxLength(50)]

        public string ZipCode { get; set; }
    }
}
