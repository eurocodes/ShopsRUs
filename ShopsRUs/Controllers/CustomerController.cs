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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _service.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById([FromQuery]int id)
        {
            var customer = await _service.GetCustomerByIdAsync(id);
            if (customer == null) return NotFound();

            return Ok(customer);
        }

        [HttpGet("get-by-name")]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {
            var customer = await _service.GetCustomerByNameAsync(name);
            if (customer == null) return NotFound();

            return Ok(customer);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CustomerDto customer)
        {
            if (!ModelState.IsValid) return BadRequest();

            var createdCustomer = await _service.CreateCustomerAsync(_mapper.Map<Customer>(customer));

            if (await _service.SaveChangesAsync()) return Ok(createdCustomer);
            return BadRequest();

        }
    }
}
