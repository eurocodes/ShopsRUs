using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helpers
{
    public static class Utilities
    {
        public static Invoice GenerateInvoice(Customer customer, int percentage, ICollection<Product> productsList, Invoice invoice)
        {
            double amount = 0, invoiceAmount = 0;
            int id = 0;
            foreach (var product in productsList)
            {
                product.Id = ++id;
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
                double AmountBasedDiscount = Math.Floor(invoiceAmount / 100) * 5;
                invoiceAmount -= AmountBasedDiscount;
                invoice.AmountBasedDiscount = AmountBasedDiscount;
            }

            invoice.Amount = amount;
            invoice.InvoiceAmount = invoiceAmount;

            return invoice;
        }
    }
}
