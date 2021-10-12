using AutoMapper;
using Domain.DTO;
using Domain.Models;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopsRUs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceRepository _repository;
        private readonly IMapper _mapper;

        public InvoiceController(IInvoiceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost("generate-invoice/{customerId}")]
        public async Task<IActionResult> GetInvoice( int customerId, [FromBody] InvoiceDto invoiceDto)
        {
            var productsToAdd = new List<Product>();
            foreach (var item in invoiceDto.ProductsDto)
            {
                productsToAdd.Add(_mapper.Map<Product>(item));
            }
            var invoice = await _repository.GetTotalInvoiceAmountAsync(customerId, productsToAdd);
            var invoiceToReturn = _mapper.Map<InvoiceReturnDto>(invoice);
            invoiceToReturn.PercentageDiscount = invoice.Customer.UserType.Discount.Percentage + "%";
            invoiceToReturn.Amount = "$" + invoice.Amount;
            invoiceToReturn.InvoiceAmount = "$" + invoice.InvoiceAmount;
            invoiceToReturn.AmountBasedDiscount = "$" + invoice.AmountBasedDiscount;
            return Ok(invoiceToReturn);
        }
    }
}
