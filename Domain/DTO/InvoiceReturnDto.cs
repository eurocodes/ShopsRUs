using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class InvoiceReturnDto
    {
        public CustomerDto Customer { get; set; }
        public int CustomerId { get; set; }
        public string Amount { get; set; }
        public string InvoiceAmount { get; set; }
        public string PercentageDiscount { get; set; }
        public string AmountBasedDiscount { get; set; }
        public ICollection<ProductDto> Products { get; set; }
    }
}
