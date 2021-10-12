using Domain;
using Domain.DTO;
using Domain.Models;
using Infrastructure.Helpers;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext _context;
        private readonly IProductRepository _productRepository;

        public InvoiceRepository(AppDbContext context, IProductRepository productRepository)
        {
            _context = context;
            _productRepository = productRepository;
        }
        public async Task<Invoice> GetTotalInvoiceAmountAsync(int customerId, ICollection<Product> productsList)
        {
            Invoice invoice = new();
            var customer = await _context.Customers.Include(x => x.UserType).FirstOrDefaultAsync(c => c.Id == customerId);
            if (customer == null)
                return null;
            var discount = await _context.Discounts.FirstOrDefaultAsync(d => d.Id == customer.UserType.DiscountId);
            var percentage = discount.Percentage;
            invoice = Utilities.GenerateInvoice(customer, percentage, productsList, invoice);
            
            invoice.Customer = customer;
            invoice.Products = productsList;
            return invoice;
        }
    }
}
