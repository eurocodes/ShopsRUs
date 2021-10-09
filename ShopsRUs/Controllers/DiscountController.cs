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
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountServices _service;
        private readonly IMapper _mapper;

        public DiscountController(IDiscountServices service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var discounts = await _service.GetAllDiscountAsync();
            return Ok(discounts);
        }

        [HttpGet("get-by-type")]
        public async Task<IActionResult> GetByName([FromQuery] string type)
        {
            var discount = await _service.GetDiscountByTypeAsync(type);
            if (discount == null) return NotFound();

            return Ok(discount);
        }

        [HttpPost("add-new")]
        public async Task<IActionResult> Create([FromBody] DiscountDto discount)
        {
            if (!ModelState.IsValid) return BadRequest();

            var creted = await _service.AddDiscountAsync(_mapper.Map<Discount>(discount));

            if (creted) return Ok();
            return BadRequest();

        }
    }
}
