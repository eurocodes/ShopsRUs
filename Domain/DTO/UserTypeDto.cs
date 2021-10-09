using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class UserTypeDto
    {
        public string _UserType { get; set; }
        public DiscountDto DiscountDto { get; set; }
        public int DiscountId { get; set; }
    }
}
