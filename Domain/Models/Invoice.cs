using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Invoice : IEntityBase
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public double Amount { get; set; }
        public double InvoiceAmount { get; set; }
        public double AmountBasedDiscount { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
