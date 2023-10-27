using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace SimpleCRUD.Web.Models.ViewModels
{
    public class CreatePersonRequest
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        //public string StreetName { get; set; }
        //public string City { get; set; }
        //public string State { get; set; }
        //public string ZipCode { get; set; }
        //public string EmailAddress { get; set; }

        public List<Addresses> Addresses { get; set; } = new List<Addresses>();
        public List<EmailAddresses> EmailAddresses { get; set; } = new List<EmailAddresses>();
    }
}
