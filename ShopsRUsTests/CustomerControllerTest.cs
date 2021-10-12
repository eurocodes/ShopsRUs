using AutoMapper;
using Domain;
using Domain.DTO;
using Domain.Models;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ShopsRUs.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopsRUsTests
{
    public class CustomerControllerTest
    {
        private readonly Mock<ICustomerRepository> _repository = new Mock<ICustomerRepository>();
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
        private CustomerController _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new CustomerController(_repository.Object, _mapper.Object);
        }

        [Test]
        public async Task TestGetAllCustomers_Returns_List_Of_Customers()
        {
            var testCustomerList = new List<Customer>
            {
                //Arrenge
                new Customer
                {
                    Id = 1,
                    FirstName = "Ugo",
                    LastName = "Umeoke",
                    UserName = "ugee",
                    Email = "u.umeoke@yahoo.com",
                    Location = "Abuja",
                    UserTypeId = 1
                },
                new Customer
                {
                    FirstName = "Ramona",
                    LastName= "Durham",
                    UserName= "taryosky",
                    Email = "taryosky@gmail.com",
                    Location = "Abuja",
                    UserTypeId = 2
                },
            };
            _repository.Setup(x => x.GetAllCustomersAsync()).ReturnsAsync(testCustomerList);

            //Act
            var customerResult = await _sut.GetAll();
            var customer = (customerResult as OkObjectResult).Value as ICollection<Customer>;

            //Assert
            Assert.IsTrue(customer.Count == 2);
        }

        [Test]
        public async Task GetById_Should_Return_A_Customer()
        {
            //Arrenge
            var testCustomer = new Customer
            {
                Id = 1,
                FirstName = "Ramona",
                LastName = "Durham",
                UserName = "taryosky",
                Email = "taryosky@gmail.com",
                Location = "Abuja",
                UserTypeId = 2,
            };

            _repository.Setup(x => x.GetCustomerByIdAsync(1)).ReturnsAsync(testCustomer);

            //Act
            var customerResult = await _sut.GetById(1);
            var customer = (customerResult as OkObjectResult).Value as Customer;

            //Assert
            Assert.AreEqual(customer, testCustomer);
        }

        [Test]
        public async Task GetById_Should_Return_NotFound_When_Customer_DoesNot_Exist()
        {
            //Arrenge
            _repository.Setup(x => x.GetCustomerByIdAsync(1)).ReturnsAsync(() => null);

            //Act
            var customerResult = await _sut.GetById(1);
            var customer = customerResult as NotFoundResult;

            //Assert
            Assert.AreEqual(customerResult, customer);
        }

        [Test]
        public async Task GetByName_Should_Return_A_Customer()
        {
            //Arrenge
            var testCustomer = new Customer
            {
                FirstName = "Ramona",
                LastName = "Durham",
                UserName = "taryosky",
                Email = "taryosky@gmail.com",
                Location = "Abuja",
                UserTypeId = 2
            };

            _repository.Setup(x => x.GetCustomerByNameAsync("taryosky")).ReturnsAsync(testCustomer);

            //Act
            var customerResult = await _sut.GetByName("taryosky");
            var customer = (customerResult as OkObjectResult).Value as Customer;

            //Assert
            Assert.AreEqual(customer, testCustomer);
        }

        [Test]
        public async Task GetByName_Should_Return_NotFound_When_CustomerName_DoesNot_Exist()
        {
            //Arrenge
            _repository.Setup(x => x.GetCustomerByNameAsync("anyname")).ReturnsAsync(() => null);

            //Act
            var customerResult = await _sut.GetByName("anyname");
            var customer = customerResult as NotFoundResult;

            //Assert
            Assert.AreEqual(customerResult, customer);
        }
    }
}