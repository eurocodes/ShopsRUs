using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IDiscountServices
    {
        Task<bool> AddDiscountAsync(Discount discount);
        Task<Discount> GetDiscountByTypeAsync(string type);
        Task<ICollection<Discount>> GetAllDiscountAsync();
    }
}
