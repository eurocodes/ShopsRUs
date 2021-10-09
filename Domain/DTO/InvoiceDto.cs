using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class InvoiceDto
    {
        [Required]
        public int CustomerId { get; set; }
        public ICollection<ProductDto> ProductsDto { get; set; }
    }
}
