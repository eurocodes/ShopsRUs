using AutoMapper;
using Domain.DTO;
using Domain.Models;
using Infrastructure.Interfaces;
using Moq;
using NUnit.Framework;
using ShopsRUs.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopsRUsTests
{
    public class InvoiceControllerTest
    {
        private readonly Mock<IInvoiceRepository> _repository = new Mock<IInvoiceRepository>();
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
        private InvoiceController _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new InvoiceController(_repository.Object, _mapper.Object);
        }

        [Test]
        public async Task TestGetInvoice()
        {
            var productsToBuy = new List<ProductDto>
            {
                new ProductDto
                {
                    Name = "Clothing",
                    Price = 80,
                },
                new ProductDto
                {
                    Name = "Gadget",
                    Price = 100,
                }
            };

            var customer = new Customer
            {
                FirstName = "Ramona",
                LastName = "Durham",
                UserName = "taryosky",
                Email = "taryosky@gmail.com",
                Location = "Abuja",
                UserTypeId = 1,
                UserType = new UserType
                {
                    _UserType = "Employee",
                    DiscountId = 1,
                    Discount = new Discount
                    {
                        Percentage = 30,
                    }
                }
            };
        }
    }
}
