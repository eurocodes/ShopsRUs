using Domain.DTO;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<Invoice> GetTotalInvoiceAmountAsync(int customerId, ICollection<Product> products);
    }
}
