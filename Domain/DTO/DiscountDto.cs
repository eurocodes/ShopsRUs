using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class DiscountDto
    {
        [Required]
        public string Type { get; set; }
        public int Percentage { get; set; }
    }
}
