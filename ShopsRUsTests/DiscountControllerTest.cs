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
    public class DiscountControllerTest
    {
        private readonly Mock<IDiscountRepository> _repository = new Mock<IDiscountRepository>();
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
        private DiscountController _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new DiscountController(_repository.Object, _mapper.Object);
        }

        [Test]
        public async Task TestGetAllDiscounts_Returns_List_Of_Discounts()
        {
            var testDiscountList = new List<Discount>
            {
                //Arrenge
                new Discount
                {
                    Id = 1,
                    Type = "Employee",
                    Percentage = 30
                },
                new Discount
                {
                    Id = 2,
                    Type = "Affiliate",
                    Percentage = 10
                },
            };
            _repository.Setup(x => x.GetAllDiscountAsync()).ReturnsAsync(testDiscountList);

            //Act
            var customerResult = await _sut.GetAll();
            var customer = (customerResult as OkObjectResult).Value as ICollection<Discount>;

            //Assert
            Assert.IsTrue(customer.Count == 2);
        }

        [Test]
        public async Task TestGetById_Should_Return_A_One_Discount()
        {
            //Arrenge
            var testDiscount = new Discount
            {
                Id = 1,
                Type = "Employee",
                Percentage = 30
            };

            _repository.Setup(x => x.GetDiscountByTypeAsync("Employee")).ReturnsAsync(testDiscount);

            //Act
            var discountResult = await _sut.GetByType("Employee");
            var discount = (discountResult as OkObjectResult).Value as Discount;

            //Assert
            Assert.AreEqual(discount, testDiscount);
        }

        [Test]
        public async Task TestGetById_Should_Return_NotFound_When_Customer_DoesNot_Exist()
        {
            //Arrenge
            _repository.Setup(x => x.GetDiscountByTypeAsync("Employee")).ReturnsAsync(() => null);

            //Act
            var discountResult = await _sut.GetByType("Employee");
            var discount = discountResult as NotFoundResult;

            //Assert
            Assert.AreEqual(discountResult, discount);
        }

        [Test]
        public async Task AddDiscount()
        {
            //Arrenge
            var discountDto = new DiscountDto
            {
                Type = "Employee",
                Percentage = 30
            };

            var testDiscount = new Discount
            {
                Id = 1,
                Type = discountDto.Type,
                Percentage = discountDto.Percentage
            };


            _repository.Setup(x => x.AddDiscountAsync(testDiscount)).ReturnsAsync(() => true);

            //Act
            var discountResult = await _sut.Create(discountDto);
            var discount = discountResult as Customer;

            //Assert
            Assert.AreEqual(discount, null);
        }
    }
}