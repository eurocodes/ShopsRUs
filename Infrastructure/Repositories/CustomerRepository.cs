using Domain;
using Domain.Models;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            if (customer != null)
            {
                customer.UserType = await _context.UserTypes.FirstOrDefaultAsync(u => u.Id == customer.UserTypeId);
                customer.UserType.Discount = await _context.Discounts.FirstOrDefaultAsync(d => d.Id == customer.UserType.DiscountId);
                await _context.Customers.AddAsync(customer);
                return customer;
            }

            return null;

        }

        public async Task<ICollection<Customer>> GetAllCustomersAsync()
        {
            var customersList = await _context.Customers
                .Include(c => c.UserType).ThenInclude(x => x.Discount).ToListAsync();
            return customersList;
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {

            var customer = await _context.Customers.Include(c => c.UserType)
                .ThenInclude(x => x.Discount)
                .FirstOrDefaultAsync(c => c.Id == id);
            return customer;
        }

        public async Task<Customer> GetCustomerByNameAsync(string name)
        {
            var customer = await _context.Customers.Include(c => c.UserType)
                .ThenInclude(x => x.Discount)
                .FirstOrDefaultAsync(c => c.UserName.ToLower() == name.ToLower());
            return customer;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 1;
        }
    }
}
