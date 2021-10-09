using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class UserType : IEntityBase
    {
        public int Id { get; set; }
        public string _UserType { get; set; }
        public Discount Discount { get; set; }
        public int DiscountId { get; set; }
    }
}
