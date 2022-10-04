using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook.Models
{
    internal class ContactPerson
    {
        public Guid Id { get; set; } = Guid.NewGuid();
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
