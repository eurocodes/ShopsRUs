using Domain;
using Domain.Models;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Product> AddProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            if (await _context.SaveChangesAsync() >= 1)
                return product;
            return null;
        }
    }
}
