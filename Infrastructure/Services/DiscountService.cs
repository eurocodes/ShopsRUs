using Domain;
using Domain.Models;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class DiscountService : IDiscountServices
    {
        private readonly AppDbContext _context;

        public DiscountService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddDiscountAsync(Discount discount)
        {
            await _context.Discounts.AddAsync(discount);
            await _context.SaveChangesAsync();

            // Add UserType
            var userType = new UserType
            {
                _UserType = discount.Type,
                Discount = discount
            };
            userType.Discount.Id = _context.Discounts.Count();
            userType.DiscountId = userType.Discount.Id;
            await _context.UserTypes.AddAsync(userType);
            return await _context.SaveChangesAsync() >= 1;
        }

        public async Task<ICollection<Discount>> GetAllDiscountAsync()
        {
            var discounts = await _context.Discounts.ToListAsync();
            return discounts;
        }

        public async Task<Discount> GetDiscountByTypeAsync(string type)
        {
            var discount = await _context.Discounts.FirstOrDefaultAsync(d => d.Type.ToLower() == type.ToLower());
            return discount;
        }
    }
}
