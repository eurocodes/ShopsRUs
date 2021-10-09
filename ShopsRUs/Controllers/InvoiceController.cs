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
        private readonly IInvoiceService _service;
        private readonly IMapper _mapper;

        public InvoiceController(IInvoiceService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("generate-invoice")]
        public async Task<IActionResult> GetInvoice([FromBody] InvoiceDto invoiceDto)
        {
            var productsToAdd = new List<Product>();
            foreach (var item in invoiceDto.ProductsDto)
            {
                productsToAdd.Add(_mapper.Map<Product>(item));
            }
            var invoice = await _service.GetTotalInvoiceAmountAsync(invoiceDto.CustomerId, productsToAdd);
            return Ok(invoice);
        }
    }
}
