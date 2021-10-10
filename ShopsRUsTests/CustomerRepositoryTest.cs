using Domain;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using NUnit.Framework;
using System.Threading.Tasks;

namespace ShopsRUsTests
{
    public class CustomerRepositoryTest
    {

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void TestGetAllCustomers()
        {
            //AppDbContext context = new AppDbContext();
            //ICustomerService _service = new CustomerService(context);
            //var customers = await _service.GetAllCustomersAsync();
            //Assert.IsTrue(customers.Count > 0);
        }
    }
}