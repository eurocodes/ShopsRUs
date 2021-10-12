using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface ICustomerRepository
    {
        Task<bool> CreateCustomerAsync(Customer customer);
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<Customer> GetCustomerByNameAsync(string name);
        Task<ICollection<Customer>> GetAllCustomersAsync();

        Task<bool> SaveChangesAsync();
    }
}
