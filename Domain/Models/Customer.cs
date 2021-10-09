using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Customer: IEntityBase
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Location { get; set; }
        public UserType UserType { get; set; }
        public int UserTypeId { get; set; }
        public DateTime DateJoined { get; set; } = DateTime.Now.Date;
        public ICollection<Invoice> Invoices { get; set; }
    }
}
