using Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Domain
{
    public class AppPreseeder
    {
        private static readonly string path = Path.GetFullPath(@"../Domain/SeedData/");

        public static async Task Seed(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
            context.Database.EnsureCreated();

            //Seed Doscount
            if (!context.Discounts.Any())
            {
                var discounts = GetSeedData<Discount>(File.ReadAllText(path + "Discount.json"));
                await context.Discounts.AddRangeAsync(discounts);
                await context.SaveChangesAsync();
            }

            //Seed UserTypes
            if (!context.UserTypes.Any())
            {
                var userType = GetSeedData<UserType>(File.ReadAllText(path + "Usertype.json"));
                foreach (var item in userType)
                {
                    item.Discount = context.Discounts.FirstOrDefault(d => d.Id == item.DiscountId);
                }
                await context.UserTypes.AddRangeAsync(userType);
                await context.SaveChangesAsync();
            }

            //Seed Customer
            if (!context.Customers.Any())
            {
                var customers = GetSeedData<Customer>(File.ReadAllText(path + "Customer.json"));
                foreach (var user in customers)
                {
                    user.UserType = context.UserTypes.FirstOrDefault(u => u.Id == user.UserTypeId);
                    user.UserType.Discount = context.Discounts.FirstOrDefault(d => d.Id == user.UserType.Id);
                }
                await context.Customers.AddRangeAsync(customers);
                await context.SaveChangesAsync();
            }

        }

        //Get sample data from json files
        private static List<T> GetSeedData<T>(string location)
        {
            var output = JsonSerializer.Deserialize<List<T>>(location, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return output;
        }
    }
}
