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
        private readonly IDiscountRepository _repository;
        private readonly IMapper _mapper;

        public DiscountController(IDiscountRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var discounts = await _repository.GetAllDiscountAsync();
            return Ok(discounts);
        }

        [HttpGet("get-by-type")]
        public async Task<IActionResult> GetByName([FromQuery] string type)
        {
            var discount = await _repository.GetDiscountByTypeAsync(type);
            if (discount == null) return NotFound();

            return Ok(discount);
        }

        [HttpPost("add-new")]
        public async Task<IActionResult> Create([FromBody] DiscountDto discount)
        {
            if (!ModelState.IsValid) return BadRequest();

            var creted = await _repository.AddDiscountAsync(_mapper.Map<Discount>(discount));

            if (creted) return Ok();
            return BadRequest();

        }
    }
}
