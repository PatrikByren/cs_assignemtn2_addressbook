using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace addressbook.Models
{
    internal class ContactPerson
    {
        [Key]public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string StreetAddress { get; set; }  
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}
