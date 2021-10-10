using Domain;
using Domain.DTO;
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
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext _context;
        private readonly IProductRepository _productService;

        public InvoiceRepository(AppDbContext context, IProductRepository productService)
        {
            _context = context;
            _productService = productService;
        }
        public async Task<Invoice> GetTotalInvoiceAmountAsync(int customerId, ICollection<Product> productsList)
        {
            Invoice invoice = new();
            double amount = 0, invoiceAmount = 0;
            var customer = await _context.Customers.Include(x => x.UserType).FirstOrDefaultAsync(c => c.Id == customerId);
            if (customer == null)
                return null;
            var discount = await _context.Discounts.FirstOrDefaultAsync(d => d.Id == customer.UserType.DiscountId);
            var percentage = discount.Percentage;

            foreach (var product in productsList)
            {
                if (product.Name.ToLower() != "groceries")
                {
                    invoiceAmount += (product.Price - (product.Price * percentage / 100));
                }
                else
                {

                    invoiceAmount += product.Price;
                }
                if (product.Name.ToLower() != "groceries" 
                    && percentage == 0
                    && DateTime.Now.Year - customer.DateJoined.Year > 2)
                {
                    invoiceAmount += (product.Price - (product.Price * 5 / 100));
                }
                amount += product.Price;
            }
            if (invoiceAmount >= 100)
            {
                invoiceAmount -= (Math.Floor(invoiceAmount / 100) * 5);
            }

            invoice.Amount = amount;
            invoice.InvoiceAmount = invoiceAmount;
            invoice.Customer = customer;
            invoice.Products = productsList;
            return invoice;
        }
    }
}
